# 来源资料录入模板

## 文档信息
- 模块编号：`M02-03`
- 文件路径：`02_Templates/template_source.md`
- 对应实体：`Source（来源资料）`
- 当前版本：`v1.1.0`
- 当前状态：`approved`

---

## 使用说明
本模板用于录入来源资料实体。来源资料是地方习俗实例事实判断的证据载体，用于保证内容可追溯、可核查。

使用要求：
- 所有事实性记录都应尽量对应明确来源。
- 来源资料可以同时支撑多个地方习俗实例。
- 首期 `media_files` 以文件名或相对路径记录；二期如 `Media` 独立建模，再升级为 `MED` 引用。

---

## 录入字段

### 1. 标识与分类

#### 【必填】来源标识
- 字段名：`source_id`
- 填写规则：新建时留空，按 `SRC-XXXX` 规则分配。

#### 【必填】来源标题
- 字段名：`source_title`

#### 【必填】来源类型
- 字段名：`source_type`
- 枚举来源：`source_type`

### 2. 责任信息

#### 【选填】作者 / 机构
- 字段名：`author`

#### 【选填】出版 / 记录年份
- 字段名：`publish_year`

#### 【选填】出版机构
- 字段名：`publisher`

#### 【选填】卷次页码
- 字段名：`volume_page`

### 3. 内容信息

#### 【选填】来源内容摘要
- 字段名：`source_content`

#### 【选填】来源全文
- 字段名：`source_full_text`

#### 【选填】原文摘录
- 字段名：`source_quotation`

### 4. 获取与馆藏

#### 【选填】获取方式
- 字段名：`access_method`
- 枚举来源：`access_method`

#### 【选填】获取日期
- 字段名：`access_date`
- 格式：`YYYY-MM-DD`

#### 【选填】馆藏 / 存放位置
- 字段名：`archive_location`

### 5. 可靠性评估

#### 【选填】可靠性评估
- 字段名：`reliability_assessment`
- 枚举来源：`reliability_level`

#### 【选填】评估依据
- 字段名：`reliability_reason`

### 6. 关联对象

#### 【选填】关联地点
- 字段名：`related_place_id`

#### 【选填】关联地方习俗实例
- 字段名：`related_custom_instance_ids`
- 填写规则：多个 `CUS-XXXX` 用逗号分隔。

### 7. 媒体与版权

#### 【必填】是否含媒体
- 字段名：`is_media`
- 可选值：`是 / 否`

#### 【选填】媒体文件列表
- 字段名：`media_files`
- 填写规则：首期填文件名或相对路径；二期可升级为 `MED-XXXX` 列表。
- 示例：`dingpo_chunjie_2024_001.jpg`

#### 【选填】版权状态
- 字段名：`copyright_status`
- 枚举来源：`copyright_status`

#### 【选填】使用限制
- 字段名：`usage_restrictions`

### 8. 记录信息

#### 【必填】创建人
- 字段名：`creator`

#### 【必填】创建时间
- 字段名：`created_at`
- 格式：`YYYY-MM-DD`

#### 【必填】更新时间
- 字段名：`updated_at`
- 格式：`YYYY-MM-DD`

#### 【必填】版本号
- 字段名：`version`
- 示例：`v1.1.0`

---

## 待核查项
- [ ] `source_type`、`access_method`、`copyright_status` 是否使用标准枚举
- [ ] `related_custom_instance_ids` 是否使用正式字段名
- [ ] 是否写清了来源责任信息和获取路径
- [ ] `media_files` 是否按首期口径记录为文件名或相对路径
- [ ] 是否避免把来源资料和媒体资料混成同一层级实体

---

## 录入示例

```md
source_id: SRC-0001
source_title: 《涵江区志》
source_type: 地方志
author: 涵江区地方志编纂委员会
publish_year: 2010
publisher: 中华书局
volume_page: 第245-250页
source_content: 记录相关习俗内容的摘要。
source_full_text:
source_quotation:
access_method: 图书馆查阅
access_date: 2026-03-23
archive_location: 福建省图书馆地方文献室
reliability_assessment: 高
reliability_reason: 官方出版物，可追溯。
related_place_id: PLC-0001
related_custom_instance_ids: CUS-0001
is_media: 否
media_files:
copyright_status: 合理使用
usage_restrictions: 引用需注明出处。
creator: 张三
created_at: 2026-03-23
updated_at: 2026-03-23
version: v1.1.0
```

---

## 修改记录
| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M02-03 | v1.0.0 | 完成来源资料录入模板首版 | 来源数据录入 |
| 2026-03-23 | Codex | M02-03 | v1.1.0 | 对齐字段表，修正 `related_custom_instance_ids`、`media_files` 口径并补齐 `updated_at` | 模板层、样板数据层、媒体边界 |

---

## 本次完成内容
- 将来源模板字段名与字段表完全对齐。
- 明确来源资料与媒体资料在首期的依附关系。
- 补齐 `updated_at`。

## 未决问题
- `source_full_text` 的存储策略后续可按实际资料量再细化。

## 建议下一个模块
- `M02-04 录入与审核清单`
