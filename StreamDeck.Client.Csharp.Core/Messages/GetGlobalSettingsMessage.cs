using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class GetGlobalSettingsMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "getGlobalSettings"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    public GetGlobalSettingsMessage(string pluginUUID)
    {
        Context = pluginUUID;
    }
}
