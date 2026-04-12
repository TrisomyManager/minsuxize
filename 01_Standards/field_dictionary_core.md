# 核心字段表

## 文档信息
- 模块编号：`M01-02`
- 文件路径：`01_Standards/field_dictionary_core.md`
- 当前版本：`v1.2.1`
- 当前状态：`approved`

---

## 1. 字段总览

### 1.1 字段设计原则
- 首期只为 5 个核心实体建立完整字段组：`Place / Festival / Ritual / CustomInstance / Source`。
- 地点是锚点对象，`CustomInstance` 必须通过 `place_id` 绑定地点。
- 模板中的每个字段名都必须先在本表中出现，再允许进入模板层。
- 扩展 / 预留实体不纳入首期常规发号，但可在本表末尾以“扩展模板支持字段”方式预留字段口径。
- 列表字段统一使用 `*_ids`、`*_files`、`*_list`；描述字段统一使用 `*_description`。

### 1.2 字段类型定义
| 类型代码 | 类型名称 | 说明 | 示例 |
|---|---|---|---|
| `ID` | 标识符 | 唯一标识，格式为 `前缀-四位数字` | `PLC-0001` |
| `TXT` | 短文本 | 单行文本 | `福建省莆田市涵江区` |
| `LONG` | 长文本 | 多行说明、流程、阐释 | `习俗详细描述` |
| `ENUM` | 枚举 | 从枚举字典中选择 | `活跃传承` |
| `DATE` | 日期 | 标准日期格式 `YYYY-MM-DD` | `2026-03-23` |
| `REF` | 引用 | 引用其他实体 ID | `FES-0001` |
| `LIST` | 列表 | 多个值或多个 ID | `SRC-0001,SRC-0002` |
| `BOOL` | 布尔 | 是 / 否 | `是` |

### 1.3 首期启用标记
| 标记 | 含义 | 说明 |
|---|---|---|
| `P0` | 必做 | 首期必须稳定落地 |
| `P1` | 应做 | 首期建议启用 |
| `P2` | 预留 | 首期可保留字段，不强制填充 |

---

## 2. 地点（Place）字段

| 字段名 | 中文说明 | 字段类型 | 是否必填 | 示例 | 备注 | 首期启用 |
|---|---|---|---|---|---|---|
| `place_id` | 地点标识 | ID | 是 | `PLC-0001` | 主键 | P0 |
| `place_name` | 地点名称 | TXT | 是 | `福建省莆田市涵江区江口镇` | 完整层级名称 | P0 |
| `place_level` | 地点层级 | ENUM | 是 | `乡镇` | 对应 `place_level` 枚举组 | P0 |
| `parent_place_id` | 父地点标识 | REF | 是 | `PLC-0002` | 根节点可写 `NULL` | P0 |
| `country` | 国家 | TXT | 是 | `中国` | 标准化名称 | P0 |
| `province` | 省 | TXT | 是 | `福建省` | 省级行政区 | P0 |
| `city` | 市 | TXT | 是 | `莆田市` | 地级行政区 | P0 |
| `district` | 县区 | TXT | 否 | `涵江区` | 县级行政区 | P0 |
| `town` | 乡镇街道 | TXT | 否 | `江口镇` | 乡级行政区 | P0 |
| `village` | 行政村 | TXT | 否 | `顶坡村` | 行政村名称 | P0 |
| `natural_village` | 自然村 | TXT | 否 | `后厝自然村` | 自然村名称 | P0 |
| `spatial_point` | 空间点位 | TXT | 否 | `119.12,25.46` | 经纬度或具体点位说明 | P1 |
| `place_alias` | 地点别名 | TXT | 否 | `江口` | 俗称、旧称 | P1 |
| `place_description` | 地点简介 | LONG | 否 | `位于莆田市东北部...` | 人文与地理简介 | P1 |
| `admin_code` | 行政区划代码 | TXT | 否 | `350303104` | 官方代码 | P2 |
| `related_source_ids` | 关联来源标识列表 | LIST | 否 | `SRC-0001,SRC-0002` | 供模板和样板回填 | P2 |
| `related_custom_instance_ids` | 关联地方习俗实例标识列表 | LIST | 否 | `CUS-0001,CUS-0002` | 供模板和样板回填 | P2 |
| `creator` | 创建者 | TXT | 是 | `张三` | 记录创建者 | P0 |
| `created_at` | 创建时间 | DATE | 是 | `2026-03-23` | 记录创建时间 | P0 |
| `updated_at` | 更新时间 | DATE | 是 | `2026-03-23` | 最后更新时间 | P0 |
| `version` | 版本号 | TXT | 是 | `v1.0.0` | 记录版本 | P0 |

