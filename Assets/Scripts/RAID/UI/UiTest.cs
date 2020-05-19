using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BroadcastMessage("OnShotFired");
        }
    }
};
