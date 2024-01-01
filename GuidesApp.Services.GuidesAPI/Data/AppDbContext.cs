using GuidesApp.Services.GuidesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GuidesApp.Services.GuidesAPI.Data
{
	public class AppDbContext : DbContext 
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		public DbSet<Guide> Guides { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
           .Where(e => e.Entity is Guide &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (Guide)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDateTime = now;
                }

                entity.LastModifiedDateTime = now;
            }
            return base.SaveChanges();
        }
    }
}

