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

    }
}

