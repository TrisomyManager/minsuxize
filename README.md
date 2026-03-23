# China Folkways Knowledge Project

中国传统文化习俗数字记录与知识系统的 Git 仓库骨架。

## 仓库目标
本仓库用于建设一个以“地方习俗实例”为核心、未来可转为维基网站、地图展示平台与知识图谱系统的知识工程。

## 使用方式
1. 先阅读 `00_Project_Brief/project_brief.md`。
2. 根据 `00_Project_Brief/module_queue.md` 选择当前要执行的模块。
3. 执行模块前，先把 `07_Prompt_Library/shared/` 下的总控提示词与交接提示词发给 AI。
4. 再把对应的模块提示词发给 AI。
5. 将 AI 产出回填到指定文件。
6. 通过 `05_Operations/review_log.md` 和 `05_Operations/change_log.md` 记录变更。

## 目录说明
- `00_Project_Brief/`：项目总说明、阶段状态、模块队列
- `01_Standards/`：术语、命名、实体、字段、关系、枚举
- `02_Templates/`：录入模板与审核模板
- `03_Pilot_Data/`：试点数据与样板
- `04_Productization/`：网站、页面、检索、图谱需求
- `05_Operations/`：AI 协作、交接、审校、变更记录
- `06_Future/`：数据库、图谱、CMS 等后续草案
- `07_Prompt_Library/`：模块化 AI 提示词库

## 推荐启动顺序
1. M00-01 项目主说明书整理
2. M00-02 命名与版本规范
3. M00-03 术语表
4. M01-01 核心实体目录
5. M01-02 核心字段表
6. M02-02 地方习俗实例模板

## 协作原则
- 单次会话只完成一个模块。
- 没有真实资料时，不得伪造地方事实。
- 所有新增内容都要标版本、责任人、修改说明。
- 抽象词条与地方实例必须严格分离。
- 每条内容应绑定地点、时间和来源。

## Git 建议
- 主分支：`main`
- 开发分支命名：`module/M01-02-field-dictionary-core`
- 提交信息格式：`[模块编号] 动作：简述`
  - 示例：`[M01-02] feat: 完成核心字段表首版`
  - 示例：`[M02-04] docs: 更新审核清单`
