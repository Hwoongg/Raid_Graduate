using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    [SerializeField] float MuzzleRotSpeed = 30.0f; // 중심 회전 속도
    [SerializeField] bool LeftTurn = true;

    float TurnWay;

    // Start is called before the first frame update
    void Start()
    {
        if (LeftTurn)
            TurnWay = 1;
        else
            TurnWay = -1;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, (MuzzleRotSpeed * TurnWay) * Time.deltaTime);
    }
}
