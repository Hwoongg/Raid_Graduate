using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 회전탄 스크립트
//

public class SpinBullet : Bullet
{
    public override void Update()
    {
        base.Update();

        BulletSpin();
    }

    void BulletSpin()
    {
        Quaternion rot = Quaternion.AngleAxis(3.0f, transform.forward);
        transform.rotation = transform.rotation * rot;
    }
}
