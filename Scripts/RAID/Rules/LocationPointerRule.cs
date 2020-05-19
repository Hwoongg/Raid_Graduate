using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationPointerRule : MonoBehaviour, ILogicEvent
{
    EventSet EventSet;
    GameObject LocationPointerPf;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_SYSTEM, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }
    
    void StartToTrack(Transform tf)
    {
        var pointer = Pooler.PoolInstance.GetPooledObject("LocationPointer");
        var pointerRef = pointer.GetComponent<UILocationPointer>();
        pointerRef.Player = GameObject.Find("Player").transform;
        pointerRef.Target = tf;
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_MISSILE_SPAWNED:
                if (string.Equals("MISSILE", (string)obj[1]))
                {
                    StartToTrack((Transform)obj[0]);
                }
                break;

            case eEventMessage.ON_OTHER_PLAYER_SPAWNED:
                if (string.Equals("OTHER_PLAYER", (string)obj[1]))
                {
                    StartToTrack((Transform)obj[0]);
                }
                break;
        }
    }
}
