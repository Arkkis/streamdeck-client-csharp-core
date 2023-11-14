using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class DeviceDidDisconnectEvent : BaseEvent
{
    [JsonPropertyName("device")]
    public string Device { get; set; } = null!;
}
