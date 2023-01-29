using HttpClientDemo.Models;
using Polly;
using Polly.Retry;

namespace HttpClientDemo.Clients;

public class JokesClient : IJokesClient
{
    private const int MaxRetries = 3;

    private static readonly Random Random = new Random();

    private readonly IHttpClientFactory _httpClientFactory;

    private readonly AsyncRetryPolicy _retryPolicy;

    public JokesClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;

        _retryPolicy = Policy.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }

    public async Task<JokeCount?> GetCount()
    {
        var client = _httpClientFactory.CreateClient("dadjokesapi");

        return await _retryPolicy.ExecuteAsync(async () => 
        {
            if (Random.Next(1, 3) == 1)
            {
                throw new HttpRequestException("Simulated API not responding");
            }

            return await client.GetFromJsonAsync<JokeCount?>("joke/count");
        });
    }

    public async Task<Joke?> GetRandomJoke()
    {
        var client = _httpClientFactory.CreateClient("dadjokesapi");

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            if (Random.Next(1, 3) == 1)
            {
                throw new HttpRequestException("Simulated API not responding");
            }

            return await client.GetFromJsonAsync<Joke?>("random/joke?nsfw=false");
        });
   }
}

public interface IJokesClient
{
    Task<JokeCount?> GetCount();

    Task<Joke?> GetRandomJoke();
}