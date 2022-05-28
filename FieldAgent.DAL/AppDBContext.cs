using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldAgent.DAL
{
    public class AppDBContext : DbContext 
    {
        public DbSet<Agency> Agency { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Alias> Alias { get; set; }
        public DbSet<Mission> Mission { get; set; } 
        public DbSet<Location> Location { get; set; }
        public DbSet<SecurityClearance> SecurityClearance { get; set; }
        public DbSet<AgencyAgent> AgencyAgent { get; set; }
        public DbSet<MissionAgent> MissionAgent { get; set; }

        public AppDBContext() : base()
        {

        }

        public AppDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AgencyAgent>()
                .HasKey(aa => new { aa.AgencyId, aa.AgentId });
            builder.Entity<MissionAgent>()
                .HasKey(ma => new { ma.MissionId, ma.AgentId });
        }
        
    }
}
