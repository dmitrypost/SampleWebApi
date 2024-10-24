using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SampleWebApi.Infrastructure.Configuration;

namespace SampleWebApi.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            Env.Load();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            
            var connectionString =
                EnvironmentVariableReader.GetValue(EnvironmentVariableKeys.DatabaseConnectionString);

            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}