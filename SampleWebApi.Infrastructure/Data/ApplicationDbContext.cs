using Microsoft.EntityFrameworkCore;
using System.Data;
using SampleWebApi.Core.Models;

namespace SampleWebApi.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public IDbConnection Connection => Database.GetDbConnection();
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            DbInitializer.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
