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
        missileTimer = new Timer(15.0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Slash) && !bMissileLaunch)
        {
            bMissileLaunch = true;
        }

        missileTimer.Update();
        if(missileTimer.IsTimeOver())
        {
            bMissileLaunch = true;
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

        if (BossHealth.isDead)
            gameObject.SetActive(false);
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
}
