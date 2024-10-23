using Autofac;
using SampleWebApi.Extensions;

public class Startup(IConfiguration configuration)
{
    // This method gets called by the runtime to configure services.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddJwtServices();
        //services.AddSwaggerGen(); // Example: Adding Swagger for API documentation
    }

    // ConfigureContainer is where you register services with Autofac
    public void ConfigureContainer(ContainerBuilder builder)
    {
        // This is where you register Autofac modules or individual dependencies.
        // Registers all Autofac modules found in the current assembly
        builder.RegisterAssemblyModules(typeof(Startup).Assembly);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            //app.UseSwagger();
            //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication(); 
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