---

## 3. 节日（Festival）字段

| 字段名 | 中文说明 | 字段类型 | 是否必填 | 示例 | 备注 | 首期启用 |
|---|---|---|---|---|---|---|
| `festival_id` | 节目标识 | ID | 是 | `FES-0001` | 主键 | P0 |
| `festival_name` | 节日名称 | TXT | 是 | `春节` | 标准名称 | P0 |
| `festival_name_en` | 节日英文名 | TXT | 否 | `Spring Festival` | 英文名称 | P2 |
| `festival_alias` | 节日别名 | LIST | 否 | `农历新年,过年,大年` | 多个别名 | P1 |
| `festival_type` | 节日类型 | ENUM | 是 | `传统节日` | 对应 `festival_type` 枚举组 | P0 |
| `lunar_date` | 农历日期 | TXT | 否 | `正月初一` | 农历时间描述 | P0 |
| `solar_date` | 公历日期 | TXT | 否 | `1-2月之间` | 公历时间范围 | P1 |
| `origin_description` | 起源简述 | LONG | 否 | `源于岁首祭祀活动...` | 历史起源 | P1 |
| `general_customs` | 通用习俗 | LONG | 是 | `贴春联、放鞭炮、拜年...` | 抽象层通用描述 | P0 |
| `symbolic_meaning` | 象征意义 | LONG | 否 | `辞旧迎新、阖家团圆...` | 文化阐释 | P1 |
| `included_ritual_ids` | 包含仪式标识列表 | LIST | 否 | `RIT-0001,RIT-0002` | 与关系目录对齐 | P1 |
| `related_festival_ids` | 相关节日标识列表 | LIST | 否 | `FES-0002,FES-0003` | 相关节日 | P2 |
| `related_custom_instance_ids` | 关联地方习俗实例标识列表 | LIST | 否 | `CUS-0001,CUS-0002` | 供模板和样板回填 | P2 |
| `creator` | 创建者 | TXT | 是 | `张三` | 记录创建者 | P0 |
| `created_at` | 创建时间 | DATE | 是 | `2026-03-23` | 记录创建时间 | P0 |
| `updated_at` | 更新时间 | DATE | 是 | `2026-03-23` | 最后更新时间 | P0 |
| `version` | 版本号 | TXT | 是 | `v1.0.0` | 记录版本 | P0 |

---

## 4. 仪式（Ritual）字段

