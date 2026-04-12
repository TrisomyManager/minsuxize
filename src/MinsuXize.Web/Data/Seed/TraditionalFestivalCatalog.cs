using MinsuXize.Web.Data.Entities;

namespace MinsuXize.Web.Data.Seed;

public static class TraditionalFestivalCatalog
{
    public static IReadOnlyList<FestivalEntity> CreateFestivals() =>
    [
        BuildFestival(5, "元宵节", "岁时节日", "农历正月十五", "灯会、团圆、赏灯、社火"),
        BuildFestival(6, "龙抬头", "岁时节日", "农历二月初二", "春耕启动、剃龙头、祭土地"),
        BuildFestival(7, "上巳节", "岁时节日", "农历三月初三", "祓禊、踏青、临水修禊"),
        BuildFestival(8, "寒食节", "岁时节日", "清明前一二日", "禁火、冷食、祭扫"),
        BuildFestival(9, "端午节", "岁时节日", "农历五月初五", "艾草、龙舟、粽子、辟邪"),
        BuildFestival(10, "七夕节", "岁时节日", "农历七月初七", "乞巧、牛郎织女、女性手艺"),
        BuildFestival(11, "中秋节", "岁时节日", "农历八月十五", "月饼、赏月、团圆、祭月"),
        BuildFestival(12, "重阳节", "岁时节日", "农历九月初九", "登高、敬老、菊花、祈福"),
        BuildFestival(13, "寒衣节", "岁时节日", "农历十月初一", "祭祖、送寒衣、焚纸"),
        BuildFestival(14, "下元节", "岁时节日", "农历十月十五", "酬谢水官、祈福消灾"),
        BuildFestival(15, "冬至", "岁时节日", "节气节点，约公历12月21日至23日", "祭祖、饮食节俗、岁终转换"),
        BuildFestival(16, "腊八节", "岁时节日", "农历十二月初八", "腊八粥、岁末开端、祭祀"),
        BuildFestival(17, "除夕", "岁时节日", "农历十二月三十或二十九", "守岁、贴春联、年夜饭、祭祖"),
        BuildFestival(18, "花朝节", "岁时节日", "农历二月十二或二月十五", "赏花、祭花神、踏青"),
        BuildFestival(19, "中和节", "岁时节日", "农历二月初二前后", "迎富、劝农、和中致用"),
        BuildFestival(20, "春社", "岁时节日", "立春后第五个戊日", "祭社神、祈丰收、乡里聚会"),
        BuildFestival(21, "浴佛节", "岁时节日", "农历四月初八", "浴佛、放生、行善祈福"),
        BuildFestival(22, "秋社", "岁时节日", "立秋后第五个戊日", "谢社神、庆丰收、尝新"),
        BuildFestival(23, "小年", "岁时节日", "农历腊月二十三或二十四", "祭灶、扫尘、备年货"),
        BuildFestival(101, "立春", "节气", "公历约2月3日至5日", "迎春、鞭春牛、春耕"),
        BuildFestival(102, "雨水", "节气", "公历约2月18日至20日", "农事准备、祈丰"),
        BuildFestival(103, "惊蛰", "节气", "公历约3月5日至7日", "驱虫、祭白虎、春雷"),
        BuildFestival(104, "春分", "节气", "公历约3月20日至22日", "竖蛋、祭日、农耕平衡"),
        BuildFestival(105, "清明", "节气", "公历约4月4日至6日", "扫墓、插柳、踏青"),
        BuildFestival(106, "谷雨", "节气", "公历约4月19日至21日", "采茶、春播、祈雨"),
        BuildFestival(107, "立夏", "节气", "公历约5月5日至7日", "尝新、称人、迎夏"),
        BuildFestival(108, "小满", "节气", "公历约5月20日至22日", "麦熟将满、农作观察"),
        BuildFestival(109, "芒种", "节气", "公历约6月5日至7日", "忙种忙收、送花神"),
        BuildFestival(110, "夏至", "节气", "公历约6月21日至22日", "祭神、面食、消夏"),
        BuildFestival(111, "小暑", "节气", "公历约7月6日至8日", "消暑、伏天起始"),
        BuildFestival(112, "大暑", "节气", "公历约7月22日至24日", "消夏、防暑、祈安"),
        BuildFestival(113, "立秋", "节气", "公历约8月7日至9日", "啃秋、贴秋膘、迎秋"),
        BuildFestival(114, "处暑", "节气", "公历约8月22日至24日", "出暑、秋收准备"),
        BuildFestival(115, "白露", "节气", "公历约9月7日至9日", "酿酒、收露、秋凉"),
        BuildFestival(116, "秋分", "节气", "公历约9月22日至24日", "祭月、秋收平衡"),
        BuildFestival(117, "寒露", "节气", "公历约10月8日至9日", "登高、赏菊、秋收"),
        BuildFestival(118, "霜降", "节气", "公历约10月23日至24日", "进补、秋收收尾"),
        BuildFestival(119, "立冬", "节气", "公历约11月7日至8日", "迎冬、补冬、祭祖"),
        BuildFestival(120, "小雪", "节气", "公历约11月22日至23日", "腌制、冬藏、农闲转换"),
        BuildFestival(121, "大雪", "节气", "公历约12月6日至8日", "封藏、进补、御寒"),
        BuildFestival(122, "冬至", "节气", "公历约12月21日至23日", "祭祖、团圆、阳生"),
        BuildFestival(123, "小寒", "节气", "公历约1月5日至7日", "腊祭、御寒、年节准备"),
        BuildFestival(124, "大寒", "节气", "公历约1月19日至21日", "岁末收尾、备年货")
    ];

    private static FestivalEntity BuildFestival(int id, string name, string category, string lunarLabel, string topics) =>
        new()
        {
            Id = id,
            Name = name,
            Category = category,
            LunarLabel = lunarLabel,
            Summary = $"作为传统{category}目录节点，用于汇总全国不同地区在 {name} 前后的细俗做法。",
            CoreTopicsJson = JsonListSerializer.Serialize(topics.Split('、'))
        };
}
