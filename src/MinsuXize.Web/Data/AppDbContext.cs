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
    public DbSet<SourceEvidenceEntity> Sources => Set<SourceEvidenceEntity>();
    public DbSet<EntrySourceEntity> EntrySources => Set<EntrySourceEntity>();
    public DbSet<SubmissionRecordEntity> Submissions => Set<SubmissionRecordEntity>();
    public DbSet<ReviewHistoryEntity> ReviewHistories => Set<ReviewHistoryEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegionEntity>().ToTable("Regions");
        modelBuilder.Entity<FestivalEntity>().ToTable("Festivals");
        modelBuilder.Entity<FolkloreEntryEntity>().ToTable("Entries");
        modelBuilder.Entity<SourceEvidenceEntity>().ToTable("Sources");
        modelBuilder.Entity<EntrySourceEntity>().ToTable("EntrySources");
        modelBuilder.Entity<SubmissionRecordEntity>().ToTable("Submissions");

        modelBuilder.Entity<EntrySourceEntity>()
            .HasIndex(item => new { item.EntryId, item.SourceId })
            .IsUnique();
            
        modelBuilder.Entity<ReviewHistoryEntity>().ToTable("ReviewHistories");
            
        // 配置 FolkloreEntryEntity 的新字段
        modelBuilder.Entity<FolkloreEntryEntity>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
        modelBuilder.Entity<FolkloreEntryEntity>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
        modelBuilder.Entity<FolkloreEntryEntity>()
            .Property(e => e.Status)
            .HasDefaultValue("draft");
            
        modelBuilder.Entity<FolkloreEntryEntity>()
            .Property(e => e.Version)
            .HasDefaultValue(1);
            
        modelBuilder.Entity<FolkloreEntryEntity>()
            .Property(e => e.CreatedBy)
            .HasDefaultValue("system");
            
        modelBuilder.Entity<FolkloreEntryEntity>()
            .Property(e => e.ImagesJson)
            .HasDefaultValue("[]");
            
        modelBuilder.Entity<FolkloreEntryEntity>()
            .Property(e => e.VideosJson)
            .HasDefaultValue("[]");
            
        modelBuilder.Entity<FolkloreEntryEntity>()
            .Property(e => e.AudiosJson)
            .HasDefaultValue("[]");
            
        // 配置 SubmissionRecordEntity 的新字段
        modelBuilder.Entity<SubmissionRecordEntity>()
            .Property(e => e.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
        modelBuilder.Entity<SubmissionRecordEntity>()
            .Property(e => e.UpdatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
            
        modelBuilder.Entity<SubmissionRecordEntity>()
            .Property(e => e.Status)
            .HasDefaultValue(0); // 0 = pending
            
        modelBuilder.Entity<SubmissionRecordEntity>()
            .Property(e => e.Version)
            .HasDefaultValue(1);
            
        modelBuilder.Entity<SubmissionRecordEntity>()
            .Property(e => e.CreatedBy)
            .HasDefaultValue("anonymous");
            
        modelBuilder.Entity<SubmissionRecordEntity>()
            .Property(e => e.ImagesJson)
            .HasDefaultValue("[]");
            
        modelBuilder.Entity<SubmissionRecordEntity>()
            .Property(e => e.VideosJson)
            .HasDefaultValue("[]");
            
        modelBuilder.Entity<SubmissionRecordEntity>()
            .Property(e => e.AudiosJson)
            .HasDefaultValue("[]");
            
        // 忽略 LocationInfoData 类，因为它是一个复杂类型，不是实体
        modelBuilder.Ignore<LocationInfoData>();
    }
}