| 字段名 | 中文说明 | 字段类型 | 是否必填 | 示例 | 备注 | 首期启用 |
|---|---|---|---|---|---|---|
| `ritual_id` | 仪式标识 | ID | 是 | `RIT-0001` | 主键 | P0 |
| `ritual_name` | 仪式名称 | TXT | 是 | `祭祖仪式` | 标准名称 | P0 |
| `ritual_alias` | 仪式别名 | LIST | 否 | `上坟,扫墓,拜山` | 多个别名 | P1 |
| `ritual_type` | 仪式类型 | ENUM | 是 | `祭祀仪式` | 对应 `ritual_type` 枚举组 | P0 |
| `ritual_purpose` | 仪式目的 | LONG | 否 | `向祖先表达敬意...` | 目的说明 | P1 |
| `general_procedure` | 通用流程 | LONG | 是 | `1. 准备供品...` | 抽象层流程 | P0 |
| `key_elements` | 核心要素 | LONG | 否 | `供品、香烛、纸钱...` | 仪式关键元素 | P1 |
| `participants` | 参与者 | LONG | 否 | `家族成员、主祭人...` | 抽象层参与者 | P1 |
| `used_objects_description` | 使用器物描述 | LONG | 否 | `香炉、供桌、祭文...` | 与模板层对齐 | P1 |
| `officiant_role_description` | 主持角色描述 | LONG | 否 | `通常由家族长辈主持...` | 与模板层对齐 | P1 |
| `related_festival_ids` | 关联节日标识列表 | LIST | 否 | `FES-0001,FES-0005` | 与关系目录对齐 | P1 |
| `realized_in_custom_instance_ids` | 已实现地方习俗实例标识列表 | LIST | 否 | `CUS-0001,CUS-0002` | 供模板和样板回填 | P2 |
| `creator` | 创建者 | TXT | 是 | `张三` | 记录创建者 | P0 |
| `created_at` | 创建时间 | DATE | 是 | `2026-03-23` | 记录创建时间 | P0 |
| `updated_at` | 更新时间 | DATE | 是 | `2026-03-23` | 最后更新时间 | P0 |
| `version` | 版本号 | TXT | 是 | `v1.0.0` | 记录版本 | P0 |

---

## 5. 地方习俗实例（CustomInstance）字段

