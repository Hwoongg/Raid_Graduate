using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material Data", menuName = "SO as data/Material")]
public class MaterialScriptableObjectPrototype : ScriptableObject, ISerializationCallbackReceiver
{
    #region Methods
    public void OnBeforeSerialize()
    {
        //
    }

    public void OnAfterDeserialize()
    {
        RuntimeName = InitialName;
        RuntimeShaderType = InitialShaderType;
        RuntimeMeshType = InitialMeshType;
        RuntimeAlbedoTexture = InitialAlbedoTexture;
        RuntimeNormalTexture = InitialNormalTexture;
        RuntimeRampTexture = InitialRampTexture;
        RuntimeOutlineColor = InitialOutlineColor;
        RuntimeOutlineWidth = InitialOutlineWidth;
        RuntimeTintColor = InitialTintColor;
        RuntimeMaterialIndex = InitialMaterialIndex;
    }
    #endregion

    #region Fields
    /// <summary>
    /// Identifier of Material part.
    /// </summary>
    public string InitialName;
    [System.NonSerialized]
    public string RuntimeName;

    /// <summary>
    /// Shader type that determines which shaders is applied for.
    /// </summary>
    public enum eShaderType : short
    {
        EYE,
        HAIR,
        NORMAL
    };

    /// <summary>
    /// Shader Type.
    /// </summary>
    public eShaderType InitialShaderType = eShaderType.NORMAL;
    [System.NonSerialized]
    public eShaderType RuntimeShaderType;

    /// <summary>
    /// Mesh type that determines whether mesh types.
    /// </summary>
    public enum eMeshType : short
    {
        STATIC_MESH,
        SKINNED_MESH
    };

    /// <summary>
    /// Mesh type.
    /// </summary>
    public eMeshType InitialMeshType = eMeshType.SKINNED_MESH;
    [System.NonSerialized]
    public eMeshType RuntimeMeshType;

    /// <summary>
    /// Albedo Texture.
    /// </summary>
    public Texture2D InitialAlbedoTexture;
    [System.NonSerialized]
    public Texture2D RuntimeAlbedoTexture;

    /// <summary>
    /// Normal Texture.
    /// </summary>
    public Texture2D InitialNormalTexture;
    [System.NonSerialized]
    public Texture2D RuntimeNormalTexture;

    /// <summary>
    /// Ramp Texture.
    /// </summary>
    public Texture2D InitialRampTexture;
    [System.NonSerialized]
    public Texture2D RuntimeRampTexture;

    /// <summary>
    /// Tint Color.
    /// </summary>
    public Color InitialTintColor = Color.white;
    [System.NonSerialized]
    public Color RuntimeTintColor = Color.white;

    /// <summary>
    /// Outline Color.
    /// </summary>
    public Color InitialOutlineColor = Color.black;
    [System.NonSerialized]
    public Color RuntimeOutlineColor = Color.black;

    /// <summary>
    /// Outline width.
    /// </summary>
    public float InitialOutlineWidth = 0.3f;
    [System.NonSerialized]
    public float RuntimeOutlineWidth;

    public int InitialMaterialIndex = 0;
    [System.NonSerialized]
    public int RuntimeMaterialIndex = 0;
    #endregion
};
