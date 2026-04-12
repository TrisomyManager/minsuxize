# 地方习俗实例样板

## 文档信息
- 模块编号：`M03-03`
- 文件路径：`03_Pilot_Data/custom_instances/custom_instance_sample.md`
- 对应模板：`02_Templates/template_custom_instance.md`
- 当前版本：`v1.1.1`
- 当前状态：`approved`

---

## 重要声明
本文件仅为结构样板，所有内容均为占位示例，不代表真实地方民俗事实。

使用要求：
- 可以复制本文件作为地方习俗实例骨架。
- 可以保留 `CUS-XXXX`、`PLC-XXXX`、`SRC-XXXX`、`待补`、`待核查`、`待回填` 等占位写法。
- 不得虚构习俗流程、禁忌、意义、存续状态依据和来源编号。
- 所有事实内容必须最终回填到有来源支撑的正式内容。

---

## 占位写法规则
| 占位类型 | 写法 | 适用场景 |
|---|---|---|
| 待分配 | `CUS-XXXX` | 实例编号未正式分配 |
| 待补 | `（待补：说明）` | 信息尚未查证完成 |
| 待核查 | `（待核查：说明）` | 信息存疑，暂不能作为确定事实 |
| 待回填 | `（待回填：XXX-XXXX）` | 需在地点、节日、仪式、来源建立后回填 |

---

## 字段对齐样板

```md
custom_instance_id: CUS-XXXX
custom_instance_name: 中国某省某市某县某镇某村某时间某习俗
custom_theme: 某习俗主题
place_id: PLC-XXXX
place_name: 中国某省某市某县某镇某村
specific_venue: （待补：如祠堂、庙宇、家中）
festival_id: （待回填：FES-XXXX）
festival_name: （待补：如适用）
ritual_id: （待回填：RIT-XXXX）
ritual_name: （待补：如适用）
scene_description: （待补：若不完全属于固定节日或仪式）
occurrence_time: （待补：如每年农历正月初四）
record_time_range: （待补：资料记录或观察时间）
time_rule_description: （待补：如有特殊择日规则）
custom_description: （待补：该实践是什么）
custom_procedure: （待补：按顺序描述具体流程）
spatial_arrangement: （待补）
movement_route: （选填）
participant_composition: （待补）
participation_conditions: （选填）
officiant_description: （待补）
role_selection_method: （选填）
objects_description: （待补）
object_requirements: （选填）
special_attire: （选填）
offerings_description: （选填）
ritual_texts: （选填）
behavior_taboos: （待补）
rules_and_constraints: （选填）
symbolic_meaning_local: （待补）
functional_role: （选填）
regional_differences: （待补：如有对比来源）
historical_origin: （选填）
variants_notes: （选填）
continuity_status: 待评估
continuity_description: （选填）
source_ids: SRC-XXXX
reliability_level: 待核查
review_status: draft
creator: 占位录入人
created_at: YYYY-MM-DD
updated_at: YYYY-MM-DD
version: v1.1.1
```

---

## 可占位内容与必须查证内容

| 字段 | 可先占位 | 必须查证后填写 |
|---|---|---|
| `custom_instance_name` | 是 | 最终名称必须对应真实地点和真实主题 |
| `place_id` / `place_name` | 否 | 必须绑定真实地点，地点是锚点对象 |
| `festival_id` / `ritual_id` | 是 | 若回填则必须指向真实实体 |
| `occurrence_time` | 是 | 最终时间表达必须准确 |
| `custom_description` | 是 | 最终描述不得脱离来源 |
| `custom_procedure` | 否 | 必须有来源支撑后填写 |
| `behavior_taboos` | 否 | 必须有来源支撑后填写 |
| `symbolic_meaning_local` | 否 | 必须有来源支撑后填写 |
| `regional_differences` | 是 | 若填写，必须有对比来源支撑 |
| `continuity_status` | 是 | 最终状态需有依据 |
| `source_ids` | 否 | 必须使用真实来源编号 |
| `review_status` | 是 | 必须使用标准枚举 |

---

## 最小可用样板标准
- 必须完整保留模板字段名。
- 必须体现“地点是锚点对象”和“无来源不录事实”两条规则。
- 必须明确哪些字段仍是占位，哪些字段不能先写成事实。
- 不得把样板内容直接提交为正式事实稿。

---

## 修改记录

| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M03-03 | v1.0.0 | 完善地方习俗实例样板，明确占位规范、预搭骨架范围、必须查证内容 | 实例数据录入 |
| 2026-03-24 | Codex | M03-03 | v1.1.0 | 改写为与地方习俗实例模板字段一一对应的结构样板，并强化地点锚点与来源约束 | 样板层、模板层、状态同步 |
| 2026-03-24 | Codex | M03-03 | v1.1.1 | 将样板中的审核状态示例统一为后台状态码，便于作为 Sprint 1 的结构样板输入 | 样板层、产品映射层、开发启动包 |

---

## 本次完成内容

- 将地方习俗实例样板改写为与 `template_custom_instance.md` 对齐的字段样板。
- 明确可占位字段、必须查证字段和审核状态口径。
- 强化样板不得误当事实的规则。

## 未决问题

- 若后续产品映射层要求把流程拆成更细步骤，本样板可再同步增强。

## 建议下一个模块

- `M03-04 来源资料样板`
