using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class ShowOkMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "showOk"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    public ShowOkMessage(string context)
    {
        Context = context;
    }
}
