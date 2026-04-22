#!/usr/bin/env python3
"""
Pre-publish checks for the public MinsuXize site.

The script is intentionally lightweight: it scans public Razor views and seed
content for prototype wording, checks local seed media paths, and optionally
verifies important public URLs against a running site.
"""

from __future__ import annotations

import argparse
import re
import sys
import urllib.error
import urllib.request
from pathlib import Path
from typing import Iterable


ROOT = Path(__file__).resolve().parents[1]
WEB_ROOT = ROOT / "src" / "MinsuXize.Web"
WWWROOT = WEB_ROOT / "wwwroot"
LOCAL_MEDIA_PREFIX = "media/entries/"

PUBLIC_TEXT_GLOBS = [
    "src/MinsuXize.Web/Views/**/*.cshtml",
    "src/MinsuXize.Web/Data/Seed/DbSeeder.cs",
]

BLOCKED_PUBLIC_TERMS = [
    "Region Directory",
    "Festival Directory",
    "Regional Focus",
    "Subregions",
    "Entries In Region",
    "当前阶段暂不提供",
    "复杂地图展示",
    "复杂关系图展示",
    "大型内容管理后台",
    "原型示例",
    "项目示例资料",
    "待补录",
    "示例投稿人",
    "示例审核流程",
]

PUBLIC_ROUTES = [
    "/",
    "/entries",
    "/entries/1",
    "/regions",
    "/regions/details/6",
    "/festivals",
    "/festivals/details/1",
    "/feedback",
    "/about",
]

MEDIA_ASSIGNMENT_RE = re.compile(
    r"(?P<field>ImagesJson|VideosJson|AudiosJson)\s*=\s*JsonListSerializer\.Serialize\((?P<args>.*?)\)",
    re.DOTALL,
)
CSHARP_STRING_RE = re.compile(r'"((?:\\.|[^"\\])*)"')


def iter_public_files() -> Iterable[Path]:
    for pattern in PUBLIC_TEXT_GLOBS:
        yield from ROOT.glob(pattern)


def scan_blocked_terms() -> list[str]:
    findings: list[str] = []
    for path in iter_public_files():
        text = path.read_text(encoding="utf-8")
        for line_number, line in enumerate(text.splitlines(), 1):
            for term in BLOCKED_PUBLIC_TERMS:
                if term in line:
                    rel = path.relative_to(ROOT)
                    findings.append(f"{rel}:{line_number}: blocked public term: {term}")
    return findings


def unescape_csharp_string(value: str) -> str:
    return (
        value.replace(r"\/", "/")
        .replace(r"\\", "\\")
        .replace(r"\"", '"')
    )


def is_remote_url(value: str) -> bool:
    return value.startswith("http://") or value.startswith("https://")


def scan_seed_media_paths() -> list[str]:
    seed_path = WEB_ROOT / "Data" / "Seed" / "DbSeeder.cs"
    text = seed_path.read_text(encoding="utf-8")
    findings: list[str] = []

    for match in MEDIA_ASSIGNMENT_RE.finditer(text):
        args = match.group("args")
        field = match.group("field")
        line_number = text[: match.start()].count("\n") + 1

        for raw_value in CSHARP_STRING_RE.findall(args):
            media_path = unescape_csharp_string(raw_value).strip()
            if not media_path or is_remote_url(media_path):
                continue

            relative = media_path.lstrip("~/\\/")
            normalized_relative = relative.replace("\\", "/")
            if not normalized_relative.startswith(LOCAL_MEDIA_PREFIX):
                rel = seed_path.relative_to(ROOT)
                findings.append(
                    f"{rel}:{line_number}: {field} local media should live under "
                    f"{LOCAL_MEDIA_PREFIX}: {media_path}"
                )

            absolute = WWWROOT / Path(relative)
            if not absolute.exists():
                rel = seed_path.relative_to(ROOT)
                findings.append(
                    f"{rel}:{line_number}: {field} references missing media: {media_path}"
                )

    return findings


def check_public_routes(base_url: str) -> list[str]:
    findings: list[str] = []
    normalized_base = base_url.rstrip("/")

    for route in PUBLIC_ROUTES:
        url = normalized_base + route
        try:
            with urllib.request.urlopen(url, timeout=10) as response:
                status = response.status
        except urllib.error.HTTPError as exc:
            status = exc.code
        except Exception as exc:  # noqa: BLE001 - report actionable preflight failure.
            findings.append(f"{url}: request failed: {exc}")
            continue

        if status >= 400:
            findings.append(f"{url}: unexpected HTTP {status}")

    return findings


def main() -> int:
    parser = argparse.ArgumentParser(description="Check public site readiness.")
    parser.add_argument(
        "--base-url",
        help="Optional running site URL, for example http://localhost:5088.",
    )
    args = parser.parse_args()

    findings = []
    findings.extend(scan_blocked_terms())
    findings.extend(scan_seed_media_paths())

    if args.base_url:
        findings.extend(check_public_routes(args.base_url))

    if findings:
        print("Public site check failed:")
        for finding in findings:
            print(f"- {finding}")
        return 1

    print("Public site check passed.")
    return 0


if __name__ == "__main__":
    sys.exit(main())
