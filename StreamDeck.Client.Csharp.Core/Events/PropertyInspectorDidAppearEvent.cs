using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class PropertyInspectorDidAppearEvent : BaseEvent
{
    [JsonPropertyName("action")]
    public string Action { get; set; } = null!;

    [JsonPropertyName("context")]
    public string Context { get; set; } = null!;

    [JsonPropertyName("device")]
    public string Device { get; set; } = null!;
}
