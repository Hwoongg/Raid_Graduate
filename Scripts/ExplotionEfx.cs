using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionEfx : MonoBehaviour
{
    float timer;

    private void Awake()
    {
        timer = 0;

    }
   


    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3.0f)
            GameObject.Destroy(gameObject);
    }
}
