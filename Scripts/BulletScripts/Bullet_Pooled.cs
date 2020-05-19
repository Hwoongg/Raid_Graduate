using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 풀링용 총알 스크립트.
// 파괴 시 메모리에서 제거되지 않고 비활성화 상태로 대기한다.
//

public class Bullet_Pooled : Bullet
{
    [SerializeField] float ScaleSpeed = 1.0f;
    [SerializeField] float StartScale = 0.3f;
    [SerializeField] float MaxScale = 5.0f;

    Vector3 StartVector;
    Vector3 ScaleVector;
    float NowScale;

    private void OnEnable()
    {
        transform.localScale = StartVector;
        NowScale = StartScale;
    }
    public override void Awake()
    {
        base.Awake();

        ScaleVector = Vector3.one * ScaleSpeed;
        StartVector = Vector3.one * StartScale;
    }
    public override void Update()
    {
        base.Update();

        ScaleUp();
    }
    public override void Explotion()
    {
        gameObject.SetActive(false);
    }

    void ScaleUp()
    {
        if (NowScale > MaxScale)
            return;

        transform.localScale += ScaleVector * Time.deltaTime;
        NowScale += Time.deltaTime;
        
    }
}
