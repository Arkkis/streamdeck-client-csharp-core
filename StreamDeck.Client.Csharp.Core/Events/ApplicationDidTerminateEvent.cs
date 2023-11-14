using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class ApplicationDidTerminateEvent : BaseEvent
{
    [JsonPropertyName("payload")]
    public ApplicationPayload Payload { get; set; } = null!;
}
