using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

/// <summary>
/// Payload for dial rotation event
/// </summary>
public class DialRotateEvent : BaseEvent
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
    public DialRotatePayload Payload { get; set; } = null!;
}
