using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class ApplicationDidLaunchEvent : BaseEvent
{
    [JsonPropertyName("payload")]
    public ApplicationPayload Payload { get; set; } = null!;
}
