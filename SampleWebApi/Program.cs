using Autofac.Extensions.DependencyInjection;
using DotNetEnv;

public class Program
{
    public static void Main(string[] args)
    {
        Env.Load();
        // Build and run the host
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            // Use Autofac as the DI container instead of the default .NET Core container
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())

            // Configure the WebHost to use the Startup class
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>(); // Use the Startup class for configuring services and the pipeline
            });
}