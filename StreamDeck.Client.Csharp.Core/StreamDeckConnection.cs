using StreamDeck.Client.Csharp.Core.Events;
using StreamDeck.Client.Csharp.Core.Messages;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace StreamDeck.Client.Csharp.Core;

public class StreamDeckConnection(int port, string uuid, string registerEvent)
{
    private const int BufferSize = 1024 * 1024;

    private ClientWebSocket? m_WebSocket;
    private readonly SemaphoreSlim m_SendSocketSemaphore = new(1);
    private readonly CancellationTokenSource m_CancelSource = new();
    private readonly string m_RegisterEvent = registerEvent;

    /// <summary>
    /// The port used to connect to the StreamDeck websocket
    /// </summary>
    public int Port { get; set; } = port;

    /// <summary>
    /// This is the unique identifier used to communicate with the register StreamDeck plugin.
    /// </summary>
    public string UUID { get; set; } = uuid;

    public event EventHandler<EventArgs> OnConnected = null!;
    public event EventHandler<EventArgs> OnDisconnected = null!;

    public event EventHandler<StreamDeckEventReceivedEventArgs<KeyDownEvent>> OnKeyDown = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<KeyUpEvent>> OnKeyUp = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<WillAppearEvent>> OnWillAppear = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<WillDisappearEvent>> OnWillDisappear = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<TitleParametersDidChangeEvent>> OnTitleParametersDidChange = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<DeviceDidConnectEvent>> OnDeviceDidConnect = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<DeviceDidDisconnectEvent>> OnDeviceDidDisconnect = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<ApplicationDidLaunchEvent>> OnApplicationDidLaunch = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<ApplicationDidTerminateEvent>> OnApplicationDidTerminate = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<SystemDidWakeUpEvent>> OnSystemDidWakeUp = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<DidReceiveSettingsEvent>> OnDidReceiveSettings = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<DidReceiveGlobalSettingsEvent>> OnDidReceiveGlobalSettings = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<PropertyInspectorDidAppearEvent>> OnPropertyInspectorDidAppear = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<PropertyInspectorDidDisappearEvent>> OnPropertyInspectorDidDisappear = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<SendToPluginEvent>> OnSendToPlugin = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<DialRotateEvent>> OnDialRotate = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<DialPressEvent>> OnDialPress = null!;
    public event EventHandler<StreamDeckEventReceivedEventArgs<TouchpadPressEvent>> OnTouchpadPress = null!;

    public void Run()
    {
        if (m_WebSocket == null)
        {
            m_WebSocket = new ClientWebSocket();
            _ = RunAsync();
        }
    }

    public void Stop()
    {
        m_CancelSource.Cancel();
    }

    public Task SetTitleAsync(string title, string context, SDKTarget target, int? state)
    {
        return SendAsync(new SetTitleMessage(title, context, target, state), MessageJsonContext.Default.SetTitleMessage);
    }

    public Task LogMessageAsync(string message)
    {
        return SendAsync(new LogMessage(message), MessageJsonContext.Default.LogMessage);
    }

    public Task SetImageAsync(string base64Image, string context, SDKTarget target, int? state)
    {
        return SendAsync(new SetImageMessage(base64Image, context, target, state), MessageJsonContext.Default.SetImageMessage);
    }

    public Task ShowAlertAsync(string context)
    {
        return SendAsync(new ShowAlertMessage(context), MessageJsonContext.Default.ShowAlertMessage);
    }

    public Task ShowOkAsync(string context)
    {
        return SendAsync(new ShowOkMessage(context), MessageJsonContext.Default.ShowOkMessage);
    }

    public Task SetGlobalSettingsAsync(JsonElement settings)
    {
        return SendAsync(new SetGlobalSettingsMessage(settings, UUID), MessageJsonContext.Default.SetGlobalSettingsMessage);
    }

    public Task GetGlobalSettingsAsync()
    {
        return SendAsync(new GetGlobalSettingsMessage(UUID), MessageJsonContext.Default.GetGlobalSettingsMessage);
    }

    public Task SetSettingsAsync(JsonElement settings, string context)
    {
        return SendAsync(new SetSettingsMessage(settings, context), MessageJsonContext.Default.SetSettingsMessage);
    }

    public Task GetSettingsAsync(string context)
    {
        return SendAsync(new GetSettingsMessage(context), MessageJsonContext.Default.GetSettingsMessage);
    }

