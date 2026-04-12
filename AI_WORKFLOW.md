# AI Project Structure Guide - MinsuXize

## 1. Purpose

This repository is an ASP.NET Core MVC prototype plus a knowledge-documentation workspace for recording, reviewing, and publishing Chinese local folklore practices.

The project is not a generic holiday encyclopedia. Its core goal is to preserve fine-grained, place-based ritual knowledge:

- what a community does
- when it does it
- who participates
- what objects, foods, offerings, or taboos are involved
- what oral explanations or source evidence support the record

The current codebase already supports:

- public browsing by region, festival, and keyword
- structured folklore entry display
- public submission of new folklore records
- admin login
- admin review workflow with review history and bulk review

## 2. Current State

- App type: ASP.NET Core MVC with Razor Views
- Target framework: `.NET 10`
- Database: local SQLite
- ORM: Entity Framework Core
- Main project: `src/MinsuXize.Web`
- Default local URL: `http://localhost:5088`
- Repository status: unified root repository with real browsing flow and admin review flow

As of `2026-03-23`, the latest visible milestone is:

- richer submission model with media links, location, version metadata
- admin review history
- bulk review actions

## 3. Read This First

If another AI or developer is taking over, read files in this order:

1. `AGENT_CONTEXT.md`
2. `src/MinsuXize.Web/Program.cs`
3. `src/MinsuXize.Web/Services/IFolkloreRepository.cs`
4. `src/MinsuXize.Web/Data/AppDbContext.cs`
5. `src/MinsuXize.Web/Controllers/*.cs`
6. `src/MinsuXize.Web/Models/*.cs`
7. `src/MinsuXize.Web/ViewModels/*.cs`
8. `src/MinsuXize.Web/Views/*`
9. `src/MinsuXize.Web/Data/Seed/DbSeeder.cs`

That sequence gives the fastest path to understanding:

- startup and dependency injection
- repository contract
- data storage model
- HTTP entry points
- domain objects
- page-specific view shaping
- rendered UI
- sample data and initialization assumptions

## 4. Repository Map

```text
repo-root/
├── AGENT_CONTEXT.md
├── 00_Project_Brief/
├── 01_Standards/
├── 02_Templates/
├── 03_Pilot_Data/
├── 04_Productization/
├── 05_Operations/
├── 06_Future/
├── 07_Prompt_Library/
├── src/
│   └── MinsuXize.Web/
├── docs/
├── data/
├── scripts/
├── README.md
├── AI_WORKFLOW.md
├── CHECKPOINT_*.md
└── LOCAL_DEVELOPMENT_*.md
```

## 5. Directory Responsibilities

### `src/MinsuXize.Web/`

The entire runnable web app lives here.

### `src/MinsuXize.Web/Program.cs`

App entry point. It is responsible for:

- creating the SQLite connection string
- registering MVC, auth, authorization, EF Core, and repository DI
- calling `EnsureCreated()`
- seeding initial data through `DbSeeder.Seed(dbContext)`

This file is the first place to inspect when startup behavior changes.

### `src/MinsuXize.Web/Controllers/`

HTTP entry layer. Each controller roughly maps to a user-facing flow:

- `HomeController`: landing page and search summary
- `EntriesController`: folklore entry search and details
- `RegionsController`: region index and region details
- `FestivalsController`: festival index and festival details
- `SubmitController`: public submission create + thank-you page
- `ManageController`: admin login/logout
- `AdminController`: admin review dashboard, single review, bulk review, history

### `src/MinsuXize.Web/Data/`

Persistence layer.

- `AppDbContext.cs`: EF Core table mapping and defaults
- `Entities/`: persistence-specific classes
- `Seed/`: initial catalog + sample data + helper serializers

### `src/MinsuXize.Web/Models/`

Domain/read models used by repository consumers and views. These are closer to business meaning than EF entities.

Core models:

- `Region`
- `Festival`
- `FolkloreEntry`
- `SourceEvidence`
- `SubmissionInput`
- `SubmissionRecord`
- `ReviewHistory`
- `SubmissionStatus`

### `src/MinsuXize.Web/Services/`

Application data access boundary.

- `IFolkloreRepository`: core app contract
- `EfFolkloreRepository`: SQLite/EF-backed implementation
- `InMemoryFolkloreRepository`: non-persistent alternative / fallback implementation
- `RegionPresentation`: region option building helper for nested UI selects

This folder is the best single place to understand what the UI needs from data.

### `src/MinsuXize.Web/ViewModels/`

Page-specific shaping for Razor views.

Use this folder when:

- a page needs composite data from multiple models
- a form needs display validation and helper conversion logic

Notable files:

- `HomePageViewModel`
- `EntrySearchViewModel`
- `EntryDetailsViewModel`
- `RegionDetailsViewModel`
- `FestivalDetailsViewModel`
- `SubmitEntryViewModel`
- `AdminDashboardViewModel`
- `ReviewHistoryViewModel`
- `BulkReviewViewModel`
- `AdminLoginViewModel`

