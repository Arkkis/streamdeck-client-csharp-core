using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

/// <summary>
/// Payload received when a dial is pressed or unpressed
/// </summary>
public class DialPressPayload
{
    /// <summary>
    /// Controller which issued the event
    /// </summary>
    [JsonPropertyName("controller")]
    public string Controller { get; set; } = null!;

    /// <summary>
    /// Current event settings
    /// </summary>
    [JsonPropertyName("settings")]
    public JsonElement Settings { get; set; }

    /// <summary>
    /// Coordinates of key on the stream deck
    /// </summary>
    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; } = null!;

    /// <summary>
    /// Boolean whether the dial is currently pressed or not
    /// </summary>
    [JsonPropertyName("pressed")]
    public bool IsDialPressed { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="coordinates"></param>
    /// <param name="settings"></param>
    /// <param name="controller"></param>
    /// <param name="isDialPressed"></param>
    public DialPressPayload(Coordinates coordinates, JsonElement settings, string controller, bool isDialPressed)
    {
        Coordinates = coordinates;
        Settings = settings;
        Controller = controller;
        IsDialPressed = isDialPressed;
    }

    /// <summary>
    /// Default constructor for serialization
    /// </summary>
    public DialPressPayload() { }
}
