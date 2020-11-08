using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, ILogicEvent
{
    EventSet EventSetCache;
    float timer;
    // **
    public bool bMissileLaunch { get; set; }
    // **
    public bool bLaunchComplete { get; set; }

    Animator animator;

    [SerializeField] GameObject objMissile;

    [SerializeField] Transform[] LeftFireTransform;
    [SerializeField] Transform[] RightFireTransform;

    Health BossHealth;

    public List<Transform> PlayerTfList { get; set; } = new List<Transform>();

    Timer missileTimer;
    Timer mineTimer;
    [SerializeField] GameObject minePrefab;
    [SerializeField] Transform minePoint;
    [SerializeField] BossDeadEFX deadEFX;

    bool deadEffect = false;

    Timer deadTimer;
    void OnEnable()
    {
        EventSetCache = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSetCache);
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSetCache);
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_OTHER_PLAYER_SPAWNED:
                PlayerTfList.Add(obj[0] as Transform);
                break;
        }
    }

    void Start()
    {
        timer = 0f;
        bMissileLaunch = false;
        bLaunchComplete = false;
        animator = GetComponent<Animator>();
        BossHealth = GetComponentInChildren<Health>();
        PlayerTfList.Add(GameObject.Find("LocalPlayer").transform);
        missileTimer = new Timer(45.0f);
        mineTimer = new Timer(30.0f);
        deadTimer = new Timer(5.0f);
    }

    void Update()
    {
        // 사망 처리
        if (BossHealth.isDead)
        {
            if (!deadEffect)
            {
                deadEFX.gameObject.SetActive(true);
                deadEffect = true;
            }
            else
            {
                deadTimer.Update();
                if (deadTimer.IsTimeOver())
                {
                    deadEFX.LastExplotion();
                    gameObject.SetActive(false);
                    FindObjectOfType<Stage1Rule>().StageClear();
                }
            }
        }
        else
        {
            #region Cheat Commands
            //if(Input.GetKeyDown(KeyCode.K))
            //{
            //    BossHealth.isDead = true;
            //}
            //if (Input.GetKeyDown(KeyCode.Slash) && !bMissileLaunch)
            //{
            //    bMissileLaunch = true;
            //}
            //if(Input.GetKeyDown(KeyCode.Period))
            //{
            //    MineSpread();
            //}
            #endregion

            missileTimer.Update();
            if (missileTimer.IsTimeOver())
            {
                bMissileLaunch = true;
            }

            mineTimer.Update();
            if (mineTimer.IsTimeOver())
            {
                MineSpread();
            }
            
            if (bMissileLaunch)
            {
                timer += Time.deltaTime;

                animator.SetBool("OpenSilo", true);

                if (timer > 3.0f)
                {
                    if (!bLaunchComplete)
                    {
                        StartCoroutine("LaunchMissileThread");
                        bLaunchComplete = true;
                    }

                }
                if (timer > 7.0f)
                {
                    animator.SetBool("OpenSilo", false);
                }

                if (timer > 10.0f)
                {
                    bMissileLaunch = false;
                    bLaunchComplete = false;
                    timer = 0;
                }
            }
        }

        
    }

    IEnumerator LaunchMissileThread()
    {
        float[] dists = new float[3];
        int len = LeftFireTransform.Length;
        for (int i = 0; i < len; i++)
        {
            Transform res = default;
            float minDist = 0.0f;
            var leftMissile = Instantiate(objMissile, LeftFireTransform[i].position, LeftFireTransform[i].rotation);
            for (int j = 0; j < PlayerTfList.Count; ++j)
            {
                dists[j] = (leftMissile.transform.position - PlayerTfList[j].position).magnitude;
            }

            for (int k = 0; k < PlayerTfList.Count; ++k)
            {
                if (dists[k] > minDist)
                {
                    minDist = dists[k];
                    res = PlayerTfList[k];
                }
            }

            leftMissile.GetComponent<Missile>().Target = res;

            LogicEventListener.Invoke(eEventType.FOR_ALL, eEventMessage.ON_MISSILE_SPAWNED, LeftFireTransform[i]);

            var rightMissile = Instantiate(objMissile, RightFireTransform[i].position, RightFireTransform[i].rotation);
            for (int j = 0; j < PlayerTfList.Count; ++j)
            {
                dists[j] = (rightMissile.transform.position - PlayerTfList[j].position).magnitude;
            }

            for (int k = 0; k < PlayerTfList.Count; ++k)
            {
                if (dists[k] > minDist)
                {
                    minDist = dists[k];
                    res = PlayerTfList[k];
                }
            }

            rightMissile.GetComponent<Missile>().Target = res;

            LogicEventListener.Invoke(eEventType.FOR_ALL, eEventMessage.ON_MISSILE_SPAWNED, RightFireTransform[i]);
            yield return new WaitForSeconds(0.3f);
        }

        yield break;
    }

    void MineSpread()
    {
        // 지뢰 생산
        GameObject[] objs = new GameObject[8];
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i] = Instantiate(minePrefab, minePoint.position, Quaternion.identity);
        }

        

        // 무작위 Force 부여로 확산
        for(int i=0; i<objs.Length;i++)
        {
            float randX = Random.Range(-1.0f, 1.0f);
            float randY = Random.Range(-1.0f, 1.0f);
            float randZ = Random.Range(-1.0f, 1.0f);

            Vector3 force = new Vector3(randX, randY, randZ);
            force.Normalize();

            // 지뢰의 날아가는 방향이 X축이 되도록 회전
            //Vector3 rotAxis = Vector3.Cross(objs[i].transform.right, force);
            //float angle = Vector3.Angle(objs[i].transform.right, force);

            //var Rot = Quaternion.AngleAxis(angle, rotAxis) * objs[i].transform.rotation;
            
            objs[i].transform.rotation = Random.rotation;

            
            
            objs[i].GetComponent<Rigidbody>().AddForce(force * 2000);
        }

        
    }
}
