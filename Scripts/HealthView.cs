using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HealthView : MonoBehaviourPun, IPunObservable
{
    PlayerHealth PlayerHealth;
    public static GameObject LocalPlayerInst { get; set; }

    void Awake()
    {
        if (photonView.IsMine)
        {
            HealthView.LocalPlayerInst = this.gameObject;
        }
    }

    void Start()
    {
        PlayerHealth = GetComponent<PlayerHealth>();
    }
    /// <summary>
    /// Called by PUN several times per second, so that your script can write and read synchronization data for the PhotonView.
    /// </summary>
    /// <remarks>
    /// This method will be called in scripts that are assigned as Observed component of a PhotonView.<br/>
    /// PhotonNetwork.SerializationRate affects how often this method is called.<br/>
    /// PhotonNetwork.SendRate affects how often packages are sent by this client.<br/>
    /// 
    /// Implmenting this method, you can customize which data a PhotonView regularly synchronizes.
    /// Your code defines what is being sent (content) and how your data is used by receiving clients.
    /// 
    /// Unlike other callbacks, <i>OnPhotonSerializeView only gets called when it is assigned
    /// to a PhotonView</i> as PhotonView.observed script.
    /// 
    /// To make use of this method, the PhotonStream is essential. It will be in "writing mode" on the
    /// client that controls a PhotonView (PhotonStream.IsWriting == true) and in "reading mode" on the
    /// remote clients that just receive that the controlling client sends.
    /// 
    /// If you skip writing any value into the stream, PUN will skip the update. Used carefully, this can
    /// conserve bandwidth and messages (which have a limit per room/second).
    /// 
    /// Note that OnPhotonSerializeView is not called on remote clients when the sender does not send
    /// any update. This can't be used as "x-times per second Update()".
    /// </remarks>
    /// <param name="stream"></param>
    /// <param name="info"></param>
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player -> send the others our data.
            stream.SendNext(PlayerHealth.CurrentHealth);
        }
        else
        {
            PlayerHealth.CurrentHealth = (int)stream.ReceiveNext();
        }
    }
}
