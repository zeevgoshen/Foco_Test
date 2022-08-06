using FocoTest.Models;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TestSite>().HasKey(table => new {
                table.Id,
                table.SiteId,
                table.TicketId
            });

            builder.Entity<TestSiteQueue>().HasKey(table => new {
                table.SiteId,
                table.TicketId,
                table.Id
            });
        }
        public DbSet<Test> Tests { get; set; }

        public DbSet<TestSite> TestSites { get; set; }

        public DbSet<Users> Users { get; set; }

        public DbSet<TestSiteQueue> TestSiteQueue { get; set; }


    }
}
