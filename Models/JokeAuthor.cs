using System.Text.Json.Serialization;

namespace HttpClientDemo.Models;

public record JokeAuthor
{
    public string? Id { get; set; }

    public string? Name{ get; set; } 
}