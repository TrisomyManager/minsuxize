# AGENT_CONTEXT

## 协同强制规则

如果你是 AI 助手，进入当前项目后，先仔细分析理解本文档，再开始工作。你的任务完成后，必须更新 本文件，把当前状态、已完成内容、下一步建议和交接说明写回去。重要信息不要只留在聊天里。

- 任意 Agent 开工前先读 `AGENT_CONTEXT.md`
- 任意 Agent 收工前更新 `AGENT_CONTEXT.md`
- 重要信息不要只留在聊天里，要写回本文件或对应正式文档
- 如发现本文档与代码、README、其他规划文档冲突，优先记录冲突，再去核实，不要直接凭印象覆盖

## 项目概况

`China Folkways Knowledge Project` 是一个围绕“中国地方习俗实例”建立结构化知识底座的项目。

当前仓库现在已经统一到根目录，不再以 `minsuxize/` 作为主项目目录。

项目仍然同时包含两部分内容：

1. 根目录知识工程与协作规范文档
2. 根目录下的 ASP.NET Core MVC 原型站点代码与部署配置

这意味着本项目不是“纯文档仓库”，也不是“纯网站代码仓库”，而是“知识底座 + 原型应用”同仓协作的状态。

## 当前状态

### 一句话判断

项目的文档底座已经基本搭完，但文档状态与代码状态没有完全同步。

### 已确认的事实

- 根目录 `00_Project_Brief/current_status.md` 当前口径仍是：`产品映射层已完成，具备启动 Sprint 1 条件`
- 根目录 `00_Project_Brief/module_queue.md` 当前建议下一步仍是：`Sprint 1 后台 / CMS MVP 开发启动`
- 当前仓库根目录已经并入原 `minsuxize` 远端主线内容，Web 原型现位于 `src/MinsuXize.Web/`
- 本轮已本地验证 `src/MinsuXize.Web/MinsuXize.Web.csproj` 可以成功 `dotnet build`
- 当前机器 `.NET SDK` 版本为 `10.0.201`
- 根仓库当前工作区不是干净状态，存在大量未提交文档改动
- GitHub 远端仓库为 `https://github.com/TrisomyManager/minsuxize.git`

### 当前最重要的状态结论

- 文档侧：已完成知识结构、模板、样板、产品映射和协作规则
- 代码侧：已存在一个可运行方向明确的原型，但尚未看出已完全落地“5 个核心实体完整 CMS”的全量后台
- 协同侧：目前最需要的是把“文档计划”和“真实代码进展”对齐，否则后续 Agent 很容易误判阶段
- 公开站点侧：2026-04-22 已完成一轮 P0 热修，重点清理公开页面英文残留、开发阶段文案和缺失媒体路径，`dotnet build .\src\MinsuXize.Web\MinsuXize.Web.csproj` 已通过
- 公开站点检查：2026-04-22 已新增 `scripts/check_public_site.py`，可扫描公开文案残留、种子媒体坏路径，并可通过 `--base-url` 检查关键页面 HTTP 状态
- 媒体资产规范：2026-04-22 已新增 `docs/public_media_assets.md`，公开条目媒体约定放在 `src/MinsuXize.Web/wwwroot/media/entries/{entryId}/...`
- CI 检查：2026-04-22 已新增 `.github/workflows/public-site-checks.yml`，在 push / PR / 手动触发时执行 Release 构建、公开静态检查、启动本地预览并检查关键公开 URL
- 首页视觉：2026-04-22 已将首页首屏升级为使用本地 bitmap 主视觉的全宽 hero，H1 收回为品牌名“中国民俗细则”
- 阿里云部署：2026-04-22 已新增 systemd 自动部署方案，适配当前服务器无 Docker、已有 `minsuxize.service` 和 `nginx` 的状态；已确认 `minsuzhi.cn` 的 Nginx `proxy_pass` 为 `http://localhost:5000`，另一个站点 `xrcraft.cn` 在独立 Nginx server block 中
- 阿里云部署安全边界：自动部署脚本默认只操作 `/var/www/minsuxize`、`minsuxize.service` 和 `http://127.0.0.1:5000/healthz`，不会重启 Nginx 或修改 `/etc/nginx/conf.d/*`

### TODO / 待确认

