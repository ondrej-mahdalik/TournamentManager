using System.Security.Claims;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TournamentManager.Server.App.Data;
using TournamentManager.Server.App.Models;
using TournamentManager.Server.App.MainSeeds;
using TournamentManager.Server.App.Services;

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
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthorizationDbContext>();

    serviceCollection.AddTransient<IProfileService, ApplicationProfileService>();

    serviceCollection.AddIdentityServer()
        .AddApiAuthorization<ApplicationUser, AuthorizationDbContext>();

    serviceCollection.AddAuthentication()
        .AddIdentityServerJwt();

    serviceCollection.Configure<IdentityOptions>(options =>
    {
        options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
    });
    // .AddGoogle(options =>
    // {
    //     options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new AuthenticationException();
    //     options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new AuthenticationException(); 
    // })
    // .AddMicrosoftAccount(options =>
    // {
    //     options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"] ?? throw new AuthenticationException();
    //     options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"] ?? throw new AuthenticationException();
    // });
}

void ConfigureDependencies(IServiceCollection serviceCollection)
{
    serviceCollection.AddDbContext<AuthorizationDbContext>(options =>
        options.UseSqlServer(authConnectionString));
    serviceCollection.AddDbContext<TournamentManagerDbContext>(options =>
        options.UseSqlServer(mainConnectionString));
    serviceCollection.AddDatabaseDeveloperPageExceptionFilter();
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

void SetupDatabase(WebApplication application)
{
    var scope = application.Services.CreateScope();
    var mainDbContext = scope.ServiceProvider.GetRequiredService<TournamentManagerDbContext>();
    var authDbContext = scope.ServiceProvider.GetRequiredService<AuthorizationDbContext>();

    if (seedDemoData)
    {
        mainDbContext.Database.EnsureDeleted();
        mainDbContext.Database.EnsureCreated();
        authDbContext.Database.EnsureDeleted();
        authDbContext.Database.EnsureCreated();
        
        UserSeeds.Seed(mainDbContext);
        TournamentSeeds.Seed(mainDbContext);
        SportSeeds.Seed(mainDbContext);
        mainDbContext.SaveChanges();
        // TODO Seed other models
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
