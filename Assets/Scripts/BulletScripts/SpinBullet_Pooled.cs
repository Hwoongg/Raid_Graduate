using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinBullet_Pooled : SpinBullet
{
    public override void Explotion()
    {
        gameObject.SetActive(false);
    }


}
