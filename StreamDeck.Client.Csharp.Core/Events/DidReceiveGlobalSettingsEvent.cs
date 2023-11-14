using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class DidReceiveGlobalSettingsEvent : BaseEvent
{
    [JsonPropertyName("payload")]
    public ReceiveGlobalSettingsPayload Payload { get; set; } = null!;
}
