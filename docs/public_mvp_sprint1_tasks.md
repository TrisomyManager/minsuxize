# Sprint 1 开发任务拆解

## Task 1：项目壳与运行时稳定化

- 目标：固定公众前台与轻后台的入口，保证项目可稳定构建与启动
- 涉及文件：
  - `src/MinsuXize.Web/Program.cs`
  - `src/MinsuXize.Web/Controllers/ManageController.cs`
  - `src/MinsuXize.Web/MinsuXize.Web.csproj`
- 依赖：现有 MVC、SQLite、Cookie 认证配置
- 完成标准：
  - 具备明确的前台与后台路由
  - 管理入口可通过 `/manage` 进入
  - 本地可正常构建
- 暂不处理项：
  - 多环境部署脚本
  - 复杂监控体系

## Task 2：首页

- 目标：把首页改造成公众版 MVP 首页，而不是只留搜索入口
- 涉及文件：
  - `src/MinsuXize.Web/Controllers/HomeController.cs`
  - `src/MinsuXize.Web/ViewModels/HomePageViewModel.cs`
  - `src/MinsuXize.Web/Views/Home/Index.cshtml`
  - `src/MinsuXize.Web/Views/Shared/_Layout.cshtml`
- 依赖：样板数据、仓储查询
- 完成标准：
  - 首页能展示站点定位
  - 首页能展示样板习俗实例
  - 首页能提供浏览与反馈入口
- 暂不处理项：
  - 地图化首页
  - 大型专题聚合首页

## Task 3：习俗列表页

- 目标：确保公众能以最短路径筛选和浏览习俗实例
- 涉及文件：
  - `src/MinsuXize.Web/Controllers/EntriesController.cs`
  - `src/MinsuXize.Web/ViewModels/EntrySearchViewModel.cs`
  - `src/MinsuXize.Web/Views/Entries/Index.cshtml`
- 依赖：`IFolkloreRepository`
- 完成标准：
  - 支持关键词、地区、节日筛选
  - 保留条目卡片与详情跳转
- 暂不处理项：
  - 全文搜索引擎
  - 高级排序与推荐算法

## Task 4：习俗详情页

- 目标：输出首期最完整的公开内容页
- 涉及文件：
  - `src/MinsuXize.Web/Controllers/EntriesController.cs`
  - `src/MinsuXize.Web/ViewModels/EntryDetailsViewModel.cs`
  - `src/MinsuXize.Web/Views/Entries/Details.cshtml`
- 依赖：`FolkloreEntry`、`SourceEvidence`
- 完成标准：
  - 详情页可展示内容正文、来源与补充入口
  - 详情页不依赖复杂审核或地图能力
- 暂不处理项：
  - 版本对比视图
  - 复杂媒体展示器

## Task 5：反馈提交页

- 目标：把现有投稿能力明确定位为公众反馈/补充入口
- 涉及文件：
  - `src/MinsuXize.Web/Controllers/SubmitController.cs`
  - `src/MinsuXize.Web/ViewModels/SubmitEntryViewModel.cs`
  - `src/MinsuXize.Web/Views/Submit/Create.cshtml`
  - `src/MinsuXize.Web/Views/Submit/Thanks.cshtml`
- 依赖：地区、节日下拉数据与数据库写入
- 完成标准：
  - 用户能提交补充、纠错或新线索
  - 提交成功后能进入确认页
- 暂不处理项：
  - 正式附件上传
  - 用户账号中心

## Task 6：轻后台

- 目标：保留最小可用的审核队列，而不是扩张成复杂 CMS
- 涉及文件：
  - `src/MinsuXize.Web/Controllers/ManageController.cs`
  - `src/MinsuXize.Web/Controllers/AdminController.cs`
  - `src/MinsuXize.Web/Views/Manage/Login.cshtml`
  - `src/MinsuXize.Web/Views/Admin/Index.cshtml`
  - `src/MinsuXize.Web/Views/Admin/History.cshtml`
- 依赖：Cookie 登录、提交记录、状态流转
- 完成标准：
  - 管理员可登录
  - 可查看提交队列
  - 可变更状态并查看最小历史
- 暂不处理项：
  - 多角色权限
  - 审核工作流编排
  - 内容编辑器

## Task 7：样板数据接入

- 目标：继续复用当前 SQLite 样板数据支撑页面开发和验收
- 涉及文件：
  - `src/MinsuXize.Web/Data/AppDbContext.cs`
  - `src/MinsuXize.Web/Data/Seed/DbSeeder.cs`
  - `src/MinsuXize.Web/Services/EfFolkloreRepository.cs`
- 依赖：现有 Seed 机制
- 完成标准：
  - 首页、列表、详情、后台都能消费样板数据
  - 不新增一套平行数据来源
- 暂不处理项：
  - 后台录入工具
  - 批量导入流程
