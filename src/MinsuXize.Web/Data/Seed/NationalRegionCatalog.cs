using MinsuXize.Web.Data.Entities;

namespace MinsuXize.Web.Data.Seed;

public static class NationalRegionCatalog
{
    public static IReadOnlyList<RegionEntity> CreateRegions() =>
    [
        BuildRegion(110000, "北京市", "直辖市"),
        BuildRegion(120000, "天津市", "直辖市"),
        BuildRegion(130000, "河北省", "省"),
        BuildRegion(150000, "内蒙古自治区", "自治区"),
        BuildRegion(210000, "辽宁省", "省"),
        BuildRegion(220000, "吉林省", "省"),
        BuildRegion(230000, "黑龙江省", "省"),
        BuildRegion(310000, "上海市", "直辖市"),
        BuildRegion(320000, "江苏省", "省"),
        BuildRegion(330000, "浙江省", "省"),
        BuildRegion(340000, "安徽省", "省"),
        BuildRegion(350000, "福建省", "省"),
        BuildRegion(360000, "江西省", "省"),
        BuildRegion(370000, "山东省", "省"),
        BuildRegion(410000, "河南省", "省"),
        BuildRegion(420000, "湖北省", "省"),
        BuildRegion(430000, "湖南省", "省"),
        BuildRegion(440000, "广东省", "省"),
        BuildRegion(450000, "广西壮族自治区", "自治区"),
        BuildRegion(460000, "海南省", "省"),
        BuildRegion(500000, "重庆市", "直辖市"),
        BuildRegion(510000, "四川省", "省"),
        BuildRegion(520000, "贵州省", "省"),
        BuildRegion(530000, "云南省", "省"),
        BuildRegion(540000, "西藏自治区", "自治区"),
        BuildRegion(610000, "陕西省", "省"),
        BuildRegion(620000, "甘肃省", "省"),
        BuildRegion(630000, "青海省", "省"),
        BuildRegion(640000, "宁夏回族自治区", "自治区"),
        BuildRegion(650000, "新疆维吾尔自治区", "自治区"),
        BuildRegion(710000, "台湾省", "省"),
        BuildRegion(810000, "香港特别行政区", "特别行政区"),
        BuildRegion(820000, "澳门特别行政区", "特别行政区"),
        BuildChildRegion(110100, "市辖区", "市辖区", 110000, "北京市"),
        BuildChildRegion(120100, "市辖区", "市辖区", 120000, "天津市"),
        BuildChildRegion(130100, "石家庄市", "市", 130000, "河北省"),
        BuildChildRegion(140100, "太原市", "市", 2, "山西省"),
        BuildChildRegion(150100, "呼和浩特市", "市", 150000, "内蒙古自治区"),
        BuildChildRegion(210100, "沈阳市", "市", 210000, "辽宁省"),
        BuildChildRegion(220100, "长春市", "市", 220000, "吉林省"),
        BuildChildRegion(230100, "哈尔滨市", "市", 230000, "黑龙江省"),
        BuildChildRegion(310100, "市辖区", "市辖区", 310000, "上海市"),
        BuildChildRegion(320100, "南京市", "市", 320000, "江苏省"),
        BuildChildRegion(330100, "杭州市", "市", 330000, "浙江省"),
        BuildChildRegion(340100, "合肥市", "市", 340000, "安徽省"),
        BuildChildRegion(350100, "福州市", "市", 350000, "福建省"),
        BuildChildRegion(360100, "南昌市", "市", 360000, "江西省"),
        BuildChildRegion(370100, "济南市", "市", 370000, "山东省"),
        BuildChildRegion(410100, "郑州市", "市", 410000, "河南省"),
        BuildChildRegion(420100, "武汉市", "市", 420000, "湖北省"),
        BuildChildRegion(430100, "长沙市", "市", 430000, "湖南省"),
        BuildChildRegion(440100, "广州市", "市", 440000, "广东省"),
        BuildChildRegion(450100, "南宁市", "市", 450000, "广西壮族自治区"),
        BuildChildRegion(460100, "海口市", "市", 460000, "海南省"),
        BuildChildRegion(500100, "市辖区", "市辖区", 500000, "重庆市"),
        BuildChildRegion(510100, "成都市", "市", 510000, "四川省"),
        BuildChildRegion(520100, "贵阳市", "市", 520000, "贵州省"),
        BuildChildRegion(530100, "昆明市", "市", 530000, "云南省"),
        BuildChildRegion(540100, "拉萨市", "市", 540000, "西藏自治区"),
        BuildChildRegion(610100, "西安市", "市", 610000, "陕西省"),
        BuildChildRegion(620100, "兰州市", "市", 620000, "甘肃省"),
        BuildChildRegion(630100, "西宁市", "市", 630000, "青海省"),
        BuildChildRegion(640100, "银川市", "市", 640000, "宁夏回族自治区"),
        BuildChildRegion(650100, "乌鲁木齐市", "市", 650000, "新疆维吾尔自治区"),
        BuildChildRegion(710100, "台北市", "市", 710000, "台湾省")
    ];

    private static RegionEntity BuildRegion(int id, string name, string type) =>
        new()
        {
            Id = id,
            Name = name,
            Type = type,
            ParentId = 1,
            FullPath = $"中国 / {name}",
            Summary = $"作为全国{type}级行政区入口，用于承接该地区后续的地市、区县、乡镇和村落民俗资料。",
            CulturalFocus = "当前先作为全国行政区划目录入口，后续可继续向下补充更细颗粒度的地方民俗数据。",
            HighlightsJson = JsonListSerializer.Serialize("省级目录入口", "支持按地区检索", "后续可扩展到地市区县乡村")
        };

    private static RegionEntity BuildChildRegion(int id, string name, string type, int parentId, string parentName) =>
        new()
        {
            Id = id,
            Name = name,
            Type = type,
            ParentId = parentId,
            FullPath = $"中国 / {parentName} / {name}",
            Summary = $"作为 {parentName} 下的代表性{type}节点，用于继续补充区县、乡镇、村落层面的民俗目录。",
            CulturalFocus = "当前先补到更实用的地市入口层，便于全国范围按地区向下继续补录民俗细则。",
            HighlightsJson = JsonListSerializer.Serialize("代表性地市入口", "便于继续下钻", "可承接区县乡村民俗")
        };
}
