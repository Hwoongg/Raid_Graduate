using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class TimeRuleView : MonoBehaviour, IPunObservable
{
    TimeRule TimeRuleCache;

    void Start()
    {
        TimeRuleCache = GetComponent<TimeRule>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(TimeRuleCache.remainingTime);
            stream.SendNext(TimeRuleCache.initialRemainingTime);
            stream.SendNext(TimeRuleCache.elapsedTime);
        }
        else
        {
            TimeRuleCache.remainingTime = (float)stream.ReceiveNext();
            TimeRuleCache.initialRemainingTime = (float)stream.ReceiveNext();
            TimeRuleCache.elapsedTime = (float)stream.ReceiveNext();
        }
    }
}
