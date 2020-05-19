using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailControll : MonoBehaviour
{
    NewController playerController;
    TrailRenderer trail;
    GameObject trailObject;

    private void Awake()
    {
        playerController = GetComponentInParent<NewController>();
        trail = GetComponentInChildren<TrailRenderer>();
        trailObject = trail.gameObject;
        trailObject.SetActive(true);
    }
    
    //void Update()
    //{
    //    if (playerController.mode == NewController.Mode.JET)
    //        trailObject.SetActive(true);
    //    else
    //        trailObject.SetActive(false);
    //}
}
