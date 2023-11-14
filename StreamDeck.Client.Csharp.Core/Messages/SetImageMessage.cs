using System.Text.Json.Serialization;

namespace StreamDeck.Client.Csharp.Core.Messages;

internal class SetImageMessage : IMessage
{
    [JsonPropertyName("event")]
    public string Event { get { return "setImage"; } }

    [JsonPropertyName("context")]
    public string Context { get; set; }

    [JsonPropertyName("payload")]
    public IPayload Payload { get; set; }

    public SetImageMessage(string base64Image, string context, SDKTarget target, int? state)
    {
        Context = context;
        Payload = new PayloadClass(base64Image, target, state);
    }

    private class PayloadClass : IPayload
    {
        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("target")]
        public SDKTarget Target { get; set; }

        [JsonPropertyName("state")]
        public int? State { get; set; }

        public PayloadClass(string image, SDKTarget target, int? state)
        {
            Image = image;
            Target = target;
            State = state;
        }
    }
}
