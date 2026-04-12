# 公众 MVP 瘦身重构计划

## 现有仓库哪些部分保留

- `Program.cs` 的 MVC、SQLite、Cookie 登录、自动 Seed 机制
- `Data/` 下的 `AppDbContext`、实体映射和样板数据
- `Services/IFolkloreRepository` 与 `EfFolkloreRepository`
- `Entries`、`Submit`、`Manage`、`Admin` 控制器主流程
- 现有首页、列表、详情、提交、后台页面壳子

## 哪些部分后置

- `Regions` 与 `Festivals` 作为主导航入口的角色
- 复杂审核能力
- 更细颗粒度的扩展实体
- 地图与图谱相关能力
- 更完整的版本管理与内容对比

## 哪些部分需要改名或收缩

- 把“知识工程平台”叙事收缩为“公众版地方习俗实例库”
- `SourceEvidence` 在产品层按 `Source` 理解
- `SubmissionRecord` / `SubmissionInput` 在产品层按 `FeedbackSubmission` 理解
- `Region` 在产品层按 `LocationLite` 理解
- `Festival` 在产品层按 `FestivalLite` 理解
- 首页从搜索落地页收缩为公众 MVP 首页
- 投稿页从“新建条目”叙事收缩为“补充/纠错/反馈”

## Sprint 1 代码改动顺序

1. 写清公众 MVP 范围文档
2. 固化前台与后台入口路由
3. 调整首页使用真实样板数据
4. 保持列表页与详情页继续复用
5. 调整反馈页文案与提交流程定位
6. 明确 `/manage` 作为轻后台入口
7. 跑构建并检查最小运行稳定性

## 最容易返工的点

- 过早把地区、节日、来源再拆成复杂独立后台能力
- 在没有真实用户反馈前，就设计复杂审核流
- 过早引入地图、图谱、媒体上传
- 在首期继续扩大首页和导航信息架构
- 把“反馈提交”误做成完整 CMS 编辑器

## 代码层保留 / 后置 / 清理判断

### 直接保留

- `EntriesController`
- `SubmitController`
- `ManageController`
- `AdminController`
- `AppDbContext`
- SQLite Seed 数据
- 现有后台登录与状态流转

### 后置但暂时保留

- `RegionsController`
- `FestivalsController`
- 对应视图与视图模型
- `ReviewHistory` 的更深扩展能力

### 不删但降级处理

- 首页不再只做搜索跳转页
- `Regions` / `Festivals` 不再作为首屏主路线
- `ReviewHistory` 继续存在，但只做最小可追溯，不扩展复杂版本系统
