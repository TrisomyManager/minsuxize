using Microsoft.EntityFrameworkCore;
using MinsuXize.Web.Data.Entities;

namespace MinsuXize.Web.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<RegionEntity> Regions => Set<RegionEntity>();
    public DbSet<FestivalEntity> Festivals => Set<FestivalEntity>();
    public DbSet<FolkloreEntryEntity> Entries => Set<FolkloreEntryEntity>();
    public DbSet<EntryFaqEntity> EntryFaqs => Set<EntryFaqEntity>();
    public DbSet<EntryRelationEntity> EntryRelations => Set<EntryRelationEntity>();
    public DbSet<SourceEvidenceEntity> Sources => Set<SourceEvidenceEntity>();
    public DbSet<EntrySourceEntity> EntrySources => Set<EntrySourceEntity>();
    public DbSet<SubmissionRecordEntity> Submissions => Set<SubmissionRecordEntity>();
    public DbSet<ReviewHistoryEntity> ReviewHistories => Set<ReviewHistoryEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegionEntity>().ToTable("Regions");
        modelBuilder.Entity<FestivalEntity>().ToTable("Festivals");
        modelBuilder.Entity<FolkloreEntryEntity>().ToTable("Entries");
        modelBuilder.Entity<EntryFaqEntity>().ToTable("EntryFaqs");
        modelBuilder.Entity<EntryRelationEntity>().ToTable("EntryRelations");
        modelBuilder.Entity<SourceEvidenceEntity>().ToTable("Sources");
        modelBuilder.Entity<EntrySourceEntity>().ToTable("EntrySources");
        modelBuilder.Entity<SubmissionRecordEntity>().ToTable("Submissions");
        modelBuilder.Entity<ReviewHistoryEntity>().ToTable("ReviewHistories");

        modelBuilder.Entity<RegionEntity>().HasIndex(item => item.Slug).IsUnique();
        modelBuilder.Entity<FestivalEntity>().HasIndex(item => item.Slug).IsUnique();
        modelBuilder.Entity<FolkloreEntryEntity>().HasIndex(item => item.Slug).IsUnique();
        modelBuilder.Entity<FolkloreEntryEntity>().HasIndex(item => item.ContentType);
        modelBuilder.Entity<EntryFaqEntity>().HasIndex(item => new { item.EntryId, item.SortOrder });
        modelBuilder.Entity<EntryRelationEntity>().HasIndex(item => new { item.EntryId, item.RelatedEntryId }).IsUnique();

        modelBuilder.Entity<EntrySourceEntity>()
            .HasIndex(item => new { item.EntryId, item.SourceId })
            .IsUnique();

        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.Slug).HasDefaultValue("");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.ContentType).HasDefaultValue("ritual");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.RegionFieldsJson).HasDefaultValue("{}");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.PreparationsJson).HasDefaultValue("[]");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.RegionalDifferencesJson).HasDefaultValue("[]");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.TagsJson).HasDefaultValue("[]");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.Status).HasDefaultValue("draft");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.ReviewStatus).HasDefaultValue("pending-verification");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.ConfidenceLevel).HasDefaultValue("medium");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.SourceGrade).HasDefaultValue("unverified");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.Version).HasDefaultValue(1);
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.CreatedBy).HasDefaultValue("system");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.ImagesJson).HasDefaultValue("[]");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.VideosJson).HasDefaultValue("[]");
        modelBuilder.Entity<FolkloreEntryEntity>().Property(e => e.AudiosJson).HasDefaultValue("[]");

        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.ContentType).HasDefaultValue("ritual");
        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.Status).HasDefaultValue(0);
        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.Version).HasDefaultValue(1);
        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.CreatedBy).HasDefaultValue("anonymous");
        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.ImagesJson).HasDefaultValue("[]");
        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.VideosJson).HasDefaultValue("[]");
        modelBuilder.Entity<SubmissionRecordEntity>().Property(e => e.AudiosJson).HasDefaultValue("[]");

        modelBuilder.Ignore<LocationInfoData>();
    }
}
