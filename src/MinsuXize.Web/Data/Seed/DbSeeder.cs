using MinsuXize.Web.Data.Entities;
using MinsuXize.Web.Models;

namespace MinsuXize.Web.Data.Seed;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        SeedRegions(context);
        SeedFestivals(context);
        SeedEntries(context);
        SeedSources(context);
        SeedEntrySources(context);
        SeedSubmissions(context);

        context.SaveChanges();
    }

    private static void SeedRegions(AppDbContext context)
    {
        UpsertRegion(
            context,
            new RegionEntity
            {
                Id = 1,
                Name = "中国",
                Type = "国家",
                FullPath = "中国",
                Summary = "作为项目根节点，用来承接跨地区专题、节日对照和全国范围的民俗比较。",
                CulturalFocus = "支持全国检索、跨地区差异对照和专题聚合。",
                HighlightsJson = JsonListSerializer.Serialize("全国检索", "专题聚合", "跨村落对照")
            });

        UpsertRegion(
            context,
            new RegionEntity
            {
                Id = 2,
                Name = "山西省",
                Type = "省",
                ParentId = 1,
                FullPath = "中国 / 山西省",
                Summary = "山西保留了大量与祭祖、社火、庙会、岁时节令相关的民俗材料，村落层级传统尤为丰富。",
                CulturalFocus = "岁时礼俗、家族祠堂、社火表演。",
                HighlightsJson = JsonListSerializer.Serialize("祭祖体系", "社火表演", "村落传统保存较深")
            });

        UpsertRegion(
            context,
            new RegionEntity
            {
                Id = 3,
                Name = "长治市",
                Type = "市",
                ParentId = 2,
                FullPath = "中国 / 山西省 / 长治市",
                Summary = "长治地区拥有较强的上党文化底色，节日风俗和祖先祭祀传统保留较多。",
                CulturalFocus = "上党文化、春节仪式、中元祭祀。",
                HighlightsJson = JsonListSerializer.Serialize("上党民俗圈", "村落礼俗", "农历节日仪式")
            });

        UpsertRegion(
            context,
            new RegionEntity
            {
                Id = 4,
                Name = "上党区",
                Type = "区",
                ParentId = 3,
                FullPath = "中国 / 山西省 / 长治市 / 上党区",
                Summary = "适合按乡镇和行政村进一步沉淀春节、清明、中元等风俗细则。",
                CulturalFocus = "祭祖、祭灶、年节空间布置。",
                HighlightsJson = JsonListSerializer.Serialize("行政村颗粒度", "年俗流程", "口述资料")
            });

        UpsertRegion(
            context,
            new RegionEntity
            {
                Id = 5,
                Name = "苏店镇",
                Type = "镇",
                ParentId = 4,
                FullPath = "中国 / 山西省 / 长治市 / 上党区 / 苏店镇",
                Summary = "适合作为乡镇层级的民俗聚合页，汇总下辖村落的节令差异。",
                CulturalFocus = "春节准备、祭灶、家户礼制。",
                HighlightsJson = JsonListSerializer.Serialize("镇级汇总", "多村落对照", "投稿入口")
            });

        UpsertRegion(
            context,
            new RegionEntity
            {
                Id = 6,
                Name = "秦村",
                Type = "村",
                ParentId = 5,
                FullPath = "中国 / 山西省 / 长治市 / 上党区 / 苏店镇 / 秦村",
                Summary = "原型示例村落，展示除夕祭祖与春节准备流程如何被结构化记录。",
                CulturalFocus = "除夕祭祖、祭灶、供品与禁忌。",
                HighlightsJson = JsonListSerializer.Serialize("除夕祭祖", "口述祭词", "供品结构化")
            });

        UpsertRegion(
            context,
            new RegionEntity
            {
                Id = 7,
                Name = "西火镇",
                Type = "镇",
                ParentId = 3,
                FullPath = "中国 / 山西省 / 长治市 / 西火镇",
                Summary = "原型示例镇区，展示中元相关仪式和地方表演性民俗记录。",
                CulturalFocus = "中元节、表演性民俗、铁花与火文化。",
                HighlightsJson = JsonListSerializer.Serialize("中元节", "地方表演", "社区参与")
            });

        foreach (var region in NationalRegionCatalog.CreateRegions())
        {
            UpsertRegion(context, region);
        }
    }

    private static void SeedFestivals(AppDbContext context)
    {
        UpsertFestival(
            context,
            new FestivalEntity
            {
                Id = 1,
                Name = "春节",
                Category = "岁时节日",
                LunarLabel = "农历正月初一，准备期常从腊月二十三开始",
                Summary = "记录从祭灶、扫尘、备年货到除夕、初一拜年的完整年节周期。",
                CoreTopicsJson = JsonListSerializer.Serialize("祭祖", "守岁", "供品", "禁忌")
            });

        UpsertFestival(
            context,
            new FestivalEntity
            {
                Id = 2,
                Name = "祭灶",
                Category = "岁末节点",
                LunarLabel = "农历腊月二十三或二十四",
                Summary = "记录灶神上天前的供品、祭词、时间选择与性别分工。",
                CoreTopicsJson = JsonListSerializer.Serialize("糖瓜", "祭词", "送灶", "年节启动")
            });

        UpsertFestival(
            context,
            new FestivalEntity
            {
                Id = 3,
                Name = "中元节",
                Category = "岁时节日",
                LunarLabel = "农历七月十五",
                Summary = "围绕祭祖、超度、河灯、火文化与地方表演传统展开。",
                CoreTopicsJson = JsonListSerializer.Serialize("祭祖", "河灯", "地方表演", "社区参与")
            });

        UpsertFestival(
            context,
            new FestivalEntity
            {
                Id = 4,
                Name = "清明",
                Category = "岁时节日",
                LunarLabel = "公历四月上旬，节气前后",
                Summary = "侧重扫墓、插柳、踏青和家族祭祀秩序。",
                CoreTopicsJson = JsonListSerializer.Serialize("扫墓", "插柳", "踏青", "祖先记忆")
            });

        foreach (var festival in TraditionalFestivalCatalog.CreateFestivals())
        {
            UpsertFestival(context, festival);
        }
    }

    private static void SeedEntries(AppDbContext context)
    {
        UpsertEntry(
            context,
            new FolkloreEntryEntity
            {
                Id = 1,
                Title = "秦村除夕祭祖",
                RegionId = 6,
                FestivalId = 1,
                Summary = "以家户为单位完成供桌布置、上香、叩拜、焚纸与合家团圆，强调祖先归位与新岁开启。",
                HistoricalOrigin = "和北方家户祭祖传统相连，在村落层面表现出明显的家族秩序与岁末时间感。",
                SymbolicMeaning = "通过祖先归位、家人团聚和新旧岁交替，确认家族连续性与来年平安。",
                InheritanceStatus = "仍有延续，但完整祭词和动作细节主要掌握在年长者手中。",
                ChangeNotes = "年轻人多参与摆供、拍照与拜年，完整口传内容正在减少。",
                OralText = "请祖先归位、护佑儿孙、合家安稳、五谷丰登。",
                RitualProcessJson = JsonListSerializer.Serialize(
                    "黄昏前后整理堂屋与供桌，摆放香炉、供品、纸钱。",
                    "家中长者带领上香，按长幼顺序依次叩拜。",
                    "焚化纸钱并口述请祖先归位、保佑来年平安。",
                    "祭毕回到年夜饭场景，形成神圣时间与家庭团聚的衔接。"),
                ItemsUsedJson = JsonListSerializer.Serialize("香炉", "纸钱", "馒头或花馍", "酒水", "熟食供品"),
                TaboosJson = JsonListSerializer.Serialize("祭拜过程中避免嬉笑打闹", "供桌未撤前不随意挪动物件", "祭词期间避免说不吉利的话"),
                ParticipantsJson = JsonListSerializer.Serialize("家中长者主礼", "家庭成员按长幼顺序参与", "晚辈负责摆供与辅助")
            });

        UpsertEntry(
            context,
            new FolkloreEntryEntity
            {
                Id = 2,
                Title = "秦村祭灶",
                RegionId = 6,
                FestivalId = 2,
                Summary = "通过糖瓜、清水、草料与送灶祭词，标记春节周期正式启动。",
                HistoricalOrigin = "与北方岁末祭灶传统相连，保留送灶神上天、盼其言好事的观念。",
                SymbolicMeaning = "用甜味供品和送行物件表达对新一年吉庆、平安与家宅安稳的期待。",
                InheritanceStatus = "仍被记得，但仪式完整度和供品讲究程度已明显简化。",
                ChangeNotes = "部分家庭只保留象征性供糖与口头表达，具体祭词不再统一。",
                OralText = "上天言好事，回宫降吉祥。",
                RitualProcessJson = JsonListSerializer.Serialize(
                    "腊月二十三或二十四黄昏前后清理灶台和周边空间。",
                    "摆放糖瓜、清水、草料与纸马等送行物件。",
                    "由家中长者焚香并简短念诵送灶祭词。",
                    "焚化纸马或象征物，表示灶神启程。"),
                ItemsUsedJson = JsonListSerializer.Serialize("糖瓜", "清水", "草料", "纸马", "线香"),
                TaboosJson = JsonListSerializer.Serialize("不说晦气话", "仪式前不打翻供品", "灶台清理后避免马上重油烹煮"),
                ParticipantsJson = JsonListSerializer.Serialize("家中长者主礼", "女性成员多负责清扫和备供", "儿童参与分食糖瓜")
            });

        UpsertEntry(
            context,
            new FolkloreEntryEntity
            {
                Id = 3,
                Title = "西火镇中元节表演性民俗",
                RegionId = 7,
                FestivalId = 3,
                Summary = "中元节既有祭祖与超度意味，也叠加了社区观看、参与和展示性的地方民俗表达。",
                HistoricalOrigin = "在中元祭祀传统基础上，和地方火文化、公共节庆表演相互交织。",
                SymbolicMeaning = "兼具祭祀、慰灵、祈福与社区凝聚的多重功能。",
                InheritanceStatus = "仍具有较强公共性，但表演部分正在经历现代展示化转变。",
                ChangeNotes = "如今更容易被外地访客当作表演观看，因此更需要记录其原本的节俗语境。",
                OralText = "敬先人、安四邻、望一年平顺。",
                RitualProcessJson = JsonListSerializer.Serialize(
                    "先完成家户或村落层面的祭祖与供奉。",
                    "傍晚后转入公共空间的观看与参与场景。",
                    "通过火、光和群体动作形成强烈节日氛围。",
                    "活动结束后由长辈说明禁忌和来年祈愿。"),
                ItemsUsedJson = JsonListSerializer.Serialize("香火", "纸钱", "灯火器具", "表演道具"),
                TaboosJson = JsonListSerializer.Serialize("表演空间内避免随意穿行", "祭祀时段不喧哗", "焚化物件需由熟悉流程者处理"),
                ParticipantsJson = JsonListSerializer.Serialize("家户祭祖成员", "社区组织者", "表演参与者", "围观村民与返乡青年")
            });
    }

    private static void SeedSources(AppDbContext context)
    {
        UpsertSource(
            context,
            new SourceEvidenceEntity
            {
                Id = 1,
                SourceType = "田野口述",
                Title = "秦村除夕祭祖口述记录",
                Contributor = "村中年长主持者口述，站点筹备阶段整理",
                RecordedAt = "2026-02-02",
                Citation = "口述记录，待补录受访者授权信息与录音编号。",
                Notes = "适合作为示例，后续应补充更完整的田野编号体系。"
            });

        UpsertSource(
            context,
            new SourceEvidenceEntity
            {
                Id = 2,
                SourceType = "田野观察",
                Title = "祭灶流程观察札记",
                Contributor = "项目示例资料",
                RecordedAt = "2026-01-31",
                Citation = "祭灶时间、供品与祭词结构化观察记录。",
                Notes = "展示结构化字段如何支撑后续检索和比对。"
            });

        UpsertSource(
            context,
            new SourceEvidenceEntity
            {
                Id = 3,
                SourceType = "专题整理",
                Title = "西火镇中元表演性民俗整理",
                Contributor = "项目示例资料",
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

    private static void SeedSubmissions(AppDbContext context)
    {
        UpsertSubmission(
            context,
            new SubmissionRecordEntity
            {
                Id = 1,
                ContributorName = "示例投稿人",
                RegionId = 6,
                FestivalId = 1,
                Title = "补充秦村年夜饭供品细节",
                Summary = "补充了鱼、年糕和饺子在年夜饭中的摆放顺序与象征意义。",
                SourceSummary = "来自家庭长辈口述，建议补录音频来源。",
                Contact = "demo@example.local",
                SubmittedAtUtc = DateTime.UtcNow.AddDays(-2),
                Status = (int)SubmissionStatus.PendingReview
            });

        UpsertSubmission(
            context,
            new SubmissionRecordEntity
            {
                Id = 2,
                ContributorName = "示例审核流程",
                RegionId = 7,
                FestivalId = 3,
                Title = "补充西火镇中元节观看秩序",
                Summary = "增加公共空间站位、观看秩序与禁忌说明。",
                SourceSummary = "整理自返乡青年与村中老人回忆。",
                Contact = "review@example.local",
                SubmittedAtUtc = DateTime.UtcNow.AddDays(-1),
                Status = (int)SubmissionStatus.NeedsRevision,
                ReviewerNote = "请补充更明确的受访对象和时间。"
            });
    }

    private static void UpsertRegion(AppDbContext context, RegionEntity entity)
    {
        var existing = context.Regions.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.Regions.Add(entity);
            return;
        }

        existing.Name = entity.Name;
        existing.Type = entity.Type;
        existing.ParentId = entity.ParentId;
        existing.FullPath = entity.FullPath;
        existing.Summary = entity.Summary;
        existing.CulturalFocus = entity.CulturalFocus;
        existing.HighlightsJson = entity.HighlightsJson;
    }

    private static void UpsertFestival(AppDbContext context, FestivalEntity entity)
    {
        var existing = context.Festivals.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.Festivals.Add(entity);
            return;
        }

        existing.Name = entity.Name;
        existing.Category = entity.Category;
        existing.LunarLabel = entity.LunarLabel;
        existing.Summary = entity.Summary;
        existing.CoreTopicsJson = entity.CoreTopicsJson;
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
        existing.RegionId = entity.RegionId;
        existing.FestivalId = entity.FestivalId;
        existing.Summary = entity.Summary;
        existing.HistoricalOrigin = entity.HistoricalOrigin;
        existing.SymbolicMeaning = entity.SymbolicMeaning;
        existing.InheritanceStatus = entity.InheritanceStatus;
        existing.ChangeNotes = entity.ChangeNotes;
        existing.OralText = entity.OralText;
        existing.RitualProcessJson = entity.RitualProcessJson;
        existing.ItemsUsedJson = entity.ItemsUsedJson;
        existing.TaboosJson = entity.TaboosJson;
        existing.ParticipantsJson = entity.ParticipantsJson;
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

    private static void UpsertSubmission(AppDbContext context, SubmissionRecordEntity entity)
    {
        var existing = context.Submissions.FirstOrDefault(item => item.Id == entity.Id);
        if (existing is null)
        {
            context.Submissions.Add(entity);
            return;
        }

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
    }
}
