using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ///////////////////////////////////////////////
//
// 무기 자동 재장전 컴포넌트. 
// SwitchableWeapon의 경우 
// 무기 오브젝트 밖에 부착하여 Update가 멈추지 않도록 할 것
//
// ///////////////////////////////////////////////
public class AutoReloader : MonoBehaviour
{
    Weapon weapon;

    Timer reloadTimer;
    
    void Update()
    {
        if (reloadTimer != null)
        {
            reloadTimer.Update();

            if(reloadTimer.IsTimeOver())
            {
                weapon.ReloadOneAmmo();
            }
        }

    }

    public void SetWeapon(Weapon _w)
    {
        weapon = _w;
    }

    public void SetTimer(float _time)
    {
        reloadTimer = new Timer(_time);
    }
}
