using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class ReceiveGlobalSettingsPayload
{
    [JsonPropertyName("settings")]
    public JsonElement Settings { get; set; }
}
