using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class LocationPointerRule : MonoBehaviour, ILogicEvent
{
    EventSet EventSet;
    GameObject LocationPointerPf;

    static float DisposeTimeCounter;
    [SerializeField] float DisposingTime;
    public static bool IsDisposable = false;
    [SerializeField] GameObject PointerPf;
    Transform Player;

    static LocationPointerRule()
    {
        DisposeTimeCounter = 0.0f;
    }

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void Start()
    {
        string displayingString = DisposingTime > 0 ? DisposingTime.ToString() : $"Disposing Time cannot be negative number.";
        Dbg.LogE($"Current Disposing Counter is [{displayingString}]");
        Player = GameObject.Find("LocalPlayer").transform;
    }

    void StartToTrack(Transform tf)
    {
        // Retrieve pooled LocationPointer.
        var pointer = Instantiate(PointerPf, UnityEngine.Random.insideUnitSphere * 5.0f, Quaternion.identity);
        // Get LocationPointer Hard Reference.
        var pointerRef = pointer.GetComponent<UILocationPointer>();
        // 
        var playerRef = GameObject.Find("LocalPlayer").transform;
        pointerRef.Player = playerRef;
        // Set the pointing target. It lasts until the target has been died.
        pointerRef.Target = tf;
        //
        // TODO: Parent of pooled locationPointer must be the LocationPointerAnchor of Player.
        //
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_MISSILE_SPAWNED:
                StartToTrack(obj[0] as Transform);
                break;

            case eEventMessage.ON_BULLET_SPAWNED:
                StartToTrack(obj[0] as Transform);
                break;

            case eEventMessage.ON_OTHER_PLAYER_SPAWNED:
                StartToTrack(obj[0] as Transform);
                break;
        }
    }
}
