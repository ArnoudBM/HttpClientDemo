using HttpClientDemo.Models;

namespace HttpClientDemo.Clients;

public class JokesClient : IJokesClient
{
    private readonly Uri _baseAddress = new Uri("https://dad-jokes.p.rapidapi.com");
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public JokesClient(IConfiguration configuration)
    {
        _apiKey = GetApiKey(configuration);

        _httpClient = new HttpClient();
        _httpClient.BaseAddress = _baseAddress;
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", _baseAddress.Host);
    }

    public async Task<JokeCount?> GetCount()
    {
        return await _httpClient.GetFromJsonAsync<JokeCount?>("joke/count");
    }

    public async Task<Joke?> GetRandomJoke()
    {
        return await _httpClient.GetFromJsonAsync<Joke?>("random/joke?nsfw=false");
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