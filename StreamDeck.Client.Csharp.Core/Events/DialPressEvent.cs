using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

/// <summary>
/// Payload for Dial press/unpress event
/// </summary>
public class DialPressEvent : BaseEvent
{
    /// <summary>
    /// Action Name
    /// </summary>
    [JsonPropertyName("action")]
    public string Action { get; set; } = null!;

    /// <summary>
    /// Unique Action UUID
    /// </summary>
    [JsonPropertyName("context")]
    public string Context { get; set; } = null!;

    /// <summary>
    /// Device UUID key was pressed on
    /// </summary>
    [JsonPropertyName("device")]
    public string Device { get; set; } = null!;

    /// <summary>
    /// Information on dial rotation
    /// </summary>
    [JsonPropertyName("payload")]
    public DialPressPayload Payload { get; set; } = null!;
}
