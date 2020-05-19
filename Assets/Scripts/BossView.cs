using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BossView : MonoBehaviourPun, IPunObservable
{
    [SerializeField] Boss BossCache;

    void Start()
    {
        Dbg.LogCheckAssigned(BossCache, this);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(BossCache.bMissileLaunch);
            stream.SendNext(BossCache.bLaunchComplete);
        }
        else
        {
            BossCache.bMissileLaunch = (bool)stream.ReceiveNext();
            BossCache.bLaunchComplete = (bool)stream.ReceiveNext();
        }
    }
}
