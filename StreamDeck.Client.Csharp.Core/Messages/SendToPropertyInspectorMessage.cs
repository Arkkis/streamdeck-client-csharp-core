using System.Text.Json;
using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class SendToPropertyInspectorMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "sendToPropertyInspector"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    [JsonPropertyName("payload")]
    public JsonElement Payload { get; set; }

    [JsonPropertyName("action")]
    public string Action { get; set; }

    public SendToPropertyInspectorMessage(string action, JsonElement data, string context)
    {
        Context = context;
        Payload = data;
        Action = action;
    }
}
