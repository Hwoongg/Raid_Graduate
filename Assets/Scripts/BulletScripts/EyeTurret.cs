using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 구체형 포탑에 사용되는 스크립트.
//

[System.Serializable]
public class BulletPoolSet
{
    public ObjectPool BulletPool;
    public float BetweenTime;
    [HideInInspector] public float BetweenTimer;

    BulletPoolSet()
    {
        BetweenTimer = 0;
    }

    public BulletPoolSet(ObjectPool _pool)
    {
        BetweenTimer = 0;
        BulletPool = _pool;
    }
}

public class EyeTurret : MonoBehaviour
{
    

    // //////////////////////////////////////////////
    //
    // 눈알 회전에 관련된 변수들
    [Header("- 눈알 회전에 관련된 변수들")]
    //
    // /////////////////////////////////////////////

    // 회전 속도
    [SerializeField] float RotSpeed = 10.0f;

    // 눈알 오브젝트
    [SerializeField] Transform EyeTransform;

    // 조준 대상
    Transform Target;
    [SerializeField] float FindTime = 5.0f;
    float FindTimer;
    TargetSearcher targetSearcher;

    // /////////////////////////////////////////////
    //
    // 포구 조작에 관련된 변수들
    [Header("- 포구 조작에 관련된 변수들")]
    //
    // /////////////////////////////////////////////

    //// 발사될 포탄 프리셋
    //[SerializeField] BulletSet[] BulletSets; // Turret.cs 의 상단에 정의되어있음
    [SerializeField] BulletPoolSet bulletPoolSet;
    Muzzle[] FirePoints; // 발사 지점 오브젝트에 Muzzle 컴포넌트가 있어야 한다
    [SerializeField] float MuzzleRotSpeed = 30.0f; // 중심 회전 속도
    [SerializeField] float MuzzleAngleSpeed = 30.0f; // 각도 변화 속도
    [SerializeField] float MinAngle = -30.0f;
    [SerializeField] float MaxAngle = 100.0f;
    float angleDirection; // 조절 방향

    // /////////////////////////////////////////////
    //
    // 포탄 발사에 관련된 변수들
    [Header("- 포탄 발사에 관련된 변수들")]
    //
    // /////////////////////////////////////////////
    
    // 포탑 발사 시간
    [SerializeField] float FireTime = 1.0f;
    float FireTimer;

    // 포탑 재장전 시간
    [SerializeField] float ReloadTime = 3.0f;
    float ReloadTimer;

    // 포탑의 현재 상태 변수
    enum State
    {
        WAIT,
        FIRE
    }
    State state;

    BulletColor bulletColor;

    private void Awake()
    {
        FirePoints = GetComponentsInChildren<Muzzle>();
        FindTimer = 0;
        FireTimer = 0;
        ReloadTimer = 0;
        state = State.FIRE;
        angleDirection = 1;
        targetSearcher = GetComponentInChildren<TargetSearcher>();
        bulletColor = BulletColor.RED;
    }
    
    
    void Update()
    {
        FindTimer += Time.deltaTime;

        if (FindTimer > FindTime)
        {
            if(targetSearcher == null)
            {
                FindTargetSelf();
            }
            else
            {
                FindTimer = 0;
                Target = targetSearcher.GetTargetTransform();
            }
        }

        if (Target == null)
            return;

        Quaternion LookDir = Quaternion.LookRotation(Target.position - EyeTransform.position);

        // 회전 전의 EyeTransform의 right 이기 때문에 문제가 발생한다. 사용 불가.
        //LookDir = Quaternion.AngleAxis(-90.0f, EyeTransform.right) * LookDir;

        // 대상을 바라보도록 전환
        EyeTransform.rotation = Quaternion.Slerp(EyeTransform.rotation, LookDir, RotSpeed * Time.deltaTime);

        // 포구 중심점 회전
        RotateMuzzleCenter();

        // 포구 각도 회전
        RotateMuzzleAngle();

        switch (state)
        {
            case State.WAIT:
                WaitStateWork();
                break;

            case State.FIRE:
                FireStateWork();
                break;
        }

    }

    void FindTargetSelf()
    {
        FindTimer = 0;
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Fire()
    {
        //for (int i = 0; i < BulletSets.Length; i++)
        //{
        //    BulletSets[i].BulletTimer += Time.deltaTime;

        //    if (BulletSets[i].BulletTimer > BulletSets[i].ReloadTime)
        //    {
        //        for (int j = 1; j < FirePoints.Length; j++)
        //        {
        //            Instantiate(BulletSets[i].bulletObject, FirePoints[j].transform.position,
        //                FirePoints[j].transform.rotation);
        //        }

        //        BulletSets[i].BulletTimer = 0;
        //    }

        //}


        bulletPoolSet.BetweenTimer += Time.deltaTime;

        if (bulletPoolSet.BetweenTimer > bulletPoolSet.BetweenTime)
        {
            for (int j = 1; j < FirePoints.Length; j++)
            {
                bulletPoolSet.BulletPool.Spawn(FirePoints[j].transform.position,
                        FirePoints[j].transform.rotation, bulletColor);
            }
            ChangeBulletColor();

            bulletPoolSet.BetweenTimer = 0;
        }


    }

    [ContextMenu("Show Muzzle List")]
    void ShowMuzzleList()
    {
        for (int i = 0; i < FirePoints.Length; i++)
            Debug.Log("Muzzle" + i + " : " + FirePoints[i]);
    }

    // 총구 각도를 변화시키는 함수
    void RotateMuzzleAngle()
    {
        
        for (int i=1;i<FirePoints.Length;i++)
        {
            Quaternion rot =
                Quaternion.AngleAxis(MuzzleAngleSpeed * Time.deltaTime * angleDirection,
            FirePoints[i].transform.right);

            FirePoints[i].transform.rotation = rot * FirePoints[i].transform.rotation;
        }

        float nowAngle =
            Vector3.SignedAngle(FirePoints[1].transform.forward,
            FirePoints[0].transform.forward,
            FirePoints[1].transform.right);
        

        if (nowAngle > MaxAngle || nowAngle < MinAngle)
        {
            angleDirection *= -1;
        }

    }
    

    // 총구 중심을 회전시키는 함수
    void RotateMuzzleCenter()
    {
        FirePoints[0].transform.Rotate(0, 0, MuzzleRotSpeed * Time.deltaTime);
    }

    // 쿨돌리다가 발사상태로 활성화 시킴
    void WaitStateWork()
    {
        ReloadTimer += Time.deltaTime;

        if (ReloadTimer > ReloadTime)
        {
            state = State.FIRE;
            FireTimer = 0;
        }
    }

    // 발사하다가 대기상태로 만듬
    void FireStateWork()
    {
        FireTimer += Time.deltaTime;

        Fire();

        if(FireTimer>FireTime)
        {
            state = State.WAIT;
            ReloadTimer = 0;
        }
    }
    
    void ChangeBulletColor()
    {
        if (bulletColor == BulletColor.RED)
            bulletColor = BulletColor.BLUE;
        else
            bulletColor = BulletColor.RED;
    }
}
