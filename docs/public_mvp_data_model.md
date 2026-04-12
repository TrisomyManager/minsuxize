# 公众 MVP 数据模型

## 首期核心对象说明

### 1. FolkloreEntry

地方习俗实例，是首期最核心的公开内容对象。

### 2. Source

首期代码中复用 `SourceEvidence`，表示一条习俗实例的来源依据。

### 3. FeedbackSubmission

首期代码中复用 `SubmissionRecord` / `SubmissionInput`，表示用户补充、纠错或新线索提交。

### 4. LocationLite

首期代码中复用 `Region`，只承担最小地区层级与展示用途，不扩展成复杂地理系统。

### 5. FestivalLite

首期代码中复用 `Festival`，只承担节日或时间节点分类用途，不扩展成独立知识体系。

## 每个对象的最小字段集合

### FolkloreEntry

- `Id`
- `Title`
- `RegionId`
- `FestivalId`
- `Summary`
- `HistoricalOrigin`
- `SymbolicMeaning`
- `RitualProcess`
- `ItemsUsed`
- `Participants`
- `Taboos`
- `OralText`
- `InheritanceStatus`
- `SourceIds`
- `UpdatedAt`

### Source

- `Id`
- `SourceType`
- `Title`
- `Contributor`
- `RecordedAt`
- `Citation`
- `Url`
- `Notes`

### FeedbackSubmission

- `Id`
- `ContributorName`
- `RegionId`
- `FestivalId`
- `Title`
- `Summary`
- `SourceSummary`
- `Contact`
- `Status`
- `SubmittedAt`
- `ReviewerNote`

### LocationLite

- `Id`
- `Name`
- `Type`
- `ParentId`
- `FullPath`

### FestivalLite

- `Id`
- `Name`
- `Category`
- `LunarLabel`
- `Summary`

## 对象之间的最小关系

- 一个 `FolkloreEntry` 归属一个 `LocationLite`
- 一个 `FolkloreEntry` 归属一个 `FestivalLite`
- 一个 `FolkloreEntry` 关联多条 `Source`
- 一个 `FeedbackSubmission` 指向一个 `LocationLite`
- 一个 `FeedbackSubmission` 指向一个 `FestivalLite`

## 哪些字段先做文本即可

- 来源说明
- 变化说明
- 口述内容
- 位置说明
- 联系方式
- 审核备注
- 媒体链接

## 为什么当前阶段不独立建模扩展实体

- 首期目标是形成真实公众闭环，而不是追求知识图谱完整度。
- 当前样板数据量和运营流程都不足以支撑复杂实体拆分。
- 先用文本字段承接差异，等真实反馈积累后，再判断哪些信息值得独立成实体。
- 过早拆分会让控制器、视图、后台和审核流程一起膨胀，拖慢上线。
