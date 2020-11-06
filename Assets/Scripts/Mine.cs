using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Timer sleepTimer;

    bool isAttackMode;
    Rigidbody rig;

    [SerializeField] GameObject efxExplotion;

    Vector3 rotFactor;
    Vector3 moveFactor;
    bool isMovable;

    Timer lifeTimer;
    
    void Start()
    {
        sleepTimer = new Timer(1.5f);
        isAttackMode = false;
        rig = GetComponent<Rigidbody>();
        rotFactor = new Vector3(0, 60, 0);
        moveFactor = new Vector3(5, 0, 0);
        isMovable = false;
        float lifeTime = Random.Range(10.0f, 20.0f);
        lifeTimer = new Timer(lifeTime);
    }
    
    void Update()
    {
        sleepTimer.Update();
        lifeTimer.Update();

        if(sleepTimer.IsTimeOver())
        {
            rig.Sleep();
            isMovable = true;
        }

        if(lifeTimer.IsTimeOver())
        {
            Explotion();
        }

        // 자전
        transform.Rotate(rotFactor * Time.deltaTime);

        // 이동
        if (isMovable)
            transform.Translate(moveFactor * Time.deltaTime);
    }

    // 밀치기 상태로 변경
    public void HitReaction(Vector3 dir)
    {
        isAttackMode = true;
        rig.AddForce(dir * 10000);

        sleepTimer.ResetTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어랑 박으면 폭발
        if (other.tag == "Player")
        {
            PlayerHealth ph = other.transform.GetComponent<PlayerHealth>();
            if (ph)
            {
                ph.TakeDamage(30);
                Explotion();
            }
        }

        // 공격모드일때 보스랑 박으면 보스데미지
        if (other.transform.tag == "Enemy")
        {
            if (isAttackMode)
            {
                Health h = other.transform.GetComponent<Health>();
                if (h)
                {
                    h.TakeDamage(300);
                    Explotion();
                }
            }
        }
    }


    void Explotion()
    {
        GameObject.Instantiate(efxExplotion, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
