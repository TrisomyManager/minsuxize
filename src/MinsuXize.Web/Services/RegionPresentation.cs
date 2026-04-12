using Microsoft.AspNetCore.Mvc.Rendering;
using MinsuXize.Web.Models;

namespace MinsuXize.Web.Services;

public static class RegionPresentation
{
    public static IReadOnlyList<SelectListItem> BuildRegionOptions(
        IReadOnlyList<Region> regions,
        int? selectedId = null,
        params string[] excludedTypes)
    {
        var excludedTypeSet = excludedTypes.Length == 0
            ? new HashSet<string>(StringComparer.Ordinal)
            : excludedTypes.ToHashSet(StringComparer.Ordinal);

        var depthMap = BuildDepthMap(regions);

        return OrderForDisplay(regions)
            .Where(item => !excludedTypeSet.Contains(item.Type))
            .Select(item => new SelectListItem(
                $"{BuildPrefix(depthMap.GetValueOrDefault(item.Id))}{item.Name}",
                item.Id.ToString(),
                item.Id == selectedId))
            .ToList();
    }

    public static IReadOnlyList<Region> OrderForDisplay(IReadOnlyList<Region> regions)
    {
        var regionById = regions.ToDictionary(item => item.Id);
        var childrenByParent = regions.ToLookup(item => item.ParentId);

        var ordered = new List<Region>(regions.Count);
        var visited = new HashSet<int>();

        foreach (var root in regions
                     .Where(item => item.ParentId is null || !regionById.ContainsKey(item.ParentId.Value))
                     .OrderBy(item => item.Id))
        {
            AppendRegion(root);
        }

        foreach (var region in regions.OrderBy(item => item.Id))
        {
            AppendRegion(region);
        }

        return ordered;

        void AppendRegion(Region region)
        {
            if (!visited.Add(region.Id))
            {
                return;
            }

            ordered.Add(region);

            var children = childrenByParent[region.Id].OrderBy(item => item.Id).ToList();
            if (children.Count == 0)
            {
                return;
            }

            foreach (var child in children)
            {
                AppendRegion(child);
            }
        }
    }

    public static IReadOnlyList<int> GetRegionTreeIds(IReadOnlyList<Region> regions, int rootId)
    {
        var childrenByParent = regions.ToLookup(item => item.ParentId);

        var collected = new List<int>();
        var pending = new Queue<int>();
        var visited = new HashSet<int>();

        pending.Enqueue(rootId);

        while (pending.Count > 0)
        {
            var currentId = pending.Dequeue();
            if (!visited.Add(currentId))
            {
                continue;
            }

            collected.Add(currentId);

            var children = childrenByParent[currentId];
            if (!children.Any())
            {
                continue;
            }

            foreach (var childId in children.Select(item => item.Id))
            {
                pending.Enqueue(childId);
            }
        }

        return collected;
    }

    private static IReadOnlyDictionary<int, int> BuildDepthMap(IReadOnlyList<Region> regions)
    {
        var regionById = regions.ToDictionary(item => item.Id);
        var depthMap = new Dictionary<int, int>(regions.Count);

        foreach (var region in regions)
        {
            depthMap[region.Id] = CalculateDepth(region, regionById, depthMap);
        }

        return depthMap;
    }

    private static int CalculateDepth(
        Region region,
        IReadOnlyDictionary<int, Region> regionById,
        IDictionary<int, int> depthMap)
    {
        if (depthMap.TryGetValue(region.Id, out var knownDepth))
        {
            return knownDepth;
        }

        if (region.ParentId is null || !regionById.TryGetValue(region.ParentId.Value, out var parentRegion))
        {
            return 0;
        }

        var depth = CalculateDepth(parentRegion, regionById, depthMap) + 1;
        depthMap[region.Id] = depth;
        return depth;
    }

    private static string BuildPrefix(int depth) =>
        depth <= 1
            ? string.Empty
            : $"{string.Concat(Enumerable.Repeat("--", depth - 1))} ";
}
