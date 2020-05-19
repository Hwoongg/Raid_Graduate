using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_AvgNormal : MonoBehaviour
{
    void Start()
    {
#if UNITY_EDITOR
        UnityEngine.Profiling.Profiler.BeginSample("Make Outline");
#endif
        // new Material.
        Material material = default;
        // Part Name.
        GameObject outlineGo = default;
        Mesh normalAveragedMesh = default;
        //CustomDebug.Log("Awake is executed!");

        int len = mMaterialSOArray.Length;
        for (int i = 0; i < len; ++i, ++counter)
        {
            material = default;
            //CustomDebug.Log("material creation is executed!");
            switch (mMaterialSOArray[i].RuntimeShaderType)
            {
                case MaterialScriptableObjectPrototype.eShaderType.EYE:
                    // TODO: Find out which style of shader is proper for eyes.
                    material = new Material(Shader.Find("Custom/Outline_AvgNormal"))
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    Dbg.Log($"<color=red>Eyes shader is applied! Count : {counter.ToString()}</color>", this);
                    break;

                case MaterialScriptableObjectPrototype.eShaderType.HAIR:
                    // TODO: Have to research with hair shader.
                    material = new Material(Shader.Find("Custom/Anisotropic Hair"))
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    Dbg.Log($"<color=red>Hair shader is applied! Count : {counter.ToString()}</color>", this);
                    break;

                case MaterialScriptableObjectPrototype.eShaderType.NORMAL:
                    // TODO: Next level -> Sobel edge detected outline.
                    material = new Material(Shader.Find("Custom/Outline_AvgNormal"))
                    {
                        hideFlags = HideFlags.HideAndDontSave
                    };
                    Dbg.Log($"<color=red>Normal shader is applied! Count : {counter.ToString()}</color>", this);
                    break;

                default:
                    Debug.LogError($"Error", this);
                    break;
            }            

            if (mMaterialSOArray[i].RuntimeMeshType == MaterialScriptableObjectPrototype.eMeshType.STATIC_MESH)
            {
                MakeOutlineGameObjectForStaticMesh(ref material, mMaterialSOArray[i].RuntimeName, ref normalAveragedMesh, out outlineGo);
                ApplyShaderPropertiesForStaticMesh(ref outlineGo, ref material, i);
            }
            else
                if (mMaterialSOArray[i].RuntimeMeshType == MaterialScriptableObjectPrototype.eMeshType.SKINNED_MESH)
            {
                MakeOutlineGameObjectForSkinnedMesh(ref material, mMaterialSOArray[i].RuntimeName, ref normalAveragedMesh, out outlineGo);
                ApplyShaderPropertiesForSkinnedMesh(ref outlineGo, ref material, i);
            }
            //Destroy(outlineGo);
        }
#if UNITY_EDITOR
        UnityEngine.Profiling.Profiler.EndSample();
#endif
    }

    /// <summary>
    /// Make outline gameObject varied from the staticMesh or skinnedMesh. thre reason make a new gameobject is simply
    /// it is much easier to adjust parameters.
    /// </summary>
    /// <param name="newMat"></param>
    /// <param name="partName"></param>
    /// <param name="outGo"></param>
    void MakeOutlineGameObjectForSkinnedMesh(ref Material newMat, string partName, ref Mesh averagedMesh, out GameObject outGo)
    {
        var go = new GameObject(partName + "_Outline");
        go.transform.SetParent(transform);
        
        // If mesh is rigged.
        if (null != GetComponent<SkinnedMeshRenderer>())
        {
            go.AddComponent<SkinnedMeshRenderer>();
            if (false == IsNormalAveraged)
            {
                averagedMesh = Instantiate(GetComponent<SkinnedMeshRenderer>().sharedMesh);
                Dbg.Log($"Vertices Count of {averagedMesh.name} is {averagedMesh.vertexCount}.");
                SmoothingGroupExtension.MeshNormalAverage(ref averagedMesh);
                IsNormalAveraged = true;
            }
            go.GetComponent<SkinnedMeshRenderer>().sharedMesh = averagedMesh;
            go.GetComponent<SkinnedMeshRenderer>().material = newMat;
        }
        go.transform.localPosition = transform.position;
        go.transform.localRotation = transform.rotation;
        go.transform.localScale = transform.localScale;
        outGo = go;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="newMat"></param>
    /// <param name="partName"></param>
    /// <param name="outGo"></param>
    void MakeOutlineGameObjectForStaticMesh(ref Material newMat, string partName, ref Mesh averagedMesh, out GameObject outGo)
    {
        var go = new GameObject(partName + "_Outline");
        go.transform.SetParent(transform);

        if (null != GetComponent<MeshFilter>())
        {
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            if (false == IsNormalAveraged)
            {
                averagedMesh = Instantiate(GetComponent<MeshFilter>().sharedMesh);
                SmoothingGroupExtension.MeshNormalAverage(ref averagedMesh);
                IsNormalAveraged = true;
            }
            go.GetComponent<MeshFilter>().sharedMesh = averagedMesh;
            go.GetComponent<MeshRenderer>().material = newMat;
        }
        go.transform.localPosition = transform.position;
        go.transform.localRotation = transform.rotation;
        go.transform.localScale = transform.localScale;
        outGo = go;
    }

    /// <summary>
    /// Apply material properties including keywords and more.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="newMat"></param>
    void ApplyShaderPropertiesForStaticMesh(ref GameObject go, ref Material newMat, int idx)
    {
        var newMesh = GetComponent<MeshFilter>();
        var body = GetComponent<MeshRenderer>();
        newMesh.sharedMesh = go.GetComponent<MeshFilter>().sharedMesh;
        body.materials[idx] = go.GetComponent<MeshRenderer>().material;
        switch (mMaterialSOArray[idx].RuntimeShaderType)
        {
            case MaterialScriptableObjectPrototype.eShaderType.EYE:
                // Eyes don't need normal mapping.
                body.material.SetFloat("_OutlineWidth", mMaterialSOArray[idx].RuntimeOutlineWidth);
                body.material.SetColor("_OutlineColor", mMaterialSOArray[idx].RuntimeOutlineColor);
                body.material.SetColor("_TintColor", mMaterialSOArray[idx].RuntimeTintColor);
                body.material.SetTexture("_MainTex", mMaterialSOArray[idx].RuntimeAlbedoTexture);
                body.material.SetTexture("_RampTex", mMaterialSOArray[idx].RuntimeRampTexture);
                break;

            case MaterialScriptableObjectPrototype.eShaderType.HAIR:
                body.material.SetFloat("_OutlineWidth", mMaterialSOArray[idx].RuntimeOutlineWidth);
                body.material.SetColor("_OutlineColor", mMaterialSOArray[idx].RuntimeOutlineColor);
                body.material.SetColor("_TintColor", mMaterialSOArray[idx].RuntimeTintColor);
                body.material.SetTexture("_MainTex", mMaterialSOArray[idx].RuntimeAlbedoTexture);
                // TODO: Shader directive didn't work now.  
                body.material.EnableKeyword("__NORMAL_ON__");
                body.material.SetTexture("_NormalTex", mMaterialSOArray[idx].RuntimeNormalTexture);
                body.material.SetTexture("_RampTex", mMaterialSOArray[idx].RuntimeRampTexture);
                break;

            case MaterialScriptableObjectPrototype.eShaderType.NORMAL:
                body.material.SetFloat("_OutlineWidth", mMaterialSOArray[idx].RuntimeOutlineWidth);
                body.material.SetColor("_OutlineColor", mMaterialSOArray[idx].RuntimeOutlineColor);
                body.material.SetColor("_TintColor", mMaterialSOArray[idx].RuntimeTintColor);
                body.material.SetTexture("_MainTex", mMaterialSOArray[idx].RuntimeAlbedoTexture);
                body.material.EnableKeyword("__NORMAL_ON__");
                body.material.SetTexture("_NormalTex", mMaterialSOArray[idx].RuntimeNormalTexture);
                body.material.SetTexture("_RampTex", mMaterialSOArray[idx].RuntimeRampTexture);
                break;
        }
    }

    void ApplyShaderPropertiesForSkinnedMesh(ref GameObject go, ref Material newMat, int idx)
    {
        var body = GetComponent<SkinnedMeshRenderer>();
        body.materials[idx] = go.GetComponent<SkinnedMeshRenderer>().material;

        switch (mMaterialSOArray[idx].RuntimeShaderType)
        {
            case MaterialScriptableObjectPrototype.eShaderType.EYE:
                // Eyes don't need normal mapping.
                body.material.SetFloat("_OutlineWidth", mMaterialSOArray[idx].RuntimeOutlineWidth);
                body.material.SetColor("_OutlineColor", mMaterialSOArray[idx].RuntimeOutlineColor);
                body.material.SetColor("_TintColor", mMaterialSOArray[idx].RuntimeTintColor);
                body.material.SetTexture("_MainTex", mMaterialSOArray[idx].RuntimeAlbedoTexture);
                body.material.SetTexture("_RampTex", mMaterialSOArray[idx].RuntimeRampTexture);
                break;

            case MaterialScriptableObjectPrototype.eShaderType.HAIR:
                body.material.SetFloat("_OutlineWidth", mMaterialSOArray[idx].RuntimeOutlineWidth);
                body.material.SetColor("_OutlineColor", mMaterialSOArray[idx].RuntimeOutlineColor);
                body.material.SetColor("_TintColor", mMaterialSOArray[idx].RuntimeTintColor);
                body.material.SetTexture("_MainTex", mMaterialSOArray[idx].RuntimeAlbedoTexture);
                // TODO: Shader directive didn't work now.  
                //body.sharedMaterial.EnableKeyword("__NORMAL_ON__");
                body.material.SetTexture("_NormalTex", mMaterialSOArray[idx].RuntimeNormalTexture);
                body.material.SetTexture("_RampTex", mMaterialSOArray[idx].RuntimeRampTexture);
                break;

            case MaterialScriptableObjectPrototype.eShaderType.NORMAL:
                body.material.SetFloat("_OutlineWidth", mMaterialSOArray[idx].RuntimeOutlineWidth);
                body.material.SetColor("_OutlineColor", mMaterialSOArray[idx].RuntimeOutlineColor);
                body.material.SetColor("_TintColor", mMaterialSOArray[idx].RuntimeTintColor);
                body.material.SetTexture("_MainTex", mMaterialSOArray[idx].RuntimeAlbedoTexture);
                //body.sharedMaterial.EnableKeyword("__NORMAL_ON__");
                body.material.SetTexture("_NormalTex", mMaterialSOArray[idx].RuntimeNormalTexture);
                body.material.SetTexture("_RampTex", mMaterialSOArray[idx].RuntimeRampTexture);
                break;
        }
    }

    /// <summary>
    /// Material array for storing the new outline material.
    /// </summary>
    [SerializeField] MaterialScriptableObjectPrototype[] mMaterialSOArray;
    /// <summary>
    /// 
    /// </summary>
    static int counter = 1;
    /// <summary>
    /// 
    /// </summary>
    bool IsNormalAveraged = false;
};
