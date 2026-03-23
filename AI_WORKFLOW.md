# AI 工作流文档 - 中国民俗细则项目

## 项目概述

**项目名称：** minsuxize (中国民俗细则)
**项目目标：** 建立中国民俗文化的数字化记录、整理、审核与发布平台
**技术栈：** ASP.NET Core MVC, SQLite, C#
**项目状态：** 原型阶段，基础框架已搭建

## 当前状态 (2026-03-22)

### ✅ 已完成功能
1. **基础架构**
   - ASP.NET Core MVC 项目结构
   - SQLite 数据库集成
   - 依赖注入配置
   - 静态资源支持

2. **数据模型**
   - `Region` - 行政区划（省-市-县-乡镇-村五级）
   - `Festival` - 节日/时间节点
   - `FolkloreEntry` - 民俗条目
   - `SourceEvidence` - 来源证据
   - `SubmissionRecord` - 投稿记录

3. **核心功能**
   - 地区浏览与导航
   - 节日详情展示
   - 民俗条目查看
   - 管理员登录系统（用户名：admin，密码：MinsuAdmin123!）

4. **数据初始化**
   - 全国省级行政区划数据
   - 代表性省会/首府节点
   - 传统节日目录
   - 24 节气目录
   - 山西长治示例数据

### 🔄 正在进行中
- 项目刚刚从 GitHub 克隆完成
- 正在进行项目分析和优化建议制定

### ❌ 待实现功能
1. 用户投稿系统前端界面
2. 审核工作流完整实现
3. 版本历史追踪
4. 地理空间可视化
5. 多媒体文件上传
6. 高级搜索功能
7. API 接口
8. 用户注册/登录系统

## 最近修改记录

### 2026-03-23 - 第三阶段：审核工作流增强
**执行者：** 清风 (Qingfeng)
**修改内容：**
1. **审核历史模型** - 创建了 ReviewHistory 和 ReviewHistoryEntity 数据模型
2. **数据库扩展** - 在 AppDbContext 中添加了 ReviewHistories 表
3. **仓库接口扩展** - 更新了 IFolkloreRepository 接口，添加了：
   - UpdateSubmissionStatus（支持审核人参数）
   - GetReviewHistory（获取审核历史）
   - AddReviewHistory（添加审核记录）
   - BulkUpdateSubmissionStatus（批量更新状态）
4. **仓库实现更新** - 更新了 EfFolkloreRepository 和 InMemoryFolkloreRepository
5. **控制器扩展** - AdminController 添加了：
   - BulkReview 方法（批量审核）
   - History 方法（查看审核历史）
6. **视图模型创建** - 创建了 ReviewHistoryViewModel 和 BulkReviewViewModel
7. **视图创建** - 创建了 History.cshtml（审核历史页面）和 _BulkActions.cshtml（批量操作部分视图）
8. **管理界面增强** - 更新了 Admin/Index.cshtml：
   - 添加了批量操作面板
   - 添加了审核历史链接
   - 改进了 UI 布局

**技术细节：**
- 审核历史记录完整的变更信息（谁、何时、从什么状态改为什么状态）
- 批量操作使用 JavaScript 客户端交互
- 审核人追踪使用 User.Identity?.Name
- 数据库表自动创建和迁移

**验证结果：**
- ✅ 项目编译成功（0 错误，0 警告）
- ✅ 数据库自动创建并填充数据
- ✅ 服务正常运行在 http://localhost:5088
- ✅ 数据库表结构已更新（包含 ReviewHistories 表）
- ✅ 所有新功能代码已实现

### 2026-03-23 - 第二阶段：用户投稿系统增强
**执行者：** 清风 (Qingfeng)
**修改内容：**
1. **数据模型扩展** - 为 SubmissionRecord 添加完整的新字段：
   - CreatedAt/UpdatedAt (创建/更新时间)
   - CreatedBy (创建者)
   - Version (版本号)
   - ChangeLog (变更日志)
   - Images/Videos/Audios (多媒体链接列表)
   - Location (地理位置信息)
   - 对应的 SubmissionRecord 模型也同步更新

2. **数据库更新** - 更新了 SubmissionRecordEntity 和数据库上下文配置，添加了新字段的默认值

3. **视图模型增强** - SubmitEntryViewModel 添加了：
   - 图片/视频/音频链接输入字段（支持多个链接，逗号分隔）
   - 地理位置字段（纬度、经度、地址）
   - 变更说明字段
   - 便捷方法将输入转换为列表和对象

4. **控制器更新** - SubmitController 现在处理所有新字段的提交

5. **仓库层更新** - EfFolkloreRepository 和 InMemoryFolkloreRepository 支持新字段的存储和检索

6. **视图更新**：
   - Create.cshtml：添加了新字段的表单控件
   - Thanks.cshtml：显示投稿详情，包括多媒体链接和地理位置
   - Admin/Index.cshtml：管理员界面显示所有新字段信息

7. **编译修复** - 解决了 LocationInfo 类命名冲突问题，改为 LocationInfoData