### `src/MinsuXize.Web/Views/`

Razor UI layer.

Key folders:

- `Home/`
- `Entries/`
- `Regions/`
- `Festivals/`
- `Submit/`
- `Manage/`
- `Admin/`
- `Shared/`

Important view files:

- `Views/Home/Index.cshtml`
- `Views/Entries/Index.cshtml`
- `Views/Entries/Details.cshtml`
- `Views/Submit/Create.cshtml`
- `Views/Submit/Thanks.cshtml`
- `Views/Manage/Login.cshtml`
- `Views/Admin/Index.cshtml`
- `Views/Admin/History.cshtml`
- `Views/Admin/_BulkActions.cshtml`
- `Views/Shared/_Layout.cshtml`

### `src/MinsuXize.Web/wwwroot/`

Static assets:

- `css/site.css`
- `js/site.js`
- vendor libs under `lib/`

### `src/MinsuXize.Web/App_Data/`

Local database directory.

Current important files:

- `minsuxize.db`
- `minsuxize.pre-preview-backup-20260323.db`

This folder is runtime state, not source-of-truth design.

### `docs/`

Design/reference notes outside the runnable app.

### Root checkpoint files

These are handoff and progress notes:

- `CHECKPOINT_20260323.md`
- `CHECKPOINT_20260323_2.md`
- `CHECKPOINT_20260323_3.md`
- `LOCAL_DEVELOPMENT_PLAN.md`
- `LOCAL_DEVELOPMENT_SUMMARY.md`

Useful for historical context, but not the primary architectural source.

## 6. Runtime Architecture

The app currently follows a simple MVC + repository pattern:

```text
HTTP Request
→ Controller
→ IFolkloreRepository
→ EfFolkloreRepository
→ AppDbContext / SQLite
→ mapped domain model
→ ViewModel
→ Razor View
→ HTML response
```

Authentication is cookie-based and only protects admin flows.

```text
ManageController login
→ cookie auth
→ AdminOnly policy
→ AdminController routes
```

## 7. Main User Flows

### A. Public browse flow

```text
Home / Entries / Regions / Festivals
→ repository queries
→ domain models
→ view models
→ public Razor pages
```

Purpose:

- browse folklore by geography
- browse folklore by festival/time node
- search across entry content and metadata

### B. Public submission flow

```text
GET /Submit/Create
→ show structured form

POST /Submit/Create
→ validate form
→ create SubmissionInput
→ repository.CreateSubmission(...)
→ redirect to /Submit/Thanks/{id}
```

Submission data currently supports:

- contributor identity
- region
- festival
- title and summary
- source summary
- contact
- image/video/audio link lists
- location
- changelog note

### C. Admin authentication flow

```text
/manage/login
→ credential check against AdminAuthOptions
→ issue auth cookie
→ redirect to review dashboard
```

### D. Admin review flow

```text
/manage/review
→ list submissions
→ update one submission status
or bulk update selected submissions
→ record review history
→ optionally view /manage/review/history/{id}
```

## 8. Important Routes

### Public routes

- `/`
- `/Entries`
- `/Entries/Details/{id}`
- `/Regions`
- `/Regions/Details/{id}`
- `/Festivals`
- `/Festivals/Details/{id}`
- `/Submit/Create`
- `/Submit/Thanks/{id}`

### Admin routes

- `/manage/login`
- `/manage/logout`
- `/manage/review`
- `/manage/review/update`
- `/manage/review/bulk-update`
- `/manage/review/history/{id}`

## 9. Data Model Overview

### Region

Geographic hierarchy node.

Purpose:

- province → city → district/county → town → village traversal
- regional grouping for folklore entries

### Festival

Time anchor.

Purpose:

- represent holidays, solar terms, and ritual dates
- attach multiple entries across different places

### FolkloreEntry

Published or publishable folklore content.

Contains:

- descriptive narrative
- ritual sequence
- items used
- taboos
- participants
- source references
- metadata such as version, status, media, and location

### SourceEvidence

Evidence object linked to entries.

### SubmissionRecord

User-submitted candidate content waiting for or moving through review.

### ReviewHistory

Audit trail for admin review changes.

## 10. File-Level Change Guide

When changing a feature, start here:

### Search or entry listing behavior

- `Controllers/HomeController.cs`
- `Controllers/EntriesController.cs`
- `Services/IFolkloreRepository.cs`
- `Services/EfFolkloreRepository.cs`
- `ViewModels/EntrySearchViewModel.cs`
- `Views/Home/Index.cshtml`
- `Views/Entries/Index.cshtml`

### Region browsing

- `Controllers/RegionsController.cs`
- `ViewModels/RegionIndexViewModel.cs`
- `ViewModels/RegionDetailsViewModel.cs`
- `Views/Regions/*`

