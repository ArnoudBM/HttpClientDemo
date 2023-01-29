using HttpClientDemo.Models;

namespace HttpClientDemo.Clients;

public class JokesClient : IJokesClient
{
    private readonly HttpClient _httpClient;

    public JokesClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JokeCount?> GetCount()
    {
        return await _httpClient.GetFromJsonAsync<JokeCount?>("joke/count");
    }

    public async Task<Joke?> GetRandomJoke()
    {
        return await _httpClient.GetFromJsonAsync<Joke?>("random/joke?nsfw=false");
    }
}

public interface IJokesClient
{
    Task<JokeCount?> GetCount();

    Task<Joke?> GetRandomJoke();
}