    public Task SetStateAsync(uint state, string context)
    {
        return SendAsync(new SetStateMessage(state, context), MessageJsonContext.Default.SetStateMessage);
    }

    public Task SendToPropertyInspectorAsync(string action, JsonElement data, string context)
    {
        return SendAsync(new SendToPropertyInspectorMessage(action, data, context), MessageJsonContext.Default.SendToPropertyInspectorMessage);
    }

    public Task SwitchToProfileAsync(string device, string profileName, string context)
    {
        return SendAsync(new SwitchToProfileMessage(device, profileName, context), MessageJsonContext.Default.SwitchToProfileMessage);
    }
    public Task OpenUrlAsync(string uri)
    {
        return OpenUrlAsync(new Uri(uri));
    }

    public Task OpenUrlAsync(Uri uri)
    {
        return SendAsync(new OpenUrlMessage(uri), MessageJsonContext.Default.OpenUrlMessage);
    }

    public Task SetFeedbackAsync(Dictionary<string, string> dictKeyValues, string context)
    {
        return SendAsync(new SetFeedbackMessage(dictKeyValues, context), MessageJsonContext.Default.SetFeedbackMessage);
    }

    public Task SetFeedbackAsync(JsonElement feedbackPayload, string context)
    {
        return SendAsync(new SetFeedbackMessageEx(feedbackPayload, context), MessageJsonContext.Default.SetFeedbackMessageEx);
    }

    public Task SetFeedbackLayoutAsync(string layout, string context)
    {
        return SendAsync(new SetFeedbackLayoutMessage(layout, context), MessageJsonContext.Default.SetFeedbackLayoutMessage);
    }

    private Task SendAsync(IMessage message, JsonTypeInfo jsonTypeInfo)
    {
        return SendAsync(JsonSerializer.Serialize(message, jsonTypeInfo));
    }

