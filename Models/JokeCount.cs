using System.Text.Json.Serialization;

namespace HttpClientDemo.Models;

public record JokeCount
{
    public bool Success { get; set; } 

    public int? Body { get; set; }
}