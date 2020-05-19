using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfxFaceRotate : MonoBehaviour
{
    // 카메라 오브젝트 트랜스폼
    Transform CamTransform;

    // 현재 이펙트 트랜스폼
    Transform EfxTransform;


    // Start is called before the first frame update
    void Start()
    {
        EfxTransform = GetComponent<Transform>();
        CamTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        TurnEffect();
    }

    void TurnEffect()
    {
        //
        // 월드공간 기준으로 연산됨에 유의합니다.
        //


        // 카메라가 이펙트를 바라보는 방향벡터 생성
        Vector3 EyeDir = EfxTransform.position - CamTransform.position;
        EyeDir.Normalize();
        

        // 시선과 총구방향을 외적
        Vector3 CrossVector = Vector3.Cross(EyeDir, EfxTransform.right);

        // 나온 외적축과 현재 이펙트의 면방향(Y축) 과의 각도를 라디안으로 산출
        float Angle = Vector3.Angle(EfxTransform.up, CrossVector);

        // 이펙트의 x축 기준으로 회전하는 쿼터니언 생성
        var Rot = Quaternion.AngleAxis(Angle, EfxTransform.right) * EfxTransform.rotation;

        EfxTransform.rotation = Rot;
    }
}