- TODO：确认所有旧文档里对 `minsuxize/` 子目录的引用，逐步迁移为根目录口径
- TODO：确认根仓库主分支到底应为 `main` 还是 `master`，并统一到远端分支策略
- TODO：确认 `minsuxize/` 当前是否已上线到真实 Render 环境，现阶段只能确认有部署配置，不能确认线上状态
- TODO：确认根目录大量已修改文档是否已经过人工审核，避免后续 Agent 误覆盖
- TODO：为公开站点建立正式媒体资产目录和发布前静态资源检查，当前 P0 只隐藏缺失媒体，未补齐真实图片/音视频
- TODO：继续补真实图片/音视频资产和媒体授权信息；当前检查脚本只负责防止坏链进入公开站，不替代内容治理
- TODO：自动部署启用前，需要先按 `docs/aliyun_systemd_deploy.md` 的 One-Time Bootstrap 在服务器生成 `/var/www/minsuxize/publish` 首次发布产物，再把 `minsuxize.service` 从当前 `/var/www/minsuxize/MinsuXize.Web.dll` 调整为推荐的 `/var/www/minsuxize/publish/MinsuXize.Web.dll`；部署脚本已加入路径安全检查，未调整前会拒绝发布

## 项目结构速览

### 根目录知识工程层

- `00_Project_Brief/`：项目总说明、范围原则、阶段状态、模块队列
- `01_Standards/`：术语、命名、实体、字段、关系、枚举
- `02_Templates/`：录入模板、审核清单
- `03_Pilot_Data/`：样板数据和试点区域样例
- `04_Productization/`：站点信息架构、页面类型、检索/地图/图谱需求
- `05_Operations/`：AI 协作协议、交接模板、审校日志、变更日志
- `06_Future/`：Sprint 1 开发说明、CMS MVP 范围、数据模型草案、实现优先级
- `07_Prompt_Library/`：模块化提示词库

### 可运行原型层

- `src/MinsuXize.Web/`：当前唯一明确的可构建 Web 原型
- `src/MinsuXize.Web/Program.cs`：应用入口、DI、认证、SQLite、Seed
- `src/MinsuXize.Web/Controllers/`：首页、条目、地区、节日、投稿、后台登录、审核
- `src/MinsuXize.Web/Data/`：EF Core DbContext、实体、Seed 数据
- `src/MinsuXize.Web/Services/`：仓储接口与实现，当前数据访问主边界
- `src/MinsuXize.Web/ViewModels/`：页面视图模型
- `src/MinsuXize.Web/Views/`：Razor 页面
- `data/seed/`：种子数据文件
- `docs/`：原型相关产品/开发说明
- `AI_WORKFLOW.md`：应用接手说明

## 运行 / 构建 / 部署方式

### 根目录

- 根目录既是文档入口，也是代码仓库入口
- 真正需要构建和运行的是根目录下的 `src/MinsuXize.Web/`

### 本地构建

在根目录执行：

```powershell
dotnet build .\src\MinsuXize.Web\MinsuXize.Web.csproj
```

本轮已验证以上构建可通过。

### 本地运行

```powershell
dotnet run --project .\src\MinsuXize.Web\MinsuXize.Web.csproj --launch-profile http
```

已知开发地址来自 `launchSettings.json`：

- `http://localhost:5088`
- 健康检查：`/healthz`
- 后台登录页：`/manage/login`

说明：

- 本轮未重新完成整站运行连通性实测，只确认了项目可成功构建
- 历史文档 `LOCAL_DEVELOPMENT_SUMMARY.md` 记录过本地运行成功

### 配置与数据

- 开发环境配置：`src/MinsuXize.Web/appsettings.Development.json`
- 默认管理员账号：`admin`
- 默认管理员密码：`MinsuAdmin123!`
- 开发数据库：`src/MinsuXize.Web/App_Data/minsuxize.db`

注意：

- 默认管理员密码已写在开发配置中，只适合本地开发，不适合生产环境
- `App_Data/` 是运行态数据，不应被误当成文档事实源

### 部署

当前已看到 Render 部署相关文件：

- `render.yaml`
- `Dockerfile`
- `.dockerignore`

已知部署关键信息：

- 运行时：Docker
- 生产端口：`10000`
- 健康检查：`/healthz`
- 持久化目录：`/app/App_Data`
- 关键环境变量：
  - `ASPNETCORE_ENVIRONMENT=Production`
  - `ConnectionStrings__DefaultConnection=Data Source=/app/App_Data/minsuxize.db`
  - `AdminAuth__Username`
  - `AdminAuth__Password`

## 重要约束

### 内容与知识约束

- 不得编造民俗事实
- 抽象词条与地方习俗实例必须分离
- 地点是锚点对象
- 每条地方习俗实例都应绑定地点、时间和来源
- `Media` 在规划文档中首期不是独立核心实体，只是 `Source` 的附属能力

