using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class ApplicationPayload
{
    [JsonPropertyName("application")]
    public string Application { get; set; } = null!;
}
