using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
public class OutlineController : MonoBehaviour
{
    [MenuItem("Raid_Controller/Set All of the outline width to thin")]  
    public static void __SetOutlineWidthThin()
    {
        __SetOutlineWidhBase(1.5f);
    }

    [MenuItem("Raid_Controller/Set All of the outline width to normal")]
    public static void __SetOutlineWidthNormal()
    {
        __SetOutlineWidhBase(3.0f);
    }

    [MenuItem("Raid_Controller/Set All of the ouline width to thick")]
    public static void __SetOutlineWidthThick()
    {
        __SetOutlineWidhBase(5.0f);
    }

    public static void __SetOutlineWidhBase(float width)
    {
        var arrStaticGo = FindObjectsOfType<MeshFilter>();
        int len = 0;
        
        foreach (var e in arrStaticGo)
        {
            var mats = e.GetComponent<MeshRenderer>().materials;
            for (int i = 0; i < mats.Length; ++i)
            {
                mats[i].SetFloat("_OutlineWidth", width);
                ++len;
            }
        }
        Debug.Log($"{len.ToString()} of Targets changed the width of outline to thin!");
    }
};
#endif
