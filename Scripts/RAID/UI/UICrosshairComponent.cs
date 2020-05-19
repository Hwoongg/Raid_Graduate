using System.Collections;
using UnityEngine;

public class UICrosshairComponent : MonoBehaviour, ILogicEvent
{
    public enum eCrosshairType : ushort
    {
        BAR,
        CIRCLE,
    };

    [SerializeField] eCrosshairType CrosshairType = eCrosshairType.BAR;
    public eCrosshairType crosshairType { get { return CrosshairType; } set { CrosshairType = value; } }

    internal enum ePushDirection : ushort
    {
        NORTH,
        EAST,
        WEST,
        SOUTH
    };

    [SerializeField] ePushDirection PushingDirection;

    EventSet EventSet;

    Vector2 OriginalPosition;

    RectTransform RectTf;
    float SpreadStep = 2.0f;
    int FireCounter = 0;
    bool IsFiring = false;
    Vector2 MovingDir;

    Coroutine[] CrosshairCoroutines = new Coroutine[2];
    
    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_SYSTEM | eEventType.FOR_PLAYER, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        RectTf = GetComponent<RectTransform>();
        OriginalPosition = new Vector2(RectTf.anchoredPosition.x, RectTf.anchoredPosition.y);
        switch (PushingDirection)
        {
            case ePushDirection.NORTH:
                MovingDir = Vector2.up;
                break;

            case ePushDirection.EAST:
                MovingDir = Vector2.right;
                break;

            case ePushDirection.WEST:
                MovingDir = Vector2.left;
                break;

            case ePushDirection.SOUTH:
                MovingDir = Vector2.down;
                break;
        };
        StartCoroutine(Yielder.GetCoroutine(() => 
        {
            CrosshairCoroutines[0] = StartCoroutine(_SpreadCrosshair());
            CrosshairCoroutines[1] = StartCoroutine(_CloseCrosshair());
        }, 0.5f));

    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_GAME_ENDED:
                StopCoroutine(CrosshairCoroutines[0]);
                StopCoroutine(CrosshairCoroutines[1]);
                break;

            case eEventMessage.ON_SHOT_FIRED:
                FireCounter = Mathf.Max(30, FireCounter + 1);
                break;
        }
    }

    IEnumerator _CloseCrosshair()
    {
        while (true)
        {
            if (Mathf.Abs(RectTf.anchoredPosition.x) > Mathf.Abs(OriginalPosition.x) ||
                Mathf.Abs(RectTf.anchoredPosition.y) > Mathf.Abs(OriginalPosition.y))
            {
                RectTf.anchoredPosition += -MovingDir;
            }
            yield return Yielder.WaitNextFrame;
        }
    }

    IEnumerator _SpreadCrosshair()
    {
        while (true)
        {
            if (FireCounter > 0)
            {
                FireCounter = Mathf.Min(0, FireCounter - 1);
                RectTf.anchoredPosition += MovingDir * SpreadStep;
                yield return Yielder.WaitNextFrame;
            }

            yield return Yielder.WaitUntil(0, () => FireCounter > 0);
        }
    }
};
