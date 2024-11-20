using elasticsearch_demo_project.Common;
using elasticsearch_demo_project.Models;
using Microsoft.EntityFrameworkCore;

namespace elasticsearch_demo_project.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Game>()
               .HasOne(g => g.Publisher)
               .WithMany(p => p.Games)
               .HasForeignKey(g => g.PublisherCode)
               .HasPrincipalKey(p => p.PublisherCode)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GameGenre>()
                .HasKey(gg => gg.Id);

            modelBuilder.Entity<GameGenre>()
                .HasOne(gg => gg.Game)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GameGenre>()
                .HasOne(gg => gg.Genre)
                .WithMany(g => g.GameGenres)
                .HasForeignKey(gg => gg.GenreCode)
                .HasPrincipalKey(g => g.GenreCode)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
