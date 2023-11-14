using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class SetSettingsMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "setSettings"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    [JsonPropertyName("payload")]
    public JsonElement Payload { get; set; }

    public SetSettingsMessage(JsonElement settings, string context)
    {
        Context = context;
        Payload = settings;
    }
}
