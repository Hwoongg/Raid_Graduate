using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public struct EventSet
{
    public eEventType Type;
    public ILogicEvent Event;

    public EventSet(eEventType t, ILogicEvent evt)
    {
        Type = t; Event = evt;
    }
};

/// <summary>
/// Controll all of Logic events.
/// </summary>
public static class LogicEventListener
{
    /// <summary>
    /// All of logic events are stored in here.
    /// </summary>
    public static HashSet<EventSet> logicEventSet { get; set; } = new HashSet<EventSet>();
    /// <summary>
    /// If Listener is invoking, prevent register and unregister.
    /// </summary>
    internal static bool IsInvoking = false;

    public static void RegisterEvent(EventSet evt)
    {
        if (true == IsInvoking)
        {
            return;
        }

        if (Utils.IsNull(evt))
        {
            Dbg.LogE($"Logic Event Tuple is null. Check RegisterEvent()");
        }
        logicEventSet.Add(evt);
    }
    
    public static void UnregisterEvent(EventSet evt)
    {
        if (true == IsInvoking)
        {
            return;
        }

        if (Utils.IsNull(evt))
        {
            Dbg.LogE($"Logic Event Tuple is null. Check RegisterEvent()");
        }

        logicEventSet.Remove(evt);
    }

    public static void Invoke(eEventType type, eEventMessage msg)
    {
        IsInvoking = true;
        var e = logicEventSet.GetEnumerator();
        while (e.MoveNext())
        {
            if (e.Current.Type.HasFlag(type))
            {
                e.Current.Event.OnInvoked(msg, null);
            }
        }
        IsInvoking = false;
    }

    public static void Invoke(eEventType type, eEventMessage msg, params object[] obj)
    {
        IsInvoking = true;
        var e = logicEventSet.GetEnumerator();
        while (e.MoveNext())
        {
            if (e.Current.Type.HasFlag(type))
            {
                e.Current.Event.OnInvoked(msg, obj);
            }
        }
        IsInvoking = false;
    }
};
