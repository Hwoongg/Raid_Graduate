﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//
// 쏘는 부분 재정의 하여 뒤로 밀리도록 해야함.
// 애니메이터 전환 기능.
// UI 전환 기능.
// Enable 시 저격 UI 호출, Disable시 복귀.
// 이동 기능 제어 기능. PlayerController 자체를 비활성화 하면 시점 회전도 잠김에 유의.
//

public class SniperRifle : SwitchableWeapon
{
    // 움직임 제어를 위한 컨트롤러 참조
    NewController playerController;
    NewTPSCamera playerCamera;

    // 반동 움직임 관련 변수들
    Transform playerTransform; // 밀려날 캐릭터의 Transform
    [SerializeField]
    float ReboundPower = 5.0f;
    Vector3 ReboundDir;
    float Deceleration = 5.0f;
    Vector3 NowRebounding = Vector3.zero; // 현재 밀리고 있는 양

    // UI 교체 관련 변수들
    [SerializeField]
    GameObject SnipingUI;
    float UiSwitchDelay; // 무기교체 딜레이의 절반으로 초기화 될 것
    float UiTimer;



    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("Sniping On");
        timer = 0;

        // 이동 기능 제어를 위한 Controller State 추가
        playerController.mode = NewController.Mode.SNIPING;

        // UI 교체 타이머 초기화.
        UiTimer = 0;
        
    }

    protected override void Awake()
    {
        base.Awake();
        playerController = FindObjectOfType<NewController>();
        playerTransform = GameObject.Find("Player").transform;
        playerCamera = Camera.main.GetComponent<NewTPSCamera>();
        UiSwitchDelay = WeaponSwitchDelay * 0.5f;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        // 저격 UI 비활성화
        SnipingUI.SetActive(false);

        // 카메라 모드 복귀
        playerCamera.ChangeMode(NewTPSCamera.Mode.NORMAL, true);
    }

    protected override void Update()
    {
        if (!photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            return;
        }

        timer += Time.deltaTime;

        
        if (Input.GetMouseButton(0) && (Time.timeScale != 0))
        {
            
            if (timer >= timeBetweenBullets)
            {
                ReboundDir = -playerCamera.transform.forward; // 밀림 방향 결정
                Fire();
            }
        }
        else
        {
            animator.SetBool("onFire", false);
        }

        if (timer >= timeBetweenBullets * effectDisplayTime)
        {
            DisableEffects();
        }


        // UI + 카메라 전환
        if (UiTimer > UiSwitchDelay)
        {
            playerCamera.mode = NewTPSCamera.Mode.SNIPING;
            SnipingUI.SetActive(true);
        }


        // 뒤로 밀림 효과. 플레이어 컨트롤러와 같은 감쇄공식 적용.
        NowRebounding = NowRebounding - (NowRebounding * Deceleration * Time.deltaTime);
        playerTransform.Translate(NowRebounding, Space.World);

        if (Input.GetKeyDown(KeyCode.Q) && WeaponSwitchTimer > WeaponSwitchDelay)
        {
            // 애니메이터 전환
            animator.SetBool("isSniping", false);

            //// 저격 UI 비활성화
            //SnipingUI.SetActive(false);

            //// 카메라 모드 복귀
            //playerCamera.ChangeMode(NewTPSCamera.Mode.NORMAL, true);
            
            // 무기 교체
            SwitchWeapons();
        }

        

        // 타이머 진행
        WeaponSwitchTimer += Time.deltaTime;
        UiTimer += Time.deltaTime;

        
    }

    // 뒤로 밀림 가속 기능 추가 재정의
    // 애니메이터 조작 관련 개선 필요. Weapon의 Fire와 대조할 것
    protected override void Fire()
    {
        base.Fire();

        animator.SetBool("onFire", true);

        // 뒤로 밀림 가속
        NowRebounding += ReboundDir * ReboundPower;
    }
}
