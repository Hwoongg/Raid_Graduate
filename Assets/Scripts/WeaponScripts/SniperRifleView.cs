using Photon.Pun;
using UnityEngine;

public class SniperRifleView : MonoBehaviourPun, IPunObservable
{
    [SerializeField] SniperRifle SniperRifleCache;
    public static GameObject LocalPlayerInst { get; set; }

    void Awake()
    {
        if (photonView.IsMine)
        {
            SniperRifleView.LocalPlayerInst = this.gameObject;
        }
    }

    void Start()
    {
        Dbg.LogCheckAssigned(SniperRifleCache, this);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(SniperRifleCache.IsReloading);
            stream.SendNext(SniperRifleCache.IsFiring);
            stream.SendNext(SniperRifleCache.IsEnemyHit);
            stream.SendNext(SniperRifleCache.EnemyHitPoint);
        }
        else
        {
            SniperRifleCache.IsReloading = (bool)stream.ReceiveNext();
            SniperRifleCache.IsFiring = (bool)stream.ReceiveNext();
            SniperRifleCache.IsEnemyHit = (bool)stream.ReceiveNext();
            SniperRifleCache.EnemyHitPoint = (Vector3)stream.ReceiveNext();
        }
    }
}
