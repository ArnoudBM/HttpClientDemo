using System.Text.Json.Serialization;

namespace HttpClientDemo.Models;

public record JokeBody
{
    public Guid Id { get; set; }

    public string? Setup { get; set; }

    public string? Punchline { get; set; }

    public string? Type { get; set; }

    public string[]? Likes { get; set; }

    public JokeAuthor? Author { get; set; }

    public bool Approved { get; set; }

    public long Date { get; set; }

    public bool Nsfw { get; set; }

    public Uri? ShareableLink { get; set; }
}