### Festival browsing

- `Controllers/FestivalsController.cs`
- `ViewModels/FestivalDetailsViewModel.cs`
- `Views/Festivals/*`

### Submission form

- `Controllers/SubmitController.cs`
- `ViewModels/SubmitEntryViewModel.cs`
- `Models/SubmissionInput.cs`
- `Models/SubmissionRecord.cs`
- `Data/Entities/SubmissionRecordEntity.cs`
- `Services/EfFolkloreRepository.cs`
- `Views/Submit/Create.cshtml`
- `Views/Submit/Thanks.cshtml`

### Admin login

- `Controllers/ManageController.cs`
- `Options/AdminAuthOptions.cs`
- `Views/Manage/Login.cshtml`
- `Program.cs`

### Admin review workflow

- `Controllers/AdminController.cs`
- `Models/ReviewHistory.cs`
- `Data/Entities/ReviewHistoryEntity.cs`
- `Services/IFolkloreRepository.cs`
- `Services/EfFolkloreRepository.cs`
- `ViewModels/AdminDashboardViewModel.cs`
- `ViewModels/ReviewHistoryViewModel.cs`
- `Views/Admin/Index.cshtml`
- `Views/Admin/History.cshtml`
- `Views/Admin/_BulkActions.cshtml`

### Database schema / persistence behavior

- `Data/AppDbContext.cs`
- `Data/Entities/*`
- `Data/Seed/DbSeeder.cs`
- `Program.cs`

## 11. Known Design Constraints

These are important for any future AI contributor.

### 1. Database creation currently uses `EnsureCreated()`

The app does not use EF Core migrations yet.

Implication:

- a brand-new database works
- an older existing database may not automatically gain new columns or tables

Practical result already observed:

- older local DBs can fail startup after schema expansion

### 2. Seed data and runtime schema are tightly coupled

`DbSeeder.Seed(...)` expects the live schema to already match the current entity definitions.

### 3. Backup and legacy files exist in source folders

Examples:

- `Data/Seed/DbSeeder.cs.backup`
- `Data/Seed/DbSeeder.cs.old`
- `Views/Admin/Index.cshtml.backup2`
- `Views/Submit/Create.cshtml.backup`
- `Data/Entities/SubmissionRecordEntity.cs.backup`

These files are historical artifacts, not active runtime files.

### 4. Generated folders should usually be ignored

Do not treat these as primary source:

- `bin/`
- `obj/`
- `.vs/`
- `output/`
- `.artifacts/`

### 5. The current repository contract is the architectural backbone

Avoid bypassing `IFolkloreRepository` from controllers unless a larger architectural refactor is intentional.

## 12. Current Design Gaps

These are not instructions to implement now. They are the main design-level gaps still visible.

### Schema evolution strategy

The project needs a migration-safe database evolution path instead of relying on deleting the DB.

### Review-to-publication boundary

The distinction between:

- a reviewed submission
- a published folklore entry

is still more implicit than explicit. A later design pass should define whether approval copies data into `FolkloreEntry`, links submission-to-entry, or supports revision chains.

### Version history is partial

The model contains version-related metadata, but there is not yet a full entry revision system.

### Media support is link-based, not upload-based

Current UI accepts URLs, not managed uploads.

### Map/location support is storage-only

Coordinates can be saved, but there is not yet a geographic visualization experience.

### User identity model is minimal

Admin login exists, but there is no real public user account system yet.

## 13. Recommended Design Direction

If future work resumes, the most coherent order is:

1. stabilize database evolution
2. formalize submission-to-entry publication rules
3. define revision/version model
4. decide upload/storage strategy for media
5. add geographic visualization
6. add richer user identity and moderation controls

## 14. AI Collaboration Notes

When another AI edits this repository, these rules will reduce mistakes:

- start from `Program.cs`, repository interface, and the relevant controller
- prefer changing domain flow through `IFolkloreRepository`
- keep controllers thin and UI-focused
- use `ViewModels` for page composition instead of overloading domain models
- verify whether a change touches both domain models and EF entities
- check whether seed data must be updated when schema changes
- treat `App_Data/minsuxize.db` as runtime state, not canonical source
- ignore backup files unless explicitly recovering history

## 15. Local Run Commands

Build:

```powershell
dotnet build .\src\MinsuXize.Web\MinsuXize.Web.csproj
```

Run:

```powershell
dotnet run --project .\src\MinsuXize.Web\MinsuXize.Web.csproj --launch-profile http
```

Default preview URL:

```text
http://localhost:5088
```

## 16. Maintainer Notes

- Main maintainer: `TrisomyManager`
- Public repository: `https://github.com/TrisomyManager/minsuxize`
- `AGENT_CONTEXT.md` is the root handoff file and should be read first
- This document is the app-oriented structure guide
- Checkpoint files remain useful as historical progress logs, not as the main design source

---

Last updated: `2026-03-23`
