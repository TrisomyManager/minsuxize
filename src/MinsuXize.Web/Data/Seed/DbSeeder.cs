using System.Text.Json;
using MinsuXize.Web.Data.Entities;
using MinsuXize.Web.Models;

namespace MinsuXize.Web.Data.Seed;

public static class DbSeeder
{
    private static readonly DateTime SeedUpdatedAt = new(2026, 4, 20, 9, 0, 0, DateTimeKind.Utc);

    public static void Seed(AppDbContext context)
    {
        SeedRegions(context);
        SeedFestivals(context);
        SeedEntries(context);
        SeedSources(context);
        SeedEntrySources(context);
        SeedFaqs(context);
        SeedRelations(context);
        SeedPdfRegions(context);
        SeedPdfFestivals(context);
        SeedPdfEntries(context);
        SeedPdfSources(context);
        SeedPdfEntrySources(context);
        SeedPdfFaqs(context);
        SeedPdfRelations(context);
        SeedSubmissions(context);

        context.SaveChanges();
    }

    private static void SeedRegions(AppDbContext context)
    {
        UpsertRegion(context, new RegionEntity
        {
            Id = 1,
            Slug = "china",
            Name = "中国",
            Type = "国家",
            FullPath = "中国",
            Summary = "作为项目根节点，用于承接跨地区民俗资料、节日对照和全国范围检索。",
            CulturalFocus = "全国检索、跨地区差异对照和专题聚合。",
            HighlightsJson = JsonListSerializer.Serialize("全国检索", "专题聚合", "跨村落对照"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 2,
            Slug = "shanxi",
            Name = "山西省",
            Type = "省",
            ParentId = 1,
            FullPath = "中国 / 山西省",
            Summary = "山西保留了大量与祭祖、社火、庙会、岁时节令相关的民俗材料，村落层级传统尤其丰富。",
            CulturalFocus = "岁时礼俗、家族祠堂、社火表演。",
            HighlightsJson = JsonListSerializer.Serialize("祭祖体系", "社火表演", "村落传统保存较深"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 3,
            Slug = "changzhi",
            Name = "长治市",
            Type = "市",
            ParentId = 2,
            FullPath = "中国 / 山西省 / 长治市",
            Summary = "长治地区具有较强的上党文化底色，节日风俗和祭祖传统留存较多。",
            CulturalFocus = "上党文化、春节仪式、中元祭祀。",
            HighlightsJson = JsonListSerializer.Serialize("上党民俗", "村落礼俗", "农历节日仪式"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 4,
            Slug = "shangdang",
            Name = "上党区",
            Type = "区",
            ParentId = 3,
            FullPath = "中国 / 山西省 / 长治市 / 上党区",
            Summary = "适合按乡镇和行政村继续沉淀春节、清明、中元等风俗细则。",
            CulturalFocus = "祭祖、祭灶、年节空间布置。",
            HighlightsJson = JsonListSerializer.Serialize("行政村颗粒度", "年俗流程", "口述资料"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 5,
            Slug = "sudian-town",
            Name = "苏店镇",
            Type = "镇",
            ParentId = 4,
            FullPath = "中国 / 山西省 / 长治市 / 上党区 / 苏店镇",
            Summary = "作为乡镇层级的民俗聚合页，汇总下辖村落的节令差异。",
            CulturalFocus = "春节准备、祭灶、家户礼制。",
            HighlightsJson = JsonListSerializer.Serialize("镇级汇总", "多村落对照", "投稿入口"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 6,
            Slug = "qin-village",
            Name = "秦村",
            Type = "村",
            ParentId = 5,
            FullPath = "中国 / 山西省 / 长治市 / 上党区 / 苏店镇 / 秦村",
            Summary = "围绕除夕祭祖与春节准备流程整理的村落记录点，适合继续补充家户仪式和口述资料。",
            CulturalFocus = "除夕祭祖、祭灶、家庭仪式。",
            HighlightsJson = JsonListSerializer.Serialize("除夕祭祖", "祭灶流程", "家庭礼制"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 7,
            Slug = "xihuo-town",
            Name = "西火镇",
            Type = "镇",
            ParentId = 4,
            FullPath = "中国 / 山西省 / 长治市 / 上党区 / 西火镇",
            Summary = "以中元节表演性民俗为特色，适合展示社区参与和公共节庆。",
            CulturalFocus = "中元节、火文化、公共表演。",
            HighlightsJson = JsonListSerializer.Serialize("中元表演", "火文化", "社区参与"),
            UpdatedAt = SeedUpdatedAt
        });
    }

    private static void SeedFestivals(AppDbContext context)
    {
        UpsertFestival(context, new FestivalEntity
        {
            Id = 1,
            Slug = "spring-festival",
            Name = "春节",
            Category = "岁时节日",
            LunarLabel = "农历正月初一",
            Summary = "年度最重要的传统节日之一，涵盖除夕祭祖、年夜饭、拜年、社火等多种仪式与公共活动。",
            CoreTopicsJson = JsonListSerializer.Serialize("祭祖", "年夜饭", "拜年", "社火"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertFestival(context, new FestivalEntity
        {
            Id = 2,
            Slug = "kitchen-god-festival",
            Name = "祭灶",
            Category = "岁末节俗",
            LunarLabel = "农历腊月二十三或二十四",
            Summary = "记录灶神上天前的供品、祭词、时间选择与家庭分工。",
            CoreTopicsJson = JsonListSerializer.Serialize("糖瓜", "祭词", "送灶", "年节启动"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertFestival(context, new FestivalEntity
        {
            Id = 3,
            Slug = "zhongyuan-festival",
            Name = "中元节",
            Category = "岁时节日",
            LunarLabel = "农历七月十五",
            Summary = "围绕祭祖、超度、河灯、火文化与地方表演传统展开。",
            CoreTopicsJson = JsonListSerializer.Serialize("祭祖", "河灯", "地方表演", "社区参与"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertFestival(context, new FestivalEntity
        {
            Id = 4,
            Slug = "qingming",
            Name = "清明",
            Category = "岁时节日",
            LunarLabel = "公历四月上旬，节气前后",
            Summary = "侧重扫墓、插柳、踏青和家族祭祀秩序。",
            CoreTopicsJson = JsonListSerializer.Serialize("扫墓", "插柳", "踏青", "祖先记忆"),
            UpdatedAt = SeedUpdatedAt
        });
    }

    private static void SeedEntries(AppDbContext context)
    {
        UpsertEntry(context, new FolkloreEntryEntity
        {
            Id = 1,
            Title = "秦村除夕祭祖",
            Slug = "qin-village-new-year-eve-ancestor-ritual",
            ContentType = "ritual",
            RegionId = 6,
            FestivalId = 1,
            Summary = "除夕傍晚在堂屋或院中设供桌，通过焚香、献供、叩拜、念祷等流程完成年度祭祖仪式。",
            RegionFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string>
            {
                ["province"] = "山西省",
                ["city"] = "长治市",
                ["county"] = "上党区",
                ["town"] = "苏店镇",
                ["village"] = "秦村"
            }),
            HistoricalOrigin = "与北方家户祭祖传统相连，在春节时间节点中承担请祖、敬祖和家族团聚的功能。",
            SymbolicMeaning = "通过祭祖强化家族记忆与代际秩序，也把除夕从普通生活时间转换为年节礼俗时间。",
            InheritanceStatus = "仍被部分家庭保留，但年轻一代参与度下降，仪式文本多由长辈口传。",
            ChangeNotes = "供品从传统三牲逐步简化为水果、糕点和熟食；祭词从固定文本转向即兴表达。",
            OralText = "祖先在上，子孙今日备香烛供品，愿家中平安、来年顺遂。",
            PreparationsJson = JsonListSerializer.Serialize("打扫堂屋或院落", "准备香烛、供品和纸钱", "确认祭拜顺序与参与人员"),
            RitualProcessJson = JsonListSerializer.Serialize("摆放供桌和供品", "由长辈点香并说明祭拜对象", "家庭成员按长幼顺序叩拜", "念祷或口述新年愿望", "焚化纸钱并撤供"),
            RegionalDifferencesJson = JsonListSerializer.Serialize("同镇不同村对供品数量要求不同", "部分家庭会先祭灶再祭祖", "迁居家庭常把仪式压缩到年夜饭前"),
            ItemsUsedJson = JsonListSerializer.Serialize("香烛", "纸钱", "馒头或花馍", "酒水", "熟食供品"),
            TaboosJson = JsonListSerializer.Serialize("祭拜过程中避免嬉笑打闹", "供桌未撤前不随意移动供品", "祭词期间避免说不吉利的话"),
            ParticipantsJson = JsonListSerializer.Serialize("家中长辈主持", "家庭成员按长幼顺序参与", "晚辈负责摆供和撤供"),
            TagsJson = JsonListSerializer.Serialize("祭祖", "春节", "家户仪式", "山西"),
            CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0, DateTimeKind.Utc),
            UpdatedAt = SeedUpdatedAt,
            CreatedBy = "admin",
            Status = "published",
            ReviewStatus = "verified",
            ConfidenceLevel = "medium",
            SourceGrade = "field-note",
            Version = 2,
            ChangeLog = "重构为结构化词条模板",
            LocationJson = "{\"Latitude\":36.1911,\"Longitude\":113.1012,\"AddressDetail\":\"山西省长治市上党区苏店镇秦村\"}"
        });

        UpsertEntry(context, new FolkloreEntryEntity
        {
            Id = 2,
            Title = "秦村祭灶",
            Slug = "qin-village-kitchen-god-offering",
            ContentType = "ritual",
            RegionId = 6,
            FestivalId = 2,
            Summary = "通过糖瓜、清水、草料与送灶祭词，标记春节周期正式启动。",
            RegionFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string>
            {
                ["province"] = "山西省",
                ["city"] = "长治市",
                ["county"] = "上党区",
                ["town"] = "苏店镇",
                ["village"] = "秦村"
            }),
            HistoricalOrigin = "与北方岁末祭灶传统相近，保留送灶神上天、盼其言好事的观念。",
            SymbolicMeaning = "用甜味供品和送行物件表达对新一年吉庆、平安与家宅稳定的期望。",
            InheritanceStatus = "仍被记得，但仪式完整度和供品讲究程度已明显简化。",
            ChangeNotes = "部分家庭只保留象征性供糖与口头表达，具体祭词不再统一。",
            OralText = "上天言好事，回宫降吉祥。",
            PreparationsJson = JsonListSerializer.Serialize("清理灶台", "准备糖瓜和清水", "准备纸马或象征性送行物"),
            RitualProcessJson = JsonListSerializer.Serialize("腊月二十三或二十四黄昏前后清理灶台", "摆放糖瓜、清水、草料与纸马", "长辈焚香并念送灶祭词", "焚化纸马或象征物"),
            RegionalDifferencesJson = JsonListSerializer.Serialize("有的家庭在二十三祭灶，有的在二十四祭灶", "供品是否包含草料存在差异"),
            ItemsUsedJson = JsonListSerializer.Serialize("糖瓜", "清水", "草料", "纸马", "线香"),
            TaboosJson = JsonListSerializer.Serialize("不说晦气话", "仪式前不打翻供品", "灶台清理后避免马上重油烹饪"),
            ParticipantsJson = JsonListSerializer.Serialize("家中长辈主持", "女性成员多负责清扫和备供", "儿童参与分食糖瓜"),
            TagsJson = JsonListSerializer.Serialize("祭灶", "岁末", "家户仪式", "春节准备"),
            CreatedAt = new DateTime(2026, 2, 28, 15, 20, 0, DateTimeKind.Utc),
            UpdatedAt = SeedUpdatedAt,
            CreatedBy = "admin",
            Status = "published",
            ReviewStatus = "pending-verification",
            ConfidenceLevel = "medium",
            SourceGrade = "oral-history",
            Version = 2,
            ChangeLog = "补充准备事项与地区差异",
            LocationJson = "{\"Latitude\":36.1923,\"Longitude\":113.1025,\"AddressDetail\":\"山西省长治市上党区苏店镇秦村\"}"
        });

        UpsertEntry(context, new FolkloreEntryEntity
        {
            Id = 3,
            Title = "西火镇中元节表演性民俗",
            Slug = "xihuo-zhongyuan-performance-ritual",
            ContentType = "ritual",
            RegionId = 7,
            FestivalId = 3,
            Summary = "中元节既有祭祖与超度意味，也叠加了社区观看、参与和展示性的地方民俗表达。",
            RegionFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string>
            {
                ["province"] = "山西省",
                ["city"] = "长治市",
                ["county"] = "上党区",
                ["town"] = "西火镇"
            }),
            HistoricalOrigin = "在中元祭祀传统基础上，与地方火文化、公共节庆表演相互交织。",
            SymbolicMeaning = "兼具祭祀、慰灵、祈福与社区凝聚的多重功能。",
            InheritanceStatus = "仍具有较强公共性，但表演部分正在经历现代展示化转变。",
            ChangeNotes = "如今更容易被外地访客当作表演观看，因此更需要记录其原本的节俗语境。",
            OralText = "敬先人，安四邻，望一年平顺。",
            PreparationsJson = JsonListSerializer.Serialize("确认公共活动空间", "准备香火和表演道具", "安排参与者与围观秩序"),
            RitualProcessJson = JsonListSerializer.Serialize("先完成家户或村落层面的祭祀与供奉", "傍晚后转入公共空间的观看与参与场景", "通过火、光和群体动作形成节日氛围", "活动结束后由长辈说明禁忌和来年祝愿"),
            RegionalDifferencesJson = JsonListSerializer.Serialize("公共参与程度比家户仪式更高", "部分村落更强调祭祀，部分村落更强调表演"),
            ItemsUsedJson = JsonListSerializer.Serialize("香火", "纸钱", "灯火器具", "表演道具"),
            TaboosJson = JsonListSerializer.Serialize("表演空间内避免随意穿行", "祭祀时段不喧哗", "焚化物件需由熟悉流程者处理"),
            ParticipantsJson = JsonListSerializer.Serialize("家户祭祀成员", "社区组织者", "表演参与者", "围观村民与返乡青年"),
            TagsJson = JsonListSerializer.Serialize("中元节", "公共仪式", "火文化", "表演性民俗"),
            CreatedAt = new DateTime(2026, 3, 5, 11, 30, 0, DateTimeKind.Utc),
            UpdatedAt = SeedUpdatedAt,
            CreatedBy = "admin",
            Status = "published",
            ReviewStatus = "pending-verification",
            ConfidenceLevel = "low",
            SourceGrade = "compiled-note",
            Version = 2,
            ChangeLog = "补充公共性与展示化说明",
            LocationJson = "{\"Latitude\":36.2056,\"Longitude\":113.0879,\"AddressDetail\":\"山西省长治市上党区西火镇\"}"
        });
    }

    private static void SeedSources(AppDbContext context)
    {
        UpsertSource(context, new SourceEvidenceEntity
        {
            Id = 1,
            SourceType = "田野口述",
            SourceLevel = "field-note",
            Title = "秦村除夕祭祖口述记录",
            Contributor = "村中年长主持者口述，站点筹备阶段整理",
            RecordedAt = "2026-02-02",
            Citation = "口述记录，来源信息仍在继续核对。",
            Notes = "后续需要补充更完整的田野编号和授权说明。"
        });

        UpsertSource(context, new SourceEvidenceEntity
        {
            Id = 2,
            SourceType = "田野观察",
            SourceLevel = "oral-history",
            Title = "祭灶流程观察笔记",
            Contributor = "站点整理资料",
            RecordedAt = "2026-01-31",
            Citation = "祭灶时间、供品与祭词结构化观察记录。",
            Notes = "用于展示结构化字段如何支撑后续检索和对照。"
        });

        UpsertSource(context, new SourceEvidenceEntity
        {
            Id = 3,
            SourceType = "专题整理",
            SourceLevel = "compiled-note",
            Title = "西火镇中元表演性民俗整理",
            Contributor = "站点整理资料",
            RecordedAt = "2025-08-20",
            Citation = "按节日节点、参与角色、表演内容拆分字段。",
            Notes = "适合后续扩展到图片、视频和来源链接。"
        });
    }

    private static void SeedEntrySources(AppDbContext context)
    {
        UpsertEntrySource(context, new EntrySourceEntity { Id = 1, EntryId = 1, SourceId = 1 });
        UpsertEntrySource(context, new EntrySourceEntity { Id = 2, EntryId = 2, SourceId = 2 });
        UpsertEntrySource(context, new EntrySourceEntity { Id = 3, EntryId = 3, SourceId = 3 });
    }

    private static void SeedFaqs(AppDbContext context)
    {
        UpsertFaq(context, new EntryFaqEntity { Id = 1, EntryId = 1, SortOrder = 1, Question = "这个仪式一般在什么时候进行？", Answer = "通常在除夕傍晚、年夜饭前后进行，不同家庭会根据长辈安排略有差异。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 2, EntryId = 1, SortOrder = 2, Question = "是否每户都保留完整流程？", Answer = "不是。部分家庭只保留上香、供品和口头祈愿，完整叩拜和焚化流程有所简化。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 3, EntryId = 2, SortOrder = 1, Question = "祭灶和春节有什么关系？", Answer = "祭灶常被视为春节准备周期的启动仪式，之后进入更密集的清扫、备供和年节安排。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 4, EntryId = 3, SortOrder = 1, Question = "为什么说它具有表演性？", Answer = "因为它除了祭祀功能，还包含公共空间中的观看、参与和展示，参与者不只限于单个家庭。" });
    }

    private static void SeedRelations(AppDbContext context)
    {
        UpsertRelation(context, new EntryRelationEntity { Id = 1, EntryId = 1, RelatedEntryId = 2, RelationType = "same-region", Note = "同村春节周期内的家户仪式" });
        UpsertRelation(context, new EntryRelationEntity { Id = 2, EntryId = 2, RelatedEntryId = 1, RelationType = "same-region", Note = "同村春节周期内的家户仪式" });
        UpsertRelation(context, new EntryRelationEntity { Id = 3, EntryId = 3, RelatedEntryId = 1, RelationType = "compare", Note = "同属祭祀性民俗，可对照家户与公共空间差异" });
    }

    private static void SeedSubmissions(AppDbContext context)
    {
        UpsertSubmission(context, new SubmissionRecordEntity
        {
            Id = 1,
            RelatedEntryId = 1,
            ContentType = "ritual",
            ContributorName = "读者补充",
            RegionId = 6,
            FestivalId = 1,
            Title = "补充秦村年夜饭供品细节",
            Summary = "补充鱼、年糕和饺子在年夜饭中的摆放顺序与象征意义。",
            SourceSummary = "来自家庭长辈口述，建议补录音频来源。",
            Contact = "demo@example.local",
            SubmittedAtUtc = DateTime.UtcNow.AddDays(-2),
            Status = (int)SubmissionStatus.PendingReview,
            CreatedAt = DateTime.UtcNow.AddDays(-2),
            UpdatedAt = DateTime.UtcNow.AddDays(-2),
            CreatedBy = "读者补充",
            Version = 1,
            ChangeLog = "示例投稿"
        });
    }

    private static void SeedPdfRegions(AppDbContext context)
    {
        UpsertRegion(context, new RegionEntity
        {
            Id = 100,
            Slug = "qinghai-gangcha",
            Name = "青海刚察",
            Type = "县",
            ParentId = 1,
            FullPath = "中国 / 青海省 / 海北藏族自治州 / 刚察县",
            Summary = "从习俗调研 PDF 中整理出的藏族婚俗重点地区，用于承载迎亲赛马、下马礼、入帐式等礼俗记录。",
            CulturalFocus = "藏族婚俗、迎亲仪式、歌舞对答",
            HighlightsJson = JsonListSerializer.Serialize("藏族婚俗", "迎亲赛马", "入帐式"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 101,
            Slug = "shanxi-gaoping-suzhuang",
            Name = "山西高平苏庄村",
            Type = "村",
            ParentId = 2,
            FullPath = "中国 / 山西省 / 晋城市 / 高平市 / 河西镇 / 苏庄村",
            Summary = "从习俗调研 PDF 中整理出的传统婚俗样本村，关注婚书、嫁妆、院落路线、花馍喜饼等礼俗细节。",
            CulturalFocus = "村落婚俗、嫁妆礼制、面食礼物",
            HighlightsJson = JsonListSerializer.Serialize("传统婚俗", "花馍喜饼", "院落路线"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 102,
            Slug = "wuling-tujia",
            Name = "武陵山区土家族地区",
            Type = "区域",
            ParentId = 1,
            FullPath = "中国 / 武陵山区土家族地区",
            Summary = "用于整理土家族丧葬歌舞、跳丧等具有地域和族群特征的仪式资料。",
            CulturalFocus = "土家族丧俗、跳丧、歌舞仪式",
            HighlightsJson = JsonListSerializer.Serialize("土家族", "跳丧", "丧葬歌舞"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 103,
            Slug = "gansu-tianshui",
            Name = "甘肃天水",
            Type = "市",
            ParentId = 1,
            FullPath = "中国 / 甘肃省 / 天水市",
            Summary = "用于承载天水黑社火等社区节庆与夜间巡游类民俗条目。",
            CulturalFocus = "社火、巡游、社区节庆",
            HighlightsJson = JsonListSerializer.Serialize("黑社火", "夜间巡游", "社区节庆"),
            UpdatedAt = SeedUpdatedAt
        });

        UpsertRegion(context, new RegionEntity
        {
            Id = 104,
            Slug = "north-china-rural",
            Name = "华北乡村",
            Type = "区域",
            ParentId = 1,
            FullPath = "中国 / 华北乡村",
            Summary = "用于整理填仓节、社日、庙会、初一十五烧香等具有北方乡村代表性的礼俗资料。",
            CulturalFocus = "岁时节令、农事祭祀、民间信仰",
            HighlightsJson = JsonListSerializer.Serialize("岁时节令", "农事祭祀", "民间信仰"),
            UpdatedAt = SeedUpdatedAt
        });
    }

    private static void SeedPdfFestivals(AppDbContext context)
    {
        UpsertFestival(context, new FestivalEntity { Id = 100, Slug = "lantern-festival", Name = "元宵节", Category = "岁时节日", LunarLabel = "农历正月十五", Summary = "赏灯、猜谜、社火和公共巡游交织的春节后段节日。", CoreTopicsJson = JsonListSerializer.Serialize("灯俗", "社火", "团圆"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 101, Slug = "dragon-boat-festival", Name = "端午节", Category = "岁时节日", LunarLabel = "农历五月初五", Summary = "兼具纪念、避疫、驱邪、竞渡和饮食礼俗的传统节日。", CoreTopicsJson = JsonListSerializer.Serialize("龙舟", "艾草", "粽子", "避疫"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 102, Slug = "she-day", Name = "社日", Category = "农事节令", LunarLabel = "春社、秋社", Summary = "围绕土地神祭祀和村社共同体组织展开的农事节令。", CoreTopicsJson = JsonListSerializer.Serialize("土地神", "春祈秋报", "村社"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 103, Slug = "dragon-king-temple-fair", Name = "龙王庙会", Category = "庙会", LunarLabel = "多依地方例期", Summary = "以祈雨、酬神、集市和社交为核心的地方性庙会。", CoreTopicsJson = JsonListSerializer.Serialize("祈雨", "庙会", "龙王信仰"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 104, Slug = "mule-horse-fair", Name = "骡马会", Category = "集市庙会", LunarLabel = "多依地方例期", Summary = "传统牲畜交易、物资交换和地方节庆结合的乡村集会。", CoreTopicsJson = JsonListSerializer.Serialize("牲畜交易", "物资交流", "庙会"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 105, Slug = "green-seedling-fair", Name = "青苗会", Category = "农事节令", LunarLabel = "春夏农事期", Summary = "围绕禾苗成长、祈雨护苗和村落共同体参与展开的农事仪式。", CoreTopicsJson = JsonListSerializer.Serialize("护苗", "祈雨", "农事"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 106, Slug = "torch-festival", Name = "火把节", Category = "民族节日", LunarLabel = "多为农历六月二十四前后", Summary = "以举火、赛会、歌舞和祈丰收为核心的多民族节日。", CoreTopicsJson = JsonListSerializer.Serialize("火把", "祈丰收", "歌舞"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 107, Slug = "water-splashing-festival", Name = "泼水节", Category = "民族节日", LunarLabel = "傣历新年", Summary = "以净水祝福、浴佛、歌舞和新年庆祝为核心的民族节日。", CoreTopicsJson = JsonListSerializer.Serialize("泼水祝福", "浴佛", "新年"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 108, Slug = "farmers-harvest-festival", Name = "中国农民丰收节", Category = "当代节庆", LunarLabel = "秋分", Summary = "围绕丰收展示、农产品交流、乡村文化展演形成的当代乡村节庆。", CoreTopicsJson = JsonListSerializer.Serialize("丰收", "农产品", "乡村振兴"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 109, Slug = "first-fifteenth-incense", Name = "初一十五烧香", Category = "日常礼俗", LunarLabel = "每月初一、十五", Summary = "围绕月相、佛道信仰和家庭祈愿形成的周期性烧香礼俗。", CoreTopicsJson = JsonListSerializer.Serialize("烧香", "月相", "祈愿"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 110, Slug = "wedding-customs", Name = "婚俗", Category = "人生礼仪", LunarLabel = "依婚期择日", Summary = "涵盖订婚、迎亲、入门、宴席、嫁妆与歌舞对答等人生礼仪。", CoreTopicsJson = JsonListSerializer.Serialize("迎亲", "嫁妆", "宴席"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 111, Slug = "funeral-customs", Name = "丧俗", Category = "人生礼仪", LunarLabel = "依丧期", Summary = "围绕送别、安魂、慰藉和宗族社区互助展开的人生礼仪。", CoreTopicsJson = JsonListSerializer.Serialize("送别", "安魂", "社区互助"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 112, Slug = "birth-and-full-month", Name = "洗三与满月", Category = "人生礼仪", LunarLabel = "出生第三日、满月", Summary = "围绕新生儿清洁、命名、祝福和亲族确认展开的育儿礼俗。", CoreTopicsJson = JsonListSerializer.Serialize("洗三", "满月", "祝福"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 113, Slug = "zhuazhou", Name = "抓周", Category = "人生礼仪", LunarLabel = "周岁", Summary = "以物件选择象征未来志趣的周岁礼俗。", CoreTopicsJson = JsonListSerializer.Serialize("周岁", "择物", "祝福"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 114, Slug = "tianchuan-festival", Name = "天穿节", Category = "冷门节日", LunarLabel = "农历正月二十前后", Summary = "与女娲补天传说、煎饼补天和避针线等习俗相关的冷门节日。", CoreTopicsJson = JsonListSerializer.Serialize("补天", "煎饼", "女娲"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 115, Slug = "tiancang-festival", Name = "填仓节", Category = "冷门节日", LunarLabel = "农历正月二十五前后", Summary = "通过画灰囤、添粮、祭仓等方式祈求仓廪充足的节日。", CoreTopicsJson = JsonListSerializer.Serialize("灰囤", "粮仓", "祈丰"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 116, Slug = "winter-clothes-festival", Name = "寒衣节", Category = "岁时节日", LunarLabel = "农历十月初一", Summary = "以送寒衣、祭祖和慎终追远为核心的秋冬祭祀节日。", CoreTopicsJson = JsonListSerializer.Serialize("送寒衣", "祭祖", "慎终追远"), UpdatedAt = SeedUpdatedAt });
        UpsertFestival(context, new FestivalEntity { Id = 117, Slug = "shehuo", Name = "社火", Category = "社区节庆", LunarLabel = "春节至元宵前后", Summary = "以巡游、扮演、鼓乐和社区组织为核心的地方公共节庆。", CoreTopicsJson = JsonListSerializer.Serialize("巡游", "扮演", "鼓乐"), UpdatedAt = SeedUpdatedAt });
    }

    private static void SeedPdfEntries(AppDbContext context)
    {
        UpsertEntry(context, PdfEntry(100, "春节农村岁时礼俗", "spring-festival-rural-cycle", "festival", 1, 1, "全国乡村", "春节在农村语境中常被视为一年礼俗周期的核心，包含扫尘、备供、祭祖、拜年、守岁与亲族往来等环节。", "以岁首更新、祖先祭祀和家族团聚为主线，叠加地方性的庙会、社火和饮食传统。", "辞旧迎新、家族团圆、祈福纳祥。", "多数地区仍保持家庭团聚和年节饮食，但完整祭祀流程在城市化和人口流动中逐渐简化。", "PDF 中将春节列为农村节日体系的核心入口，本条先整理为总览型词条。", new[] { "扫尘", "备办年货与供品", "贴春联或门神", "准备年夜饭" }, new[] { "腊月进入清扫与备供阶段", "除夕完成祭祖、团圆饭和守岁", "正月初一开始拜年与亲族往来", "元宵前后转入灯会、社火等公共活动" }, new[] { "北方更常见饺子、旺火、社火等组合", "南方部分地区更强调年糕、祭灶和宗族祠堂活动" }, new[] { "春联", "供品", "香烛", "年饭" }, new[] { "祭祀时忌嬉闹冲撞供桌", "正月初一部分地区忌扫地倒垃圾" }, new[] { "家庭成员", "宗族长辈", "返乡亲属" }, new[] { "春节", "农村节日", "祭祖", "年俗" }));
        UpsertEntry(context, PdfEntry(101, "元宵节赏灯猜谜与社火", "lantern-festival-rural-lantern-customs", "festival", 1, 100, "全国乡村与城镇", "元宵节承接春节尾声，以赏灯、猜谜、吃元宵、社火巡游和公共娱乐为主要内容。", "灯节传统与正月望日、城市灯会、乡村社火相互交织。", "团圆、光明、驱邪迎祥。", "灯会和社火常被纳入地方文旅活动，传统民间组织方式仍需记录。", "PDF 将元宵列入全国性节日目录，本条整理其乡村公共活动面向。", new[] { "制作或布置灯彩", "准备元宵或汤圆", "组织社火队伍" }, new[] { "傍晚点灯或挂灯", "进行猜谜、观灯和社火巡游", "家中食用元宵或汤圆", "活动结束后收灯、谢灯" }, new[] { "北方社火和秧歌较常见", "南方水乡灯会、龙灯、板凳龙等形态更丰富" }, new[] { "灯笼", "谜条", "鼓乐器具", "元宵" }, new[] { "巡游路线忌随意阻断", "灯火区域注意防火" }, new[] { "社火队", "村社组织者", "观众", "家庭成员" }, new[] { "元宵节", "灯俗", "社火", "春节周期" }));
        UpsertEntry(context, PdfEntry(102, "清明扫墓祭祖", "qingming-tomb-sweeping-ancestor-ritual", "ritual", 1, 4, "全国", "清明扫墓祭祖以墓地清理、供奉、焚纸或献花、追思先人为核心，是春季重要的祖先祭祀礼俗。", "清明兼具节气、寒食传统和祖先祭祀功能，逐渐形成踏青与祭扫并行的节日结构。", "慎终追远、家族记忆、春季更新。", "城市公墓和网络祭扫兴起，但家族共同祭扫仍是许多乡村的重要活动。", "PDF 将清明列入全国性节日目录，本条按可检索模板整理。", new[] { "清理墓地工具", "供品或鲜花", "香烛纸钱", "家族祭扫安排" }, new[] { "抵达墓地并清理杂草尘土", "摆放供品或鲜花", "上香、叩拜、追思", "焚纸或以环保方式表达纪念", "家族成员共同返家或踏青" }, new[] { "部分地区重供饭菜酒水", "城市地区更常用鲜花和集中祭扫" }, new[] { "香烛", "纸钱", "鲜花", "供品" }, new[] { "忌踩踏他人墓地", "山林祭扫需严格防火" }, new[] { "家族成员", "长辈", "返乡亲属" }, new[] { "清明", "祭祖", "扫墓", "慎终追远" }));
        UpsertEntry(context, PdfEntry(103, "端午避疫与龙舟习俗", "dragon-boat-festival-warding-epidemic-customs", "festival", 1, 101, "全国，尤以水网地区龙舟活动突出", "端午节包含赛龙舟、食粽、挂艾草菖蒲、佩香囊等礼俗，兼具纪念、避疫和公共竞渡功能。", "端午与夏季疫病防护、屈原纪念、龙舟竞渡等多重传统结合。", "驱邪避疫、纪念先贤、社区协作。", "龙舟竞赛日益赛事化，家庭避疫礼俗仍在多地保存。", "PDF 将端午列入全国节日目录，本条整理其避疫和公共活动。", new[] { "包粽子", "备艾草菖蒲", "制作香囊", "组织龙舟队伍" }, new[] { "节前备粽与清理居处", "门前悬挂艾草菖蒲", "佩香囊或饮雄黄酒等地方做法", "水乡地区举行龙舟竞渡", "亲友互赠粽子" }, new[] { "南方龙舟活动更密集", "北方部分地区更强调挂艾草、吃粽与家庭避疫" }, new[] { "粽子", "艾草", "菖蒲", "香囊", "龙舟" }, new[] { "水上活动需重视安全", "雄黄酒等做法不宜简单照搬为饮用建议" }, new[] { "家庭成员", "龙舟队", "村社组织者" }, new[] { "端午", "避疫", "龙舟", "粽子" }));
        UpsertEntry(context, PdfEntry(104, "社日土地神祭祀", "she-day-land-god-ritual", "ritual", 104, 102, "华北及多地乡村", "社日围绕土地神祭祀展开，常与春祈秋报、村社聚餐和农事秩序相关。", "社日源于古代社祭传统，进入乡村社会后与土地庙、村社共同体和农事节律结合。", "敬土地、祈丰收、凝聚村社。", "部分地区社日已淡化为庙会或集体聚餐，也有地方以非遗和村史形式重新整理。", "PDF 将社日列入农事和乡村节日类型，本条整理其仪式结构。", new[] { "修整土地庙或社坛", "准备酒食供品", "召集村社成员" }, new[] { "在社坛或土地庙前摆供", "由主持者上香祭拜", "祈求风调雨顺与五谷丰登", "祭后分食供品或举行村社聚会" }, new[] { "北方常见土地庙祭祀", "南方部分地区与宗族社祭、社戏结合" }, new[] { "香烛", "酒食", "社旗", "土地神位" }, new[] { "忌轻慢社坛和土地神位", "祭品分食需遵从本地顺序" }, new[] { "村社长者", "农户", "庙会组织者" }, new[] { "社日", "土地神", "农事", "村社" }));
        UpsertEntry(context, PdfEntry(105, "龙王庙会祈雨", "dragon-king-temple-fair-rain-prayer", "ritual", 104, 103, "华北及旱作农业区", "龙王庙会常围绕祈雨、酬神、集市与社交展开，是农业社会水利信仰和公共活动的结合。", "龙王信仰与农业对雨水的依赖密切相关，庙会则提供了祭祀、交易和公共娱乐空间。", "祈求雨水、调和风雨、维系村社秩序。", "现代水利条件改变后，祈雨功能减弱，庙会的集市和文化展示功能增强。", "PDF 将龙王庙会列为地方性庙会，本条整理其祈雨逻辑。", new[] { "打扫庙宇", "准备香烛供品", "安排会期与集市摊位" }, new[] { "迎神或开庙", "上香献供并诵念祈雨愿词", "村民赶会、交易和观看演出", "雨愿达成或会期结束后酬神谢会" }, new[] { "旱作区更强调祈雨", "交通便利地区更偏向集市和文旅活动" }, new[] { "香烛", "供品", "龙王神像", "戏台或集市摊位" }, new[] { "庙内忌喧哗冲撞神位", "求雨叙事不宜脱离当地水利背景" }, new[] { "庙会会首", "农户", "商贩", "观众" }, new[] { "龙王庙会", "祈雨", "庙会", "农事信仰" }));
        UpsertEntry(context, PdfEntry(106, "骡马会与农村物资交流", "mule-horse-fair-rural-exchange", "festival", 104, 104, "北方及西南部分乡村集市", "骡马会以牲畜交易为核心，也承担农具、土产、饮食、娱乐和社交功能。", "在交通和农耕生产条件下，骡马等牲畜交易曾是乡村经济的重要节点。", "生产交换、乡土社交、庙会集市。", "机械化后牲畜交易弱化，但部分地方仍以会期、集市和旅游活动保留名称与场景。", "PDF 将骡马会列入农村庙会和交易型节俗目录。", new[] { "确定会期", "设交易场地", "准备牲畜检验和集市摊位" }, new[] { "商贩与农户入场", "进行牲畜查看、议价和交易", "同步开展农具土产交易", "会期内安排饮食、戏曲或游艺活动" }, new[] { "山地交通不便地区历史上更依赖骡马交易", "现代部分地区转为综合物资交流会" }, new[] { "牲畜", "农具", "交易票据", "集市摊位" }, new[] { "牲畜交易需遵守检疫和市场管理", "不宜将现代展演等同于传统交易全貌" }, new[] { "农户", "商贩", "会首", "赶集者" }, new[] { "骡马会", "集市", "物资交流", "农村经济" }));
        UpsertEntry(context, PdfEntry(107, "青苗会护苗祈丰", "green-seedling-fair-crop-prayer", "ritual", 104, 105, "北方及多地农区", "青苗会围绕禾苗生长、祈雨护苗、驱虫避灾和村社协作展开，是农事节律中的重要礼俗。", "青苗会与春夏农事节点相关，体现农户对风调雨顺和作物成活的期待。", "护苗、祈丰、共同承担农事风险。", "部分地方仪式淡化，但其作为农事文化记忆仍可用于村史和非遗整理。", "两份 PDF 均提到青苗会，本条作为农事节令词条整合。", new[] { "察看田苗", "准备祭品", "约定巡田或祭祀路线" }, new[] { "村民或代表到田边、庙宇或社坛集合", "祭拜土地、龙王或农事相关神灵", "巡田查看苗情", "祈求雨水、避虫灾和丰收" }, new[] { "北方多与祈雨、土地神结合", "南方部分地区可与尝新、祭田公田婆等传统相近" }, new[] { "香烛", "供品", "农具", "青苗" }, new[] { "忌随意踩踏田苗", "记录时需区分青苗会与现代农技活动" }, new[] { "农户", "村社长者", "仪式主持者" }, new[] { "青苗会", "农事", "护苗", "祈丰" }));
        UpsertEntry(context, PdfEntry(108, "彝族火把节", "yi-torch-festival", "festival", 1, 106, "彝族聚居区", "火把节以点火把、绕村寨、歌舞赛会和祈丰收为主要内容，是彝族等民族重要节日。", "火把节与火崇拜、驱虫祈丰、英雄传说和族群聚会相结合。", "驱邪护田、祈求丰收、族群凝聚。", "节日庆典与旅游活动结合明显，仍需区分地方传统流程和舞台化表达。", "PDF 将彝族火把节列入民族节日目录，本条作为总览词条。", new[] { "扎制火把", "准备节日服饰", "安排歌舞赛会场地" }, new[] { "傍晚点燃火把", "绕村寨或田地巡行", "举行歌舞、摔跤、赛马等活动", "以火光象征驱邪护田和祝福丰收" }, new[] { "不同彝族支系会期和传说叙事不同", "旅游区更常见集中晚会和大型表演" }, new[] { "火把", "民族服饰", "鼓乐", "祭品" }, new[] { "火把巡游需注意防火", "不宜把单一景区流程当作全部传统" }, new[] { "村寨成员", "青年男女", "祭司或长者", "游客" }, new[] { "彝族", "火把节", "民族节日", "祈丰" }));
        UpsertEntry(context, PdfEntry(109, "傣族泼水节", "dai-water-splashing-festival", "festival", 1, 107, "傣族聚居区", "泼水节是傣历新年礼俗，包含浴佛、泼水祝福、歌舞游行和亲友往来。", "节日与南传佛教、傣历新年和以水净秽祈福的观念相结合。", "洗净旧岁、祝福新年、社区欢聚。", "公共泼水活动已成为重要旅游符号，浴佛和家庭礼仪仍是理解节日的关键。", "PDF 将傣族泼水节列入民族节日目录，本条整理为结构化入口。", new[] { "备净水", "准备节日服饰", "布置佛寺或公共场地" }, new[] { "到佛寺浴佛或礼佛", "向长辈或亲友以水表达祝福", "公共场地进行泼水、歌舞和巡游", "节后走亲访友、共享食物" }, new[] { "寺院礼仪和广场泼水比重因地而异", "城市节庆常加入巡游和展演" }, new[] { "净水", "银盆或水具", "民族服饰", "花枝" }, new[] { "不宜向老人、儿童或不愿参与者强行泼水", "佛寺礼仪需遵守当地规范" }, new[] { "傣族社区成员", "僧侣", "游客", "亲友" }, new[] { "傣族", "泼水节", "新年", "净水祝福" }));
        UpsertEntry(context, PdfEntry(110, "中国农民丰收节乡村庆祝", "farmers-harvest-festival-modern-rural", "festival", 1, 108, "全国乡村", "中国农民丰收节以秋分为时间节点，组织丰收展示、农产品交流、乡村文艺和农事体验活动。", "作为当代设立的节庆，它吸收传统秋收庆贺、尝新、集市和乡村展演元素。", "礼赞丰收、展示乡村、促进农产品交流。", "节日常由政府、合作社和村社共同组织，正在形成传统资源与现代传播结合的新形态。", "PDF 将其列入当代农村节庆，本条作为现代节庆入口。", new[] { "布置丰收展台", "准备农产品", "组织文艺或农事活动" }, new[] { "开场仪式或丰收展示", "农产品展销和品鉴", "开展歌舞、竞技或农事体验", "总结表彰种养能手或乡贤贡献" }, new[] { "粮食主产区强调粮食作物", "果蔬、茶叶、水产地区会突出地方特色产业" }, new[] { "农产品", "展台", "农具", "舞台设备" }, new[] { "避免把展示活动替代对真实生产主体的记录", "食品品鉴需注意安全和标识" }, new[] { "农户", "合作社", "村委会", "游客" }, new[] { "丰收节", "当代节庆", "乡村振兴", "农产品" }));

        UpsertEntry(context, PdfEntry(120, "初一十五烧香习俗", "first-fifteenth-incense-burning", "ritual", 104, 109, "全国，城乡形态有差异", "初一十五烧香是按农历朔望日进行的周期性祈愿礼俗，兼具佛教、道教、月相崇拜和家庭信仰因素。", "PDF 资料将其来源分为佛教十斋日、道教一气化三清和古代月相崇拜等线索。", "敬神礼佛、祈福平安、在固定时间重申家庭愿望。", "城市多转向寺庙礼佛和简化家庭上香，乡村仍常见神龛、祖先牌位和灶神等复合对象。", "本条来自非固定节日与冷门习俗 PDF，来源等级标记为 compiled-note，需继续核实地方细节。", new[] { "清洁神龛或供桌", "准备香、烛、供水或供果", "确认通风和防火条件" }, new[] { "净手并整理供桌", "点香后双手持香行礼", "按地方习惯插三支、六支、九支或十三支香", "默念祈愿或诵经", "待香燃尽后清理香灰和供品" }, new[] { "城市家庭更常使用电子香炉或寺庙代替家中神龛", "农村家庭可能同时祭拜祖先、灶神和地方神", "南北在香数、供品和跪拜动作上存在差异" }, new[] { "线香", "香炉", "烛台", "供果", "供水" }, new[] { "忌用口吹灭香火", "忌香灰四散无人看管", "寺庙内需遵守禁烟、防火和礼佛秩序" }, new[] { "家庭成员", "寺庙香客", "长辈或主持者" }, new[] { "初一十五", "烧香", "民间信仰", "日常礼俗" }));
        UpsertEntry(context, PdfEntry(121, "青海刚察藏族婚俗", "gangcha-tibetan-wedding-customs", "ritual", 100, 110, "青海省海北藏族自治州刚察县", "青海刚察藏族婚俗包含偷亲、迎亲赛马、下马礼、入帐式和唱曲对答等环节，强调双方家族、牧区空间和歌舞礼仪。", "当地婚俗与藏族牧区生活、亲族网络和吉祥祝福观念有关。", "缔结姻亲、展示勇健与礼仪、祝福新家庭。", "部分环节在现代婚礼中被保留为象征性表演或仪式片段。", "PDF 对该婚俗列出若干流程，本条先整理成可继续补证的结构化模板。", new[] { "择定婚期", "准备哈达、酒食与服饰", "安排迎亲骑手或车队", "准备帐房或入门空间" }, new[] { "双方家族完成婚约确认", "迎亲队伍出发并进行赛马或象征性迎亲", "新娘到达后完成下马礼", "进入帐房或新居并接受祝福", "亲友以歌舞、唱曲对答和宴饮庆贺" }, new[] { "牧区传统中马匹和帐房意象更突出", "现代城镇婚礼可能以车队和舞台化歌舞替代部分环节" }, new[] { "哈达", "酒食", "民族服饰", "马匹或迎亲车辆", "帐房陈设" }, new[] { "迎亲过程需尊重女方家庭礼节", "涉及偷亲等说法时应区分历史习俗与现代法律伦理" }, new[] { "新郎新娘", "双方父母", "迎亲队伍", "唱曲者" }, new[] { "藏族婚俗", "青海刚察", "迎亲", "人生礼仪" }));
        UpsertEntry(context, PdfEntry(122, "山西高平苏庄村婚俗", "suzhuang-wedding-customs", "ritual", 101, 110, "山西省高平市苏庄村", "苏庄村婚俗以婚书、嫁妆、喜服、院落路线、花馍喜饼等物件和空间秩序为核心，体现村落家庭礼制。", "PDF 将其作为山西传统村落婚俗样本，强调院落空间和礼物系统。", "确认婚姻关系、展示家族体面、建立姻亲往来。", "现代婚礼简化了部分繁复程序，但花馍、喜饼和嫁妆展示仍有文化辨识度。", "本条与项目原有山西村落数据相邻，后续可补接田野记录。", new[] { "书写或保存婚书", "准备嫁妆和喜服", "蒸制花馍喜饼", "布置院落路线和迎亲空间" }, new[] { "订婚或婚约确认", "迎亲队伍按院落路线入门", "展示嫁妆与喜服", "分发花馍、喜饼等礼物", "宴席和亲友祝福" }, new[] { "晋东南村落更强调院落和亲族秩序", "城市婚礼中婚书和嫁妆展示多被影像、酒店流程取代" }, new[] { "婚书", "嫁妆", "喜服", "花馍", "喜饼" }, new[] { "迎亲路线和入门顺序需听从本家长辈安排", "记录时需尊重个人隐私和婚礼肖像授权" }, new[] { "新郎新娘", "双方亲属", "迎亲队伍", "厨师和帮忙村民" }, new[] { "山西婚俗", "苏庄村", "花馍", "嫁妆" }));
        UpsertEntry(context, PdfEntry(123, "阴婚（冥婚）习俗批判性记录", "ghost-marriage-custom-critical-record", "ritual", 104, 110, "华北等地历史上有零散记录", "阴婚又称冥婚，指围绕亡者婚配想象形成的历史习俗。本词条仅作民俗史和风险警示记录，不作为实践指南。", "PDF 将其历史来源、信仰基础、流程和法律伦理问题列为研究对象。", "反映传统宗族、亡者安置和婚姻观念中的特殊历史现象。", "现代社会中相关行为涉及严重法律和伦理风险，应以批判性、保护性方式记录。", "本条刻意弱化操作流程，只保留学术说明和风险提示。", new[] { "仅收集公开文献和合法访谈资料", "隐去个人信息", "标注法律伦理风险" }, new[] { "确认资料来源是否可靠", "区分传说、历史个案和现实违法行为", "记录信仰背景与社会问题", "在页面中加入禁止实践和求助提醒" }, new[] { "不同地区称谓和叙事不同", "现代新闻个案不能直接等同于传统民俗全貌" }, new[] { "文献资料", "访谈记录", "法律风险说明" }, new[] { "不得提供促成或实施相关行为的流程", "不得传播个人隐私和遗体相关敏感信息", "发现现实违法线索应依法处理" }, new[] { "研究者", "资料整理者", "伦理审查者" }, new[] { "阴婚", "冥婚", "批判性记录", "法律伦理" }, "low", "restricted-critical-record"));
        UpsertEntry(context, PdfEntry(124, "土家族跳丧（撒尔嗬）", "tujia-funeral-dance-saerhe", "ritual", 102, 111, "武陵山区土家族地区", "土家族跳丧又称撒尔嗬，是丧葬场合中以歌舞节奏表达悼念、慰藉和送别的仪式。", "其形成与土家族生死观、社区互助和丧葬歌舞传统相关。", "以歌舞送别亡者、安慰生者、维系社区互助。", "现代葬礼中表演化和舞台化趋势增强，原有丧礼语境需要被谨慎记录。", "PDF 将跳丧列入丧俗专题，本条作为族群仪式入口。", new[] { "确认丧礼时间和场地", "邀请熟悉曲牌和动作的人员", "准备鼓乐或节奏器具" }, new[] { "亲友守灵或聚集", "领舞者带动歌舞节奏", "众人围绕灵堂或指定空间起舞", "以唱词表达悼念、劝慰和祝愿", "仪式随丧礼节点收束" }, new[] { "不同土家族地区曲调、动作和称谓不同", "舞台展演常脱离真实丧礼时空" }, new[] { "鼓乐", "丧礼空间", "唱词", "服饰" }, new[] { "真实丧礼记录需征得家属同意", "不宜将哀悼场景娱乐化" }, new[] { "逝者亲属", "领舞者", "村社成员", "吊唁者" }, new[] { "土家族", "跳丧", "撒尔嗬", "丧俗" }));
        UpsertEntry(context, PdfEntry(125, "洗三与满月礼", "birth-third-day-full-month-ritual", "ritual", 1, 112, "全国多地", "洗三与满月是围绕新生儿出生后早期阶段展开的育儿礼俗，包含清洗、祝福、命名、宴请和亲族确认。", "习俗与产育观念、亲族网络和对新生命的祝福有关。", "祝愿新生儿平安成长，确认亲族关系。", "现代医学护理改变了洗浴方式，但满月宴和亲友祝福仍广泛存在。", "PDF 将洗三与满月列入生育成年礼俗专题。", new[] { "准备温水和清洁用品", "准备红蛋、礼物或满月宴", "邀请亲族" }, new[] { "出生第三日或地方约定日期进行象征性清洗", "长辈说吉祥话或赠礼", "满月时宴请亲友", "亲友赠送红包、衣物或育儿用品" }, new[] { "北方部分地区强调洗三婆和吉祥话", "现代城市更多保留满月宴而简化洗三" }, new[] { "温水", "红蛋", "婴儿衣物", "礼金" }, new[] { "新生儿护理应以医学安全为先", "不宜强行执行可能影响健康的传统做法" }, new[] { "新生儿", "父母", "祖辈", "亲友" }, new[] { "洗三", "满月", "生育礼俗", "人生礼仪" }));
        UpsertEntry(context, PdfEntry(126, "抓周周岁礼", "zhuazhou-first-birthday-divination", "ritual", 1, 113, "全国多地", "抓周是在儿童周岁时摆放若干象征性物件，让孩子自由抓取，用以表达亲友对未来志趣的祝福和趣味性预测。", "习俗与周岁庆贺、儿童成长节点和象征物占验有关。", "祝愿成长、寄托期待、增强家庭仪式感。", "现代抓周常与摄影、亲子宴和定制道具结合。", "PDF 将抓周列入生育成年礼俗专题。", new[] { "准备象征物件", "布置周岁空间", "邀请亲友见证" }, new[] { "孩子换上周岁服饰", "家人摆放书、笔、算盘、印章等物件", "让孩子自由抓取", "长辈解释象征含义并送祝福", "举行周岁宴或拍照留念" }, new[] { "各地物件组合不同", "现代家庭常加入听诊器、鼠标、球类等新物件" }, new[] { "书本", "笔", "算盘", "印章", "玩具" }, new[] { "物件应避免尖锐或误吞风险", "抓取结果只宜作祝福和游戏，不宜给孩子贴标签" }, new[] { "儿童", "父母", "祖辈", "亲友" }, new[] { "抓周", "周岁", "儿童礼俗", "象征物" }));
        UpsertEntry(context, PdfEntry(127, "天穿节煎饼补天", "tianchuan-festival-pancake-sky-mending", "ritual", 1, 114, "部分北方及中原地区", "天穿节与女娲补天传说相关，部分地区有摊煎饼、食煎饼或以圆形食物象征补天的习俗。", "民间传说认为正月某日天有缺漏，以女娲补天故事解释煎饼补天和避针线等禁忌。", "补缺迎祥、祈求一年平安。", "该节日知名度较低，多以地方记忆和民俗资料形式保存。", "PDF 将天穿节列为冷门节日，本条整理其代表性食俗。", new[] { "准备面糊", "摊制煎饼", "讲述女娲补天传说" }, new[] { "节日当天摊制圆形煎饼", "家人分食煎饼", "部分地区将煎饼放置屋顶或高处象征补天", "老人向儿童讲述补天故事" }, new[] { "会期日期和做法因地而异", "有些地区仅保留食俗而不再进行象征性放置" }, new[] { "面糊", "鏊子或平底锅", "煎饼" }, new[] { "部分地区忌动针线", "不宜把传说解释为统一历史事实" }, new[] { "家庭成员", "长辈", "儿童" }, new[] { "天穿节", "女娲补天", "煎饼", "冷门节日" }));
        UpsertEntry(context, PdfEntry(128, "填仓节灰囤仪式", "tiancang-festival-ash-granary", "ritual", 104, 115, "华北乡村", "填仓节常在正月二十五前后举行，部分地区以草木灰或柴灰在院中画粮囤，并放入五谷象征仓廪充盈。", "习俗与粮仓崇拜、年后农事准备和祈丰心理相关。", "祈求粮食充足、家业兴旺。", "现代家庭粮仓功能减弱，灰囤仪式多以记忆和展示方式保存。", "PDF 将填仓节列为冷门节日，本条整理北方代表性做法。", new[] { "准备草木灰或替代材料", "准备五谷", "清扫院落或门前空地" }, new[] { "在院中用灰画圆形或方形粮囤", "在囤心放置五谷或硬币", "家人围绕粮囤说吉祥话", "有些地区配合吃干饭、烙饼等食俗" }, new[] { "华北地区灰囤形态较突出", "城市家庭常以象征性摆放或文字记录替代" }, new[] { "草木灰", "五谷", "扫帚", "硬币" }, new[] { "灰料需注意清洁和通风", "公共空间不宜随意撒灰影响环境" }, new[] { "家庭成员", "长辈", "儿童" }, new[] { "填仓节", "灰囤", "粮仓", "祈丰" }));
        UpsertEntry(context, PdfEntry(129, "寒衣节送寒衣", "winter-clothes-festival-paper-clothes", "ritual", 1, 116, "全国多地", "寒衣节在农历十月初一举行，民间以祭祖、焚送纸衣或献供方式表达对亡者冬季保暖的关怀。", "习俗与秋冬换季、祖先祭祀和慎终追远观念相关。", "追思祖先、寄托孝思、提醒冬令到来。", "环保祭祀和集中焚烧设施改变了传统焚纸方式。", "PDF 将寒衣节列为冷门或较少被大众熟悉的节日之一。", new[] { "准备纸衣或鲜花供品", "确定祭祀地点", "注意防火规定" }, new[] { "到墓地、祠堂或家中祭位前摆供", "上香或默哀追思", "按当地规范焚化纸衣或改用环保方式", "清理现场并向后辈讲述家族记忆" }, new[] { "北方地区送寒衣叙事更常见", "城市公墓多要求集中焚烧或禁烧" }, new[] { "纸衣", "香烛", "供品", "鲜花" }, new[] { "严禁野外违规用火", "不宜在禁烧区域焚纸" }, new[] { "家族成员", "长辈", "祭扫者" }, new[] { "寒衣节", "祭祖", "送寒衣", "秋冬节俗" }));
        UpsertEntry(context, PdfEntry(130, "天水黑社火夜间巡游", "tianshui-black-shehuo-night-parade", "ritual", 103, 117, "甘肃天水", "天水黑社火以夜间巡游、角色扮演、鼓乐和火光氛围为特色，是社区节庆与民间表演结合的地方民俗。", "社火传统与春节元宵期间的迎神、驱邪、娱神和社区娱乐相关。", "驱邪迎祥、公共欢庆、展示社区组织能力。", "现代黑社火常被纳入城市节庆活动，需要继续记录传统组织者、路线和角色含义。", "PDF 将天水黑社火列入社区节庆专题。", new[] { "确定巡游路线", "准备角色服饰和脸谱", "安排鼓乐队和安全照明" }, new[] { "社火队伍集合装扮", "夜间按路线巡游", "鼓乐伴随角色扮演和队形变化", "沿途居民观看、迎接和互动", "巡游结束后卸妆收具" }, new[] { "天水黑社火强调夜间和黑色装扮视觉", "其他地区社火可能以白天踩街、秧歌或高跷为主" }, new[] { "服饰", "脸谱", "鼓乐", "火把或照明器具" }, new[] { "夜间巡游需重视交通与消防安全", "拍摄人物肖像需尊重参与者意愿" }, new[] { "社火队", "鼓乐手", "社区组织者", "观众" }, new[] { "天水", "黑社火", "社火", "夜间巡游" }));
    }

    private static FolkloreEntryEntity PdfEntry(
        int id,
        string title,
        string slug,
        string contentType,
        int regionId,
        int festivalId,
        string regionScope,
        string summary,
        string historicalOrigin,
        string symbolicMeaning,
        string inheritanceStatus,
        string changeNotes,
        string[] preparations,
        string[] steps,
        string[] regionalDifferences,
        string[] items,
        string[] taboos,
        string[] participants,
        string[] tags,
        string confidenceLevel = "medium",
        string reviewStatus = "pending-verification")
    {
        return new FolkloreEntryEntity
        {
            Id = id,
            Title = title,
            Slug = slug,
            ContentType = contentType,
            RegionId = regionId,
            FestivalId = festivalId,
            Summary = summary,
            RegionFieldsJson = JsonSerializer.Serialize(new Dictionary<string, string> { ["scope"] = regionScope }),
            HistoricalOrigin = historicalOrigin,
            SymbolicMeaning = symbolicMeaning,
            InheritanceStatus = inheritanceStatus,
            ChangeNotes = changeNotes,
            OralText = string.Empty,
            PreparationsJson = JsonListSerializer.Serialize(preparations),
            RitualProcessJson = JsonListSerializer.Serialize(steps),
            RegionalDifferencesJson = JsonListSerializer.Serialize(regionalDifferences),
            ItemsUsedJson = JsonListSerializer.Serialize(items),
            TaboosJson = JsonListSerializer.Serialize(taboos),
            ParticipantsJson = JsonListSerializer.Serialize(participants),
            TagsJson = JsonListSerializer.Serialize(tags),
            CreatedAt = new DateTime(2026, 4, 23, 2, 30, 0, DateTimeKind.Utc),
            UpdatedAt = SeedUpdatedAt.AddDays(3),
            CreatedBy = "pdf-import",
            Status = "published",
            ReviewStatus = reviewStatus,
            ConfidenceLevel = confidenceLevel,
            SourceGrade = "compiled-note",
            Version = 1,
            ChangeLog = "由项目根目录 PDF 调研资料整理为结构化词条"
        };
    }

    private static void SeedPdfSources(AppDbContext context)
    {
        UpsertSource(context, new SourceEvidenceEntity
        {
            Id = 100,
            SourceType = "PDF 调研报告",
            SourceLevel = "compiled-note",
            Title = "中国农村节日民俗总览与目录",
            Contributor = "项目根目录 PDF 资料",
            RecordedAt = "2026-04-23",
            Citation = "《中国农村节日民俗总览与目录.pdf》，项目本地资料。",
            Notes = "用于建立农村节日和民族节日的结构化入口。资料仍需后续与地方志、田野记录或权威文献交叉核实。"
        });

        UpsertSource(context, new SourceEvidenceEntity
        {
            Id = 101,
            SourceType = "PDF 调研报告",
            SourceLevel = "compiled-note",
            Title = "中国民间非固定节日风俗与冷门习俗研究报告",
            Contributor = "项目根目录 PDF 资料",
            RecordedAt = "2026-04-23",
            Citation = "《中国民间非固定节日风俗与冷门习俗研究报告.pdf》，项目本地资料。",
            Notes = "用于整理初一十五烧香、婚俗、丧俗、生育礼俗和冷门节日等条目。涉及敏感或争议习俗的条目已降低置信等级。"
        });
    }

    private static void SeedPdfEntrySources(AppDbContext context)
    {
        for (var entryId = 100; entryId <= 110; entryId++)
        {
            UpsertEntrySource(context, new EntrySourceEntity { Id = 1000 + entryId, EntryId = entryId, SourceId = 100 });
        }

        for (var entryId = 120; entryId <= 130; entryId++)
        {
            UpsertEntrySource(context, new EntrySourceEntity { Id = 1000 + entryId, EntryId = entryId, SourceId = 101 });
        }
    }

    private static void SeedPdfFaqs(AppDbContext context)
    {
        UpsertFaq(context, new EntryFaqEntity { Id = 1000, EntryId = 120, SortOrder = 1, Question = "初一十五烧香一定要去寺庙吗？", Answer = "不一定。PDF 资料显示它既有寺庙礼佛形态，也有家庭神龛、祖先牌位或地方神祭拜形态，具体做法需以当地习惯和场所规定为准。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 1001, EntryId = 120, SortOrder = 2, Question = "为什么常说三支香？", Answer = "常见解释会联系佛、法、僧或道教三清等象征系统，但不同地区香数并不完全一致，页面中统一标记为待核实资料。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 1002, EntryId = 121, SortOrder = 1, Question = "“偷亲”是否还能作为现代婚礼流程？", Answer = "不应把历史或民俗叙述直接当作现代实践。现代婚姻必须以双方自愿和法律规范为前提，相关说法只适合作为历史资料谨慎记录。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 1003, EntryId = 122, SortOrder = 1, Question = "苏庄村婚俗最适合继续补充什么资料？", Answer = "优先补充婚书样式、花馍喜饼图像、院落路线图、长辈口述和不同年代婚礼流程差异。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 1004, EntryId = 123, SortOrder = 1, Question = "为什么这个词条是批判性记录？", Answer = "因为相关内容涉及现实法律与伦理风险，网站只保留民俗史解释、风险提示和研究边界，不提供任何可操作流程。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 1005, EntryId = 124, SortOrder = 1, Question = "跳丧是娱乐表演吗？", Answer = "在原本语境中它属于丧葬礼仪的一部分，具有悼念和慰藉功能。舞台展演不能完全替代真实礼俗语境。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 1006, EntryId = 127, SortOrder = 1, Question = "天穿节为什么吃煎饼？", Answer = "常见民间解释把圆形煎饼与女娲补天传说联系起来，象征补缺迎祥；但具体日期和做法因地而异。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 1007, EntryId = 128, SortOrder = 1, Question = "填仓节的灰囤现在还能画吗？", Answer = "家庭院落中可做象征性记录或展示，但公共空间不宜随意撒灰；现代整理更适合保留图像、口述和流程说明。" });
        UpsertFaq(context, new EntryFaqEntity { Id = 1008, EntryId = 130, SortOrder = 1, Question = "黑社火和普通社火有什么区别？", Answer = "黑社火更强调夜间巡游、装扮色彩和火光氛围；普通社火则可能包含白天踩街、高跷、秧歌、龙灯等不同地方形态。" });
    }

    private static void SeedPdfRelations(AppDbContext context)
    {
        UpsertRelation(context, new EntryRelationEntity { Id = 1000, EntryId = 100, RelatedEntryId = 101, RelationType = "same-cycle", Note = "同属春节至元宵礼俗周期" });
        UpsertRelation(context, new EntryRelationEntity { Id = 1001, EntryId = 101, RelatedEntryId = 130, RelationType = "related-performance", Note = "元宵公共活动与社火巡游相关" });
        UpsertRelation(context, new EntryRelationEntity { Id = 1002, EntryId = 104, RelatedEntryId = 105, RelationType = "same-rural-belief", Note = "同属乡村农事信仰与庙会社群活动" });
        UpsertRelation(context, new EntryRelationEntity { Id = 1003, EntryId = 104, RelatedEntryId = 107, RelationType = "agricultural-cycle", Note = "社日与青苗会都围绕农事祈愿展开" });
        UpsertRelation(context, new EntryRelationEntity { Id = 1004, EntryId = 121, RelatedEntryId = 122, RelationType = "compare", Note = "可对照不同地域婚俗的物件、路线与歌舞差异" });
        UpsertRelation(context, new EntryRelationEntity { Id = 1005, EntryId = 125, RelatedEntryId = 126, RelationType = "life-cycle", Note = "同属儿童成长早期的人生礼仪" });
        UpsertRelation(context, new EntryRelationEntity { Id = 1006, EntryId = 127, RelatedEntryId = 128, RelationType = "rare-festival", Note = "同属正月后段较冷门节日" });
        UpsertRelation(context, new EntryRelationEntity { Id = 1007, EntryId = 129, RelatedEntryId = 102, RelationType = "ancestor-ritual", Note = "同属祖先祭祀与慎终追远主题" });
    }

    private static void UpsertRegion(AppDbContext context, RegionEntity entity)
    {
        var existing = context.Regions.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.Regions.Add(entity);
            return;
        }

        existing.Slug = entity.Slug;
        existing.Name = entity.Name;
        existing.Type = entity.Type;
        existing.ParentId = entity.ParentId;
        existing.FullPath = entity.FullPath;
        existing.Summary = entity.Summary;
        existing.CulturalFocus = entity.CulturalFocus;
        existing.HighlightsJson = entity.HighlightsJson;
        existing.UpdatedAt = entity.UpdatedAt;
    }

    private static void UpsertFestival(AppDbContext context, FestivalEntity entity)
    {
        var existing = context.Festivals.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.Festivals.Add(entity);
            return;
        }

        existing.Slug = entity.Slug;
        existing.Name = entity.Name;
        existing.Category = entity.Category;
        existing.LunarLabel = entity.LunarLabel;
        existing.Summary = entity.Summary;
        existing.CoreTopicsJson = entity.CoreTopicsJson;
        existing.UpdatedAt = entity.UpdatedAt;
    }

    private static void UpsertEntry(AppDbContext context, FolkloreEntryEntity entity)
    {
        var existing = context.Entries.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.Entries.Add(entity);
            return;
        }

        existing.Title = entity.Title;
        existing.Slug = entity.Slug;
        existing.ContentType = entity.ContentType;
        existing.RegionId = entity.RegionId;
        existing.FestivalId = entity.FestivalId;
        existing.Summary = entity.Summary;
        existing.RegionFieldsJson = entity.RegionFieldsJson;
        existing.HistoricalOrigin = entity.HistoricalOrigin;
        existing.SymbolicMeaning = entity.SymbolicMeaning;
        existing.InheritanceStatus = entity.InheritanceStatus;
        existing.ChangeNotes = entity.ChangeNotes;
        existing.OralText = entity.OralText;
        existing.PreparationsJson = entity.PreparationsJson;
        existing.RitualProcessJson = entity.RitualProcessJson;
        existing.RegionalDifferencesJson = entity.RegionalDifferencesJson;
        existing.ItemsUsedJson = entity.ItemsUsedJson;
        existing.TaboosJson = entity.TaboosJson;
        existing.ParticipantsJson = entity.ParticipantsJson;
        existing.TagsJson = entity.TagsJson;
        existing.CreatedAt = entity.CreatedAt;
        existing.UpdatedAt = entity.UpdatedAt;
        existing.CreatedBy = entity.CreatedBy;
        existing.Status = entity.Status;
        existing.ReviewStatus = entity.ReviewStatus;
        existing.ConfidenceLevel = entity.ConfidenceLevel;
        existing.SourceGrade = entity.SourceGrade;
        existing.Version = entity.Version;
        existing.ChangeLog = entity.ChangeLog;
        existing.ImagesJson = entity.ImagesJson;
        existing.VideosJson = entity.VideosJson;
        existing.AudiosJson = entity.AudiosJson;
        existing.LocationJson = entity.LocationJson;
    }

    private static void UpsertSource(AppDbContext context, SourceEvidenceEntity entity)
    {
        var existing = context.Sources.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.Sources.Add(entity);
            return;
        }

        existing.SourceType = entity.SourceType;
        existing.SourceLevel = entity.SourceLevel;
        existing.Title = entity.Title;
        existing.Contributor = entity.Contributor;
        existing.RecordedAt = entity.RecordedAt;
        existing.Citation = entity.Citation;
        existing.Url = entity.Url;
        existing.Notes = entity.Notes;
    }

    private static void UpsertEntrySource(AppDbContext context, EntrySourceEntity entity)
    {
        var existing = context.EntrySources.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.EntrySources.Add(entity);
            return;
        }

        existing.EntryId = entity.EntryId;
        existing.SourceId = entity.SourceId;
    }

    private static void UpsertFaq(AppDbContext context, EntryFaqEntity entity)
    {
        var existing = context.EntryFaqs.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.EntryFaqs.Add(entity);
            return;
        }

        existing.EntryId = entity.EntryId;
        existing.Question = entity.Question;
        existing.Answer = entity.Answer;
        existing.SortOrder = entity.SortOrder;
    }

    private static void UpsertRelation(AppDbContext context, EntryRelationEntity entity)
    {
        var existing = context.EntryRelations.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.EntryRelations.Add(entity);
            return;
        }

        existing.EntryId = entity.EntryId;
        existing.RelatedEntryId = entity.RelatedEntryId;
        existing.RelationType = entity.RelationType;
        existing.Note = entity.Note;
    }

    private static void UpsertSubmission(AppDbContext context, SubmissionRecordEntity entity)
    {
        var existing = context.Submissions.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.Submissions.Add(entity);
            return;
        }

        existing.RelatedEntryId = entity.RelatedEntryId;
        existing.ContentType = entity.ContentType;
        existing.ContributorName = entity.ContributorName;
        existing.RegionId = entity.RegionId;
        existing.FestivalId = entity.FestivalId;
        existing.Title = entity.Title;
        existing.Summary = entity.Summary;
        existing.SourceSummary = entity.SourceSummary;
        existing.Contact = entity.Contact;
        existing.SubmittedAtUtc = entity.SubmittedAtUtc;
        existing.Status = entity.Status;
        existing.ReviewerNote = entity.ReviewerNote;
        existing.CreatedAt = entity.CreatedAt;
        existing.UpdatedAt = entity.UpdatedAt;
        existing.CreatedBy = entity.CreatedBy;
        existing.Version = entity.Version;
        existing.ChangeLog = entity.ChangeLog;
    }
}
