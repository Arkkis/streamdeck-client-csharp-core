using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Events;

public class TitleParameters
{
    [JsonPropertyName("fontFamily")]
    public string FontFamily { get; set; } = null!;

    [JsonPropertyName("fontSize")]
    public uint FontSize { get; set; }

    [JsonPropertyName("fontStyle")]
    public string FontStyle { get; set; } = null!;

    [JsonPropertyName("fontUnderline")]
    public bool FontUnderline { get; set; }

    [JsonPropertyName("showTitle")]
    public bool ShowTitle { get; set; }

    [JsonPropertyName("titleAlignment")]
    public string TitleAlignment { get; set; } = null!;

    [JsonPropertyName("titleColor")]
    public string TitleColor { get; set; } = null!;
}
