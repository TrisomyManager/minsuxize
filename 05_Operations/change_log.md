# 变更日志

## 文档信息
- 模块编号：`M05-03`
- 文件路径：`05_Operations/change_log.md`
- 当前版本：`v1.3.0`
- 当前状态：`approved`

## 1. 使用目的
本日志用于记录仓库内正式文档的版本变化，说明哪些文件被改动、为何改动，以及影响哪些下游模块。

## 2. 记录规则
- 每次正式修改后必须补一条记录。
- 一条记录只对应一个文件。
- 日期使用 `YYYY-MM-DD`。
- 版本填写修改后的文档版本号。
- 影响范围不得留空。

## 3. 记录模板
| 日期 | 文件 | 模块编号 | 修改人 | 版本 | 变更摘要 | 影响范围 |
|---|---|---|---|---|---|---|
| YYYY-MM-DD | `01_Standards/example.md` | M00-00 | 执行型 AI | v1.0.0 | 用一句话写清修改内容 | 写清受影响的下游模块或文档类别 |

## 4. 当前记录
| 日期 | 文件 | 模块编号 | 修改人 | 版本 | 变更摘要 | 影响范围 |
|---|---|---|---|---|---|---|
| 2026-03-23 | `05_Operations/ai_collaboration_protocol.md` | M05-01 | Codex | v1.0.0 | 完成 AI 协作协议正式首版，固化输入包、输出包、状态与冲突处理 | 协作流程、交接、审校与日志记录 |
| 2026-03-23 | `05_Operations/handoff_template.md` | M05-02 | Codex | v1.0.0 | 完成交接模板正式首版，固化交接字段与回报结构 | 跨 AI 交接、跨轮次接力 |
| 2026-03-23 | `05_Operations/review_log.md` | M05-03 | Codex | v1.0.0 | 完成审核日志正式首版，固化审核字段与结论口径 | 审阅记录、冲突裁定、关键节点复核 |
| 2026-03-23 | `05_Operations/change_log.md` | M05-03 | Codex | v1.0.0 | 完成变更日志正式首版，固化文件级变更记录规则 | 文档追溯、版本变化记录 |
| 2026-03-23 | `01_Standards/field_dictionary_core.md` | M01-02 | Codex | v1.2.0 | 对齐模板层实际字段，补齐核心字段并明确 Media 仅为扩展模板支持字段 | 标准层、模板层、样板层 |
| 2026-03-23 | `01_Standards/enum_dictionary.md` | M01-04 | Codex | v1.1.0 | 补齐模板层所需枚举并统一字段到枚举的映射关系 | 标准层、模板层、协作审核 |
| 2026-03-23 | `02_Templates/template_place.md` | M02-01 | Codex | v1.1.0 | 对齐地点模板字段，补齐 `updated_at` 与关联字段口径 | 模板层、样板数据层 |
| 2026-03-23 | `02_Templates/template_festival.md` | M02-01 | Codex | v1.1.0 | 对齐节日模板字段，收紧抽象层与地方习俗实例边界 | 模板层、样板数据层 |
| 2026-03-23 | `02_Templates/template_ritual.md` | M02-01 | Codex | v1.1.0 | 修正仪式模板字段名漂移并补齐 `updated_at` | 模板层、样板数据层 |
| 2026-03-23 | `02_Templates/template_custom_instance.md` | M02-02 | Codex | v1.1.0 | 对齐地方习俗实例模板字段，补齐 `review_status`、`updated_at` 与地点锚点约束 | 模板层、样板数据层、审核流程 |
| 2026-03-23 | `02_Templates/template_source.md` | M02-03 | Codex | v1.1.0 | 修正来源模板字段名与媒体依附口径，补齐 `updated_at` | 模板层、样板数据层、来源边界 |
| 2026-03-23 | `02_Templates/template_media.md` | M02-03 | Codex | v1.1.0 | 明确 Media 为扩展 / 预留实体并统一 `public_display_status` 字段口径 | 模板层、媒体治理、来源边界 |
| 2026-03-23 | `00_Project_Brief/module_queue.md` | M00-00 | Codex | v1.3.0 | 同步 M01-04、M02、M05 的实际完成状态并调整下一步排程 | 项目推进节奏、状态同步 |
| 2026-03-23 | `00_Project_Brief/current_status.md` | M00-00 | Codex | v1.3.0 | 同步核心模板层与协作层已完成状态，并将当前阶段更新为样板前准备 | 项目状态判断、后续优先级 |
| 2026-03-23 | `05_Operations/ai_collaboration_protocol.md` | M05-01 | Codex | v1.1.0 | 按实际工作模式补强默认《执行报告》、总整理审核与关键节点总审校规则 | 协作模式、模板层整理审核 |
| 2026-03-23 | `05_Operations/review_log.md` | M05-03 | Codex | v1.1.0 | 补记模板层总整理审核结果并统一追溯口径 | 审核追溯、状态同步 |
| 2026-03-23 | `05_Operations/change_log.md` | M05-03 | Codex | v1.1.0 | 补记模板层与协作层本轮重要变更 | 变更追溯、模板层整理审核 |
| 2026-03-24 | `02_Templates/intake_checklist.md` | M02-04 | Codex | v1.1.0 | 增补批量录入规则并明确样板占位内容不得转写为事实 | 录入流程、样板层与审核层衔接 |
| 2026-03-24 | `02_Templates/review_checklist.md` | M02-04 | Codex | v1.1.0 | 统一审核结论口径，补充批量审核规则并收紧样板审核边界 | 审核流程、样板层、状态同步 |
| 2026-03-24 | `03_Pilot_Data/pilot_region_plan.md` | M03-01 | Codex | v1.1.0 | 补齐试点评估权重、排除条件、输出包要求和完成判定 | 样板工程、产品映射前准备 |
| 2026-03-24 | `03_Pilot_Data/places/place_profile_sample.md` | M03-02 | Codex | v1.1.0 | 改写为与地点模板字段一一对应的结构样板，并明确占位与查证边界 | 样板层、模板层、状态同步 |
| 2026-03-24 | `03_Pilot_Data/custom_instances/custom_instance_sample.md` | M03-03 | Codex | v1.1.0 | 改写为与地方习俗实例模板字段一一对应的结构样板，并强化地点锚点与来源约束 | 样板层、模板层、状态同步 |
| 2026-03-24 | `03_Pilot_Data/sources/source_record_sample.md` | M03-04 | Codex | v1.1.0 | 改写为与来源模板字段一一对应的结构样板，并统一来源与媒体边界 | 样板层、模板层、状态同步 |
| 2026-03-24 | `00_Project_Brief/module_queue.md` | M00-00 | Codex | v1.4.0 | 同步 M02-04 与 M03 完成状态，并将下一步排程切换到 M04 产品映射层 | 项目推进节奏、状态同步 |
| 2026-03-24 | `00_Project_Brief/current_status.md` | M00-00 | Codex | v1.4.0 | 同步样板层已完成状态，并将项目阶段更新为进入产品映射前准备 | 项目状态判断、后续优先级 |
| 2026-03-24 | `05_Operations/review_log.md` | M05-03 | Codex | v1.2.0 | 补记样板层总整理审核结果并确认可进入产品映射前准备 | 审核追溯、状态同步 |
| 2026-03-24 | `05_Operations/change_log.md` | M05-03 | Codex | v1.2.0 | 补记录入清单层与样板层收口后的重要变更 | 变更追溯、样板层整理审核 |
| 2026-03-24 | `01_Standards/enum_dictionary.md` | M01-04 | Codex | v1.2.0 | 将 `review_status` 统一为后台状态码，并保留中文显示含义说明 | 模板层、产品映射层、开发启动包 |
| 2026-03-24 | `01_Standards/field_dictionary_core.md` | M01-02 | Codex | v1.2.1 | 将 `review_status` 示例统一为后台状态码口径 | 模板层、产品映射层、工作流草案 |
| 2026-03-24 | `02_Templates/template_custom_instance.md` | M02-02 | Codex | v1.1.1 | 将审核状态示例统一为后台状态码，便于直接映射后台工作流 | 模板层、产品映射层、开发启动包 |
| 2026-03-24 | `03_Pilot_Data/custom_instances/custom_instance_sample.md` | M03-03 | Codex | v1.1.1 | 将样板中的审核状态示例统一为后台状态码 | 样板层、产品映射层、开发启动包 |
| 2026-03-24 | `04_Productization/site_information_architecture.md` | M04-01 | Codex | v1.1.0 | 收口前后台信息架构，明确地点锚点、实例核心、5 实体边界和后台优先开发策略 | 产品映射层、Sprint 1 开发启动 |
| 2026-03-24 | `04_Productization/page_type_specs.md` | M04-02 | Codex | v1.1.0 | 收口后台 / CMS 首期页面类型，明确表单页、详情页与字段分组映射 | 产品映射层、Sprint 1 开发启动 |
| 2026-03-24 | `04_Productization/search_and_filter_specs.md` | M04-03 | Codex | v1.1.0 | 收口首期搜索与筛选范围，统一后台状态码和 5 核心实体筛选口径 | 产品映射层、Sprint 1 开发启动 |
| 2026-03-24 | `04_Productization/map_timeline_specs.md` | M04-03 | Codex | v1.1.0 | 收口地图与时间线首期边界，明确仅做地点导航和节令浏览的最小能力 | 产品映射层、Sprint 1 开发边界 |
| 2026-03-24 | `04_Productization/graph_display_specs.md` | M04-04 | Codex | v1.1.0 | 收口图谱首期定位，改为关系浏览与预设查询的最小实现方案 | 产品映射层、Sprint 1 开发边界 |
| 2026-03-24 | `00_Project_Brief/project_brief.md` | M00-01 | Codex | v1.0.3 | 收紧描述性术语，避免将“地方实践”误作正式对象名称 | 产品映射层、开发启动包、术语一致性 |
| 2026-03-24 | `00_Project_Brief/scope_and_principles.md` | M00-01 | Codex | v1.0.3 | 收紧反例表述，统一正式对象名称为“地方习俗实例” | 产品映射层、开发启动包、术语一致性 |
| 2026-03-24 | `01_Standards/entity_catalog.md` | M01-01 | Codex | v1.1.1 | 统一实例关系描述口径，避免“地方实践”与正式术语混用 | 关系目录、产品映射层、开发启动包 |
| 2026-03-24 | `01_Standards/relation_catalog.md` | M01-03 | Codex | v1.1.1 | 统一关系说明中的正式术语口径，优化节日到实例的查询表述 | 产品映射层、开发启动包、查询描述 |
| 2026-03-24 | `00_Project_Brief/module_queue.md` | M00-00 | Codex | v1.5.0 | 同步 M04 产品映射层完成状态，并将下一步推进顺序切换到 Sprint 1 后台 / CMS MVP 开发启动 | 项目推进节奏、开发启动排程、状态同步 |
| 2026-03-24 | `00_Project_Brief/current_status.md` | M00-00 | Codex | v1.5.0 | 同步产品映射层完成状态，补记开发启动包，并更新为可启动 Sprint 1 阶段 | 项目状态判断、开发启动准备、后续优先级 |
| 2026-03-24 | `06_Future/sprint1_dev_brief.md` | M00-00 | Codex | v1.0.0 | 新增 Sprint 1 开发总说明，明确开发目标、边界、依赖与验收标准 | 开发启动包、Sprint 1 范围控制 |
| 2026-03-24 | `06_Future/cms_mvp_scope.md` | M00-00 | Codex | v1.0.0 | 新增后台 / CMS MVP 范围文档，明确 5 核心实体、表单、详情页和审核流转 | 开发启动包、后台 MVP 范围控制 |
| 2026-03-24 | `06_Future/data_model_draft.md` | M00-00 | Codex | v1.0.0 | 新增首期数据模型草案，明确 5 核心实体、字段分组、引用关系与状态字段处理 | 开发启动包、数据对象设计前置输入 |
| 2026-03-24 | `06_Future/workflow_status_draft.md` | M00-00 | Codex | v1.0.0 | 新增后台工作流状态草案，固化 `draft / in_review / approved / archived` 规则 | 开发启动包、审核流与状态机设计 |
| 2026-03-24 | `06_Future/implementation_priority.md` | M00-00 | Codex | v1.0.0 | 新增开发优先级文档，明确 Sprint 1 必做、可选、Sprint 2 再做和暂不开发项 | 开发启动包、排期与返工控制 |
| 2026-03-24 | `05_Operations/review_log.md` | M05-03 | Codex | v1.3.0 | 补记产品映射层与开发启动包总审校结果，确认具备启动 Sprint 1 条件 | 审核追溯、开发启动判断、状态同步 |
| 2026-03-24 | `05_Operations/change_log.md` | M05-03 | Codex | v1.3.0 | 补记产品映射层与开发启动包当前阶段的重要变更，确保开发启动前可追溯 | 变更追溯、开发启动准备、状态同步 |

## 5. 修改记录
| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Codex | M05-03 | v1.0.0 | 完成变更日志正式首版，补齐记录规则、模板与当前记录 | 文件级版本追溯、协作审记 |
| 2026-03-23 | Codex | M05-03 | v1.1.0 | 补记模板层与协作层本轮重要变更，确保状态同步可追溯 | 模板层、协作层、项目状态同步 |
| 2026-03-24 | Codex | M05-03 | v1.2.0 | 补记录入清单层与样板层本轮重要变更，确保进入产品映射层前状态可追溯 | 样板层、状态同步、产品映射前准备 |
| 2026-03-24 | Codex | M05-03 | v1.3.0 | 补记产品映射层、开发启动包与术语收口的本轮重要变更，确保进入 Sprint 1 前状态可追溯 | 产品映射层、开发启动准备、状态同步 |

## 本次完成内容
- 补记了产品映射层、开发启动包与术语收口的本轮重要变更。
- 使 `change_log.md` 与当前真实进展同步，并可支撑 Sprint 1 启动前追溯。

## 未决问题
- 若后续变更量继续增大，可再按阶段拆分历史归档。

## 建议下一个模块
- `Sprint 1 后台 / CMS MVP 开发启动`
