using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletSet
{
    public GameObject bulletObject;
    public float ReloadTime; // 총알 사이 간격 시간
    [HideInInspector] public float BulletTimer; // 총알 사이 간격 체크용

    BulletSet()
    {
        BulletTimer = 0;
    }
}

public class Turret : MonoBehaviour
{
    [SerializeField] Transform Cannon;
    [SerializeField] Transform FirePoint;
    Transform Body;

    Transform Target;

    [SerializeField] float RotationSpeed = 10.0f;
    [SerializeField] float MaxCannonAngle = 180.0f;

    [SerializeField] BulletSet[] BulletSets;


    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Body = GetComponent<Transform>();
    }
    

    void Update()
    {
        BodySpin();
        CannonSpin();
        Fire();
    }


    // 아래 CannonSpin() 을 기반으로 만든 함수. 주석 참고
    void BodySpin()
    {
        Vector3 targetPos = Body.worldToLocalMatrix * Target.position;
        
        targetPos.y = 0;

        targetPos.Normalize();

        float angle = Vector3.SignedAngle(Vector3.forward, targetPos, Vector3.up);

        Quaternion rot = Quaternion.AngleAxis(angle, Body.up) * Body.rotation;

        Body.rotation = Quaternion.Lerp(Body.rotation, rot, RotationSpeed * Time.deltaTime);

    }

    void CannonSpin()
    {
        // 플레이어의 좌표를 현재 터렛 기준으로 해석
        Vector3 targetPos = Cannon.worldToLocalMatrix * Target.position;
        
        // 플레이어를 향하는 방향벡터에서 x성분을 제거
        targetPos.x = 0;

        // 정규화
        targetPos.Normalize();

        // 현재 포신의 방향과 사잇각 계산.
        float angle = Vector3.SignedAngle(Vector3.forward, targetPos, Vector3.right);

        // 회전 사원수 생성. 현재 캐논의 x축 회전을 목표로 한다.
        Quaternion rot = Quaternion.AngleAxis(angle, Cannon.right) * Cannon.rotation;
        

        // 보간 회전
        Cannon.rotation = Quaternion.Lerp(Cannon.rotation, rot, RotationSpeed * Time.deltaTime);
        
    }

    void Fire()
    {
        for(int i=0; i<BulletSets.Length;i++)
        {
            BulletSets[i].BulletTimer += Time.deltaTime;
            if (BulletSets[i].BulletTimer > BulletSets[i].ReloadTime)
            {
                Instantiate(BulletSets[i].bulletObject, FirePoint.position, Cannon.rotation);
                BulletSets[i].BulletTimer = 0;
            }
        }
    }
}