**技术细节：**
- 使用 JSON 序列化存储多媒体链接和地理位置信息
- 所有新字段都有合理的默认值
- 数据库自动迁移支持新字段
- 表单验证和用户体验优化

**验证结果：**
- ✅ 项目构建成功（0 错误，0 警告）
- ✅ 数据库成功创建并填充数据
- ✅ 服务正常运行在 http://localhost:5088
- ✅ 投稿页面可访问（/submit/create）
- ✅ 管理员页面可访问（/admin）
- ✅ 所有新字段正确存储和显示

### 2026-03-23 - 数据模型扩展与数据库重构
**执行者：** 清风 (Qingfeng)
**修改内容：**
1. **数据模型扩展** - 为 FolkloreEntry 添加了以下字段：
   - CreatedAt (创建时间)
   - UpdatedAt (更新时间)
   - CreatedBy (创建者)
   - Status (状态: draft, submitted, reviewing, approved, published, archived)
   - Version (版本号)
   - ChangeLog (变更日志)
   - ImagesJson (图片列表，JSON 序列化)
   - VideosJson (视频列表，JSON 序列化)
   - AudiosJson (音频列表，JSON 序列化)
   - LocationJson (地理位置，JSON 序列化)
   - 对应的 FolkloreEntry 模型也同步更新

2. **数据库上下文配置** - 在 AppDbContext 中为新字段添加了默认值配置

3. **数据种子更新** - 更新了 DbSeeder.cs，为三个示例条目添加了新字段的示例数据

4. **仓库层修复** - 更新了 InMemoryFolkloreRepository.cs 和 EfFolkloreRepository.cs 以适配新的模型结构

5. **环境配置** - 安装 .NET 10.0 SDK，项目成功构建并运行

**技术细节：**
- 使用 SQLite 数据库，通过 `EnsureCreated()` 自动创建表结构
- 新增字段都有合理的默认值（如 CreatedAt/UpdatedAt 使用当前时间，Status 默认为 "draft"）
- 地理位置使用 JSON 序列化存储，便于扩展
- 多媒体文件路径使用 JSON 数组存储

**验证结果：**
- ✅ 项目构建成功（0 错误，0 警告）
- ✅ 数据库成功创建并填充数据
- ✅ 服务正常运行在 http://localhost:5088
- ✅ 所有数据字段正确插入

### 2026-03-22 - 项目初始化与分析
**执行者：** 清风 (Qingfeng)
**修改内容：**
1. 从 GitHub 克隆项目：`git clone https://github.com/TrisomyManager/minsuxize.git`
2. 分析项目结构和代码
3. 创建 AI 工作流文档
4. 生成详细的项目分析报告和优化建议

**分析发现：**
- 项目定位清晰，文化价值高
- 基础框架完整但功能待完善
- 数据模型设计合理但缺少关键字段
- 缺少用户投稿和审核工作流实现

**交接事项：**
- 项目已成功克隆到本地
- 可以运行 `dotnet run --project src/MinsuXize.Web/MinsuXize.Web.csproj` 启动项目
- 管理员登录：`/manage/login` (admin/MinsuAdmin123!)
- 详细优化建议见下文

## 技术架构详情

### 项目结构
```
minsuxize/
├── src/MinsuXize.Web/          # 主 Web 项目
│   ├── Controllers/            # 控制器
│   ├── Models/                 # 数据模型
│   ├── ViewModels/             # 视图模型
│   ├── Views/                  # Razor 视图
│   ├── Data/                   # 数据层
│   └── Services/               # 服务层
├── docs/                       # 文档
├── kimi_artifact_1773662837372.md  # 项目设计文档
└── README.md                   # 项目说明
```

### 数据库
- **类型：** SQLite
- **文件位置：** `src/MinsuXize.Web/App_Data/minsuxize.db` (首次运行时自动创建)
- **Provider：** Entity Framework Core

### 运行方式
```bash
# 进入项目目录
cd minsuxize

# 启动项目
dotnet run --project src/MinsuXize.Web/MinsuXize.Web.csproj

# 或先编译
dotnet build src/MinsuXize.Web/MinsuXize.Web.csproj
dotnet run --project src/MinsuXize.Web/MinsuXize.Web.csproj
```

## 优化建议（按优先级排序）

### 高优先级
1. **数据模型扩展**
   - 为 FolkloreEntry 添加时间戳字段（CreatedAt, UpdatedAt）
   - 添加状态字段（draft, submitted, reviewing, published, archived）
   - 添加版本控制字段（Version, ChangeLog）
   - 添加多媒体支持字段（Images, Videos, Audios）

2. **审核工作流实现**
   - 状态机：draft → submitted → reviewing → approved/rejected → published → archived
   - 审核界面：原版 vs 修改版对比
   - 审核记录：审核人、时间、意见
   - 批量审核功能

3. **用户投稿系统**
   - 投稿表单前端实现
   - 数据验证和提交
   - 投稿状态跟踪