### 代码与数据约束

- 当前应用使用 `SQLite + EnsureCreated()`，还不是迁移友好的正式 schema 演进方案
- 修改实体结构时，要同步考虑 `Data/Entities/`、`Models/`、`AppDbContext`、`DbSeeder` 和已有数据库文件
- `src/MinsuXize.Web/App_Data/` 下的数据文件属于运行态状态，不是唯一真相
- 控制器目前通过 `IFolkloreRepository` 访问数据，除非明确做架构调整，否则不要随意绕开

### 协作约束

- 根仓库当前已有大量未提交改动，动手前先看 `git status`
- 不要因为看到旧文档就假设代码还没开始
- 不要因为看到原型能跑就假设文档已同步更新
- 遇到“文档计划”和“代码实现”不一致时，先记录差异，再决定修文档还是修代码

### Git / 仓库约束

- 当前远端默认分支是 `main`，本地根仓库当前仍可能停留在 `master`
- 旧的 `minsuxize/` 嵌套目录不再是主项目结构，后续如仍存在本地残留，应视为历史副本而不是当前工作目录

## 下一步建议

### 当前最推荐做什么

先做“状态对齐”，再做“功能推进”。

### 推荐优先顺序

1. 同步项目状态文档：至少梳理 `00_Project_Brief/current_status.md`、`00_Project_Brief/module_queue.md`、`06_Future/*` 与真实代码进展的差异
2. 做一次“规划 vs 现状”差距盘点：确认当前原型已经实现了什么，还缺哪些 Sprint 1 / CMS MVP 要求
3. 统一文档中的路径口径，把旧的 `minsuxize/` 子目录引用迁移到根目录结构
4. 在状态清楚之后，再决定是继续补后台实体能力、补迁移机制，还是先做提交流程/审核流程闭环

### 我认为的最优先下一步

最优先不是继续盲写新功能，而是继续做“状态与结构口径对齐”：

- 把文档里的旧路径迁移到根目录
- 把规划阶段描述同步到真实代码进展
- 明确哪些文档是历史快照，哪些仍是当前执行依据

## 给下一个 Agent 的交接

### 开工前先看什么

按这个顺序阅读：

1. `AGENT_CONTEXT.md`
2. `00_Project_Brief/current_status.md`
3. `00_Project_Brief/module_queue.md`
4. `05_Operations/ai_collaboration_protocol.md`
5. 如果你要改代码，再看：
   - `AI_WORKFLOW.md`
   - `src/MinsuXize.Web/Program.cs`
   - `src/MinsuXize.Web/Services/IFolkloreRepository.cs`
   - `src/MinsuXize.Web/Data/AppDbContext.cs`
   - 相关 Controller / ViewModel / View

### 开工前先做哪几个检查

- 先跑一次 `git status --short`
- 确认本轮是改“根目录文档层”还是改 `src/MinsuXize.Web/` 代码层
- 如果要改数据结构，先确认是否会影响现有 SQLite 文件和 `DbSeeder`
- 如果要做 Git 操作，先确认到底以 `master` 还是 `main` 为准

### 下一个 Agent 最适合先处理什么

最适合先处理的是“状态对齐任务”，不是大改业务代码。

建议优先做下面任一项：

1. 更新根目录状态文档，使其如实反映 `minsuxize/` 原型已存在
2. 产出一份 Sprint 1 功能差距清单，明确“规划要求”和“当前代码实现”的差异
3. 清理旧路径引用，避免后续协作时继续把根仓库误判成“文档仓库 + 子仓库”

## 最近一次更新

