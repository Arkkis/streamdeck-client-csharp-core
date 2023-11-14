using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class GetSettingsMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "getSettings"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    public GetSettingsMessage(string context)
    {
        Context = context;
    }
}
