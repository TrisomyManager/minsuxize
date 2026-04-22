# Public Media Asset Guidelines

This note defines where public media files should live before they are linked
from public seed data or reviewed content.

## Directory

Use this structure for entry media:

```text
src/MinsuXize.Web/wwwroot/media/entries/{entryId}/images/
src/MinsuXize.Web/wwwroot/media/entries/{entryId}/videos/
src/MinsuXize.Web/wwwroot/media/entries/{entryId}/audios/
```

Examples:

```text
media/entries/1/images/ancestor-table-01.jpg
media/entries/1/videos/ancestor-ritual-fragment.mp4
media/entries/1/audios/oral-prayer-fragment.mp3
```

Seed data and reviewed content should store paths relative to `wwwroot`, not
absolute filesystem paths.

## Public Rules

- Only publish media that has permission to be shown publicly.
- Prefer short ASCII filenames that describe the subject.
- Do not link local media from `images/`, `videos/`, or `audios/` at the web
  root; use `media/entries/{entryId}/...`.
- If a media file is not ready, leave the field empty instead of adding a
  placeholder path.
- Run the public site check before deployment:

```powershell
python scripts/check_public_site.py
```

When a local preview server is running, also run:

```powershell
python scripts/check_public_site.py --base-url http://localhost:5088
```
