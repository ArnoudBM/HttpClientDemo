using HttpClientDemo.Models;

namespace HttpClientDemo.Clients;

public class JokesClient : IJokesClient
{
    private const int MaxRetries = 3;

    private static readonly Random Random = new Random();

    private readonly IHttpClientFactory _httpClientFactory;

    public JokesClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<JokeCount?> GetCount()
    {
        var client = _httpClientFactory.CreateClient("dadjokesapi");
        var retriesLeft = MaxRetries;
        JokeCount? result = null;

        while (retriesLeft > 0)
        { 
            try
            {
                if (Random.Next(1, 3) == 1)
                {
                    throw new HttpRequestException("Simulated API not responding");
                }

                result = await client.GetFromJsonAsync<JokeCount?>("joke/count");

                break;
            }
            catch (HttpRequestException)
            {
                retriesLeft--;
                if (retriesLeft == 0)
                {
                    throw;
                }
            }
        }

        return result;
    }

    public async Task<Joke?> GetRandomJoke()
    {
        var client = _httpClientFactory.CreateClient("dadjokesapi");
        var retriesLeft = MaxRetries;
        Joke? result = null;

        while (retriesLeft > 0)
        { 
            try
            {
                if (Random.Next(1, 3) == 1)
                {
                    throw new HttpRequestException("Simulated API not responding");
                }

                result = await client.GetFromJsonAsync<Joke?>("random/joke?nsfw=false");

                break;
            }
            catch (HttpRequestException)
            {
                retriesLeft--;
                if (retriesLeft == 0)
                {
                    throw;
                }
            }
        }

        return result;
   }
}

public interface IJokesClient
{
    Task<JokeCount?> GetCount();

    Task<Joke?> GetRandomJoke();
}