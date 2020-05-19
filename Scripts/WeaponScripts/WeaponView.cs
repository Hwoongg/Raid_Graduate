using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class WeaponView : MonoBehaviourPun, IPunObservable
{
    [SerializeField] AssaultRifle AssaultRifle;
    [SerializeField] SniperRifle SniperRifle;
    public static GameObject LocalPlayerInst { get; set; }

    void Awake()
    {
        if (photonView.IsMine)
        {
            WeaponView.LocalPlayerInst = this.gameObject;
        }
    }

    void Start()
    {
        Dbg.LogCheckAssigned(AssaultRifle, this);
        Dbg.LogCheckAssigned(SniperRifle, this);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(AssaultRifle.IsReloading);
            stream.SendNext(SniperRifle.IsReloading);
        }
        else
        {
            
            AssaultRifle.IsReloading = (bool)stream.ReceiveNext();
            SniperRifle.IsReloading = (bool)stream.ReceiveNext();
        }
    }
}
