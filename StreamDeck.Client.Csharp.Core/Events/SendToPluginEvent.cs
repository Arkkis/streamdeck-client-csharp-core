using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class SendToPluginEvent : BaseEvent
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = null!;

    [JsonPropertyName("context")]
    public string Context { get; set; } = null!;

    [JsonPropertyName("payload")]
    public JsonElement Payload { get; set; }
}
