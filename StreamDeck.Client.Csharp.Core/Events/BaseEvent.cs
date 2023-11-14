using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace StreamDeck.Client.Csharp.Core.Events;

public static class EventTypes
{
    public const string KeyDown = "keyDown";
    public const string KeyUp = "keyUp";
    public const string WillAppear = "willAppear";
    public const string WillDisappear = "willDisappear";
    public const string TitleParametersDidChange = "titleParametersDidChange";
    public const string DeviceDidConnect = "deviceDidConnect";
    public const string DeviceDidDisconnect = "deviceDidDisconnect";
    public const string ApplicationDidLaunch = "applicationDidLaunch";
    public const string ApplicationDidTerminate = "applicationDidTerminate";
    public const string SystemDidWakeUp = "systemDidWakeUp";
    public const string DidReceiveSettings = "didReceiveSettings";
    public const string DidReceiveGlobalSettings = "didReceiveGlobalSettings";
    public const string PropertyInspectorDidAppear = "propertyInspectorDidAppear";
    public const string PropertyInspectorDidDisappear = "propertyInspectorDidDisappear";
    public const string SendToPlugin = "sendToPlugin";
    public const string DialRotate = "dialRotate";
    public const string DialPress = "dialPress";
    public const string TouchpadPress = "touchTap";
}

public abstract class BaseEvent
{
    private static readonly Dictionary<string, JsonTypeInfo> s_EventMap = new()
    {
        { EventTypes.KeyDown, EventJsonContext.Default.KeyDownEvent },
        { EventTypes.KeyUp, EventJsonContext.Default.KeyUpEvent },

        { EventTypes.WillAppear, EventJsonContext.Default.WillAppearEvent },
        { EventTypes.WillDisappear, EventJsonContext.Default.WillDisappearEvent },

        { EventTypes.TitleParametersDidChange, EventJsonContext.Default.TitleParametersDidChangeEvent },

        { EventTypes.DeviceDidConnect, EventJsonContext.Default.DeviceDidConnectEvent },
        { EventTypes.DeviceDidDisconnect, EventJsonContext.Default.DeviceDidDisconnectEvent },

        { EventTypes.ApplicationDidLaunch, EventJsonContext.Default.ApplicationDidLaunchEvent },
        { EventTypes.ApplicationDidTerminate, EventJsonContext.Default.ApplicationDidTerminateEvent },

        { EventTypes.SystemDidWakeUp, EventJsonContext.Default.SystemDidWakeUpEvent },

        { EventTypes.DidReceiveSettings, EventJsonContext.Default.DidReceiveSettingsEvent },
        { EventTypes.DidReceiveGlobalSettings, EventJsonContext.Default.DidReceiveGlobalSettingsEvent },

        { EventTypes.PropertyInspectorDidAppear, EventJsonContext.Default.PropertyInspectorDidAppearEvent },
        { EventTypes.PropertyInspectorDidDisappear, EventJsonContext.Default.PropertyInspectorDidDisappearEvent },

        { EventTypes.SendToPlugin, EventJsonContext.Default.SendToPluginEvent },

        { EventTypes.DialRotate, EventJsonContext.Default.DialRotateEvent },
        { EventTypes.DialPress, EventJsonContext.Default.DialPressEvent },
        { EventTypes.TouchpadPress, EventJsonContext.Default.TouchpadPressEvent },
    };

    [JsonPropertyName("event")]
    public string Event { get; set; } = null!;

    internal static BaseEvent? Parse(string json)
    {
        using JsonDocument doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;

        // Check if the key "event" exists
        if (!root.TryGetProperty("event", out JsonElement eventElement))
        {
            return null;
        }

        var eventType = eventElement.GetString();
        if (eventType is null || !s_EventMap.TryGetValue(eventType, out JsonTypeInfo? jsonTypeInfo))
        {
            return null;
        }

        return JsonSerializer.Deserialize(json, jsonTypeInfo) as BaseEvent;
    }
}

[JsonSerializable(typeof(DeviceDidConnectEvent))]
[JsonSerializable(typeof(KeyDownEvent))]
[JsonSerializable(typeof(KeyUpEvent))]
[JsonSerializable(typeof(WillAppearEvent))]
[JsonSerializable(typeof(WillDisappearEvent))]
[JsonSerializable(typeof(TitleParametersDidChangeEvent))]
[JsonSerializable(typeof(DeviceDidDisconnectEvent))]
[JsonSerializable(typeof(ApplicationDidLaunchEvent))]
[JsonSerializable(typeof(ApplicationDidTerminateEvent))]
[JsonSerializable(typeof(SystemDidWakeUpEvent))]
[JsonSerializable(typeof(DidReceiveSettingsEvent))]
[JsonSerializable(typeof(DidReceiveGlobalSettingsEvent))]
[JsonSerializable(typeof(PropertyInspectorDidAppearEvent))]
[JsonSerializable(typeof(PropertyInspectorDidDisappearEvent))]
[JsonSerializable(typeof(SendToPluginEvent))]
[JsonSerializable(typeof(DialRotateEvent))]
[JsonSerializable(typeof(DialPressEvent))]
[JsonSerializable(typeof(TouchpadPressEvent))]
internal partial class EventJsonContext : JsonSerializerContext
{
}
