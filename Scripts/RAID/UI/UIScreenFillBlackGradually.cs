using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIScreenFillBlackGradually : MonoBehaviour, ILogicEvent
{
    Image Image;
    [SerializeField, Header("Increasing amount of splash alpha value.")]
    float AlphaIncrease = 1.0f;

    bool IsMovingNextScene = false;
    EventSet EventSet;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI | eEventType.FOR_SYSTEM, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        Image = GetComponent<Image>();
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, 0.0f);
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    IEnumerator _ScreenFill()
    {
        float alpha = Image.color.a;
        IsMovingNextScene = false;
        while (alpha <= 1.0f && false == IsMovingNextScene)
        {
            alpha += AlphaIncrease * Time.deltaTime;
            Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, alpha);
            yield return Yielder.GetCoroutine(0.03f);
        }
        IsMovingNextScene = true;
        LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.FADER_FULLY_APPEARED);
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            // 아무키나 눌리면 화면 채우고 다음씬으로 이동하도록 플래그 설정.
            // Set the flag that prevent another update since any key has been pressed.
            case eEventMessage.ON_ANYKEY_PRESSED:
                StartCoroutine(_ScreenFill());
                break;
        }
    }
};
