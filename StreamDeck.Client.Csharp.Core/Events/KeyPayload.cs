using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class KeyPayload
{
    [JsonPropertyName("settings")]
    public JsonElement Settings { get; set; }

    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; } = null!;

    [JsonPropertyName("state")]
    public uint State { get; set; }

    [JsonPropertyName("userDesiredState")]
    public uint UserDesiredState { get; set; }

    [JsonPropertyName("isInMultiAction")]
    public bool IsInMultiAction { get; set; }
}
