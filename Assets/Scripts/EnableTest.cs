using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Enable 이벤트 함수 실험 스크립트 입니다.
// 검증된 Enable 이벤트 실행 조건은 다음과 같습니다.
// 1. 해당 컴포넌트의 활성화 이벤트.
// 2. 해당 컴포넌트를 포함한 오브젝트의 활성화 이벤트.
// 2-1. 해당 컴포넌트를 포함한 오브젝트의 부모의 활성화 이벤트.
//

public class EnableTest : MonoBehaviour
{
    
    private void OnEnable()
    {
        Debug.Log("Enable On!");
    }

    private void OnDisable()
    {
        Debug.Log("Enable Off!");
    }
}