| 字段名 | 中文说明 | 字段类型 | 是否必填 | 示例 | 备注 | 首期启用 |
|---|---|---|---|---|---|---|
| `custom_instance_id` | 实例标识 | ID | 是 | `CUS-0001` | 主键 | P0 |
| `custom_instance_name` | 实例名称 | TXT | 是 | `福建省莆田市涵江区正月初四过大年` | 包含地点的实例名称 | P0 |
| `custom_theme` | 习俗主题 | TXT | 是 | `正月初四过大年` | 核心主题概括 | P0 |
| `place_id` | 地点标识 | REF | 是 | `PLC-0001` | 锚点字段 | P0 |
| `place_name` | 地点名称 | TXT | 是 | `福建省莆田市涵江区` | 冗余存储 | P0 |
| `specific_venue` | 具体场所 | TXT | 否 | `村中祠堂` | 发生的具体场所 | P1 |
| `festival_id` | 关联节目标识 | REF | 否 | `FES-0001` | 关联节日 | P0 |
| `festival_name` | 关联节日名称 | TXT | 否 | `春节` | 冗余存储 | P0 |
| `ritual_id` | 关联仪式标识 | REF | 否 | `RIT-0001` | 关联仪式 | P0 |
| `ritual_name` | 关联仪式名称 | TXT | 否 | `祭祖仪式` | 冗余存储 | P0 |
| `scene_description` | 场景描述 | LONG | 否 | `渔民首次出海前的祭海活动` | 无明确节日 / 仪式时补充说明 | P1 |
| `occurrence_time` | 发生时间 | TXT | 是 | `每年农历正月初四` | 时间描述 | P0 |
| `record_time_range` | 记录时间范围 | TXT | 否 | `2020-2024年观察` | 资料形成范围 | P1 |
| `time_rule_description` | 时间规则说明 | LONG | 否 | `需根据黄历择吉日` | 与 `time_rule_type` 枚举语义对齐 | P1 |
| `custom_description` | 习俗描述 | LONG | 是 | `在莆田市涵江区...` | 实例整体描述 | P0 |
| `custom_procedure` | 具体流程 | LONG | 是 | `1. 清晨祭祖...` | 具体执行步骤 | P0 |
| `spatial_arrangement` | 空间布置 | LONG | 否 | `祠堂正中摆放牌位...` | 空间布置方式 | P1 |
| `movement_route` | 移动路线 | LONG | 否 | `从祠堂出发绕村一周...` | 巡游或 procession 路线 | P1 |
| `participant_composition` | 参与人群 | LONG | 否 | `全村村民，以返乡青壮年为主` | 人群构成 | P1 |
| `participation_conditions` | 参与条件 | LONG | 否 | `仅限本村男性村民` | 身份、性别、年龄等限制 | P1 |
| `officiant_description` | 主持角色说明 | LONG | 否 | `由长房长孙主祭` | 主持者说明 | P1 |
| `role_selection_method` | 角色产生方式 | LONG | 否 | `轮值制，每年由不同房派推选` | 主持角色如何产生 | P1 |
| `objects_description` | 器物描述 | LONG | 否 | `红团、线面、贡银...` | 使用器物与用途 | P1 |
| `object_requirements` | 器物特殊要求 | LONG | 否 | `红团必须手工制作` | 制作、来源、摆放等要求 | P1 |
| `special_attire` | 特殊服饰 | LONG | 否 | `主祭人必须穿长衫马褂` | 着装要求 | P1 |
| `offerings_description` | 供品配置 | LONG | 否 | `五果六斋、三牲...` | 供品说明 | P1 |
| `ritual_texts` | 仪式唱词 / 用语 | LONG | 否 | `祭文内容...` | 唱词、祝词、祭文 | P1 |
| `behavior_taboos` | 行为禁忌 | LONG | 否 | `当天不得扫地...` | 禁忌说明 | P1 |
| `rules_and_constraints` | 规矩与约束 | LONG | 否 | `必须在日出前完成` | 必须遵守的规则 | P1 |
| `symbolic_meaning_local` | 地方意义阐释 | LONG | 否 | `当地人认为初四是祖先回家的日子...` | 地方性意义解释 | P1 |
| `functional_role` | 功能与作用 | LONG | 否 | `强化宗族凝聚力` | 社会功能 | P1 |
| `regional_differences` | 地域差异说明 | LONG | 否 | `与周边地区初一过年不同...` | 变体与差异 | P1 |
| `historical_origin` | 历史渊源 | LONG | 否 | `据《莆田县志》记载...` | 地方特色来源 | P1 |
| `continuity_status` | 存续状态 | ENUM | 是 | `活跃传承` | 对应 `continuity_status` 枚举组 | P0 |
| `continuity_description` | 存续状态说明 | LONG | 否 | `老一辈仍严格遵守...` | 状态原因说明 | P1 |
| `variants_notes` | 变体说明 | LONG | 否 | `与莆田其他地区不同...` | 对比记录 | P1 |
| `source_ids` | 来源标识列表 | LIST | 是 | `SRC-0001,SRC-0002` | 无来源不录事实 | P0 |
| `reliability_level` | 可信度等级 | ENUM | 否 | `高` | 对应 `reliability_level` 枚举组 | P1 |
| `review_status` | 审核状态 | ENUM | 是 | `draft` | 使用后台状态码：`draft / in_review / approved / archived` | P0 |
| `creator` | 创建者 | TXT | 是 | `张三` | 记录创建者 | P0 |
| `created_at` | 创建时间 | DATE | 是 | `2026-03-23` | 记录创建时间 | P0 |
| `updated_at` | 更新时间 | DATE | 是 | `2026-03-23` | 最后更新时间 | P0 |
| `version` | 版本号 | TXT | 是 | `v1.0.0` | 记录版本 | P0 |

---

## 6. 来源资料（Source）字段

