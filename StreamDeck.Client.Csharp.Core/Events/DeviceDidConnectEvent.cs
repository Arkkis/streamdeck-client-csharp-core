using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class DeviceDidConnectEvent : BaseEvent
{
    [JsonPropertyName("device")]
    public string Device { get; set; } = null!;

    [JsonPropertyName("deviceInfo")]
    public DeviceInfo DeviceInfo { get; set; } = null!;
}
