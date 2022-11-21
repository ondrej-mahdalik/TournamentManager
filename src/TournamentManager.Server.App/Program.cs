using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TournamentManager.Server.App.Data;
using TournamentManager.Server.Auth.Models;
using TournamentManager.Server.Auth.Seeds;
using TournamentManager.Server.BL.Installers;
using TournamentManager.Server.DAL;
using TournamentManager.Server.DAL.Extensions;
using TournamentManager.Server.DAL.Installers;
using TournamentManager.Server.Seeds.MainSeeds;
using TournamentManager.Server.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
    .AddJsonFile("appsettings.Secrets.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var authConnectionString = builder.Configuration.GetConnectionString("AuthConnection") ?? throw new InvalidOperationException("Connection string 'AuthConnection' not found.");
var mainConnectionString = builder.Configuration.GetConnectionString("MainConnection") ?? throw new InvalidOperationException("Connection string 'MainConnection' not found.");
var skipMigrationAndSeedDemoData = builder.Configuration.GetSection("DALOptions").GetValue<bool>("SkipMigrationAndSeedDemoData");
var dalType = builder.Configuration.GetSection("DALOptions")["Type"];

ConfigureControllers(builder.Services);
ConfigureOpenApiDocuments(builder.Services);
ConfigureDependencies(builder.Services);
ConfigureAuthentication(builder.Services);

var app = builder.Build();

UseDevelopmentSettings(app);
UseRoutingAndSecurityFeatures(app);
UseOpenApi(app);
await SetupDatabase(app);

app.Run();

void ConfigureAuthentication(IServiceCollection serviceCollection)
{
    serviceCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthorizationDbContext>();
    
    serviceCollection.AddIdentityServer()
        .AddApiAuthorization<ApplicationUser, AuthorizationDbContext>();

    serviceCollection.AddAuthentication()
        .AddIdentityServerJwt()
        .AddGoogle(options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ??
                               throw new InvalidOperationException();
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ??
                                   throw new InvalidOperationException();
        })
        .AddFacebook(options =>
        {
            options.AppId = builder.Configuration["Authentication:Facebook:AppId"] ??
                            throw new InvalidOperationException();
            options.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"] ??
                                throw new InvalidOperationException();
        });
    // .AddTwitter(options =>
    // {
    //     options.ConsumerKey = builder.Configuration["Authentication:Twitter:ConsumerKey"] ??
    //                           throw new InvalidOperationException();
    //     options.ConsumerSecret = builder.Configuration["Authentication:Twitter:ConsumerSecret"] ??
    //                              throw new InvalidOperationException();
    // });

    serviceCollection.Configure<IdentityOptions>(options =>
    {
        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    });
}

void ConfigureDependencies(IServiceCollection serviceCollection)
{
    serviceCollection.AddDbContext<AuthorizationDbContext>(options =>
        options.UseSqlServer(authConnectionString));

    switch (dalType)
    {
        case "SQLServer":
            serviceCollection.AddInstaller<DALSQLServerInstaller>(mainConnectionString);
            break;
        
        default:
        case "SQLite":
            serviceCollection.AddInstaller<DALSQLiteInstaller>(mainConnectionString);
            break;
    }
    
    serviceCollection.AddInstaller<BLInstaller>();

#if DEBUG
    serviceCollection.AddDatabaseDeveloperPageExceptionFilter();
#endif
}

void ConfigureOpenApiDocuments(IServiceCollection serviceCollection)
{
    // serviceCollection.AddEndpointsApiExplorer();
    // serviceCollection.AddOpenApiDocument(document =>
    // {
    //     document.Title = "Tournament Manager API";
    //     document.DocumentName = "v1";
    //     document.OperationProcessors.Add(new OperationSecurityScopeProcessor());
    //     document.AddSecurity("Bearer", new OpenApiSecurityScheme
    //     {
    //         In = OpenApiSecurityApiKeyLocation.Header,
    //         Description = "Please enter token",
    //         Name = "Authorization",
    //         Type = OpenApiSecuritySchemeType.Http,
    //         BearerFormat = "JWT",
    //         Scheme = "bearer"
    //     });
    // });
}

void ConfigureControllers(IServiceCollection serviceCollection)
{
    serviceCollection.AddControllersWithViews().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
    serviceCollection.AddRazorPages();
}

void UseDevelopmentSettings(WebApplication application)
{
    // Configure the HTTP request pipeline.
    if (application.Environment.IsDevelopment())
    {
        application.UseMigrationsEndPoint();
        application.UseWebAssemblyDebugging();
        application.UseDeveloperExceptionPage();
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
    // application.UseOpenApi();
    // application.UseSwaggerUi3(settings =>
    // {
    //     settings.DocumentTitle = "Tournament Manager Swagger UI";
    //     settings.SwaggerRoutes.Add(new SwaggerUi3Route("default", "/swagger/v1/swagger.json"));
    // });
}

async Task SetupDatabase(WebApplication application)
{
    var scope = application.Services.CreateScope();
    var authDbContext = scope.ServiceProvider.GetRequiredService<AuthorizationDbContext>();
    
    var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TournamentManagerDbContext>>();
    await using var mainDbContext = dbContextFactory.CreateDbContext();
    

    if (skipMigrationAndSeedDemoData)
    {
        await mainDbContext.Database.EnsureDeletedAsync();
        await mainDbContext.Database.EnsureCreatedAsync();
        await authDbContext.Database.EnsureDeletedAsync();
        await authDbContext.Database.EnsureCreatedAsync();
        
        UserSeeds.Seed(mainDbContext);
        TournamentSeeds.Seed(mainDbContext);
        SportSeeds.Seed(mainDbContext);
        TeamSeeds.Seed(mainDbContext);
        await mainDbContext.SaveChangesAsync();
        // TODO Seed other models

        await ApplicationUserSeeds.SeedAsync(scope.ServiceProvider);
    }
    else
    {
        mainDbContext.Database.Migrate();
        authDbContext.Database.Migrate();
    }
}

// Make the implicit Program class public so test projects can access it
public partial class Program
{
    
}
