using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleWebApi.Infrastructure.Configuration;
using SampleWebApi.Infrastructure.Data;

namespace SampleWebApi.Autofac
{
    public class DataModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Access the connection string
            var connectionString = EnvironmentVariableReader.GetValue(EnvironmentVariableKeys.DatabaseConnectionString);

            // Register the ApplicationDbContext with a per-lifetime scope
            builder.Register(context =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                    // Configure your DbContext here (e.g., connection string)
                    optionsBuilder.UseSqlServer(connectionString);
                    return new ApplicationDbContext(optionsBuilder.Options);
                })
                .As<ApplicationDbContext>()
                .InstancePerLifetimeScope();

            // Register UnitOfWork as IUnitOfWork
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            // Register the GenericRepository for User
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            // You can also register other repositories as needed
        }
    }
}