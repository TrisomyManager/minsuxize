/**
 * 中国民俗3D地图 - Three.js 实现
 * Folk3DMap 类
 */

class Folk3DMap {
  constructor(containerId, options = {}) {
    this.container = document.getElementById(containerId);
    if (!this.container) {
      throw new Error(`Container #${containerId} not found`);
    }

    // 配置选项
    this.options = {
      width: this.container.clientWidth,
      height: this.container.clientHeight,
      backgroundColor: 0x0a0a0f,
      mapColor: 0x1a1a2e,
      highlightColor: 0xc41e3a,
      tagColor: 0xffd700,
      ...options
    };

    // 场景元素
    this.scene = null;
    this.camera = null;
    this.renderer = null;
    this.controls = null;
    this.raycaster = new THREE.Raycaster();
    this.mouse = new THREE.Vector2();

    // 地图元素
    this.provinces = []; // 省份mesh数组
    this.tags = []; // 标签数组
    this.connections = []; // 连接线

    // 状态
    this.hoveredProvince = null;
    this.selectedProvince = null;
    this.folkloreData = [];
    this.isAnimating = false;

    // 绑定事件
    this.onMouseMove = this.onMouseMove.bind(this);
    this.onClick = this.onClick.bind(this);
    this.onResize = this.onResize.bind(this);

    this.init();
  }

  init() {
    this.createScene();
    this.createCamera();
    this.createRenderer();
    this.createLights();
    this.createControls();
    this.createChinaMap();
    this.bindEvents();
    this.animate();
  }

  // 创建场景
  createScene() {
    this.scene = new THREE.Scene();
    this.scene.background = new THREE.Color(this.options.backgroundColor);
    this.scene.fog = new THREE.FogExp2(this.options.backgroundColor, 0.02);
  }

  // 创建相机
  createCamera() {
    const aspect = this.options.width / this.options.height;
    this.camera = new THREE.PerspectiveCamera(45, aspect, 0.1, 1000);
    this.camera.position.set(0, 0, 12);
  }

  // 创建渲染器
  createRenderer() {
    this.renderer = new THREE.WebGLRenderer({ 
      antialias: true,
      alpha: true 
    });
    this.renderer.setSize(this.options.width, this.options.height);
    this.renderer.setPixelRatio(Math.min(window.devicePixelRatio, 2));
    this.renderer.shadowMap.enabled = true;
    this.renderer.shadowMap.type = THREE.PCFSoftShadowMap;
    this.container.appendChild(this.renderer.domElement);
  }

  // 创建灯光
  createLights() {
    // 环境光
    const ambientLight = new THREE.AmbientLight(0x404040, 1);
    this.scene.add(ambientLight);

    // 主光源
    const mainLight = new THREE.DirectionalLight(0xffffff, 1);
    mainLight.position.set(5, 10, 7);
    mainLight.castShadow = true;
    mainLight.shadow.mapSize.width = 2048;
    mainLight.shadow.mapSize.height = 2048;
    this.scene.add(mainLight);

    // 轮廓光（蓝色调）
    const rimLight = new THREE.PointLight(0x4a90e2, 0.5);
    rimLight.position.set(-5, 0, -5);
    this.scene.add(rimLight);

    // 暖色补光
    const warmLight = new THREE.PointLight(0xffd700, 0.3);
    warmLight.position.set(5, -5, 5);
    this.scene.add(warmLight);
  }

  // 创建控制器
  createControls() {
    this.controls = new THREE.OrbitControls(this.camera, this.renderer.domElement);
    this.controls.enableDamping = true;
    this.controls.dampingFactor = 0.05;
    this.controls.minDistance = 5;
    this.controls.maxDistance = 20;
    this.controls.maxPolarAngle = Math.PI / 1.5;
    this.controls.autoRotate = true;
    this.controls.autoRotateSpeed = 0.5;
  }

  // 创建中国地图（简化版 - 使用省份轮廓点）
  createChinaMap() {
    // 创建省份轮廓组
    this.provinceGroup = new THREE.Group();
    this.scene.add(this.provinceGroup);

    // 创建省份轮廓线
    Object.entries(PROVINCE_CENTERS).forEach(([name, data]) => {
      this.createProvince(name, data);
    });

    // 创建标签组
    this.tagGroup = new THREE.Group();
    this.scene.add(this.tagGroup);
  }

