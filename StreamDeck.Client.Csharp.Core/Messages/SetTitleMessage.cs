using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class SetTitleMessage(string title, string context, SDKTarget target, int? state) : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "setTitle"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; } = context;

    [JsonPropertyName("payload")]
    public IPayload Payload { get; set; } = new PayloadClass(title, target, state);

    private class PayloadClass(string title, SDKTarget target, int? state) : IPayload
    {
        [JsonPropertyName("title")]
        public string Title { get; set; } = title;

        [JsonPropertyName("target")]
        public SDKTarget Target { get; set; } = target;

        [JsonPropertyName("state")]
        public int? State { get; set; } = state;
    }
}
