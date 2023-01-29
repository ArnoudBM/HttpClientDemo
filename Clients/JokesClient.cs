using HttpClientDemo.Models;

namespace HttpClientDemo.Clients;

public class JokesClient : IJokesClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public JokesClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<JokeCount?> GetCount()
    {
        var client = _httpClientFactory.CreateClient("dadjokesapi");

        return await client.GetFromJsonAsync<JokeCount?>("joke/count");
    }

    public async Task<Joke?> GetRandomJoke()
    {
        var client = _httpClientFactory.CreateClient("dadjokesapi");

        return await client.GetFromJsonAsync<Joke?>("random/joke?nsfw=false");
    }
}

public interface IJokesClient
{
    Task<JokeCount?> GetCount();

    Task<Joke?> GetRandomJoke();
}