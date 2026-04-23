using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MinsuXize.Web.Models;
using MinsuXize.Web.Options;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

public class HomeController : Controller
{
    private readonly IFolkloreRepository _repository;
    private readonly SiteOptions _siteOptions;

    public HomeController(IFolkloreRepository repository, IOptions<SiteOptions> siteOptions)
    {
        _repository = repository;
        _siteOptions = siteOptions.Value;
    }

    public IActionResult Index()
    {
        var regions = _repository.GetRegions();
        var festivals = _repository.GetFestivals();
        var entries = _repository.GetEntries();
        var rituals = entries.Where(item => item.ContentType == "ritual").ToList();

        var featuredLocations = entries
            .Select(item => regions.FirstOrDefault(region => region.Id == item.RegionId))
            .Where(item => item is not null)
            .DistinctBy(item => item!.Id)
            .Take(6)
            .Cast<Region>()
            .ToList();

        ViewData["Title"] = "民俗知识入口";
        ViewData["MetaDescription"] = "中国民俗细则是面向用户、搜索引擎与 AI 协作维护的结构化民俗知识库，可按地区、节日、仪式和关键词检索。";
        ViewData["CanonicalUrl"] = AbsoluteUrl("Index", "Home");

        return View(new HomePageViewModel
        {
            LocationCount = regions.Count(item => item.Id != 1),
            FestivalCount = festivals.Count,
            RitualCount = rituals.Count,
            EntryCount = entries.Count,
            FeedbackCount = _repository.GetSubmissions().Count,
            FeaturedLocations = featuredLocations,
            FeaturedFestivals = festivals.Take(6).ToList(),
            LatestEntries = entries.OrderByDescending(item => item.UpdatedAt).Take(6).ToList(),
            PopularEntries = entries.OrderByDescending(item => item.SourceIds.Count).ThenByDescending(item => item.UpdatedAt).Take(4).ToList(),
            PopularTerms = entries.SelectMany(item => item.Tags).Distinct(StringComparer.OrdinalIgnoreCase).Take(12).ToList()
        });
    }

    [HttpGet("search")]
    public IActionResult Search(string? query)
    {
        var keyword = query?.Trim();
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return RedirectToAction(nameof(Index));
        }

        var regions = _repository.GetRegions().Where(item => item.Id != 1).ToList();
        var festivals = _repository.GetFestivals().ToList();
        var entries = _repository.GetEntries().ToList();
        var exactEntry = entries.FirstOrDefault(item => item.Title.Equals(keyword, StringComparison.OrdinalIgnoreCase) || item.Slug.Equals(keyword, StringComparison.OrdinalIgnoreCase));

        if (exactEntry is not null)
        {
            return RedirectToAction("Details", "Entries", new { slug = exactEntry.Slug });
        }

        var regionMatch = FindBestRegionMatch(regions, keyword);
        if (regionMatch is not null && MatchesExplicitly(keyword, regionMatch.Name, regionMatch.FullPath, regionMatch.Slug))
        {
            return RedirectToAction("Details", "Places", new { slug = regionMatch.Slug });
        }

        var festivalMatch = FindBestFestivalMatch(festivals, keyword);
        if (festivalMatch is not null && MatchesExplicitly(keyword, festivalMatch.Name, festivalMatch.LunarLabel, festivalMatch.Slug))
        {
            return RedirectToAction("Details", "Festivals", new { slug = festivalMatch.Slug });
        }

        var entryMatch = FindBestEntryMatch(entries, keyword);
        if (entryMatch is not null)
        {
            return RedirectToAction("Details", "Entries", new { slug = entryMatch.Slug });
        }

