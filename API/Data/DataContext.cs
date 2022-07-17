using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppUserLike> Likes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Creates the primary key of the AppUserLike table using the SourceUserId & the LikedUserId
            builder.Entity<AppUserLike>().HasKey(k => new {k.SourceUserId, k.LikedUserId});
            // Configures the relationship
            builder.Entity<AppUserLike>().HasOne(s => s.SourceUser).WithMany(l => l.LikedUsers).HasForeignKey(s => s.SourceUserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AppUserLike>().HasOne(l => l.LikedUser).WithMany(l => l.LikedByUsers).HasForeignKey(l => l.LikedUserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}