  // 创建单个省份
  createProvince(name, data) {
    // 创建省份区域（使用圆形表示简化版）
    const geometry = new THREE.CircleGeometry(0.25, 32);
    const material = new THREE.MeshPhongMaterial({
      color: this.options.mapColor,
      emissive: 0x000000,
      side: THREE.DoubleSide,
      transparent: true,
      opacity: 0.8
    });

    const province = new THREE.Mesh(geometry, material);
    province.position.set(data.x, data.y, data.z);
    province.rotation.x = -Math.PI / 2;
    province.userData = { name, ...data };
    province.castShadow = true;
    province.receiveShadow = true;

    // 添加发光边界
    const edgeGeometry = new THREE.RingGeometry(0.25, 0.28, 32);
    const edgeMaterial = new THREE.MeshBasicMaterial({
      color: 0x4a90e2,
      transparent: true,
      opacity: 0.3,
      side: THREE.DoubleSide
    });
    const edge = new THREE.Mesh(edgeGeometry, edgeMaterial);
    edge.position.set(data.x, data.y + 0.01, data.z);
    edge.rotation.x = -Math.PI / 2;
    edge.userData = { isEdge: true, parent: province };

    this.provinceGroup.add(province);
    this.provinceGroup.add(edge);
    this.provinces.push(province);
  }

  // 加载民俗数据
  loadFolkloreData(data) {
    this.folkloreData = data;
    this.createTags();
  }

  // 创建民俗标签
  createTags() {
    // 按省份分组民俗数据
    const provinceFolklore = {};
    
    this.folkloreData.forEach(item => {
      const province = this.getProvinceName(item.Region);
      if (!provinceFolklore[province]) {
        provinceFolklore[province] = [];
      }
      provinceFolklore[province].push(item);
    });

    // 为每个省份创建标签
    Object.entries(provinceFolklore).forEach(([province, items]) => {
      const center = PROVINCE_CENTERS[province];
      if (!center) return;

      // 只取最热门的1-2个民俗
      const hotItems = items
        .sort((a, b) => (b.hotLevel || 0) - (a.hotLevel || 0))
        .slice(0, 2);

      hotItems.forEach((item, index) => {
        this.createTag(item, center, index);
      });
    });
  }

  // 获取省份名称
  getProvinceName(region) {
    return REGION_MAPPING[region] || region;
  }

  // 创建单个标签
  createTag(item, center, index) {
    const offset = index === 0 ? 0 : 0.4;
    const yOffset = 0.3;

    // 创建标签背景
    const canvas = document.createElement('canvas');
    const context = canvas.getContext('2d');
    canvas.width = 256;
    canvas.height = 64;

    // 绘制标签背景
    context.fillStyle = 'rgba(26, 26, 46, 0.9)';
    context.beginPath();
    context.roundRect(0, 0, 256, 64, 16);
    context.fill();

    // 绘制边框
    context.strokeStyle = '#ffd700';
    context.lineWidth = 2;
    context.beginPath();
    context.roundRect(0, 0, 256, 64, 16);
    context.stroke();

    // 绘制图标
    context.font = '32px Arial';
    context.textAlign = 'center';
    context.fillText(getFolkloreIcon(item.Festival), 40, 44);

    // 绘制文字
    context.fillStyle = '#ffffff';
    context.font = 'bold 20px Arial';
    context.textAlign = 'left';
    const title = item.Title.length > 6 ? item.Title.slice(0, 6) + '...' : item.Title;
    context.fillText(title, 70, 40);

    const texture = new THREE.CanvasTexture(canvas);
    const material = new THREE.SpriteMaterial({ 
      map: texture,
      transparent: true
    });

    const sprite = new THREE.Sprite(material);
    sprite.position.set(
      center.x + offset,
      center.y + yOffset,
      center.z
    );
    sprite.scale.set(1.5, 0.375, 1);
    sprite.userData = { 
      type: 'tag', 
      item,
      originalScale: { x: 1.5, y: 0.375 }
    };

    this.tagGroup.add(sprite);
    this.tags.push(sprite);

    // 添加连接线
    const lineGeometry = new THREE.BufferGeometry().setFromPoints([
      new THREE.Vector3(center.x, 0.1, center.z),
      new THREE.Vector3(center.x + offset, center.y + yOffset - 0.2, center.z)
    ]);
    const lineMaterial = new THREE.LineBasicMaterial({ 
      color: 0xffd700,
      transparent: true,
      opacity: 0.5
    });
    const line = new THREE.Line(lineGeometry, lineMaterial);
    this.tagGroup.add(line);
  }

  // 事件绑定
  bindEvents() {
    this.container.addEventListener('mousemove', this.onMouseMove);
    this.container.addEventListener('click', this.onClick);
    window.addEventListener('resize', this.onResize);
  }

