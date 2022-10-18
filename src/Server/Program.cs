using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using NSwag.AspNetCore;
using NSwag.Generation.Processors.Security;
using TournamentManager.Server.Data;
using TournamentManager.Server.Models;

var builder = WebApplication.CreateBuilder(args);

var authConnectionString = builder.Configuration.GetConnectionString("AuthConnection") ?? throw new InvalidOperationException("Connection string 'AuthConnection' not found.");
var mainConnectionString = builder.Configuration.GetConnectionString("MainConnection") ?? throw new InvalidOperationException("Connection string 'MainConnection' not found.");
var seedDemoData = builder.Configuration.GetSection("DALOptions").GetValue<bool>("SeedDemoData");

ConfigureControllers(builder.Services);
ConfigureOpenApiDocuments(builder.Services);
ConfigureDependencies(builder.Services);
ConfigureAuthentication(builder.Services);

var app = builder.Build();

UseDevelopmentSettings(app);
UseRoutingAndSecurityFeatures(app);
UseOpenApi(app);
SetupDatabase(app);

app.Run();

void ConfigureAuthentication(IServiceCollection serviceCollection)
{
    serviceCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();

    serviceCollection.AddIdentityServer()
        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

    serviceCollection.AddAuthentication()
        .AddIdentityServerJwt();
}

void ConfigureDependencies(IServiceCollection serviceCollection)
{
    serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(authConnectionString));
    serviceCollection.AddDbContext<TournamentManagerDbContext>(options =>
        options.UseSqlServer(mainConnectionString));
    serviceCollection.AddDatabaseDeveloperPageExceptionFilter();
}

void ConfigureOpenApiDocuments(IServiceCollection serviceCollection)
{
    serviceCollection.AddEndpointsApiExplorer();
    serviceCollection.AddOpenApiDocument(document =>
    {
        document.Title = "Tournament Manager API";
        document.DocumentName = "v1";
        document.OperationProcessors.Add(new OperationSecurityScopeProcessor());
    });
}

void ConfigureControllers(IServiceCollection serviceCollection)
{
    serviceCollection.AddControllersWithViews();
    serviceCollection.AddRazorPages();
}

void UseDevelopmentSettings(WebApplication application)
{
    // Configure the HTTP request pipeline.
    if (application.Environment.IsDevelopment())
    {
        application.UseMigrationsEndPoint();
        application.UseWebAssemblyDebugging();
    }
    else
    {
        application.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        application.UseHsts();
    }
}

void UseRoutingAndSecurityFeatures(WebApplication application)
{
    application.UseHttpsRedirection();
    application.UseBlazorFrameworkFiles();
    application.UseStaticFiles();
    application.UseRouting();
    application.UseIdentityServer();
    application.UseAuthorization();
    application.MapRazorPages();
    application.MapControllers();
    application.MapFallbackToFile("index.html");
}

void UseOpenApi(WebApplication application)
{
    application.UseOpenApi();
    application.UseSwaggerUi3(settings =>
    {
        settings.DocumentTitle = "Tournament Manager Swagger UI";
        settings.SwaggerRoutes.Add(new SwaggerUi3Route("default", "/swagger/v1/swagger.json"));
    });
}

void SetupDatabase(WebApplication application)
{
    var scope = application.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<TournamentManagerDbContext>();
    MainDbInitializer.Initialize(dbContext, seedDemoData);
}

// Make the implicit Program class public so test projects can access it
public partial class Program
{
    
}
