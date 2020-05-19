using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 사용자 정의 타이머 클래스
//

public class Timer
{

    float nowTime;
    float maxTime;


    public Timer()
    {
        Debug.Log("정상 초기화 되지 않은 타이머가 있습니다!");
    }

    public Timer(float _maxTime)
    {
        nowTime = 0;
        maxTime = _maxTime;
    }

    public void Update()
    {
        nowTime += Time.deltaTime;
    }

    // 시간 경과 체크. 스스로 되감기
    public bool IsTimeOver()
    {
        if (nowTime > maxTime)
        {
            ResetTimer();
            return true;
        }
        else
        {
            return false;
        }
    }

    // 타이머 현재 시간 초기화
    public void ResetTimer()
    {
        nowTime = 0;
    }

    // 타이머 대기시간 수정
    public void SetMaxTime(float _maxTime)
    {
        maxTime = _maxTime;
    }
}
