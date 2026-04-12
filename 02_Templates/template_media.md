# 媒体资料录入模板

## 文档信息
- 模块编号：`M02-03`
- 文件路径：`02_Templates/template_media.md`
- 对应实体：`Media（媒体资料）`
- 当前版本：`v1.1.0`
- 当前状态：`approved`

---

## 使用说明
本模板用于规范媒体资料的元数据记录。`Media` 当前属于扩展 / 预留实体，不属于首期 5 个核心实体。

首期处理规则：
- 媒体资料默认依附于 `Source` 使用。
- 首期可不分配独立 `media_id`，优先记录 `file_name` 和相关元数据。
- 首期对外展示和公开传播必须受版权状态与公开展示状态控制。

二期扩展方向：
- 可将 `Media` 升级为独立实体并正式发放 `MED-XXXX`。
- 可建立与地点、地方习俗实例、节日、仪式的直接关系。

---

## 录入字段

### 1. 标识与文件

#### 【选填】媒体标识
- 字段名：`media_id`
- 填写规则：首期可留空，二期独立建模后按 `MED-XXXX` 分配。

#### 【必填】文件名
- 字段名：`file_name`
- 示例：`dingpo_chunjie_2024_001.jpg`

#### 【必填】媒体类型
- 字段名：`media_type`
- 枚举来源：`media_type`

#### 【选填】文件格式
- 字段名：`file_format`
- 示例：`JPEG`

### 2. 时间与地点

#### 【选填】拍摄 / 录制时间
- 字段名：`capture_time`

#### 【选填】时间描述
- 字段名：`time_description`

#### 【选填】拍摄地点
- 字段名：`capture_location`

#### 【选填】关联地点
- 字段名：`related_place_id`

### 3. 内容描述

#### 【必填】内容说明
- 字段名：`content_description`

#### 【选填】画面主体说明
- 字段名：`depicted_subjects`

#### 【选填】拍摄角度 / 景别
- 字段名：`shot_description`

### 4. 关联对象

#### 【必填】关联来源
- 字段名：`related_source_id`
- 填写规则：首期必须依附于来源资料。

#### 【选填】关联地方习俗实例
- 字段名：`related_custom_instance_id`

#### 【选填】关联节日
- 字段名：`related_festival_id`

#### 【选填】关联仪式
- 字段名：`related_ritual_id`

### 5. 版权与展示控制

#### 【必填】版权状态
- 字段名：`copyright_status`
- 枚举来源：`copyright_status`

#### 【选填】版权持有人
- 字段名：`copyright_holder`

#### 【选填】使用授权范围
- 字段名：`usage_authorization`

#### 【选填】公开展示状态
- 字段名：`public_display_status`
- 枚举来源：`public_display_status`

#### 【选填】展示限制
- 字段名：`display_restrictions`

### 6. 技术信息

#### 【选填】技术规格
- 字段名：`technical_specs`

#### 【选填】文件大小
- 字段名：`file_size`

#### 【选填】存储位置
- 字段名：`storage_location`

### 7. 备注与记录信息

#### 【选填】备注
- 字段名：`notes`

#### 【必填】创建人
- 字段名：`creator`

#### 【必填】创建时间
- 字段名：`created_at`
- 格式：`YYYY-MM-DD`

#### 【必填】更新时间
- 字段名：`updated_at`
- 格式：`YYYY-MM-DD`

#### 【必填】版本号
- 字段名：`version`
- 示例：`v1.1.0`

---

## 与来源资料模板的边界
- `template_source.md` 负责记录“证据来源”及其责任、获取、评估信息。
- `template_media.md` 负责记录具体媒体文件的元数据和展示控制信息。
- 首期如只有一份照片或视频，也应先以来源资料为主，再视需要补填媒体元数据。

---

## 待核查项
- [ ] 是否明确 `Media` 为扩展 / 预留实体而非首期核心实体
- [ ] `media_type`、`copyright_status`、`public_display_status` 是否使用标准枚举
- [ ] 是否已填写 `related_source_id`
- [ ] 是否把“是否公开”改为标准字段 `public_display_status`
- [ ] 是否避免把媒体资料与来源资料正文混写

---

## 录入示例

```md
media_id:
file_name: dingpo_chunjie_2024_001.jpg
media_type: 照片
file_format: JPEG
capture_time: 2024-02-13 08:30
time_description: 正月初四上午
capture_location: 福建省莆田市涵江区某村祠堂
related_place_id: PLC-0001
content_description: 记录现场供桌、参与者和祠堂空间布局。
depicted_subjects: 供桌、供品、参与者、祠堂牌位
shot_description: 正面中景
related_source_id: SRC-0001
related_custom_instance_id: CUS-0001
related_festival_id: FES-0001
related_ritual_id: RIT-0001
copyright_status: 需审核
copyright_holder: 张三
usage_authorization: 仅限项目研究内部使用。
public_display_status: 需审核
display_restrictions: 如公开展示需处理人物面部。
technical_specs: 4096x2730
file_size: 15.3MB
storage_location: /media/2024/hjiang/photos/
notes:
creator: 李四
created_at: 2026-03-23
updated_at: 2026-03-23
version: v1.1.0
```

---

## 修改记录
| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M02-03 | v1.0.0 | 完成媒体资料录入模板首版 | 媒体元数据录入 |
| 2026-03-23 | Codex | M02-03 | v1.1.0 | 对齐字段表与枚举字典，修正 `public_display_status`、字段名和首期边界表述 | 模板层、媒体治理、来源边界 |

---

## 本次完成内容
- 统一媒体模板字段名口径。
- 明确 `Media` 在首期属于扩展 / 预留实体。
- 将公开展示控制字段统一为 `public_display_status`。

## 未决问题
- 二期若正式独立建模 `Media`，需再补充分支、版本和文件存储规则。

## 建议下一个模块
- `M02-04 录入与审核清单`
