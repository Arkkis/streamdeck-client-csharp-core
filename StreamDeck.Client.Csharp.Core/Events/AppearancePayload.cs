using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class AppearancePayload
{
    [JsonPropertyName("settings")]
    public JsonObject Settings { get; set; } = null!;

    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; } = null!;

    [JsonPropertyName("state")]
    public uint State { get; set; }

    [JsonPropertyName("isInMultiAction")]
    public bool IsInMultiAction { get; set; }

    /// <summary>
    /// Controller which issued the event
    /// </summary>
    [JsonPropertyName("controller")]
    public string Controller { get; set; } = null!;
}
