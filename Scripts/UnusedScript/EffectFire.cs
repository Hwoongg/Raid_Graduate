using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFire : MonoBehaviour
{

    [SerializeField] ParticleSystem[] particles;
    
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        for(int i=0;i<particles.Length;i++)
        {
            particles[i].Stop();
        }
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }
    }
    
}