| 字段名 | 中文说明 | 字段类型 | 是否必填 | 示例 | 备注 | 首期启用 |
|---|---|---|---|---|---|---|
| `source_id` | 来源标识 | ID | 是 | `SRC-0001` | 主键 | P0 |
| `source_title` | 来源标题 | TXT | 是 | `《莆田市涵江区志》` | 标题或名称 | P0 |
| `source_type` | 来源类型 | ENUM | 是 | `地方志` | 对应 `source_type` 枚举组 | P0 |
| `author` | 作者 / 机构 | TXT | 否 | `涵江区地方志编纂委员会` | 作者或机构 | P0 |
| `publish_year` | 出版 / 记录年份 | TXT | 否 | `2010` | 出版或记录时间 | P0 |
| `publisher` | 出版机构 | TXT | 否 | `中华书局` | 出版社或机构 | P1 |
| `volume_page` | 卷次页码 | TXT | 否 | `第245-250页` | 具体引用位置 | P1 |
| `source_content` | 来源内容摘要 | LONG | 否 | `记载了涵江区正月初四过大年的习俗...` | 摘要 | P1 |
| `source_full_text` | 来源全文 | LONG | 否 | `...` | 长文本可留空 | P2 |
| `source_quotation` | 原文摘录 | LONG | 否 | `“江口一带，俗以正月初四为‘大年’...”` | 便于精确引用 | P1 |
| `access_method` | 获取方式 | ENUM | 否 | `图书馆查阅` | 对应 `access_method` 枚举组 | P1 |
| `access_date` | 获取日期 | DATE | 否 | `2026-03-23` | 获取 / 访问日期 | P1 |
| `archive_location` | 馆藏 / 存放位置 | TXT | 否 | `福建省图书馆地方文献室` | 档案或馆藏位置信息 | P1 |
| `reliability_assessment` | 可信度评估 | ENUM | 否 | `高` | 仍使用 `reliability_level` 枚举组 | P1 |
| `reliability_reason` | 评估依据 | LONG | 否 | `官方出版物，编纂严谨...` | 评估原因 | P1 |
| `related_place_id` | 关联地点标识 | REF | 否 | `PLC-0001` | 来源涉及的主要地点 | P0 |
| `related_custom_instance_ids` | 关联地方习俗实例标识列表 | LIST | 否 | `CUS-0001,CUS-0002` | 引用该来源的实例列表 | P0 |
| `is_media` | 是否含媒体 | BOOL | 是 | `否` | 是否包含媒体文件 | P1 |
| `media_files` | 媒体文件列表 | LIST | 否 | `dingpo_chunjie_2024_001.jpg` | 首期填文件名 / 相对路径；二期可升级为 `MED` 引用 | P1 |
| `copyright_status` | 版权状态 | ENUM | 否 | `合理使用` | 对应 `copyright_status` 枚举组 | P1 |
| `usage_restrictions` | 使用限制 | LONG | 否 | `仅限本项目研究使用` | 使用说明 | P1 |
| `creator` | 创建者 | TXT | 是 | `李四` | 记录创建者 | P0 |
| `created_at` | 创建时间 | DATE | 是 | `2026-03-23` | 记录创建时间 | P0 |
| `updated_at` | 更新时间 | DATE | 是 | `2026-03-23` | 最后更新时间 | P0 |
| `version` | 版本号 | TXT | 是 | `v1.0.0` | 记录版本 | P0 |

---

## 7. 扩展模板支持字段

以下字段用于支持 `template_media.md` 等扩展 / 预留模板，不表示 `Media` 已进入首期核心实体范围。

