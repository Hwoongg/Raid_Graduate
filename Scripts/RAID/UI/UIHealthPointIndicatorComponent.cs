using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPointIndicatorComponent : MonoBehaviour, ILogicEvent
{
    [SerializeField, Header("IT IS READ-ONLY")] float InitialHealthPoint;
    Text HealthPointText;
    EventSet EventSet;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        HealthPointText = GetComponent<Text>();
        Dbg.LogCheckAssigned(HealthPointText);
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void ILogicEvent.OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_HEALTH_POINT_CHANGED:
                if (Utils.IsValid(HealthPointText))
                {
                    HealthPointText.text = $"{obj[0].ToString()}%";
                }
                break;
        }
    }
};
