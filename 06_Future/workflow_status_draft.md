# 后台工作流状态草案

## 文档信息
- 文件路径：`06_Future/workflow_status_draft.md`
- 当前版本：`v1.0.0`
- 当前状态：`approved`

## 1. 适用范围
本草案适用于后台 / CMS MVP 中的 5 个核心实体：
- `Place`
- `Festival`
- `Ritual`
- `CustomInstance`
- `Source`

## 2. 状态定义

### 2.1 `draft`
含义：
- 草稿
- 允许继续编辑
- 尚未进入正式审核队列

进入条件：
- 新建记录
- 审核未通过后退回修改

退出条件：
- 提交审核，进入 `in_review`
- 放弃使用并归档，进入 `archived`

可操作角色：
- 录入人
- 规范工程 AI
- 样板工程 AI
- 项目负责人

与审核清单的关系：
- 提交前应先通过 `intake_checklist.md`

### 2.2 `in_review`
含义：
- 待审核
- 内容已经进入审核队列

进入条件：
- `draft` 内容完成基础录入并提交审核

退出条件：
- 审核通过，进入 `approved`
- 审核发现问题，退回 `draft`
- 暂停使用并归档，进入 `archived`

可操作角色：
- 审核型 AI
- 项目负责人

与审核清单的关系：
- 审核时必须结合 `review_checklist.md`

### 2.3 `approved`
含义：
- 审核通过
- 可以进入前台展示或下游使用

进入条件：
- `in_review` 内容通过审核

退出条件：
- 需要继续修订时，可复制或回退为 `draft`
- 停止作为现行内容使用时，进入 `archived`

可操作角色：
- 审核型 AI
- 项目负责人

与审核清单的关系：
- 通过前必须满足 `review_checklist.md` 的 P0 要求

### 2.4 `archived`
含义：
- 归档
- 保留历史参考，但不作为当前默认展示内容

进入条件：
- 历史记录停用
- 无需继续维护但需要保留追溯

退出条件：
- 如需重新启用，应转回 `draft` 再次编辑与审核

可操作角色：
- 项目负责人
- 审核型 AI

与审核清单的关系：
- 归档前应确认已有可替代现行内容，或已完成历史说明

## 3. 最小流转路径
- `draft -> in_review -> approved`
- `draft -> archived`
- `in_review -> draft`
- `in_review -> archived`
- `approved -> archived`
- `archived -> draft`

## 4. 与日志的关系
- 审核动作应记录到 `05_Operations/review_log.md`
- 结构性变更应记录到 `05_Operations/change_log.md`

## 修改记录
| 日期 | 修改人 | 版本 | 修改摘要 |
|---|---|---|---|
| 2026-03-24 | Codex | v1.0.0 | 完成后台工作流状态草案首版，固化 `draft / in_review / approved / archived` 的进入与退出规则 |
