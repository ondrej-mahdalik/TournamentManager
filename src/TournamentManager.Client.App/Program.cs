using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TournamentManager.Client.App;
using TournamentManager.Client.App.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#if DEBUG
builder.Services.AddHttpClient("TournamentManager.Server.AppAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<PublicHttpClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
#else
builder.Services.AddHttpClient("TournamentManager.Server.AppAPI", client => client.BaseAddress = new Uri("https://tournament-manager.eu"))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<PublicHttpClient>(client => client.BaseAddress = new Uri("https://tournament-manager.eu"));
#endif

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TournamentManager.Server.AppAPI"));

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
