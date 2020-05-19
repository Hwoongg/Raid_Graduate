using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISplashImageAppearGradually : MonoBehaviour, ILogicEvent
{
    CanvasRenderer Image;
    [SerializeField, Header("Increasing amount of splash alpha value.")]
    float AlphaIncrease = 1.0f;
    
    bool IsFullyAppeared = false;
    EventSet EventSet;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI | eEventType.FOR_SYSTEM, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        Image = GetComponent<CanvasRenderer>();
        Image.SetAlpha(0.0f);
        StartCoroutine(_SplashImageAppear());
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    IEnumerator _SplashImageAppear()
    {
        float alpha = Image.GetAlpha();
        while (alpha <= 1.0f && false == IsFullyAppeared)
        {
            alpha += AlphaIncrease * Time.deltaTime;
            Image.SetAlpha(alpha);
            yield return Yielder.GetCoroutine(0.05f);
        }

        LogicEventListener.Invoke(eEventType.FOR_ALL, eEventMessage.SPLASH_FULLY_APPEARED);
        // 한번만 실행되도록 보장.
        // Make secured this logic executes once.
        if (false == IsFullyAppeared)
        {
            IsFullyAppeared = true;
        }
    }    

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        //
    }
};
