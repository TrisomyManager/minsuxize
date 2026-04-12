# Sprint 1 开发总说明

## 文档信息
- 文件路径：`06_Future/sprint1_dev_brief.md`
- 当前版本：`v1.0.0`
- 当前状态：`approved`

## 1. Sprint 1 目标
Sprint 1 的目标不是一次性做完整网站，而是基于当前稳定知识底座，启动一个可用的后台 / CMS MVP。

本轮必须实现：
- 5 个核心实体的后台录入能力
- 5 个核心实体的后台详情查看能力
- 审核状态流转能力
- 基础列表、搜索与筛选能力
- 来源与媒体挂接能力

## 2. 为什么现在可以开始开发
当前已经具备以下稳定上游输入：
- 项目目标、范围和边界已经固定
- 首期 5 个核心实体已经固定
- 字段、关系、枚举已经固定
- 模板和样板已经完成对齐与验证
- 产品映射层已经明确后台优先、前台后置
- 多 AI 协作、交接、审校与变更追溯机制已经具备

因此，开发团队已经不必再等待文档层继续发散，可以直接进入 Sprint 1 的后台 / CMS MVP 建设。

## 3. 本轮开发边界
Sprint 1 只围绕后台 / CMS MVP 展开。

本轮开发边界包括：
- 5 个核心实体：`Place / Festival / Ritual / CustomInstance / Source`
- 后台表单页
- 后台详情页
- 审核队列
- 工作流状态：`draft / in_review / approved / archived`
- 基础搜索与筛选
- 来源资料中的 `media_files` 挂接

## 4. 依赖的上游文档
- `00_Project_Brief/project_brief.md`
- `00_Project_Brief/scope_and_principles.md`
- `01_Standards/naming_and_versioning.md`
- `01_Standards/glossary.md`
- `01_Standards/entity_catalog.md`
- `01_Standards/field_dictionary_core.md`
- `01_Standards/relation_catalog.md`
- `01_Standards/enum_dictionary.md`
- `02_Templates/template_place.md`
- `02_Templates/template_festival.md`
- `02_Templates/template_ritual.md`
- `02_Templates/template_custom_instance.md`
- `02_Templates/template_source.md`
- `02_Templates/template_media.md`
- `04_Productization/site_information_architecture.md`
- `04_Productization/page_type_specs.md`
- `04_Productization/search_and_filter_specs.md`
- `04_Productization/map_timeline_specs.md`
- `04_Productization/graph_display_specs.md`
- `06_Future/cms_mvp_scope.md`
- `06_Future/data_model_draft.md`
- `06_Future/workflow_status_draft.md`
- `06_Future/implementation_priority.md`

## 5. 本轮不做的内容
- 前台公开站点完整建设
- 复杂地图交互
- 复杂时间线可视化
- 独立图谱系统或图数据库实现
- 扩展实体独立建模
- 自定义字段系统
- 大规模权限系统扩展
- 真实民俗内容批量录入

## 6. 本轮验收标准
- 后台可新建、编辑、查看 5 个核心实体
- 地方习俗实例必须绑定地点，地点锚点约束在后台可执行
- 来源资料可挂接 `media_files`
- 审核状态流转符合 `draft / in_review / approved / archived`
- 列表页可按实体类型和核心枚举筛选
- 字段名、枚举值、状态码与上游文档一致
- 不把 `Media` 作为首期独立核心实体实现

## 修改记录
| 日期 | 修改人 | 版本 | 修改摘要 |
|---|---|---|---|
| 2026-03-24 | Codex | v1.0.0 | 完成 Sprint 1 开发总说明首版，明确开发目标、边界、依赖与验收标准 |
