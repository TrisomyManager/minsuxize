# 首期数据模型草案

## 文档信息
- 文件路径：`06_Future/data_model_draft.md`
- 当前版本：`v1.0.0`
- 当前状态：`approved`

## 1. 文档目标
本文件只定义首期数据对象和关系草案，不进入 SQL、表结构实现和具体技术框架。

## 2. 首期 5 个核心数据对象

### 2.1 Place
用途：
- 作为所有核心记录的地点锚点

关键字段分组：
- 标识与层级
- 行政与地理信息
- 补充说明
- 关联对象
- 记录信息

### 2.2 Festival
用途：
- 记录节日的抽象词条

关键字段分组：
- 标识与名称
- 分类与时间
- 通用描述
- 关联对象
- 记录信息

### 2.3 Ritual
用途：
- 记录仪式的抽象流程词条

关键字段分组：
- 标识与名称
- 分类与目的
- 通用结构
- 关联对象
- 记录信息

### 2.4 CustomInstance
用途：
- 记录“某地、某时、某场景中的具体习俗实践”
- 这是首期核心记录单位

关键字段分组：
- 标识与标题
- 地点锚点
- 节日 / 仪式 / 场景
- 时间信息
- 核心叙述
- 参与结构
- 器物与物质表达
- 规范与禁忌
- 地方解释与差异
- 存续与证据
- 记录信息

### 2.5 Source
用途：
- 记录支撑事实的来源资料

关键字段分组：
- 标识与分类
- 责任信息
- 内容信息
- 获取与馆藏
- 可靠性评估
- 关联对象
- 媒体与版权
- 记录信息

## 3. 关键引用关系
- `CustomInstance.place_id -> Place.place_id`
- `CustomInstance.festival_id -> Festival.festival_id`
- `CustomInstance.ritual_id -> Ritual.ritual_id`
- `CustomInstance.source_ids -> Source.source_id[]`
- `Source.related_place_id -> Place.place_id`
- `Source.related_custom_instance_ids -> CustomInstance.custom_instance_id[]`
- `Festival.included_ritual_ids -> Ritual.ritual_id[]`
- `Ritual.related_festival_ids -> Festival.festival_id[]`

## 4. 状态字段
首期统一使用：
- `review_status`

允许取值：
- `draft`
- `in_review`
- `approved`
- `archived`

说明：
- 这是后台工作流字段
- 不再使用中文状态码作为系统值

## 5. 枚举字段
首期重要枚举字段包括：
- `place_level`
- `festival_type`
- `ritual_type`
- `continuity_status`
- `source_type`
- `reliability_level`
- `access_method`
- `copyright_status`
- `review_status`

所有枚举值必须以 `01_Standards/enum_dictionary.md` 为准。

## 6. 来源与媒体字段处理方式
首期采用“来源主体 + 媒体附属字段”方案：
- `Source` 作为证据对象独立存在
- `media_files` 作为 `Source` 的字段处理
- 媒体文件在首期以文件名或相对路径挂接
- `Media` 模板只作为扩展预留，不要求首期独立建模

## 7. 为什么当前阶段不独立建模扩展实体
首期不独立建模 `ROL / OBJ / BEL / TIM / MED`，原因如下：
- 当前知识底座已经足以支撑后台 / CMS MVP
- 扩展实体如果现在独立建模，会增加表单、关系、搜索和审核复杂度
- 产品映射层已明确首期围绕 5 个核心实体即可闭环
- 扩展实体仍可通过文本字段和附属字段先被记录

## 修改记录
| 日期 | 修改人 | 版本 | 修改摘要 |
|---|---|---|---|
| 2026-03-24 | Codex | v1.0.0 | 完成首期数据模型草案首版，明确 5 个核心对象、字段分组、引用关系和状态字段 |
