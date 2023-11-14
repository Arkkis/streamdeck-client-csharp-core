using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class TitleParametersPayload
{
    [JsonPropertyName("settings")]
    public JsonElement Settings { get; set; }

    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; } = null!;

    [JsonPropertyName("state")]
    public uint State { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("titleParameters")]
    public TitleParameters TitleParameters { get; set; } = null!;
}
