using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 기본적으로 파괴 가능하며, 파괴된 뒤 재생되는 갑옷 스크립트.
// 파괴됐을 시 충돌체 제거, 머터리얼 교체. 피격불가 오브젝트로 레이어 변경.
// 일정 시간 뒤 머터리얼 복귀, 충돌체 재활성화, 레이어를 복귀시킨다.
// 충돌체 제거 코드는 문제 있을 시 추가하도록 할 것.
// 현재는 제거하지 않아도 이상 없을 것으로 예상.
//

public class RegenerationArmor : EnemyArmor
{

    // 교체될 머터리얼. 충돌체와 태그는 내장변수인듯 함.
    [SerializeField] Material WaitmodeMaterial;
    Material AwakeMaterial;
    SkinnedMeshRenderer meshRenderer;

    // 재생되는 시간 관련 변수.
    [SerializeField] float RegenTime; // 재생에 걸리는 시간
    float RegenTimer; // 재생 타이머



    protected override void Awake()
    {
        base.Awake();

        state = State.DESTRUCTIBLE; // 파괴 가능하도록 자동 초기화

        meshRenderer = GetComponent<SkinnedMeshRenderer>();
        AwakeMaterial = meshRenderer.material;

    }

    
    private void Update()
    {
        if (RegenTimer > RegenTime)
        {
            Regeneration();
        }

        RegenTimer += Time.deltaTime;
    }

    public override void Death()
    {
        // ///////////////////////////////////////////////
        //
        // 부모의 Death()에서 오브젝트 비활성화 코드 제거
        //
        isDead = true;
        //
        // ///////////////////////////////////////////////


        // 재생 타이머 초기화
        RegenTimer = 0;

        // 파괴상태로 교체
        Destruction();
    }

    // 파괴상태로 전환하는 함수
    void Destruction()
    {
        // 충돌체 제거
        // ...

        // 머터리얼 교체.
        meshRenderer.material = WaitmodeMaterial;

        // 피격불가 오브젝트로 레이어 변경.
        gameObject.layer = LayerMask.NameToLayer("Default");

        // 체력 및 상태 회복
        Recovery();
        
    }

    // 원상태로 재생하는 함수
    void Regeneration()
    {
        // 충돌체 복구
        // ...

        // 머터리얼 복구
        meshRenderer.material = AwakeMaterial;

        // 피격 가능하도록 레이어 변경.
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
}
