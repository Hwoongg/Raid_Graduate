using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

//
// 체력 관리(딜교환) 전반에 사용되는 스크립트.
// 사망 이펙트 기능 포함.
//

public class Health : MonoBehaviourPun
{
    [SerializeField] int startingHealth = 100;
    public int StartingHealth { get { return startingHealth; } set { startingHealth = value; } }

    [SerializeField] bool IsPlayer = false;
    [SerializeField] GameObject objDeadEffect;

    int currentHealth;
    public int CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }
    //[SerializeField] Image damageImage;
    //[SerializeField] float flashSpeed = 0.5f;
    //[SerializeField] Color flashColor = new Color(1f, 0f, 0f, 0.2f);

    protected bool isDead;
    protected bool damaged;


    protected virtual void Awake()
    {
        IsPlayer = gameObject.tag == "Player";
        //CustomDebug.LogCheckAssigned(HealthEvent, this);
        currentHealth = startingHealth;
        Dbg.Log($"{currentHealth}");
    }

    public virtual void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;
        Dbg.Log($"{currentHealth}");
        if (IsPlayer)
        {
            float healthPointRatio = currentHealth / startingHealth;
            LogicEventListener.Invoke(eEventType.FOR_UI, eEventMessage.ON_HEALTH_POINT_CHANGED, (object)healthPointRatio);
        }
        
        // TODO: 플레이어 아닐떄에만 폭발사
        if (currentHealth <= 0 && !isDead && !IsPlayer)
        {
            if(objDeadEffect != null)
                Instantiate(objDeadEffect, transform.position, transform.rotation);

            Death();
        }
    }

    public virtual void Death()
    {
        isDead = true;
        gameObject.SetActive(false);
    }

    public void Recovery()
    {
        isDead = false;
        currentHealth = startingHealth;
    }

    [ContextMenu("Show HP")]
    void ShowHP()
    {
        Debug.Log(gameObject.name + "의 체력 : " + currentHealth);
    }

}