    private async Task SendAsync(string text)
    {
        try
        {
            if (m_WebSocket != null)
            {
                try
                {
                    await m_SendSocketSemaphore.WaitAsync();
                    byte[] buffer = Encoding.UTF8.GetBytes(text);
                    await m_WebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, m_CancelSource.Token);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    m_SendSocketSemaphore.Release();
                }
            }
        }
        catch(Exception)
        {
            await DisconnectAsync();
        }
    }

    private async Task RunAsync()
    {
        try
        {
            _ = m_WebSocket ?? throw new NullReferenceException($"{nameof(m_WebSocket)} is null");

            await m_WebSocket.ConnectAsync(new Uri($"ws://localhost:{Port}"), m_CancelSource.Token);
            if (m_WebSocket.State != WebSocketState.Open)
            {
                await DisconnectAsync();
                return;
            }

            await SendAsync(new RegisterEventMessage(m_RegisterEvent, UUID), MessageJsonContext.Default.RegisterEventMessage);

            OnConnected?.Invoke(this, new EventArgs());
            await ReceiveAsync();
        }
        catch (Exception) {
            throw;
        }
        finally
        {
            await DisconnectAsync();
        }
    }

    private async Task<WebSocketCloseStatus> ReceiveAsync()
    {
        byte[] buffer = new byte[BufferSize];
        ArraySegment<byte> arrayBuffer = new(buffer);
        StringBuilder textBuffer = new(BufferSize);

        while (!m_CancelSource.IsCancellationRequested && m_WebSocket != null)
        {
            WebSocketReceiveResult result = await m_WebSocket.ReceiveAsync(arrayBuffer, m_CancelSource.Token);

            if (result != null)
            {
                if (result.MessageType == WebSocketMessageType.Close ||
                    (result.CloseStatus != null && result.CloseStatus.HasValue && result.CloseStatus.Value != WebSocketCloseStatus.Empty))
                {
                    return result.CloseStatus.GetValueOrDefault();
                }
                else if (result.MessageType == WebSocketMessageType.Text)
                {
                    textBuffer.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
                    if (result.EndOfMessage)
                    {
                        var evt = BaseEvent.Parse(textBuffer.ToString());
                        if (evt != null)
                        {
                            switch (evt.Event)
                            {
                                case EventTypes.KeyDown: OnKeyDown?.Invoke(this, new StreamDeckEventReceivedEventArgs<KeyDownEvent>((KeyDownEvent)evt)); break;
                                case EventTypes.KeyUp: OnKeyUp?.Invoke(this, new StreamDeckEventReceivedEventArgs<KeyUpEvent>((KeyUpEvent)evt)); break;
                                case EventTypes.WillAppear: OnWillAppear?.Invoke(this, new StreamDeckEventReceivedEventArgs<WillAppearEvent>((WillAppearEvent)evt)); break;
                                case EventTypes.WillDisappear: OnWillDisappear?.Invoke(this, new StreamDeckEventReceivedEventArgs<WillDisappearEvent>((WillDisappearEvent)evt)); break;
                                case EventTypes.TitleParametersDidChange: OnTitleParametersDidChange?.Invoke(this, new StreamDeckEventReceivedEventArgs<TitleParametersDidChangeEvent>((TitleParametersDidChangeEvent)evt)); break;
                                case EventTypes.DeviceDidConnect: OnDeviceDidConnect?.Invoke(this, new StreamDeckEventReceivedEventArgs<DeviceDidConnectEvent>((DeviceDidConnectEvent)evt)); break;
                                case EventTypes.DeviceDidDisconnect: OnDeviceDidDisconnect?.Invoke(this, new StreamDeckEventReceivedEventArgs<DeviceDidDisconnectEvent>((DeviceDidDisconnectEvent)evt)); break;
                                case EventTypes.ApplicationDidLaunch: OnApplicationDidLaunch?.Invoke(this, new StreamDeckEventReceivedEventArgs<ApplicationDidLaunchEvent>((ApplicationDidLaunchEvent)evt)); break;
                                case EventTypes.ApplicationDidTerminate: OnApplicationDidTerminate?.Invoke(this, new StreamDeckEventReceivedEventArgs<ApplicationDidTerminateEvent>((ApplicationDidTerminateEvent)evt)); break;
                                case EventTypes.SystemDidWakeUp: OnSystemDidWakeUp?.Invoke(this, new StreamDeckEventReceivedEventArgs<SystemDidWakeUpEvent>((SystemDidWakeUpEvent)evt)); break;
                                case EventTypes.DidReceiveSettings: OnDidReceiveSettings?.Invoke(this, new StreamDeckEventReceivedEventArgs<DidReceiveSettingsEvent>((DidReceiveSettingsEvent)evt)); break;
                                case EventTypes.DidReceiveGlobalSettings: OnDidReceiveGlobalSettings?.Invoke(this, new StreamDeckEventReceivedEventArgs<DidReceiveGlobalSettingsEvent>((DidReceiveGlobalSettingsEvent)evt)); break;
                                case EventTypes.PropertyInspectorDidAppear: OnPropertyInspectorDidAppear?.Invoke(this, new StreamDeckEventReceivedEventArgs<PropertyInspectorDidAppearEvent>((PropertyInspectorDidAppearEvent)evt)); break;
                                case EventTypes.PropertyInspectorDidDisappear: OnPropertyInspectorDidDisappear?.Invoke(this, new StreamDeckEventReceivedEventArgs<PropertyInspectorDidDisappearEvent>((PropertyInspectorDidDisappearEvent)evt)); break;
                                case EventTypes.SendToPlugin: OnSendToPlugin?.Invoke(this, new StreamDeckEventReceivedEventArgs<SendToPluginEvent>((SendToPluginEvent)evt)); break;
                                case EventTypes.DialRotate: OnDialRotate?.Invoke(this, new StreamDeckEventReceivedEventArgs<DialRotateEvent>((DialRotateEvent)evt)); break;
                                case EventTypes.DialPress: OnDialPress?.Invoke(this, new StreamDeckEventReceivedEventArgs<DialPressEvent>((DialPressEvent)evt)); break;
                                case EventTypes.TouchpadPress: OnTouchpadPress?.Invoke(this, new StreamDeckEventReceivedEventArgs<TouchpadPressEvent>((TouchpadPressEvent)evt)); break;
                            }
                        }
                        else
                        {
                            // Consider logging or throwing an error here
                        }

                        textBuffer.Clear();
                    }
                }
            }
        }

        return WebSocketCloseStatus.NormalClosure;
    }

    private async Task DisconnectAsync()
    {
        if (m_WebSocket != null)
        {
            ClientWebSocket socket = m_WebSocket;
            m_WebSocket = null;

            try
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnecting", m_CancelSource.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            try
            {
                socket.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            OnDisconnected?.Invoke(this, new EventArgs());
        }
    }
}
