using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MEC.Timing))]
public class TimingController : MonoBehaviour
{
    [SerializeField, Header("IT'S READ-ONLY"), Space(20)] MEC.Timing Timing;

    void Awake()
    {
        Timing = GetComponent<MEC.Timing>();
        Dbg.LogCheckAssigned(Timing, this);
        DontDestroyOnLoad(gameObject);
    }
};
