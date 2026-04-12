# China Folkways Knowledge Project

中国传统文化习俗数字记录与知识系统项目仓库。

当前这个 GitHub 仓库同时包含两部分内容：

- 知识工程与协作文档底座
- 面向公众浏览、反馈提交与后台审核的 ASP.NET Core 原型站点

## 当前协同入口

- 任意 Agent / 协作者进入项目后，先阅读 `AGENT_CONTEXT.md`
- 先用 `git status --short` 确认当前工作区状态
- 重要判断、当前状态、下一步建议，不要只留在聊天里，要回写文档

## 仓库结构

- `00_Project_Brief/`：项目总说明、范围原则、当前状态、模块队列
- `01_Standards/`：术语、命名、实体、字段、关系、枚举
- `02_Templates/`：录入模板与审核清单
- `03_Pilot_Data/`：样板数据与试点区域样例
- `04_Productization/`：站点信息架构、页面类型、检索/地图/图谱需求
- `05_Operations/`：AI 协作、交接模板、审校日志、变更日志
- `06_Future/`：Sprint 1 / CMS MVP 与数据模型草案
- `07_Prompt_Library/`：模块化提示词库
- `src/MinsuXize.Web/`：当前可运行站点
- `docs/`：原型站点相关产品和开发文档
- `data/seed/`：种子数据文件
- `scripts/`：辅助脚本

## 本地运行

```powershell
dotnet build .\src\MinsuXize.Web\MinsuXize.Web.csproj
dotnet run --project .\src\MinsuXize.Web\MinsuXize.Web.csproj --launch-profile http
```

默认本地地址：

- `http://localhost:5088`
- 健康检查：`/healthz`
- 后台登录：`/manage/login`

## 部署

当前仓库已包含 Render 部署所需文件：

- `Dockerfile`
- `.dockerignore`
- `render.yaml`

## 协作原则

- 没有真实资料时，不得编造民俗事实
- 抽象词条与地方习俗实例必须严格分离
- 每条内容应绑定地点、时间和来源
- 任意 Agent 开工前先读 `AGENT_CONTEXT.md`
- 任意 Agent 收工前更新 `AGENT_CONTEXT.md`

## 参考入口

- 总体状态与交接：`AGENT_CONTEXT.md`
- 项目状态：`00_Project_Brief/current_status.md`
- 协作协议：`05_Operations/ai_collaboration_protocol.md`
- 代码接手说明：`AI_WORKFLOW.md`
