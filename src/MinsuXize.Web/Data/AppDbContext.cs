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
    }
}
