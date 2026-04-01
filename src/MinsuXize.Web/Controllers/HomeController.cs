using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using MinsuXize.Web.Models;
using MinsuXize.Web.Services;
using MinsuXize.Web.ViewModels;

namespace MinsuXize.Web.Controllers;

public class HomeController : Controller
{
    private readonly IFolkloreRepository _repository;

    public HomeController(IFolkloreRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var allRegions = _repository.GetRegions();
        var allFestivals = _repository.GetFestivals();
        var allEntries = _repository.GetEntries()
            .OrderByDescending(item => item.UpdatedAt)
            .ThenBy(item => item.Id)
            .ToList();

        var featuredLocations = allEntries
            .Select(item => allRegions.FirstOrDefault(region => region.Id == item.RegionId))
            .Where(item => item is not null)
            .DistinctBy(item => item!.Id)
            .Take(4)
            .Cast<Region>()
            .ToList();

        ViewData["Title"] = "首页";
        ViewData["MetaDescription"] = "面向公众开放的地方习俗资料页，欢迎浏览现有记录，并提交补充或纠错。";

        return View(new HomePageViewModel
        {
            LocationCount = allRegions.Count(item => item.Id != 1),
            FestivalCount = allFestivals.Count,
            EntryCount = allEntries.Count,
            FeedbackCount = _repository.GetSubmissions().Count,
            FeaturedLocations = featuredLocations,
            FeaturedFestivals = allFestivals.Take(4).ToList(),
            FeaturedEntries = allEntries.Take(3).ToList()
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
        var exactEntry = entries.FirstOrDefault(item => item.Title.Equals(keyword, StringComparison.OrdinalIgnoreCase));

        if (exactEntry is not null)
        {
            return RedirectToAction("Details", "Entries", new { id = exactEntry.Id });
        }

        var regionMatch = FindBestRegionMatch(regions, keyword);
        var festivalMatch = FindBestFestivalMatch(festivals, keyword);
        var regionExplicit = regionMatch is not null && MatchesExplicitly(keyword, regionMatch.Name, regionMatch.FullPath);
        var festivalExplicit = festivalMatch is not null && MatchesExplicitly(keyword, festivalMatch.Name, festivalMatch.LunarLabel);

        if (regionMatch is not null && festivalMatch is not null && regionExplicit && festivalExplicit)
        {
            var regionTreeIds = _repository.GetRegionTreeIds(regionMatch.Id).ToHashSet();
            var scopedEntries = entries
                .Where(item => regionTreeIds.Contains(item.RegionId) && item.FestivalId == festivalMatch.Id)
                .ToList();

            if (scopedEntries.Count == 1)
            {
                return RedirectToAction("Details", "Entries", new { id = scopedEntries[0].Id });
            }

            return RedirectToAction("Index", "Entries", new
            {
                keyword,
                regionId = regionMatch.Id,
                festivalId = festivalMatch.Id
            });
        }

        var entryMatch = FindBestEntryMatch(entries, keyword);
        if (entryMatch is not null)
        {
            return RedirectToAction("Details", "Entries", new { id = entryMatch.Id });
        }

        if (regionMatch is not null && regionExplicit)
        {
            return RedirectToAction("Details", "Regions", new { id = regionMatch.Id });
        }

        if (festivalMatch is not null && festivalExplicit)
        {
            return RedirectToAction("Details", "Festivals", new { id = festivalMatch.Id });
        }

        return RedirectToAction("Index", "Entries", new { keyword });
    }

    [HttpGet("about")]
    public IActionResult About()
    {
        ViewData["Title"] = "关于项目";
        ViewData["MetaDescription"] = "了解这个地方习俗资料页当前提供什么内容，以及如何浏览和参与补充。";
        return View();
    }

    [HttpGet("submission-guidelines")]
    public IActionResult SubmissionGuidelines()
    {
        ViewData["Title"] = "提交说明";
        ViewData["MetaDescription"] = "查看提交补充或纠错时建议准备的信息、来源说明和基础要求。";
        return View();
    }

    [HttpGet("privacy")]
    public IActionResult Privacy()
    {
        ViewData["Title"] = "隐私说明";
        ViewData["MetaDescription"] = "查看当前站点对联系方式、来源说明和位置文本等信息的处理方式。";
        return View();
    }

    [HttpGet("terms")]
    public IActionResult Terms()
    {
        ViewData["Title"] = "使用条款";
        ViewData["MetaDescription"] = "查看当前站点的使用边界、公开内容说明和保留权利。";
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
            new(AbsoluteUrl(nameof(Privacy), "Home"), today, "yearly", 0.3m),
            new(AbsoluteUrl(nameof(Terms), "Home"), today, "yearly", 0.3m),
            new(AbsoluteUrl("Index", "Entries"), today, "daily", 0.9m),
            new(AbsoluteUrl("Index", "Regions"), today, "weekly", 0.6m),
            new(AbsoluteUrl("Index", "Festivals"), today, "weekly", 0.6m),
            new(AbsoluteUrl("Create", "Submit"), today, "weekly", 0.8m)
        };

        urls.AddRange(_repository.GetEntries().Select(item =>
            new SitemapEntry(
                AbsoluteUrl("Details", "Entries", new { id = item.Id }),
                item.UpdatedAt == default ? today : item.UpdatedAt.ToUniversalTime().Date,
                "monthly",
                0.8m)));

        urls.AddRange(_repository.GetRegions()
            .Where(item => item.Id != 1)
            .Select(item => new SitemapEntry(
                AbsoluteUrl("Details", "Regions", new { id = item.Id }),
                today,
                "weekly",
                item.ParentId == 1 ? 0.6m : 0.5m)));

        urls.AddRange(_repository.GetFestivals().Select(item =>
            new SitemapEntry(
                AbsoluteUrl("Details", "Festivals", new { id = item.Id }),
                today,
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

        return Content(
            $"{document.Declaration}{Environment.NewLine}{document}",
            "application/xml",
            Encoding.UTF8);
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

        var model = code switch
        {
            404 => new StatusPageViewModel
            {
                StatusCode = 404,
                Eyebrow = "未找到",
                Heading = "你访问的页面不存在，或位置已经调整",
                Description = "可以返回首页、继续浏览习俗列表，或通过搜索重新进入公开内容。",
                Guidance = "如果这是站内旧链接，后续可以继续补做跳转或整理入口。"
            },
            403 => new StatusPageViewModel
            {
                StatusCode = 403,
                Eyebrow = "暂不开放",
                Heading = "这个页面当前不对公众开放",
                Description = "后台页面仅用于处理反馈和整理资料，不作为公开浏览入口。",
                Guidance = "你可以继续浏览公开页面，或提交新的补充和纠错。"
            },
            _ => new StatusPageViewModel
            {
                StatusCode = code,
                Eyebrow = "处理中断",
                Heading = "当前请求暂时无法完成",
                Description = "这通常是临时错误或相关配置尚未完成。你可以稍后重试，或先返回公开页面继续浏览。",
                Guidance = "如果问题持续存在，建议记录访问路径和时间，方便后续排查。"
            }
        };

        return View(model);
    }

    private Region? FindBestRegionMatch(IReadOnlyList<Region> regions, string query) =>
        regions
            .Select(item => new { Item = item, Score = ScoreQuery(query, item.Name, item.FullPath, item.Summary) })
            .Where(result => result.Score > 0)
            .OrderByDescending(result => result.Score)
            .ThenBy(result => result.Item.FullPath.Length)
            .Select(result => result.Item)
            .FirstOrDefault();

    private Festival? FindBestFestivalMatch(IReadOnlyList<Festival> festivals, string query) =>
        festivals
            .Select(item => new { Item = item, Score = ScoreQuery(query, item.Name, item.LunarLabel, item.Category, item.Summary) })
            .Where(result => result.Score > 0)
            .OrderByDescending(result => result.Score)
            .ThenBy(result => result.Item.Name.Length)
            .Select(result => result.Item)
            .FirstOrDefault();

    private static FolkloreEntry? FindBestEntryMatch(IReadOnlyList<FolkloreEntry> entries, string query) =>
        entries
            .Select(item =>
            {
                var score = ScoreQuery(query, item.Title, item.Summary);
                return new { Item = item, Score = score };
            })
            .Where(result => result.Score >= 420)
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
        var tokens = normalized
            .Split(new[] { ' ', '　', '/', '-', '_', ',', '，' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

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
                continue;
            }

            if (value.StartsWith(normalized, StringComparison.OrdinalIgnoreCase))
            {
                best = Math.Max(best, 420);
            }

            if (value.Contains(normalized, StringComparison.OrdinalIgnoreCase))
            {
                best = Math.Max(best, 340);
            }

            if (normalized.Contains(value, StringComparison.OrdinalIgnoreCase))
            {
                best = Math.Max(best, 360);
            }

            if (tokens.Length > 1 && tokens.All(token => value.Contains(token, StringComparison.OrdinalIgnoreCase)))
            {
                best = Math.Max(best, 300);
            }

            if (tokens.Any(token => value.Equals(token, StringComparison.OrdinalIgnoreCase)))
            {
                best = Math.Max(best, 360);
            }

            if (tokens.Any(token => value.Contains(token, StringComparison.OrdinalIgnoreCase)))
            {
                best = Math.Max(best, 250);
            }
        }

        return best;
    }

    private static bool MatchesExplicitly(string query, params string?[] candidates)
    {
        var normalized = query.Trim();

        foreach (var candidate in candidates)
        {
            if (string.IsNullOrWhiteSpace(candidate))
            {
                continue;
            }

            var value = candidate.Trim();
            if (value.Equals(normalized, StringComparison.OrdinalIgnoreCase) ||
                value.Contains(normalized, StringComparison.OrdinalIgnoreCase) ||
                normalized.Contains(value, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    private string AbsoluteUrl(string action, string controller, object? values = null) =>
        Url.Action(action, controller, values, Request.Scheme)
        ?? $"{Request.Scheme}://{Request.Host}/{controller}/{action}";

    private sealed record SitemapEntry(string Url, DateTime LastModifiedUtc, string ChangeFrequency, decimal Priority);
}
