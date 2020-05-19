using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 폭발 이펙트에 내장될 스크립트입니다.
// 일정 시간 뒤 이펙트 오브젝트 자신을 자체적으로 삭제하는 스크립트.
// 추후 Instant Effect로 변경될 것.
//

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
