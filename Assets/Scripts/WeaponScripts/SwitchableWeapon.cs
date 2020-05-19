using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 스위칭 가능한 무기에 사용되는 스크립트 입니다.
// 플레이어 컨트롤러 잠금 기능을 포함할지 고려 필요합니다.
//

public class SwitchableWeapon : Weapon
{
    protected override void OnEnable()
    {
        base.OnEnable();
        WeaponSwitchTimer = 0;
    }

    protected override void Awake()
    {
        base.Awake();
        WeaponSwitchTimer = WeaponSwitchDelay;
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
        base.Update();
        WeaponSwitchTimer += Time.deltaTime;
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        base.OnInvoked(msg, obj);
    }

    // 자신의 파츠들을 등록합니다.
    [SerializeField]
    public GameObject[] MyParts;

    // 교체될 파츠들을 등록합니다.
    [SerializeField]
    GameObject[] SwitchParts;
    
    // 교체 딜레이 관련 변수들..
    [SerializeField]
    protected float WeaponSwitchDelay = 3.0f;
    protected float WeaponSwitchTimer;

    protected void SwitchWeapons()
    {
        for (int i = 0; i < SwitchParts.Length; i++)
            SwitchParts[i].SetActive(true);

        for (int i = 0; i < MyParts.Length; i++)
            MyParts[i].SetActive(false);
    }
    

}
