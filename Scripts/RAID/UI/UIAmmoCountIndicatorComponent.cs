using UnityEngine;
using UnityEngine.UI;

public class UIAmmoCountIndicatorComponent : MonoBehaviour, ILogicEvent
{
    [SerializeField, Header("IT IS READ-ONLY")] int CurrentAmmoCount;
    [SerializeField, Header("IT IS READ-ONLY"), Space(20)] int InitialAmmoCount;
    [SerializeField, Header("IT IS READ-ONLY"), Space(20)] bool IsInfinity = false;
    Text AmmoCountText;
    EventSet EventSet;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        AmmoCountText = GetComponent<Text>();
        Dbg.LogCheckAssigned(AmmoCountText);
    }

    void Disable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void ILogicEvent.OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_AMMUNITION_COUNT_CHANGED:
                if (Utils.IsValid(AmmoCountText))
                {
                    int currentAmmo = (int)obj[0];
                    CurrentAmmoCount = Mathf.Max(0, currentAmmo);
                    AmmoCountText.text = $"{currentAmmo.ToString()}/{obj[1].ToString()}";
                }
                break;

            case eEventMessage.ON_AMMUNITION_INFINITY_SKILL_ACTIVATED:
            {
                //IsInfinity = isInf;
                if (Utils.IsValid(AmmoCountText))
                {

                }
            }
            break;
        }
    }
};
