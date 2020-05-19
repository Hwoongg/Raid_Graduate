using Photon.Pun;
using UnityEngine;

public class AssaultRifleView : MonoBehaviourPun, IPunObservable
{
    [SerializeField] AssaultRifle AssaultRifleCache;
    [SerializeField] SniperRifle SniperRifleCache;
    public static GameObject LocalPlayerInst { get; set; }

    void Awake()
    {
        if (photonView.IsMine)
        {
            AssaultRifleView.LocalPlayerInst = this.gameObject;
        }
    }

    void Start()
    {
        Dbg.LogCheckAssigned(AssaultRifleCache, this);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(AssaultRifleCache.IsReloading);
            stream.SendNext(AssaultRifleCache.IsFiring);
            stream.SendNext(AssaultRifleCache.IsEnemyHit);
            stream.SendNext(AssaultRifleCache.EnemyHitPoint);
            stream.SendNext(SniperRifleCache.IsReloading);
            stream.SendNext(SniperRifleCache.IsFiring);
            stream.SendNext(SniperRifleCache.IsEnemyHit);
            stream.SendNext(SniperRifleCache.EnemyHitPoint);
        }
        else
        {
            AssaultRifleCache.IsReloading = (bool)stream.ReceiveNext();
            AssaultRifleCache.IsFiring = (bool)stream.ReceiveNext();
            AssaultRifleCache.IsEnemyHit = (bool)stream.ReceiveNext();
            AssaultRifleCache.EnemyHitPoint = (Vector3)stream.ReceiveNext();
            SniperRifleCache.IsReloading = (bool)stream.ReceiveNext();
            SniperRifleCache.IsFiring = (bool)stream.ReceiveNext();
            SniperRifleCache.IsEnemyHit = (bool)stream.ReceiveNext();
            SniperRifleCache.EnemyHitPoint = (Vector3)stream.ReceiveNext();
        }
    }
}
