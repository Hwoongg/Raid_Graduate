using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSync : MonoBehaviourPun
{
    public static GameObject LocalPlayerInst;

    private void Start()
    {
        if (photonView.IsMine)
        {
            PlayerSync.LocalPlayerInst = gameObject;
        }
        //DontDestroyOnLoad(gameObject);
    }
}
