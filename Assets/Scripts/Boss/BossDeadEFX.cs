using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadEFX : MonoBehaviour
{
    Transform[] points;

    Timer expTimer;
    [SerializeField] GameObject prfExplotion;

    bool isComplete = false;

    void Start()
    {
        points = GetComponentsInChildren<Transform>();
        expTimer = new Timer(1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isComplete)
        {
            expTimer.Update();

            if (expTimer.IsTimeOver())
            {
                RandomPointExplotion();
            }
        }
    }

    void RandomPointExplotion()
    {
        int n = Random.Range(0, points.Length);

        Instantiate(prfExplotion, points[n]);
    }

    public void LastExplotion()
    {
        //for(int i=0; i<points.Length; i++)
        //{
        //    GameObject o = Instantiate(prfExplotion, points[i]);
        //    o.transform.localScale = new Vector3(10, 10, 10);
        //}

        GameObject o = Instantiate(prfExplotion, points[0]);
        o.transform.localScale = new Vector3(100, 100, 100);

        isComplete = true;
    }
}
