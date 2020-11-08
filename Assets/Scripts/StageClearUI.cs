using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUI : MonoBehaviour
{
    [SerializeField] Text clearText;

    Color txtColor;
    [SerializeField] float blinkSpeed = 1.0f;
    float nowAlpha;
    int dir = 1;

    private void Start()
    {
        txtColor = clearText.color;
    }
    private void Update()
    {
        nowAlpha += Time.deltaTime * blinkSpeed * dir;
        if (nowAlpha > 1 || nowAlpha < 0)
            dir *= -1;

        txtColor.a = nowAlpha;
        clearText.color = txtColor;
    }
}
