using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class RegisterEventMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get; set; }

    [JsonPropertyName("uuid")]
    public string UUID { get; set; }

    public RegisterEventMessage(string eventName, string uuid)
    {
        Event = eventName;
        UUID = uuid;
    }
}

[JsonSerializable(typeof(RegisterEventMessage))]
[JsonSerializable(typeof(GetSettingsMessage))]
[JsonSerializable(typeof(GetGlobalSettingsMessage))]
[JsonSerializable(typeof(LogMessage))]
[JsonSerializable(typeof(OpenUrlMessage))]
[JsonSerializable(typeof(SendToPropertyInspectorMessage))]
[JsonSerializable(typeof(SetFeedbackLayoutMessage))]
[JsonSerializable(typeof(SetFeedbackMessage))]
[JsonSerializable(typeof(SetFeedbackMessageEx))]
[JsonSerializable(typeof(SetTitleMessage))]
[JsonSerializable(typeof(SetImageMessage))]
[JsonSerializable(typeof(ShowAlertMessage))]
[JsonSerializable(typeof(ShowOkMessage))]
[JsonSerializable(typeof(SetGlobalSettingsMessage))]
[JsonSerializable(typeof(SetSettingsMessage))]
[JsonSerializable(typeof(SetStateMessage))]
[JsonSerializable(typeof(SwitchToProfileMessage))]
internal partial class MessageJsonContext : JsonSerializerContext
{
}

