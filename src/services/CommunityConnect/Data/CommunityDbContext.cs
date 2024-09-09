using CommunityConnect.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityConnect.Data
{
    public class CommunityDbContext:DbContext
    {
        public CommunityDbContext(DbContextOptions<CommunityDbContext> options) : base(options)
        {
        }

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<RequestService> RequestServices { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complaint>()
                .Property(c => c.Status)
                .HasDefaultValue(ComplaintStatus.OPEN);
        }
    }
}
