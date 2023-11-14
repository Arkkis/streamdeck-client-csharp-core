using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class DeviceInfo
{
    [JsonPropertyName("type")]
    public DeviceType Type { get; set; }

    [JsonPropertyName("size")]
    public DeviceSize Size { get; set; } = null!;
}
