using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    float timer;
    bool bMissileLaunch;
    bool bLaunchComplete;

    Animator animator;
    
    [SerializeField] GameObject objMissile;

    [SerializeField] Transform[] LeftFireTransform;
    [SerializeField] Transform[] RightFireTransform;

    void Start()
    {
        timer = 0f;
        bMissileLaunch = false;
        bLaunchComplete = false;
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Slash) && !bMissileLaunch)
        {
            bMissileLaunch = true;
        }

        if(bMissileLaunch)
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
                animator.SetBool("OpenSilo", false);
            if (timer > 10.0f)
            {
                bMissileLaunch = false;
                bLaunchComplete = false;
                timer = 0;
            }

        }

    }
    

    //void LaunchMissle()
    //{
    //    for (int i = 0; i < MissileFireTransform.Length; i++)
    //        Instantiate(objMissile, MissileFireTransform[i].position, MissileFireTransform[i].rotation);
    //    bLaunchComplete = true;
    //}

    IEnumerator LaunchMissileThread()
    {
        for (int i = 0; i < LeftFireTransform.Length; i++)
        {
            Dbg.Log($"Missiles launched! count :{ LeftFireTransform.Length.ToString()}", this);
            Instantiate(objMissile, LeftFireTransform[i].position, LeftFireTransform[i].rotation);
            LogicEventListener.Invoke(eEventType.FOR_ALL, eEventMessage.ON_MISSILE_SPAWNED, LeftFireTransform[i], "MISSILE");
            Instantiate(objMissile, RightFireTransform[i].position, RightFireTransform[i].rotation);
            LogicEventListener.Invoke(eEventType.FOR_ALL, eEventMessage.ON_MISSILE_SPAWNED, RightFireTransform[i], "MISSILE");
            yield return new WaitForSeconds(0.3f);
        }

        yield break;
    }
}
