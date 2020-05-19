using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class ShaderController : MonoBehaviour
{
    void Start()
    {
        Dbg.LogCheckAssigned(Target, this);

        int arrayLen = 0;
        foreach (var e in Target.GetComponentsInChildren<Outline_AvgNormal>())
        {
            if (Utils.IsValid(e))
            {
                ++arrayLen;
            }
        }
        SkinnedMesh = new SkinnedMeshRenderer[arrayLen];
    }

    


    SkinnedMeshRenderer[] SkinnedMesh;
    [SerializeField] Transform Target;
    [SerializeField] float OutlieWidth = 3;
};
#endif
