using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class DeviceSize
{
    [JsonPropertyName("columns")]
    public int Columns { get; set; }

    [JsonPropertyName("rows")]
    public int Rows { get; set; }
}
