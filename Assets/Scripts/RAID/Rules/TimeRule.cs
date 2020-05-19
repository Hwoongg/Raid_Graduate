using System.Collections;
using UnityEngine;

public class TimeRule : MonoBehaviour
{
    [SerializeField] float InitialRemainingTime = 300.0f;
    public float initialRemainingTime {
        get { return InitialRemainingTime; }
        set { InitialRemainingTime = value; }
    }

    [SerializeField] float RemainingTime;
    public float remainingTime {
        get { return RemainingTime; }
        set { RemainingTime = value; }
    }

    [SerializeField] float ElapsedTime;
    public float elapsedTime {
        get { return ElapsedTime; }
        set { ElapsedTime = value; }
    }

    Coroutine TimeCalculator;

    void Start()
    {
        RemainingTime = Mathf.Max(0.0f, InitialRemainingTime);
        TimeCalculator = StartCoroutine(_UpdateTimeVariables());
    }

    IEnumerator _UpdateTimeVariables()
    {
        while (true)
        {
            if (RemainingTime > 0.0f)
            {
                RemainingTime -= 1.0f;
            }
            else
            {
                Dbg.Log($"Remaining Time is zero! Coroutine finished~", this);
                // TODO: change this coroutine repeatedly turns on and off.
                break;
            }

            ElapsedTime += 1.0f;
            float timeLeftRatio = RemainingTime / InitialRemainingTime;
            LogicEventListener.Invoke(eEventType.FOR_UI, eEventMessage.ON_REMAINING_TIME_CHANGED, (object)timeLeftRatio);
            yield return Yielder.GetCoroutine(1.0f);
        }
    }
};
