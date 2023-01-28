using HttpClientDemo.Models;

namespace HttpClientDemo.Clients;

public class JokesClient : IJokesClient
{
    private readonly Uri _baseAddress = new Uri("https://dad-jokes.p.rapidapi.com");
    private readonly string _apiKey;

    public JokesClient(IConfiguration configuration)
    {
        _apiKey = GetApiKey(configuration);
    }

    public async Task<JokeCount?> GetCount()
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = _baseAddress;
        httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
        httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _baseAddress.Host);

        return await httpClient.GetFromJsonAsync<JokeCount?>("joke/count");
    }

    public async Task<Joke?> GetRandomJoke()
    {
        using var httpClient = new HttpClient();
        httpClient.BaseAddress = _baseAddress;
        httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
        httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _baseAddress.Host);

        return await httpClient.GetFromJsonAsync<Joke?>("random/joke?nsfw=false");
    }

    private static string GetApiKey(IConfiguration configuration)
    {
        var apiKey = configuration["DadJokes:ApiKey"];

        if (apiKey is null)
            throw new InvalidOperationException("The user secret DadJokes:ApiKey is required.\r\nUse 'dotnet user-secrets init' and 'dotnet user-secrets set \"DadJokes:ApiKey\" \"<your API Key>\"'.");

        return apiKey;
    }
}

public interface IJokesClient
{
    Task<JokeCount?> GetCount();

    Task<Joke?> GetRandomJoke();
}