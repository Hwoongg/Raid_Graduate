using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    Timer sleepTimer;

    bool isAttackMode;
    Rigidbody rig;

    [SerializeField] GameObject efxExplotion;

    // Start is called before the first frame update
    void Start()
    {
        sleepTimer = new Timer(1.5f);
        isAttackMode = false;
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        sleepTimer.Update();
        if(sleepTimer.IsTimeOver())
        {
            rig.Sleep();
        }
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
