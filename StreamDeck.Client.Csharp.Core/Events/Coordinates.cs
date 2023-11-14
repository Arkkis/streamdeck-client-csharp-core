using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class Coordinates
{
    [JsonPropertyName("column")]
    public int Columns { get; set; }

    [JsonPropertyName("row")]
    public int Rows { get; set; }
}