  // 鼠标移动
  onMouseMove(event) {
    const rect = this.container.getBoundingClientRect();
    this.mouse.x = ((event.clientX - rect.left) / rect.width) * 2 - 1;
    this.mouse.y = -((event.clientY - rect.top) / rect.height) * 2 + 1;

    this.raycaster.setFromCamera(this.mouse, this.camera);

    // 检测省份hover
    const intersects = this.raycaster.intersectObjects(this.provinces);
    
    if (intersects.length > 0) {
      const province = intersects[0].object;
      
      if (this.hoveredProvince !== province) {
        // 恢复之前的省份
        if (this.hoveredProvince) {
          this.setProvinceHighlight(this.hoveredProvince, false);
        }
        
        // 高亮新省份
        this.hoveredProvince = province;
        this.setProvinceHighlight(province, true);
        
        // 停止自动旋转
        this.controls.autoRotate = false;
        
        // 触发事件
        this.onProvinceHover?.(province.userData);
      }
    } else {
      if (this.hoveredProvince) {
        this.setProvinceHighlight(this.hoveredProvince, false);
        this.hoveredProvince = null;
        this.controls.autoRotate = true;
        this.onProvinceLeave?.();
      }
    }

    // 检测标签hover
    const tagIntersects = this.raycaster.intersectObjects(this.tags);
    if (tagIntersects.length > 0) {
      const tag = tagIntersects[0].object;
      this.setTagHover(tag, true);
    } else {
      this.tags.forEach(tag => this.setTagHover(tag, false));
    }
  }

  // 点击事件
  onClick(event) {
    if (this.hoveredProvince) {
      this.selectedProvince = this.hoveredProvince;
      this.onProvinceClick?.(this.hoveredProvince.userData);
      this.focusOnProvince(this.hoveredProvince);
    }

    // 检测标签点击
    this.raycaster.setFromCamera(this.mouse, this.camera);
    const tagIntersects = this.raycaster.intersectObjects(this.tags);
    if (tagIntersects.length > 0) {
      const tag = tagIntersects[0].object;
      this.onTagClick?.(tag.userData.item);
    }
  }

  // 设置省份高亮
  setProvinceHighlight(province, highlight) {
    if (highlight) {
      province.material.emissive.setHex(0x4a90e2);
      province.material.emissiveIntensity = 0.3;
      document.body.style.cursor = 'pointer';
    } else {
      province.material.emissive.setHex(0x000000);
      province.material.emissiveIntensity = 0;
      document.body.style.cursor = 'default';
    }
  }

  // 设置标签hover效果
  setTagHover(tag, hover) {
    const original = tag.userData.originalScale;
    if (hover) {
      tag.scale.set(original.x * 1.1, original.y * 1.1, 1);
      tag.material.opacity = 1;
    } else {
      tag.scale.set(original.x, original.y, 1);
      tag.material.opacity = 0.9;
    }
  }

  // 聚焦到省份
  focusOnProvince(province) {
    const targetPosition = province.position.clone();
    targetPosition.z += 3;
    targetPosition.y += 2;

    this.isAnimating = true;
    
    // 简单的相机移动动画
    const startPos = this.camera.position.clone();
    const startTarget = this.controls.target.clone();
    const duration = 1000;
    const startTime = Date.now();

    const animateCamera = () => {
      const elapsed = Date.now() - startTime;
      const progress = Math.min(elapsed / duration, 1);
      const eased = 1 - Math.pow(1 - progress, 3);

      this.camera.position.lerpVectors(startPos, targetPosition, eased);
      this.controls.target.lerpVectors(startTarget, province.position, eased);

      if (progress < 1) {
        requestAnimationFrame(animateCamera);
      } else {
        this.isAnimating = false;
      }
    };

    animateCamera();
  }

  // 窗口大小变化
  onResize() {
    this.options.width = this.container.clientWidth;
    this.options.height = this.container.clientHeight;
    
    this.camera.aspect = this.options.width / this.options.height;
    this.camera.updateProjectionMatrix();
    this.renderer.setSize(this.options.width, this.options.height);
  }

  // 动画循环
  animate() {
    requestAnimationFrame(() => this.animate());
    
    this.controls.update();
    this.renderer.render(this.scene, this.camera);
  }

  // 销毁
  destroy() {
    this.container.removeEventListener('mousemove', this.onMouseMove);
    this.container.removeEventListener('click', this.onClick);
    window.removeEventListener('resize', this.onResize);
    
    this.renderer.dispose();
    this.container.removeChild(this.renderer.domElement);
  }

  // 筛选标签
  filterTags(category, region) {
    this.tags.forEach(tag => {
      const item = tag.userData.item;
      let visible = true;

      if (category && !this.matchesCategory(item, category)) {
        visible = false;
      }

      if (region && !this.matchesRegion(item, region)) {
        visible = false;
      }

      tag.visible = visible;
    });
  }

  matchesCategory(item, category) {
    // 简化的分类匹配逻辑
    const categoryMap = {
      '节日': ['春节', '元宵节', '清明节', '端午节', '七夕节', '中秋节', '重阳节'],
      '表演': ['舞', '戏', '歌', '杂技'],
      '手工艺': ['瓷', '画', '陶', '绣', '编'],
      '饮食': ['食', '酒', '茶']
    };

    const keywords = categoryMap[category] || [category];
    return keywords.some(k => item.Title.includes(k) || item.Description.includes(k));
  }

  matchesRegion(item, region) {
    const province = this.getProvinceName(item.Region);
    return province === region || item.Region.includes(region);
  }
}

// 导出
if (typeof module !== 'undefined' && module.exports) {
  module.exports = Folk3DMap;
}
