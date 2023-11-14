using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class ReceiveSettingsPayload
{
    [JsonPropertyName("settings")]
    public JsonElement Settings { get; set; }

    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; } = null!;

    [JsonPropertyName("isInMultiAction")]
    public bool IsInMultiAction { get; set; }
}
