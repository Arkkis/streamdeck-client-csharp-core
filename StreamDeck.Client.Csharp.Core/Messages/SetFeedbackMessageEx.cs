using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class SetFeedbackMessageEx : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "setFeedback"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    [JsonPropertyName("payload")]
    public JsonElement Payload { get; set; }

    public SetFeedbackMessageEx(JsonElement payload, string pluginUUID)
    {
        Context = pluginUUID;
        Payload = payload;
    }
}
