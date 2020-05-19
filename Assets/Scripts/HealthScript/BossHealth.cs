using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Boss Body의 Mesh Collider 갱신을 위해 상속받은 스크립트.
//

public class BossHealth : Health
{

    [HideInInspector] public SkinnedMeshRenderer meshRenderer;
    MeshCollider meshCollider;

    [SerializeField] float MeshUpdateTime = 1.0f;
    float MeshUpdateTimer;

    protected override void Awake()
    {
        base.Awake();

        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        meshCollider.sharedMesh = meshRenderer.sharedMesh;


        MeshUpdateTimer = 0;
        //gameObject.transform.SetParent(meshRenderer.rootBone);
    }

    public virtual void Update()
    {
        MeshUpdateTimer += Time.deltaTime;

        if (MeshUpdateTimer > MeshUpdateTime)
            BakeCollider();
    }

    public void BakeCollider()
    {
        MeshUpdateTimer = 0;

        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = colliderMesh;
    }
}
