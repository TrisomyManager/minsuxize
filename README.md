# 中国民俗细则

当前项目目标是一个“面向公众可浏览、可反馈、可持续积累真实数据”的最小产品，而不是继续扩张成大而全的知识工程平台。

## 当前优先能力

- 公众首页
- 习俗浏览列表
- 习俗详情页
- 提交补充 / 纠错
- 轻后台最小审核闭环

## 当前不做什么

- 复杂地图系统
- 图谱可视化
- 扩展实体独立建模
- 复杂后台权限
- 复杂上传系统
- 高级搜索
- 复杂版本历史

## 当前产品定位

首期只围绕“地方习俗实例”建立一个公众可用闭环：

- 首页：说明项目定位、展示精选内容、给出浏览与反馈入口
- 习俗列表页：按关键词、地区、节日、存续状态做最小筛选
- 习俗详情页：展示当前已知信息、来源说明和补充入口
- 反馈页：提交补充、纠错或新线索
- 轻后台：查看反馈队列，并做“发布 / 退回补充 / 归档”的最小状态处理

## 当前页面入口

- `/`：公众首页
- `/entries`：习俗浏览
- `/entries/{id}`：习俗详情
- `/feedback`：提交补充 / 纠错
- `/feedback/thanks/{id}`：提交成功页
- `/manage`：轻后台入口
- `/manage/login`：轻后台登录
- `/manage/review`：轻后台审核队列
- `/healthz`：运行检查

## 首期核心对象

- `FolkloreEntry`：地方习俗实例
- `SourceEvidence`：首期按 `Source` 使用
- `SubmissionRecord` / `SubmissionInput`：首期按 `FeedbackSubmission` 使用
- `Region`：首期按轻量地区对象使用
- `Festival`：首期按轻量节日对象使用

## 代码结构

项目主站位于 `src/MinsuXize.Web`：

- `Controllers/`：前台、反馈、轻后台入口
- `Data/`：SQLite `DbContext`、实体和 Seed
- `Models/`：领域模型
- `Services/`：仓储与页面支持逻辑
- `ViewModels/`：页面视图模型
- `Views/`：Razor 页面
- `wwwroot/`：静态资源

## 本地运行

在仓库目录执行：

```powershell
dotnet run --project .\src\MinsuXize.Web\MinsuXize.Web.csproj
```

构建检查：

```powershell
dotnet build .\src\MinsuXize.Web\MinsuXize.Web.csproj
```

## 云端部署

当前仓库已补齐 Render 部署工件：

- `Dockerfile`
- `.dockerignore`
- `render.yaml`

推荐使用 Render 的 Blueprint 部署：

1. 将当前分支推送到 GitHub
2. 在 Render 中导入仓库 Blueprint
3. 填写 `AdminAuth__Username` 和 `AdminAuth__Password`
4. 保持持久化磁盘挂载到 `/app/App_Data`

说明：

- 项目当前使用 SQLite，本地数据库文件必须放在持久化磁盘上，否则反馈数据会在重启或重新部署后丢失
- `render.yaml` 已默认把数据库路径配置为 `/app/App_Data/minsuxize.db`

## 数据与登录

- SQLite 数据库：`src/MinsuXize.Web/App_Data/minsuxize.db`
- 首次启动会自动建库并写入样板数据
- 开发环境默认管理账号见 `src/MinsuXize.Web/appsettings.Development.json`

## 本轮相关文档

- `docs/public_mvp_scope.md`
- `docs/public_mvp_pages.md`
- `docs/public_mvp_data_model.md`
- `docs/public_mvp_refactor_plan.md`
- `docs/public_mvp_sprint1_tasks.md`
