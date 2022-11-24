namespace TournamentManager.Client.App.Extensions;

public class PublicHttpClient
{
    public HttpClient Client { get; }
    
    public PublicHttpClient(HttpClient httpClient)
    {
        Client = httpClient;
    }
}
