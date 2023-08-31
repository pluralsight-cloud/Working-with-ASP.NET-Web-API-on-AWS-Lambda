using WebApiRdsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApiRdsApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        DatabaseAndSecretConfiguration databaseAndSecretConfiguration = new DatabaseAndSecretConfiguration();

        Configuration.Bind(databaseAndSecretConfiguration);    

        string connectionString = SecretsManagerService.GetConnectionStringFromSecret(databaseAndSecretConfiguration).GetAwaiter().GetResult();
        services.AddDbContext<SchoolContext>(options => options.UseSqlServer(connectionString));
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SchoolContext schoolContext)
    {
        Console.WriteLine("Attempting to connect to database...this will appear in the CloudWatch logs");
        schoolContext.Database.EnsureCreated();
        schoolContext.Seed();
        Console.WriteLine("Successfully created the database...this will appear in the CloudWatch logs");

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}