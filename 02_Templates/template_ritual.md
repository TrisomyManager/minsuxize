# 仪式录入模板

## 文档信息
- 模块编号：`M02-01`
- 文件路径：`02_Templates/template_ritual.md`
- 对应实体：`Ritual（仪式）`
- 当前版本：`v1.1.0`
- 当前状态：`approved`

---

## 使用说明
本模板用于录入仪式实体。仪式是抽象知识层对象，用于描述跨地区可识别的仪式结构，不直接替代某地某次实践。

使用要求：
- 不得把某地的具体执行细节直接写入本模板。
- 与节日、地方习俗实例的关系必须通过标准字段表达。
- 首期仍以文本方式承载器物和角色说明，不单独扩展对象实体。

---

## 录入字段

### 1. 标识与名称

#### 【必填】仪式标识
- 字段名：`ritual_id`
- 填写规则：新建时留空，按 `RIT-XXXX` 规则分配。

#### 【必填】仪式名称
- 字段名：`ritual_name`
- 示例：`祭祖仪式`

#### 【选填】仪式别名
- 字段名：`ritual_alias`
- 示例：`上坟,扫墓,拜山`

### 2. 分类与目的

#### 【必填】仪式类型
- 字段名：`ritual_type`
- 枚举来源：`ritual_type`

#### 【选填】仪式目的
- 字段名：`ritual_purpose`

### 3. 通用结构

#### 【必填】通用流程
- 字段名：`general_procedure`

#### 【选填】核心要素
- 字段名：`key_elements`

#### 【选填】参与者
- 字段名：`participants`

#### 【选填】使用器物说明
- 字段名：`used_objects_description`
- 填写规则：首期使用文本描述，不单独发 `OBJ` 编号。

#### 【选填】主持角色说明
- 字段名：`officiant_role_description`
- 填写规则：首期使用文本描述，不单独发 `ROL` 编号。

### 4. 关联对象

#### 【选填】关联节日
- 字段名：`related_festival_ids`
- 填写规则：多个 `FES-XXXX` 用逗号分隔。

#### 【选填】已实现地方习俗实例
- 字段名：`realized_in_custom_instance_ids`
- 填写规则：仅在下游实例建立后回填多个 `CUS-XXXX`。

### 5. 记录信息

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

## 待核查项
- [ ] `ritual_type` 是否使用标准枚举
- [ ] `general_procedure` 是否是抽象层通用流程
- [ ] 是否误写了某一地点的专属执行规则
- [ ] `used_objects_description` 与 `officiant_role_description` 是否仍保持文本支持口径
- [ ] `related_festival_ids` 与 `realized_in_custom_instance_ids` 是否使用标准字段名

---

## 录入示例

```md
ritual_id: RIT-0001
ritual_name: 祭祖仪式
ritual_alias: 上坟,扫墓,拜山
ritual_type: 祭祀仪式
ritual_purpose: 表达敬意并祈求祖先庇佑。
general_procedure: 准备供品、布置祭位、上香行礼、宣读祭文、焚化纸钱、收供告退。
key_elements: 供品、香烛、纸钱、祭文、祭祀空间。
participants: 主祭者、家族成员、协助者。
used_objects_description: 香炉、供桌、祭文、纸钱等。
officiant_role_description: 通常由家族长辈或特定主持者负责。
related_festival_ids: FES-0001,FES-0002
realized_in_custom_instance_ids:
creator: 张三
created_at: 2026-03-23
updated_at: 2026-03-23
version: v1.1.0
```

---

## 修改记录
| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M02-01 | v1.0.0 | 完成仪式录入模板首版 | 仪式数据录入 |
| 2026-03-23 | Codex | M02-01 | v1.1.0 | 修正字段名漂移，对齐 `related_festival_ids`、`used_objects_description`、`officiant_role_description` 与 `updated_at` | 模板层、样板数据层 |

---

## 本次完成内容
- 修正了仪式模板中的字段命名漂移。
- 对齐了节日、仪式、地方习俗实例之间的关系口径。
- 补齐 `updated_at`。

## 未决问题
- 通用流程是否需要进一步拆成步骤级结构，后续由产品映射层决定。

## 建议下一个模块
- `M02-04 录入与审核清单`