### 7.1 媒体资料（Media）预留字段
| 字段名 | 中文说明 | 字段类型 | 是否必填 | 示例 | 备注 | 首期启用 |
|---|---|---|---|---|---|---|
| `media_id` | 媒体标识 | ID | 否 | `MED-0001` | 仅二期启用 | P2 |
| `file_name` | 文件名 | TXT | 是 | `dingpo_chunjie_2024_001.jpg` | 首期可作为临时主标识 | P1 |
| `media_type` | 媒体类型 | ENUM | 是 | `照片` | 对应 `media_type` 枚举组 | P1 |
| `file_format` | 文件格式 | TXT | 否 | `JPEG` | 文件格式 | P1 |
| `capture_time` | 拍摄 / 录制时间 | TXT | 否 | `2024-02-13 08:30` | 媒体形成时间 | P1 |
| `time_description` | 时间描述 | TXT | 否 | `2024年春节正月初四上午` | 自然语言时间 | P2 |
| `capture_location` | 拍摄地点 | TXT | 否 | `福建省莆田市涵江区江口镇顶坡村祠堂` | 媒体形成地点 | P1 |
| `related_place_id` | 关联地点标识 | REF | 否 | `PLC-0001` | 与地点的关联 | P2 |
| `content_description` | 内容说明 | LONG | 是 | `顶坡村正月初四祭祖仪式现场照片` | 核心内容描述 | P1 |
| `depicted_subjects` | 画面主体说明 | LONG | 否 | `供桌、主祭人、祠堂牌位...` | 主体对象 | P2 |
| `shot_description` | 拍摄角度 / 景别 | TXT | 否 | `正面中景` | 技术描述 | P2 |
| `related_source_id` | 关联来源标识 | REF | 是 | `SRC-0001` | 首期必须依附来源 | P1 |
| `related_custom_instance_id` | 关联地方习俗实例标识 | REF | 否 | `CUS-0001` | 二期或补充回填 | P2 |
| `related_festival_id` | 关联节目标识 | REF | 否 | `FES-0001` | 二期或补充回填 | P2 |
| `related_ritual_id` | 关联仪式标识 | REF | 否 | `RIT-0001` | 二期或补充回填 | P2 |
| `copyright_status` | 版权状态 | ENUM | 是 | `已授权` | 对应 `copyright_status` 枚举组 | P1 |
| `copyright_holder` | 版权持有人 | TXT | 否 | `张三` | 版权主体 | P1 |
| `usage_authorization` | 使用授权范围 | LONG | 否 | `仅限本项目研究使用` | 授权说明 | P1 |
| `public_display_status` | 公开展示状态 | ENUM | 否 | `需审核` | 对应 `public_display_status` 枚举组 | P2 |
| `display_restrictions` | 展示限制 | LONG | 否 | `人物面部需模糊处理` | 展示限制说明 | P2 |
| `technical_specs` | 技术规格 | TXT | 否 | `4096x2730像素` | 技术信息 | P2 |
| `file_size` | 文件大小 | TXT | 否 | `15.3MB` | 文件大小 | P2 |
| `storage_location` | 存储位置 | TXT | 否 | `/media/2024/hjiang/photos/` | 文件路径或存储桶 | P2 |
| `notes` | 备注 | LONG | 否 | `原始文件为 RAW 格式` | 其他说明 | P2 |
| `creator` | 创建者 | TXT | 是 | `李四` | 记录创建者 | P1 |
| `created_at` | 创建时间 | DATE | 是 | `2026-03-23` | 记录创建时间 | P1 |
| `updated_at` | 更新时间 | DATE | 是 | `2026-03-23` | 最后更新时间 | P1 |
| `version` | 版本号 | TXT | 是 | `v1.0.0` | 记录版本 | P1 |

---

## 8. 字段命名规范

- 标识字段统一使用 `*_id`。
- 多值关联字段统一使用 `*_ids`。
- 描述类字段统一使用 `*_description`。
- 布尔字段统一使用 `is_*`。
- 时间戳字段统一使用 `created_at`、`updated_at`。
- 同一语义不得在模板层使用两个不同字段名。

---

## 9. 修改记录

| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M01-02 | v1.0.0 | 完成核心字段表正式首版，定义首期 5 个核心实体字段 | 下游模板、产品映射 |
| 2026-03-23 | Codex | M01-02 | v1.1.0 | 补充首期仅为 5 个核心实体建立字段组的原则，并明确媒体字段为二期启用 | 下游模板、产品映射、扩展实体边界 |
| 2026-03-23 | Codex | M01-02 | v1.2.0 | 对齐模板层实际字段，补齐 Place / Festival / Ritual / CustomInstance / Source 缺失字段，并新增 Media 预留字段口径 | 模板层、枚举字典、协作回填 |
| 2026-03-24 | Codex | M01-02 | v1.2.1 | 将 `review_status` 示例统一为后台状态码口径，便于直接进入开发启动阶段 | 模板层、产品映射层、工作流草案 |

---

## 本次完成内容

- 将字段表从“结构底座版”提升到“模板对齐版”。
- 补齐了模板层已经稳定使用的字段名，消除字段表与模板层的映射断点。
- 明确 `Media` 仍为扩展 / 预留实体，但允许以预留字段口径支持 `template_media.md`。

## 未决问题

- `Media` 何时从预留字段口径升级为独立实体字段组，仍待样板数据层和产品映射层确认。
- 若后续进入批量录入阶段，可能需要再为录入流程补充“批次号”“校对人”等运营字段。

## 建议下一个模块

- `M02-04 录入与审核清单`
