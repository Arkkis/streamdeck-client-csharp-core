using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class SetGlobalSettingsMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "setGlobalSettings"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    [JsonPropertyName("payload")]
    public JsonElement Payload { get; set; }

    public SetGlobalSettingsMessage(JsonElement settings, string pluginUUID)
    {
        Context = pluginUUID;
        Payload = settings;
    }
}
