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
        public DbSet<Resident> Residents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Resident)
                .WithMany(r => r.Complaints)
                .HasForeignKey(c => c.ResidentId);
        }
    }
}
