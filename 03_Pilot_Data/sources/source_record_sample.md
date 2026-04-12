# 来源资料登记样板

## 文档信息
- 模块编号：`M03-04`
- 文件路径：`03_Pilot_Data/sources/source_record_sample.md`
- 对应模板：`02_Templates/template_source.md`
- 当前版本：`v1.1.0`
- 当前状态：`approved`

---

## 重要声明
本文件仅为结构样板，所有内容均为占位示例，不代表真实来源。

使用要求：
- 可以复制本文件作为来源资料骨架。
- 可以保留 `SRC-XXXX`、`待补`、`待核查`、`待回填` 等占位写法。
- 不得虚构来源书名、作者、出版信息、版权状态和来源编号。
- 首期 `media_files` 只记录文件名或相对路径，不把 `MED-XXXX` 当作首期必填字段。

---

## 占位写法规则
| 占位类型 | 写法 | 适用场景 |
|---|---|---|
| 待分配 | `SRC-XXXX` | 来源编号未正式分配 |
| 待补 | `（待补：说明）` | 信息尚未查证完成 |
| 待核查 | `（待核查：说明）` | 信息存疑，暂不能作为确定事实 |
| 待回填 | `（待回填：CUS-XXXX）` | 需在实例建立后回填 |

---

## 字段对齐样板

```md
source_id: SRC-XXXX
source_title: （待补：来源标题）
source_type: （待补：从枚举选择）
author: （待补：作者或机构）
publish_year: （待补：年份）
publisher: （选填）
volume_page: （选填）
source_content: （待补：来源内容摘要）
source_full_text: （选填）
source_quotation: （选填）
access_method: （待补：从枚举选择）
access_date: YYYY-MM-DD
archive_location: （选填）
reliability_assessment: 待核查
reliability_reason: （待补：评估依据）
related_place_id: PLC-XXXX
related_custom_instance_ids: （待回填：CUS-XXXX）
is_media: 否
media_files: （选填：文件名或相对路径）
copyright_status: 未知
usage_restrictions: （选填）
creator: 占位录入人
created_at: YYYY-MM-DD
updated_at: YYYY-MM-DD
version: v1.1.0
```

---

## 可占位内容与必须查证内容

| 字段 | 可先占位 | 必须查证后填写 |
|---|---|---|
| `source_title` | 是 | 最终标题必须与真实来源一致 |
| `source_type` | 是 | 最终必须使用标准枚举 |
| `author` / `publish_year` / `publisher` | 是 | 需与来源实物或正式记录核对 |
| `source_content` | 是 | 最终摘要必须基于真实来源内容 |
| `access_method` / `access_date` | 是 | 获取方式和日期必须真实记录 |
| `reliability_assessment` | 是 | 最终等级和依据必须合理 |
| `related_place_id` | 是 | 最终必须指向真实地点 |
| `related_custom_instance_ids` | 是 | 仅在实例建立后回填 |
| `is_media` | 是 | 最终必须明确 |
| `media_files` | 是 | 首期只填文件名或相对路径 |
| `copyright_status` | 是 | 最终必须明确版权状态 |

---

## 来源与媒体边界说明
- `Source` 负责记录证据来源的责任、获取、摘要和可信度。
- `Media` 当前属于扩展 / 预留实体，首期默认依附于 `Source` 使用。
- 首期如果来源包含照片、视频、音频，只在 `media_files` 中填写文件名或相对路径，不要求建立独立 `MED` 编号。

---

## 最小可用样板标准
- 必须保留与模板一致的字段名。
- 必须明确哪些字段仍是占位内容。
- 必须使用 `related_custom_instance_ids` 正式字段名。
- 必须明确 `media_files` 首期不等于 `MED-XXXX` 列表。

---

## 修改记录

| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M03-04 | v1.0.0 | 完善来源资料登记样板，明确可信度评估、版权状态、关联方式 | 来源数据录入 |
| 2026-03-24 | Codex | M03-04 | v1.1.0 | 改写为与来源模板字段一一对应的结构样板，并统一来源与媒体边界口径 | 样板层、模板层、状态同步 |

---

## 本次完成内容

- 将来源资料样板改写为与 `template_source.md` 对齐的字段样板。
- 统一使用 `related_custom_instance_ids` 正式字段名。
- 明确首期 `media_files` 只记录文件名或相对路径。

## 未决问题

- 若二期正式独立建模 `Media`，本样板需再补充 `MED` 回填规则。

## 建议下一个模块

- `M04-01 网站信息架构`
