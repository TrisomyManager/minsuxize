using MinsuXize.Web.Models;

namespace MinsuXize.Web.Services;

public sealed class InMemoryFolkloreRepository : IFolkloreRepository
{
    private readonly object _gate = new();
    private readonly List<Region> _regions;
    private readonly List<Festival> _festivals;
    private readonly List<FolkloreEntry> _entries;
    private readonly List<SourceEvidence> _sources;
    private readonly List<SubmissionRecord> _submissions;
    private readonly List<ReviewHistory> _reviewHistories;
    private int _submissionId;
    private int _reviewHistoryId;

    public InMemoryFolkloreRepository()
    {
        _regions =
        [
            new Region
            {
                Id = 1,
                Name = "中国",
                Type = "国家",
                FullPath = "中国",
                Summary = "作为项目根节点，用来承接跨地区专题、节日对照和全国范围的民俗比较。",
                CulturalFocus = "支持全国检索、跨地区差异对照和专题聚合。",
                Highlights = ["全国检索", "专题聚合", "跨村落对照"]
            },
            new Region
            {
                Id = 2,
                Name = "山西省",
                Type = "省",
                ParentId = 1,
                FullPath = "中国 / 山西省",
                Summary = "山西保留了大量与祭祖、社火、庙会、岁时节令相关的民俗材料，村落层级传统尤为丰富。",
                CulturalFocus = "岁时礼俗、家族祠堂、社火表演。",
                Highlights = ["祭祖体系", "社火表演", "村落传统保存较深"]
            },
            new Region
            {
                Id = 3,
                Name = "长治市",
                Type = "市",
                ParentId = 2,
                FullPath = "中国 / 山西省 / 长治市",
                Summary = "长治地区拥有较强的上党文化底色，节日风俗和祖先祭祀传统保留较多。",
                CulturalFocus = "上党文化、春节仪式、中元祭祀。",
                Highlights = ["上党民俗圈", "村落礼俗", "农历节日仪式"]
            },
            new Region
            {
                Id = 4,
                Name = "上党区",
                Type = "区",
                ParentId = 3,
                FullPath = "中国 / 山西省 / 长治市 / 上党区",
                Summary = "适合按乡镇和行政村进一步沉淀春节、清明、中元等风俗细则。",
                CulturalFocus = "祭祖、祭灶、年节空间布置。",
                Highlights = ["行政村颗粒度", "年俗流程", "口述资料"]
            },
            new Region
            {
                Id = 5,
                Name = "苏店镇",
                Type = "镇",
                ParentId = 4,
                FullPath = "中国 / 山西省 / 长治市 / 上党区 / 苏店镇",
                Summary = "适合作为乡镇层级的民俗聚合页，汇总下辖村落的节令差异。",
                CulturalFocus = "春节准备、祭灶、家户礼制。",
                Highlights = ["镇级汇总", "多村落对照", "投稿入口"]
            },
            new Region
            {
                Id = 6,
                Name = "秦村",
                Type = "村",
                ParentId = 5,
                FullPath = "中国 / 山西省 / 长治市 / 上党区 / 苏店镇 / 秦村",
                Summary = "原型示例村落，展示除夕祭祖与春节准备流程如何被结构化记录。",
                CulturalFocus = "除夕祭祖、祭灶、供品与禁忌。",
                Highlights = ["除夕祭祖", "口述祭词", "供品结构化"]
            },
            new Region
            {
                Id = 7,
                Name = "西火镇",
                Type = "镇",
                ParentId = 3,
                FullPath = "中国 / 山西省 / 长治市 / 西火镇",
                Summary = "原型示例镇区，展示中元相关仪式和地方表演性民俗记录。",
                CulturalFocus = "中元节、表演性民俗、铁花与火文化。",
                Highlights = ["中元节", "地方表演", "社区参与"]
            }
        ];

        _festivals =
        [
            new Festival
            {
                Id = 1,
                Name = "春节",
                Category = "岁时节日",
                LunarLabel = "农历正月初一，准备期常从腊月二十三开始",
                Summary = "记录从祭灶、扫尘、备年货到除夕、初一拜年的完整年节周期。",
                CoreTopics = ["祭祖", "守岁", "供品", "禁忌"]
            },
            new Festival
            {
                Id = 2,
                Name = "祭灶",
                Category = "岁末节点",
                LunarLabel = "农历腊月二十三或二十四",
                Summary = "记录灶神上天前的供品、祭词、时间选择与性别分工。",
                CoreTopics = ["糖瓜", "祭词", "送灶", "年节启动"]
            },
            new Festival
            {
                Id = 3,
                Name = "中元节",
                Category = "岁时节日",
                LunarLabel = "农历七月十五",
                Summary = "围绕祭祖、超度、河灯、火文化与地方表演传统展开。",
                CoreTopics = ["祭祖", "河灯", "地方表演", "社区参与"]
            },
            new Festival
            {
                Id = 4,
                Name = "清明",
                Category = "岁时节日",
                LunarLabel = "公历四月上旬，节气前后",
                Summary = "侧重扫墓、插柳、踏青和家族祭祀秩序。",
                CoreTopics = ["扫墓", "插柳", "踏青", "祖先记忆"]
            }
        ];

        _sources =
        [
            new SourceEvidence
            {
                Id = 1,
                SourceType = "田野口述",
                Title = "秦村除夕祭祖口述记录",
                Contributor = "村中年长主持者口述，站点筹备阶段整理",
                RecordedAt = "2026-02-02",
                Citation = "口述记录，待补录受访者授权信息与录音编号。",
                Notes = "适合作为示例，后续应补充更完整的田野编号体系。"
            },
            new SourceEvidence
            {
                Id = 2,
                SourceType = "田野观察",
                Title = "祭灶流程观察札记",
                Contributor = "项目示例资料",
                RecordedAt = "2026-01-31",
                Citation = "祭灶时间、供品与祭词结构化观察记录。",
                Notes = "展示结构化字段如何支撑后续检索和比对。"
            },
            new SourceEvidence
            {
                Id = 3,
                SourceType = "专题整理",
                Title = "西火镇中元表演性民俗整理",
                Contributor = "项目示例资料",
                RecordedAt = "2025-08-20",
                Citation = "按节日节点、参与角色、表演内容拆分字段。",
                Notes = "适合后续扩展到图片、视频和来源链接。"
            }
        ];

        _entries =
        [
            new FolkloreEntry
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
                RitualProcess =
                [
                    "黄昏前后整理堂屋与供桌，摆放香炉、供品、纸钱。",
                    "家中长者带领上香，按长幼顺序依次叩拜。",
                    "焚化纸钱并口述请祖先归位、保佑来年平安。",
                    "祭毕回到年夜饭场景，形成神圣时间与家庭团聚的衔接。"
                ],
                ItemsUsed = ["香炉", "纸钱", "馒头或花馍", "酒水", "熟食供品"],
                Taboos = ["祭拜过程中避免嬉笑打闹", "供桌未撤前不随意挪动物件", "祭词期间避免说不吉利的话"],
                Participants = ["家中长者主礼", "家庭成员按长幼顺序参与", "晚辈负责摆供与辅助"],
                SourceIds = [1],
                // 新增字段
                CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 3, 15, 14, 30, 0, DateTimeKind.Utc),
                CreatedBy = "admin",
                Status = "published",
                Version = 1,
                ChangeLog = "初始版本",
                Images = ["images/秦村祭祖1.jpg", "images/秦村祭祖2.jpg"],
                Videos = ["videos/秦村祭祖仪式.mp4"],
                Audios = ["audios/祭词录音.mp3"],
                Location = new GeoLocation(36.1911, 113.1012, "山西省长治市上党区苏店镇秦村")
            },
            new FolkloreEntry
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
                RitualProcess =
                [
                    "腊月二十三或二十四黄昏前后清理灶台和周边空间。",
                    "摆放糖瓜、清水、草料与纸马等送行物件。",
                    "由家中长者焚香并简短念诵送灶祭词。",
                    "焚化纸马或象征物，表示灶神启程。"
                ],
                ItemsUsed = ["糖瓜", "清水", "草料", "纸马", "线香"],
                Taboos = ["不说晦气话", "仪式前不打翻供品", "灶台清理后避免马上重油烹煮"],
                Participants = ["家中长者主礼", "女性成员多负责清扫和备供", "儿童参与分食糖瓜"],
                SourceIds = [2],
                // 新增字段
                CreatedAt = new DateTime(2026, 2, 28, 15, 20, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 3, 10, 9, 45, 0, DateTimeKind.Utc),
                CreatedBy = "admin",
                Status = "published",
                Version = 1,
                ChangeLog = "初始版本",
                Images = ["images/祭灶1.jpg"],
                Videos = [],
                Audios = [],
                Location = new GeoLocation(36.1923, 113.1025, "山西省长治市上党区苏店镇秦村")
            },
            new FolkloreEntry
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
                RitualProcess =
                [
                    "先完成家户或村落层面的祭祖与供奉。",
                    "傍晚后转入公共空间的观看与参与场景。",
                    "通过火、光和群体动作形成强烈节日氛围。",
                    "活动结束后由长辈说明禁忌和来年祈愿。"
                ],
                ItemsUsed = ["香火", "纸钱", "灯火器具", "表演道具"],
                Taboos = ["表演空间内避免随意穿行", "祭祀时段不喧哗", "焚化物件需由熟悉流程者处理"],
                Participants = ["家户祭祖成员", "社区组织者", "表演参与者", "围观村民与返乡青年"],
                SourceIds = [3],
                // 新增字段
                CreatedAt = new DateTime(2026, 3, 5, 11, 30, 0, DateTimeKind.Utc),
                UpdatedAt = new DateTime(2026, 3, 18, 16, 15, 0, DateTimeKind.Utc),
                CreatedBy = "admin",
                Status = "published",
                Version = 1,
                ChangeLog = "初始版本",
                Images = ["images/中元节1.jpg", "images/中元节2.jpg", "images/中元节3.jpg"],
                Videos = ["videos/中元节表演.mp4"],
                Audios = ["audios/中元节祭词.mp3"],
                Location = new GeoLocation(36.2056, 113.0879, "山西省长治市上党区西火镇")
            }
        ];

        _submissions =
        [
            new SubmissionRecord
            {
                Id = 1,
                ContributorName = "示例投稿人",
                RegionId = 6,
                FestivalId = 1,
                Title = "补充秦村年夜饭供品细节",
                Summary = "补充了鱼、年糕和饺子在年夜饭中的摆放顺序与象征意义。",
                SourceSummary = "来自家庭长辈口述，建议补录音频来源。",
                Contact = "demo@example.local",
                SubmittedAt = DateTime.UtcNow.AddDays(-2),
                Status = SubmissionStatus.PendingReview
            },
            new SubmissionRecord
            {
                Id = 2,
                ContributorName = "示例审核流程",
                RegionId = 7,
                FestivalId = 3,
                Title = "补充西火镇中元节观看秩序",
                Summary = "增加公共空间站位、观看秩序与禁忌说明。",
                SourceSummary = "整理自返乡青年与村中老人回忆。",
                Contact = "review@example.local",
                SubmittedAt = DateTime.UtcNow.AddDays(-1),
                Status = SubmissionStatus.NeedsRevision,
                ReviewerNote = "请补充更明确的受访对象和时间。"
            }
        ];

        _reviewHistories = [];
        _submissionId = _submissions.Max(item => item.Id);
        _reviewHistoryId = 0;
    }

    public IReadOnlyList<Region> GetRegions() => _regions;

    public Region? GetRegionById(int id) => _regions.FirstOrDefault(item => item.Id == id);

    public IReadOnlyList<Region> GetChildRegions(int parentId) =>
        _regions.Where(item => item.ParentId == parentId).OrderBy(item => item.Id).ToList();

    public IReadOnlyList<int> GetRegionTreeIds(int regionId) =>
        RegionPresentation.GetRegionTreeIds(_regions, regionId);

    public IReadOnlyList<Festival> GetFestivals() => _festivals;

    public Festival? GetFestivalById(int id) => _festivals.FirstOrDefault(item => item.Id == id);

    public IReadOnlyList<FolkloreEntry> GetEntries() => _entries;

    public FolkloreEntry? GetEntryById(int id) => _entries.FirstOrDefault(item => item.Id == id);

    public IReadOnlyList<FolkloreEntry> GetEntriesByRegion(int regionId)
    {
        var regionTreeIds = GetRegionTreeIds(regionId).ToHashSet();
        return _entries.Where(item => regionTreeIds.Contains(item.RegionId)).OrderBy(item => item.Title).ToList();
    }

    public IReadOnlyList<FolkloreEntry> GetEntriesByFestival(int festivalId) =>
        _entries.Where(item => item.FestivalId == festivalId).OrderBy(item => item.Title).ToList();

    public IReadOnlyList<SourceEvidence> GetSourcesForEntry(int entryId)
    {
        var entry = GetEntryById(entryId);
        if (entry is null)
        {
            return [];
        }

        return _sources.Where(item => entry.SourceIds.Contains(item.Id)).ToList();
    }

    public IReadOnlyList<SubmissionRecord> GetSubmissions() =>
        _submissions.OrderByDescending(item => item.SubmittedAt).ToList();

    public SubmissionRecord? GetSubmissionById(int id) =>
        _submissions.FirstOrDefault(item => item.Id == id);

    public int GetPendingSubmissionCount() =>
        _submissions.Count(item => item.Status == SubmissionStatus.PendingReview);

    public int CreateSubmission(SubmissionInput input)
    {
        lock (_gate)
        {
            _submissionId++;

            var submission = new SubmissionRecord
            {
                Id = _submissionId,
                ContributorName = input.ContributorName,
                RegionId = input.RegionId,
                FestivalId = input.FestivalId,
                Title = input.Title,
                Summary = input.Summary,
                SourceSummary = input.SourceSummary,
                Contact = input.Contact,
                SubmittedAt = DateTime.UtcNow,
                Status = SubmissionStatus.PendingReview,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = input.ContributorName,
                Version = 1,
                ChangeLog = input.ChangeLog,
                Images = input.Images,
                Videos = input.Videos,
                Audios = input.Audios,
                Location = input.Location
            };

            _submissions.Add(submission);
            return submission.Id;
        }
    }

    public void UpdateSubmissionStatus(int submissionId, SubmissionStatus status, string? reviewerNote, string reviewerName)
    {
        lock (_gate)
        {
            var submission = _submissions.FirstOrDefault(item => item.Id == submissionId);
            if (submission is null)
            {
                return;
            }

            var oldStatus = submission.Status;
            submission.Status = status;
            submission.ReviewerNote = reviewerNote;
            submission.UpdatedAt = DateTime.UtcNow;
            
            // 记录审核历史
            var history = new ReviewHistory
            {
                Id = ++_reviewHistoryId,
                SubmissionId = submissionId,
                OldStatus = oldStatus,
                NewStatus = status,
                Reviewer = reviewerName,
                ReviewerNote = reviewerNote,
                ReviewedAt = DateTime.UtcNow,
                ChangeSummary = $"状态从 {oldStatus} 更改为 {status}"
            };
            
            _reviewHistories.Add(history);
        }
    }
    
    public IReadOnlyList<ReviewHistory> GetReviewHistory(int submissionId)
    {
        lock (_gate)
        {
            return _reviewHistories
                .Where(h => h.SubmissionId == submissionId)
                .OrderByDescending(h => h.ReviewedAt)
                .ToList();
        }
    }
    
    public void AddReviewHistory(ReviewHistory history)
    {
        lock (_gate)
        {
            history.Id = ++_reviewHistoryId;
            _reviewHistories.Add(history);
        }
    }
    
    public void BulkUpdateSubmissionStatus(List<int> submissionIds, SubmissionStatus status, string? reviewerNote, string reviewerName)
    {
        lock (_gate)
        {
            foreach (var submissionId in submissionIds)
            {
                var submission = _submissions.FirstOrDefault(item => item.Id == submissionId);
                if (submission is null) continue;
                
                var oldStatus = submission.Status;
                submission.Status = status;
                submission.ReviewerNote = reviewerNote;
                submission.UpdatedAt = DateTime.UtcNow;
                
                // 记录审核历史
                var history = new ReviewHistory
                {
                    Id = ++_reviewHistoryId,
                    SubmissionId = submissionId,
                    OldStatus = oldStatus,
                    NewStatus = status,
                    Reviewer = reviewerName,
                    ReviewerNote = reviewerNote,
                    ReviewedAt = DateTime.UtcNow,
                    ChangeSummary = $"批量操作：状态从 {oldStatus} 更改为 {status}"
                };
                
                _reviewHistories.Add(history);
            }
        }
    }
}
