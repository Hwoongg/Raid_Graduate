using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHowling : MonoBehaviour
{
    float timer;

    [SerializeField] float HowlingTiming = 30.0f;

    AudioSource HowlingSound;

    private void Awake()
    {
        timer = 0;
        HowlingSound = GetComponent<AudioSource>();
    }
    
    
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > HowlingTiming)
        {
            Howling();
        }
    }

    void Howling()
    {
        HowlingSound.Play();
        timer = 0;
    }
}
