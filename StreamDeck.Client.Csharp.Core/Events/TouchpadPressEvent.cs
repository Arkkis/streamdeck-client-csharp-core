using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

/// <summary>
/// Payload for touchpad press
/// </summary>
public class TouchpadPressEvent : BaseEvent
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
    /// Information on touchpad press
    /// </summary>
    [JsonPropertyName("payload")]
    public TouchpadPressPayload Payload { get; set; } = null!;
}
