using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : MonoBehaviour
{
    float value;

    private void Awake()
    {
        value = 0;
    }

    public float GetValue()
    {
        return value;
    }
}
