using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 별도의 본체 Health 클래스를 추가 참조,
// 받은 피해를 방어력 파라미터와 연산하여 
// 아머의 내구도(체력)과 함께 감산.
//
// 파괴 가능, 불가의 두가지 상태가 존재합니다.
// 열거형으로 상태를 구분하여 아머 내구도 감산 여부를 조절합니다.
//


public class EnemyArmor : BossHealth
{

    // 파괴 가능 여부 상태
    public enum State
    {
        DESTRUCTIBLE,
        INDESTRUCTIBLE
    }
    public State state;

    // 갑옷일 입고있는 본체의 체력 컴포넌트
    [SerializeField] Health MainHealth;

    [SerializeField] int ArmorValue;

    

    protected override void Awake()
    {
        base.Awake();
    }

    
    public override void TakeDamage(int amount)
    {
        amount -= ArmorValue;

        if (amount < 1)
            amount = 1;

        if(state == State.DESTRUCTIBLE)
            base.TakeDamage(amount);

        MainHealth.TakeDamage(amount);
    }
}
