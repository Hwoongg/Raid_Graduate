using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPressAnyKeysFlickerGradually : MonoBehaviour, ILogicEvent
{
    [SerializeField] CanvasRenderer PressAnyKeys;
    [SerializeField] CanvasRenderer PressAnyKeysTransparent;

    [SerializeField, Header("Decreasing amount of PressAndKey Texts")] float AlphaDescrease = 0.03f;
    [SerializeField, Header("Increasing amount of PressAndKey Texts"), Space(20)] float AlphaIncrease = 0.03f;

    EventSet EventSet;
    bool IsMovingNextScene = false;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI | eEventType.FOR_SYSTEM, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        Dbg.LogCheckAssigned(PressAnyKeys, this);
        Dbg.LogCheckAssigned(PressAnyKeysTransparent, this);
        PressAnyKeys.SetAlpha(1.0f);
        PressAnyKeysTransparent.SetAlpha(1.0f);
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void Update()
    {
        if (false == IsMovingNextScene)
        {
            float alpha = PressAnyKeys.GetAlpha();
            while (alpha >= 0.0f)
            {
                alpha -= AlphaDescrease * Time.deltaTime;
                PressAnyKeys.SetAlpha(alpha);
                PressAnyKeysTransparent.SetAlpha(alpha);
            }

            while (alpha <= 1.0)
            {
                alpha += AlphaIncrease * Time.deltaTime;
                PressAnyKeys.SetAlpha(alpha);
                PressAnyKeysTransparent.SetAlpha(alpha);
            }
        }
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            //case eEventMessage.ON_ANYKEY_PRESSED:
            //    IsMovingNextScene = true;
            //    break;
        }
    }
};
