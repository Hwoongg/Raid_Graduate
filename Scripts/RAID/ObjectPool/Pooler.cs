using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public int AmountToPool;
    [Space(10)]
    public GameObject PoolingTarget;
    [Header("If there's nothing that is able to reuse from the pooled list, Create new one immediately."), Space(10)]
    public bool ShouldExpand;
}

public class Pooler : MonoBehaviour
{
    /// <summary>
    /// Static Instance to access easily. (Singleton).
    /// </summary>
    public static Pooler PoolInstance;
    /// <summary>
    /// Object list to pool. (Edit on Editor)
    /// </summary>
    [SerializeField] List<PoolItem> PoolTargets;
    /// <summary>
    /// Pooled object actually used at runtime.
    /// </summary>
    List<GameObject> PooledObjects = new List<GameObject>();

    void Awake()
    {
        if (Utils.IsNull(PoolInstance))
        {
            PoolInstance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        int len = PoolTargets.Count;
        for (int i = 0; i < len; ++i)
        {
            var PoolObj = PoolTargets[i];
            var newGo = Instantiate<GameObject>(PoolObj.PoolingTarget);
            newGo.SetActive(false);
            PooledObjects.Add(newGo);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        int len = PooledObjects.Count;
        for (int i = 0; i < len; ++i)
        {
            var forwarded = PooledObjects[i];
            if (!forwarded.activeInHierarchy && !forwarded.activeSelf && forwarded.CompareTag(tag))
            {
                return forwarded;
            }
        }

        len = PoolTargets.Count;
        for (int i = 0; i < len; ++i)
        {
            var forwarded = PoolTargets[i];
            if (forwarded.PoolingTarget.CompareTag(tag) && forwarded.ShouldExpand)
            {
                var created = Instantiate<GameObject>(forwarded.PoolingTarget);
                created.SetActive(false);
                PooledObjects.Add(created);
                return created;
            }
        }
        return null;
    }
}
