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
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        if (!photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            return;
        }

        base.Update();

        if(Input.GetKeyDown(KeyCode.Q) && WeaponSwitchTimer > WeaponSwitchDelay)
        {
            // 애니메이터 전환.
            animator.SetBool("isSniping", true);

            // 무기 교체
            SwitchWeapons();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            // Skill_RapidFire
            // ...
        }
    }

    void Skill_RapidFire()
    {

    }
}
