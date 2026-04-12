# 地点底档样板

## 文档信息
- 模块编号：`M03-02`
- 文件路径：`03_Pilot_Data/places/place_profile_sample.md`
- 对应模板：`02_Templates/template_place.md`
- 当前版本：`v1.1.0`
- 当前状态：`approved`

---

## 重要声明
本文件仅为结构样板，所有内容均为占位示例，不代表真实地方事实。

使用要求：
- 可复制本文件作为地点底档骨架。
- 可保留 `PLC-XXXX`、`某省某市某县某镇某村`、`待补`、`待回填` 等占位写法。
- 不得把占位内容直接当作真实地点信息录入正式数据。
- 涉及行政区划代码、空间点位、历史沿革、来源编号等内容，必须查证后填写。

---

## 占位写法规则
| 占位类型 | 写法 | 适用场景 |
|---|---|---|
| 待分配 | `PLC-XXXX` | 地点编号未正式分配 |
| 待补 | `（待补：说明）` | 信息尚未查证完成 |
| 待回填 | `（待回填：XXX-XXXX）` | 需在关联实体建立后回填 |
| 可留空 | 空值 | 明确属于选填字段 |

---

## 字段对齐样板

```md
place_id: PLC-XXXX
place_name: 中国某省某市某县某镇某村
place_level: 行政村
parent_place_id: PLC-YYYY
country: 中国
province: 某省
city: 某市
district: 某县区
town: 某镇
village: 某村
natural_village: （待补：如适用）
spatial_point: （待补：经纬度或清晰位置描述）
admin_code: （待补：行政区划代码）
place_alias: （待补：当地俗称、旧称）
place_description: （待补：地理、人文、历史背景摘要）
related_source_ids: SRC-XXXX
related_custom_instance_ids: （待回填：CUS-XXXX）
creator: 占位录入人
created_at: YYYY-MM-DD
updated_at: YYYY-MM-DD
version: v1.1.0
```

---

## 可占位内容与必须查证内容

| 字段 | 可先占位 | 必须查证后填写 |
|---|---|---|
| `place_name` | 是 | 最终名称必须与真实地点一致 |
| `place_level` | 是 | 最终层级必须与实际行政或空间层级一致 |
| `parent_place_id` | 是 | 父地点必须真实存在 |
| `spatial_point` | 否 | 需实地测量或有可靠位置说明 |
| `admin_code` | 否 | 需对照正式行政区划资料 |
| `place_alias` | 是 | 最终需确认是否为当地通行称呼 |
| `place_description` | 是 | 涉及事实时必须有来源支撑 |
| `related_source_ids` | 否 | 必须使用真实来源编号 |
| `related_custom_instance_ids` | 是 | 仅在实例建立后回填 |

---

## 最小可用样板标准
- 必须保留与模板一致的字段名。
- 必须明确哪些字段仍是占位内容。
- 必须写明本文件不是事实稿。
- 不得出现虚构真实编号、虚构行政代码或虚构来源。

---

## 修改记录

| 日期 | 修改人 | 模块编号 | 版本 | 修改摘要 | 影响范围 |
|---|---|---|---|---|---|
| 2026-03-23 | Kimi Code CLI | M03-02 | v1.0.0 | 完善地点底档样板，明确占位与查证要求 | 地点数据录入 |
| 2026-03-24 | Codex | M03-02 | v1.1.0 | 改写为与地点模板字段一一对应的结构样板，并明确占位与查证边界 | 样板层、模板层、状态同步 |

---

## 本次完成内容

- 将地点底档样板改写为与 `template_place.md` 对齐的字段样板。
- 明确哪些内容可占位，哪些内容必须查证后填写。
- 强化“样板不得误当事实”的使用边界。

## 未决问题

- 若后续地点模板新增空间点位细分字段，本样板需同步扩展。

## 建议下一个模块

- `M03-03 地方习俗实例样板`
