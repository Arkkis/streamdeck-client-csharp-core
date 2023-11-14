using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class ShowAlertMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "showAlert"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    public ShowAlertMessage(string context)
    {
        Context = context;
    }
}
