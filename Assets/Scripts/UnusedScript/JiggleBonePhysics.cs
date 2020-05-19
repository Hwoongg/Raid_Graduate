using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiggleBonePhysics : MonoBehaviour
{
    /// <summary>
    /// 부모 본의 트랜스폼
    /// </summary>
    Transform parentTransform;
    /// <summary>
    /// 이 스크립트가 포함된 본 오브젝트의 리지드 바디
    /// </summary>
    Rigidbody boneRigidbody;
    /// <summary>
    /// 이전 프레임까지의 부모 본의 위치
    /// </summary>
    Vector3 prevFrameParentPosition = Vector3.zero;
    /// <summary>
    /// 관성 가중치
    /// </summary>
    public float power = 0f;
    /// <summary>
    /// 변경된 위치의 크기의 제한. 제한 값이 너무 크면 이 본이 제대로 따라가지 못해서
    /// 각 관절들이 이상한 위치로 날아가는 문제가 발생할 수 있다.
    /// </summary>
    public float clampDist = 0.03f;

    void Start()
    {
        parentTransform = transform.parent;
        prevFrameParentPosition = parentTransform.position;

        boneRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 delta = (prevFrameParentPosition - parentTransform.position);
        boneRigidbody.AddForce(Vector3.ClampMagnitude(delta, clampDist) * power);

        prevFrameParentPosition = parentTransform.position;
    }
    
}
