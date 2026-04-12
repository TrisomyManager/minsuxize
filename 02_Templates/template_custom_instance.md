# 地方习俗实例录入模板

## 文档信息
- 模块编号：`M02-02`
- 文件路径：`02_Templates/template_custom_instance.md`
- 对应实体：`CustomInstance（地方习俗实例）`
- 当前版本：`v1.1.1`
- 当前状态：`approved`

---

## 使用说明
本模板用于录入地方习俗实例。地方习俗实例是本项目的核心记录对象，必须描述“某地、某时间、某场景下的具体实践”。

使用要求：
- 必须绑定地点。地点是锚点对象。
- 必须关联来源。无来源不录事实。
- 可关联节日或仪式，但不强制两者同时存在。
- 不得把抽象节日词条或抽象仪式词条直接替代地方习俗实例。

---

## 录入字段

### 1. 标识与标题

#### 【必填】地方习俗实例标识
- 字段名：`custom_instance_id`
- 填写规则：新建时留空，按 `CUS-XXXX` 规则分配。

#### 【必填】地方习俗实例名称
- 字段名：`custom_instance_name`
- 命名格式：`[地点][时间或场景][习俗主题]`
- 示例：`福建省莆田市涵江区正月初四过大年`

#### 【必填】习俗主题
- 字段名：`custom_theme`
- 示例：`正月初四过大年`

### 2. 地点锚点

#### 【必填】地点标识
- 字段名：`place_id`
- 填写规则：引用已建地点 `PLC-XXXX`。

#### 【必填】地点名称
- 字段名：`place_name`
- 填写规则：与 `place_id` 对应，作为冗余存储。

#### 【选填】具体场所
- 字段名：`specific_venue`
- 示例：`村中祠堂`

### 3. 节日 / 仪式 / 场景

#### 【选填】关联节日标识
- 字段名：`festival_id`

#### 【选填】关联节日名称
- 字段名：`festival_name`

#### 【选填】关联仪式标识
- 字段名：`ritual_id`

#### 【选填】关联仪式名称
- 字段名：`ritual_name`

#### 【选填】场景描述
- 字段名：`scene_description`
- 填写规则：当实例不适合只由节日或仪式概括时补充场景。

### 4. 时间信息

#### 【必填】发生时间
- 字段名：`occurrence_time`

#### 【选填】记录时间范围
- 字段名：`record_time_range`

#### 【选填】时间规则说明
- 字段名：`time_rule_description`

### 5. 核心叙述

#### 【必填】习俗描述
- 字段名：`custom_description`

#### 【必填】具体流程
- 字段名：`custom_procedure`

#### 【选填】空间布置
- 字段名：`spatial_arrangement`

#### 【选填】移动路线
- 字段名：`movement_route`

### 6. 参与结构

#### 【选填】参与人群构成
- 字段名：`participant_composition`

#### 【选填】参与条件
- 字段名：`participation_conditions`

#### 【选填】主持角色说明
- 字段名：`officiant_description`

#### 【选填】角色产生方式
- 字段名：`role_selection_method`

### 7. 器物与物质表达

#### 【选填】器物描述
- 字段名：`objects_description`

#### 【选填】器物特殊要求
- 字段名：`object_requirements`

#### 【选填】特殊服饰
- 字段名：`special_attire`

#### 【选填】供品配置
- 字段名：`offerings_description`

#### 【选填】仪式唱词 / 用语
- 字段名：`ritual_texts`

### 8. 规范与禁忌

#### 【选填】行为禁忌
- 字段名：`behavior_taboos`

#### 【选填】规则与约束
- 字段名：`rules_and_constraints`

### 9. 地方解释与差异

#### 【选填】地方象征意义
- 字段名：`symbolic_meaning_local`

#### 【选填】功能与作用
- 字段名：`functional_role`

#### 【选填】地域差异说明
- 字段名：`regional_differences`

#### 【选填】历史渊源
- 字段名：`historical_origin`

#### 【选填】变体说明
- 字段名：`variants_notes`

### 10. 存续与证据

#### 【必填】存续状态
- 字段名：`continuity_status`
- 枚举来源：`continuity_status`

#### 【选填】存续状态说明
- 字段名：`continuity_description`

#### 【必填】来源资料
- 字段名：`source_ids`
- 填写规则：至少一个 `SRC-XXXX`。

#### 【选填】可靠性等级
- 字段名：`reliability_level`
- 枚举来源：`reliability_level`

#### 【必填】审核状态
- 字段名：`review_status`
- 枚举来源：`review_status`
- 建议使用：`draft / in_review / approved / archived`

### 11. 记录信息

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
- [ ] 是否明确体现“某地、某时间、某场景”的具体实践
- [ ] `place_id` 是否已绑定有效地点
- [ ] `source_ids` 是否至少包含一个有效来源
- [ ] 是否与节日词条、仪式词条保持边界
- [ ] `continuity_status`、`reliability_level`、`review_status` 是否使用标准枚举
- [ ] 是否避免未核实事实、推测性结论和虚构细节

---

## 录入示例

```md
custom_instance_id: CUS-0001
custom_instance_name: 福建省莆田市涵江区正月初四过大年
custom_theme: 正月初四过大年
place_id: PLC-0001
place_name: 福建省莆田市涵江区
specific_venue: 村中祠堂
festival_id: FES-0001
festival_name: 春节
ritual_id: RIT-0001
ritual_name: 祭祖仪式
scene_description:
occurrence_time: 每年农历正月初四
record_time_range: 2020-2024年观察
time_rule_description: 以农历正月初四为固定时间点。
custom_description: 作为结构示例，不填写未核实事实。
custom_procedure: 按时间顺序记录当地具体执行流程。
spatial_arrangement:
movement_route:
participant_composition:
participation_conditions:
officiant_description:
role_selection_method:
objects_description:
object_requirements:
special_attire:
offerings_description:
ritual_texts:
behavior_taboos:
rules_and_constraints:
symbolic_meaning_local:
functional_role:
regional_differences:
historical_origin:
continuity_status: 待评估
continuity_description:
variants_notes:
source_ids: SRC-0001
reliability_level: 待核查
review_status: draft
creator: 张三
created_at: 2026-03-23
updated_at: 2026-03-23
version: v1.1.1
```

---

## 修改记录
| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M02-02 | v1.0.0 | 完成地方习俗实例录入模板首版 | 核心数据录入 |
| 2026-03-23 | Codex | M02-02 | v1.1.0 | 对齐字段表、补齐 `review_status` 和 `updated_at`，强化地点锚点与来源约束 | 模板层、样板数据层、协作审核 |
| 2026-03-24 | Codex | M02-02 | v1.1.1 | 将审核状态示例统一为后台状态码，便于与产品映射和工作流草案直接对接 | 模板层、产品映射层、开发启动包 |

---

## 本次完成内容
- 将地方习俗实例模板与核心字段表完全对齐。
- 强化“地点是锚点对象”和“无来源不录事实”的约束。
- 补齐审核状态与更新时间字段。

## 未决问题
- 若后续进入产品映射层，可再评估是否需要把流程拆成更强结构化子步骤。

## 建议下一个模块
- `M02-04 录入与审核清单`
