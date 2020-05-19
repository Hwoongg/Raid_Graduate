using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//
// 애니메이터 전환 기능 추가.
// Q 입력시 무기 교체, E 입력시 스킬 발동.
// 저격소총에서 교체된 후 1초 뒤 컨트롤러 이동 기능 활성화
//

public class AssaultRifle : SwitchableWeapon
{
    NewController playerController;
    [SerializeField]
    float MoveableTime = 1.5f;
    float MoveableTimer;

    [SerializeField] GameObject[] HideObjects;

    [SerializeField] GameObject ObjSkillEfx;
    ParticleSystem[] EfxParticles;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        //Debug.Log("Rifle On");

        MoveableTimer = 0;
    }

    protected override void Awake()
    {
        base.Awake();

        playerController = FindObjectOfType<NewController>();
        
    }

    protected override void Start()
    {
        base.Start();

        EfxParticles = ObjSkillEfx.GetComponentsInChildren<ParticleSystem>();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();

        if (photonView.IsMine)
        {
            if (MoveableTimer > MoveableTime)
            {
                playerController.mode = NewController.Mode.NORMAL;
            }

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Skill_RapidFire
            Skill_RapidFire();
        }

            if (Input.GetKeyDown(KeyCode.Q) && WeaponSwitchTimer > WeaponSwitchDelay)
            {
                // 애니메이터 전환.
                animator.SetBool("isSniping", true);

                // 무기 교체
                SwitchWeapons();
            }
            MoveableTimer += Time.deltaTime;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Skill_Rapid"))
            HideWeapon();
        else
            ViewWeapon();
        
        MoveableTimer += Time.deltaTime;
    }

    void Skill_RapidFire()
    {
        // 애니메이터 전환
        animator.SetTrigger("Skill_Rapid");

        // 총 오브젝트 제거
        // ... Update에서 처리

        // 스킬 효과 적용
        for (int i = 0; i < EfxParticles.Length; i++)
            EfxParticles[i].Play();

        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");

        for(int i=0; i<Players.Length; i++)
        {
            Weapon[] weapons = Players[i].GetComponentsInChildren<Weapon>();
            for (int j = 0; j < weapons.Length; j++)
            {
                weapons[j].CurrentBullet += 100;
                LogicEventListener.Invoke(eEventType.FOR_UI, eEventMessage.ON_AMMUNITION_COUNT_CHANGED, CurrentBullet, weapons[j].MaxBullet);
            }
        }
    }

    public void HideWeapon()
    {
        for (int i = 0; i < HideObjects.Length; i++)
            HideObjects[i].SetActive(false);
    }

    public void ViewWeapon()
    {
        for (int i = 0; i < HideObjects.Length; i++)
            HideObjects[i].SetActive(true);
    }
}
