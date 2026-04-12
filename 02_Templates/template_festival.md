# 节日录入模板

## 文档信息
- 模块编号：`M02-01`
- 文件路径：`02_Templates/template_festival.md`
- 对应实体：`Festival（节日）`
- 当前版本：`v1.1.0`
- 当前状态：`approved`

---

## 使用说明
本模板用于录入节日实体。节日是抽象知识层对象，用于回答“这是什么节日、通常如何被理解和实践”，不直接承载某一地点的具体做法。

使用要求：
- 不得把“地方习俗实例”写进节日模板。
- 地方差异通过 `related_custom_instance_ids` 回填，不在本模板中展开。
- 字段与枚举必须与标准层保持一致。

---

## 录入字段

### 1. 标识与名称

#### 【必填】节日标识
- 字段名：`festival_id`
- 填写规则：新建时留空，按 `FES-XXXX` 规则分配。

#### 【必填】节日名称
- 字段名：`festival_name`
- 示例：`春节`

#### 【选填】节日英文名
- 字段名：`festival_name_en`
- 示例：`Spring Festival`

#### 【选填】节日别名
- 字段名：`festival_alias`
- 填写规则：多个值用逗号分隔。
- 示例：`农历新年,过年,大年`

### 2. 分类与时间

#### 【必填】节日类型
- 字段名：`festival_type`
- 枚举来源：`festival_type`

#### 【选填】农历日期
- 字段名：`lunar_date`
- 示例：`正月初一`

#### 【选填】公历日期
- 字段名：`solar_date`
- 示例：`1月下旬至2月中旬之间`

### 3. 核心描述

#### 【选填】起源说明
- 字段名：`origin_description`

#### 【必填】通用习俗
- 字段名：`general_customs`
- 填写规则：只写跨地区可成立的通用做法。

#### 【选填】象征意义
- 字段名：`symbolic_meaning`

### 4. 关联对象

#### 【选填】包含仪式
- 字段名：`included_ritual_ids`
- 填写规则：多个 `RIT-XXXX` 用逗号分隔。

#### 【选填】相关节日
- 字段名：`related_festival_ids`
- 填写规则：多个 `FES-XXXX` 用逗号分隔。

#### 【选填】关联地方习俗实例
- 字段名：`related_custom_instance_ids`
- 填写规则：仅在下游实例建立后回填。

### 5. 记录信息

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
- [ ] `festival_type` 是否使用标准枚举
- [ ] `general_customs` 是否仍保持抽象层表述
- [ ] 是否误写了某地的具体流程、供品或禁忌
- [ ] `related_custom_instance_ids` 是否仅用于回填，不替代正文

---

## 录入示例

```md
festival_id: FES-0001
festival_name: 春节
festival_name_en: Spring Festival
festival_alias: 农历新年,过年,大年
festival_type: 传统节日
lunar_date: 正月初一
solar_date: 1月下旬至2月中旬之间
origin_description: 作为抽象节日词条，仅记录稳定结构。
general_customs: 贴春联、团圆聚餐、拜年、压岁钱等跨地区通行做法。
symbolic_meaning: 辞旧迎新、团圆纳福。
included_ritual_ids: RIT-0001,RIT-0002
related_festival_ids: FES-0009,FES-0010
related_custom_instance_ids:
creator: 张三
created_at: 2026-03-23
updated_at: 2026-03-23
version: v1.1.0
```

---

## 修改记录
| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M02-01 | v1.0.0 | 完成节日录入模板首版 | 节日数据录入 |
| 2026-03-23 | Codex | M02-01 | v1.1.0 | 对齐字段表与枚举字典，补齐 `updated_at` 并收紧抽象层边界 | 模板层、样板数据层 |

---

## 本次完成内容
- 对齐 `Festival` 字段组与模板字段名。
- 统一节日模板与地方习俗实例模板的边界。
- 补齐 `updated_at` 与关联回填口径。

## 未决问题
- 节日日期是否需要进一步细分到更强结构化格式，后续由产品映射层决定。

## 建议下一个模块
- `M02-04 录入与审核清单`