### 中优先级
4. **版本控制系统**
   - 类似 Git 的版本历史
   - 版本对比（diff 视图）
   - 版本回滚功能

5. **地理信息系统**
   - 集成 Leaflet 或 Mapbox
   - 行政区划层级钻取
   - 地图标注和聚类

6. **多媒体支持**
   - 文件上传功能
   - 格式验证和大小限制
   - 图片压缩和缩略图生成

### 低优先级
7. **检索系统增强**
   - 多维度联合检索
   - 全文搜索支持
   - 智能推荐系统

8. **用户系统完善**
   - 用户注册/登录
   - 贡献度统计
   - 信誉系统

9. **API 设计**
   - RESTful API
   - OpenAPI/Swagger 文档
   - 数据导出功能

## 下一步任务清单

### 第一阶段（1-2周）
- [ ] 运行现有项目，验证功能
- [ ] 扩展 FolkloreEntry 数据模型
- [ ] 实现用户投稿前端界面
- [ ] 创建审核工作流界面
- [ ] 添加版本历史基础功能

### 第二阶段（2-3周）
- [ ] 集成基础地图功能
- [ ] 实现多媒体文件上传
- [ ] 优化搜索功能
- [ ] 添加数据导出功能

### 第三阶段（3-4周）
- [ ] 实现 API 接口
- [ ] 完善用户系统
- [ ] 集成高级搜索
- [ ] 性能优化

## 交接检查清单

**每次修改后请更新：**

### 1. 代码修改记录
- [ ] 修改的文件列表
- [ ] 修改的具体内容
- [ ] 修改原因和目的

### 2. 数据库变更
- [ ] 新增/修改的表结构
- [ ] 数据迁移脚本（如果有）
- [ ] 示例数据更新

### 3. 运行状态验证
- [ ] 项目能否正常启动
- [ ] 核心功能是否正常
- [ ] 管理员登录是否正常
- [ ] 数据展示是否正常

### 4. 待解决问题
- [ ] 已知的 bug 列表
- [ ] 未完成的功能
- [ ] 需要进一步调研的技术点

### 5. 下一步建议
- [ ] 推荐的下一个任务
- [ ] 需要注意的技术细节
- [ ] 潜在的风险和解决方案

## 技术细节

### 管理员配置
- **登录地址：** `/manage/login`
- **默认账号：** admin / MinsuAdmin123!
- **建议：** 尽快修改默认密码，使用 `dotnet user-secrets` 或环境变量管理

### 数据持久化
- 当前使用 SQLite 便于开发
- 生产环境建议切换到 PostgreSQL
- 迁移时需要替换数据库 provider 和连接字符串

### 项目依赖
```xml
<!-- 查看 src/MinsuXize.Web/MinsuXize.Web.csproj 获取完整依赖列表 -->
```

## 文化价值与注意事项

### 文化敏感性
- 民俗记录涉及宗教、民族等敏感内容
- 需要建立内容审查机制
- 避免使用"迷信""落后"等标签化语言

### 数据准确性
- 用户投稿内容需要严格审核
- 建议建立专家审核机制
- 重要内容需要多方验证

### 可持续性
- 考虑与高校/研究机构合作
- 申请文化保护相关基金
- 建立志愿者社区

## 联系信息

**项目维护者：** TrisomyManager
**GitHub 仓库：** https://github.com/TrisomyManager/minsuxize
**最后更新：** 2026-03-23

---

**给下一个 AI 的留言：**

你好！我是清风，已经完成了第一阶段的数据模型扩展工作。项目现在包含完整的状态管理和多媒体支持。

**已完成的工作：**
1. ✅ 数据模型扩展 - FolkloreEntry 现在支持时间戳、状态、版本控制、多媒体文件和地理位置
2. ✅ 数据库重构 - SQLite 数据库已更新，包含所有新字段
3. ✅ 数据种子更新 - 三个示例条目已添加完整的新字段数据
4. ✅ 代码修复 - 修复了 InMemoryFolkloreRepository 和 EfFolkloreRepository 的编译错误
5. ✅ 环境配置 - .NET 10.0 SDK 已安装，项目构建成功

**当前状态：**
- 项目正常运行在 http://localhost:5088
- 数据库已包含扩展后的示例数据
- 所有新增字段都已正确集成

**下一步建议：**
1. **用户投稿系统** - 现在是时候实现前端投稿界面了
2. **审核工作流** - 基于新的 Status 字段实现状态机
3. **管理员界面** - 添加对新字段的显示和编辑功能
4. **多媒体上传** - 实现图片、视频、音频文件上传功能
5. **地理位置可视化** - 集成地图显示功能

**技术注意事项：**
- 地理位置使用 JSON 存储，便于扩展
- 多媒体文件路径使用 JSON 数组存储
- 状态字段支持完整的审核工作流（draft → submitted → reviewing → approved → published → archived）
- 所有新字段都有合理的默认值

项目进展顺利，基础架构已经完善。接下来可以专注于用户体验和功能完善。

- 清风 (Qingfeng) 于 2026-03-23