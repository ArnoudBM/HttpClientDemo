using System.Text.Json.Serialization;

namespace HttpClientDemo.Models;

public record Joke
{
    public bool Success { get; set; } 

    public JokeBody[]? Body { get; set; }
}