        return RedirectToAction("Index", "Entries", new { keyword });
    }

    [HttpGet("about")]
    public IActionResult About()
    {
        ViewData["Title"] = "关于项目";
        ViewData["MetaDescription"] = "了解中国民俗细则如何整理地区、节日、仪式、来源与待核实信息，以及如何参与补充。";
        ViewData["CanonicalUrl"] = AbsoluteUrl(nameof(About), "Home");
        return View();
    }

    [HttpGet("submission-guidelines")]
    public IActionResult SubmissionGuidelines()
    {
        ViewData["Title"] = "提交说明";
        ViewData["MetaDescription"] = "查看提交民俗资料、补充来源或纠错时建议准备的信息。";
        ViewData["CanonicalUrl"] = AbsoluteUrl(nameof(SubmissionGuidelines), "Home");
        return View();
    }

    [HttpGet("privacy")]
    public IActionResult Privacy()
    {
        ViewData["Title"] = "隐私说明";
        ViewData["MetaDescription"] = "查看站点对联系方式、来源说明和位置文本等信息的处理方式。";
        ViewData["CanonicalUrl"] = AbsoluteUrl(nameof(Privacy), "Home");
        return View();
    }

    [HttpGet("terms")]
    public IActionResult Terms()
    {
        ViewData["Title"] = "使用条款";
        ViewData["MetaDescription"] = "查看站点的使用边界、公开内容说明和保留权利。";
        ViewData["CanonicalUrl"] = AbsoluteUrl(nameof(Terms), "Home");
        return View();
    }

    [HttpGet("robots.txt")]
    [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any)]
    public IActionResult Robots()
    {
        var lines = new[]
        {
            "User-agent: *",
            "Allow: /",
            "Disallow: /manage",
            $"Sitemap: {AbsoluteUrl(nameof(Sitemap), "Home")}"
        };

        return Content(string.Join('\n', lines), "text/plain", Encoding.UTF8);
    }

    [HttpGet("sitemap.xml")]
    [ResponseCache(Duration = 1800, Location = ResponseCacheLocation.Any)]
    public IActionResult Sitemap()
    {
        var today = DateTime.UtcNow.Date;
        var urls = new List<SitemapEntry>
        {
            new(AbsoluteUrl(nameof(Index), "Home"), today, "daily", 1.0m),
            new(AbsoluteUrl(nameof(About), "Home"), today, "monthly", 0.7m),
            new(AbsoluteUrl(nameof(SubmissionGuidelines), "Home"), today, "monthly", 0.7m),
            new(AbsoluteUrl("Index", "Entries"), today, "daily", 0.9m),
            new(AbsoluteUrl("Index", "Places"), today, "weekly", 0.7m),
            new(AbsoluteUrl("Index", "Festivals"), today, "weekly", 0.7m),
            new(AbsoluteUrl("Index", "Rituals"), today, "weekly", 0.7m),
            new(AbsoluteUrl("Create", "Submit"), today, "weekly", 0.8m)
        };

        urls.AddRange(_repository.GetEntries().Select(item =>
            new SitemapEntry(
                AbsoluteUrl("Details", "Entries", new { slug = item.Slug }),
                item.UpdatedAt == default ? today : item.UpdatedAt.ToUniversalTime().Date,
                "monthly",
                0.8m)));

        urls.AddRange(_repository.GetRegions()
            .Where(item => item.Id != 1)
            .Select(item => new SitemapEntry(
                AbsoluteUrl("Details", "Places", new { slug = item.Slug }),
                item.UpdatedAt == default ? today : item.UpdatedAt.ToUniversalTime().Date,
                "weekly",
                item.ParentId == 1 ? 0.6m : 0.5m)));

        urls.AddRange(_repository.GetFestivals().Select(item =>
            new SitemapEntry(
                AbsoluteUrl("Details", "Festivals", new { slug = item.Slug }),
                item.UpdatedAt == default ? today : item.UpdatedAt.ToUniversalTime().Date,
                "weekly",
                0.5m)));

        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var document = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            new XElement(
                ns + "urlset",
                urls
                    .GroupBy(item => item.Url)
                    .Select(group => group.OrderByDescending(item => item.LastModifiedUtc).First())
                    .Select(item => new XElement(
                        ns + "url",
                        new XElement(ns + "loc", item.Url),
                        new XElement(ns + "lastmod", item.LastModifiedUtc.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                        new XElement(ns + "changefreq", item.ChangeFrequency),
                        new XElement(ns + "priority", item.Priority.ToString("0.0", CultureInfo.InvariantCulture))))));

        return Content($"{document.Declaration}{Environment.NewLine}{document}", "application/xml", Encoding.UTF8);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        ViewData["Title"] = "系统异常";
        ViewData["MetaRobots"] = "noindex, nofollow, noarchive";
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Status(int code)
    {
        ViewData["Title"] = code == 404 ? "页面未找到" : "访问异常";
        ViewData["MetaRobots"] = "noindex, nofollow, noarchive";
        Response.StatusCode = code;

        return View(new StatusPageViewModel
        {
            StatusCode = code,
            Eyebrow = code == 404 ? "未找到" : "请求中断",
            Heading = code == 404 ? "你访问的页面不存在，或位置已经调整" : "当前请求暂时无法完成",
            Description = code == 404 ? "可以返回首页、继续浏览知识条目，或通过搜索重新进入公开内容。" : "这通常是临时错误或相关配置尚未完成。",
            Guidance = "如果这是站内旧链接，后续可以继续补做跳转或整理入口。"
        });
    }

    private Region? FindBestRegionMatch(IReadOnlyList<Region> regions, string query) =>
        regions
            .Select(item => new { Item = item, Score = ScoreQuery(query, item.Name, item.FullPath, item.Slug, item.Summary) })
            .Where(result => result.Score > 0)
            .OrderByDescending(result => result.Score)
            .ThenBy(result => result.Item.FullPath.Length)
            .Select(result => result.Item)
            .FirstOrDefault();

    private Festival? FindBestFestivalMatch(IReadOnlyList<Festival> festivals, string query) =>
        festivals
            .Select(item => new { Item = item, Score = ScoreQuery(query, item.Name, item.LunarLabel, item.Slug, item.Category, item.Summary) })
            .Where(result => result.Score > 0)
            .OrderByDescending(result => result.Score)
            .ThenBy(result => result.Item.Name.Length)
            .Select(result => result.Item)
            .FirstOrDefault();

    private static FolkloreEntry? FindBestEntryMatch(IReadOnlyList<FolkloreEntry> entries, string query) =>
        entries
            .Select(item => new { Item = item, Score = ScoreQuery(query, item.Title, item.Slug, item.Summary, item.ContentType) })
            .Where(result => result.Score >= 300)
            .OrderByDescending(result => result.Score)
            .ThenBy(result => result.Item.Title.Length)
            .Select(result => result.Item)
            .FirstOrDefault();

    private static int ScoreQuery(string query, params string?[] candidates)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return 0;
        }

        var normalized = query.Trim();
        var tokens = normalized.Split([' ', '　', '/', '-', '_', ',', '，'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var best = 0;

        foreach (var candidate in candidates)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                continue;
            }

            var value = candidate.Trim();
            if (value.Equals(normalized, StringComparison.OrdinalIgnoreCase))
            {
                best = Math.Max(best, 520);
            }
            else if (value.Contains(normalized, StringComparison.OrdinalIgnoreCase) || normalized.Contains(value, StringComparison.OrdinalIgnoreCase))
            {
                best = Math.Max(best, 360);
            }
            else if (tokens.Length > 1 && tokens.All(token => value.Contains(token, StringComparison.OrdinalIgnoreCase)))
            {
                best = Math.Max(best, 300);
            }
            else if (tokens.Any(token => value.Contains(token, StringComparison.OrdinalIgnoreCase)))
            {
                best = Math.Max(best, 220);
            }
        }

        return best;
    }

    private static bool MatchesExplicitly(string query, params string?[] candidates)
    {
        var normalized = query.Trim();
        return candidates
            .Where(candidate => !string.IsNullOrWhiteSpace(candidate))
            .Any(candidate =>
                candidate!.Equals(normalized, StringComparison.OrdinalIgnoreCase) ||
                candidate.Contains(normalized, StringComparison.OrdinalIgnoreCase) ||
                normalized.Contains(candidate, StringComparison.OrdinalIgnoreCase));
    }

    private string AbsoluteUrl(string action, string controller, object? values = null)
    {
        var path = Url.Action(action, controller, values) ?? "/";
        return new Uri(new Uri(NormalizedBaseUrl()), path).ToString();
    }

    private string NormalizedBaseUrl() => _siteOptions.BaseUrl.TrimEnd('/') + "/";

    private sealed record SitemapEntry(string Url, DateTime LastModifiedUtc, string ChangeFrequency, decimal Priority);
}