- 更新日期：`2026-04-22`
- 更新人：`Codex`
- 本次动作：
  - 执行公开站点 P0 热修：处理空白/坏图/文案不合适问题中的首批高影响项
  - 将地区、节日公开页面的英文 eyebrow 改为中文表达
  - 将关于页中“当前阶段暂不提供”“复杂地图”“大型内容管理后台”等开发阶段语言改为面向公众的后续能力说明
  - 调整条目详情页空来源状态文案，避免显示成未完成占位
  - 条目详情页新增媒体可渲染检查：本地媒体文件不存在时不再输出坏链接，远程 `http/https` 链接仍可展示
  - 清理 `DbSeeder` 中不存在的图片/视频/音频种子路径，避免新部署继续写入坏媒体路径
  - 清理公开种子内容里的“原型示例村落”“项目示例资料”“待补录”等不适合上线的措辞
  - 使用 Playwright CLI 对首页、条目列表、条目详情、地区目录、地区详情、节日目录、节日详情、提交页、关于页做本地浏览器检查
  - 新增 `scripts/check_public_site.py`，将公开词残留、种子媒体坏路径、关键 URL 状态检查固化为发布前脚本
  - 更新 `.gitignore`，忽略 Playwright CLI 临时快照和本地截图产物
  - 新增公开媒体资产规范文档 `docs/public_media_assets.md`
  - 新建公开媒体目录占位 `src/MinsuXize.Web/wwwroot/media/entries/.gitkeep`
  - 升级条目详情页媒体区：图片以后以缩略图展示，音视频以资料项展示；无有效媒体时不渲染空面板
  - 发布前检查脚本新增本地媒体目录规范检查，本地媒体必须位于 `media/entries/`
  - 新增 GitHub Actions 工作流 `.github/workflows/public-site-checks.yml`，将公开站点检查接入 CI
  - 生成本地首页主视觉 `src/MinsuXize.Web/wwwroot/media/site/home-archive-hero.png`
  - 调整首页 H1 和首屏文案，让品牌名成为第一视觉信号
  - 更新首页首屏 CSS：全宽背景图、左侧文本区、浅色次按钮、移动端 hero 高度收敛并露出下一段内容
  - 新增阿里云 systemd 部署脚本 `scripts/deploy_aliyun_systemd.sh`
  - 新增 GitHub Actions 自动部署工作流 `.github/workflows/deploy-aliyun-systemd.yml`
  - 新增阿里云部署说明 `docs/aliyun_systemd_deploy.md`
  - 部署方案采用 `/var/www/minsuxize/repo` 拉源码、`/var/www/minsuxize/publish` 放发布产物、`/var/www/minsuxize/App_Data` 持久化 SQLite 数据
- 本轮核验结果：
  - 已执行 `dotnet build .\src\MinsuXize.Web\MinsuXize.Web.csproj`
  - 构建通过：`0` 个警告，`0` 个错误
  - 已执行 `dotnet build .\src\MinsuXize.Web\MinsuXize.Web.csproj --configuration Release`
  - Release 构建通过：`0` 个警告，`0` 个错误
  - 已执行 `python scripts/check_public_site.py --base-url http://localhost:5088`，检查通过
  - 已执行 `python scripts/check_public_site.py`，检查通过
  - 浏览器检查的关键公开页面均返回 `200`，未发现坏图、横向溢出或已列出的开发词残留
  - 本地 PowerShell 对一条长形式 Release 预览模拟命令做了策略拦截；CI 文件本身使用 Linux shell，已通过本地 Release 构建和检查脚本验证核心步骤
  - 已用 Playwright CLI 检查首页桌面和移动端首屏，移动端不横向溢出，并可露出下一段内容
  - 已根据用户提供的服务器信息确认：Alibaba Cloud Linux 3，有 `nginx`、`git`、`dotnet`，无 Docker，`minsuxize.service` active running
  - 已本地验证新增部署相关文件后 Release 构建和 `python scripts/check_public_site.py` 仍通过
  - 部署自动化还未真正连到阿里云执行；启用前需要确认 `systemctl cat minsuxize.service`，确保 service 的 `WorkingDirectory` / `ExecStart` 指向 `/var/www/minsuxize/publish`
  - 已创建本地 Git 提交，提交内容为本轮公开站点修复、检查脚本、首页视觉资产和阿里云 systemd 部署方案
  - 本机尝试 `git fetch origin main` 和 `git push origin main` 均失败，错误为无法连接 `github.com:443`；因此当前提交尚未推送到 GitHub，服务器直接拉取远端前需要先完成推送
  - 已通过 GitHub API 将本轮修改写入远端 `main`；第一笔远端提交为 `942763d9273977ed7dd4e00031d61e0be8874be1`
  - 因为服务器 Secrets 尚未配置、服务器也尚未完成 `/var/www/minsuxize/publish` 一次性切换，已将 `.github/workflows/deploy-aliyun-systemd.yml` 改为仅 `workflow_dispatch` 手动触发
  - 旧的 `.github/workflows/seed-data.yml` 会在每次 push 后尝试提交并 push `data/`，但 GitHub Actions 默认无写权限导致失败；已改为仅 `workflow_dispatch` 手动触发，避免每次推送红灯
  - 工作流触发方式修正后已再次通过 GitHub API 写入远端 `main`，当前远端最新提交为 `85d5d45c4648457fa5b95ec70313c8de05aa7e0d`
  - 最新一次 GitHub Actions `Public Site Checks` 已通过；阿里云部署不会再因 push 自动触发，需手动运行 workflow 或在服务器执行部署命令
