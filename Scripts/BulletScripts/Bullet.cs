using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 탄이 날아가고, 맞고, 터지는 것에 대해 정의된 스크립트.
//

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float Speed = 10.0f;

    [SerializeField] float DestroyTime = 0;
    float Timer;


    public virtual void Awake()
    {
        Timer = 0;
    }
    public virtual void Start()
    {

    }

    public virtual void Update()
    {
        DestroyCount();
        transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);
        
    }

    public virtual void Explotion()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerHealth>().TakeDamage(1);
        Explotion();
    }

    public void DestroyCount()
    {
        Timer += Time.deltaTime;

        if (Timer > DestroyTime)
        {
            Timer = 0;
            Explotion();
        }
    }

    
}
