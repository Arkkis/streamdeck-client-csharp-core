namespace StreamDeck.Client.Csharp.Core;

public class StreamDeckEventReceivedEventArgs<T> : EventArgs
{
    public T Event { get; set; }
    internal StreamDeckEventReceivedEventArgs(T evt)
    {
        Event = evt;
    }
}
