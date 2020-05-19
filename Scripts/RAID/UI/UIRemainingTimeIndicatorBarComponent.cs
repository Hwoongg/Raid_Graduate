using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRemainingTimeIndicatorBarComponent : MonoBehaviour, ILogicEvent
{
    Image RemainingTimeBar;
    EventSet EventSet;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        RemainingTimeBar = GetComponent<Image>();
        RemainingTimeBar.fillMethod = Image.FillMethod.Horizontal;
        Dbg.LogCheckAssigned(RemainingTimeBar);
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void ILogicEvent.OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_REMAINING_TIME_CHANGED:
                if (Utils.IsValid(RemainingTimeBar))
                {
                    RemainingTimeBar.SetAllDirty();
                    RemainingTimeBar.fillAmount = (float)obj[0];
                }
                break;
        }
    }
};
