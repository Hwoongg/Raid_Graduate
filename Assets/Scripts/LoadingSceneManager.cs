using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class LoadingSceneManager : RulePrototype
{
    [SerializeField] Slider progressBar;
    AsyncOperation ao;
    string loadSceneName = "Stage1";

    Timer delayTimer;
    [SerializeField] GameObject btnJoin;

    void Start()
    {
        delayTimer = new Timer(5.0f);
        btnJoin.GetComponent<Button>().onClick.AddListener(Join);
        btnJoin.SetActive(false);

        PhotonNetwork.LoadLevel(loadSceneName);

        ao = PhotonNetwork.GetAO();
        ao.allowSceneActivation = false; // 자동 실행 블락
    }
    
    void Update()
    {
        if(progressBar.value <= ao.progress)
        {
            progressBar.value += Time.deltaTime;
        }

        
        if(progressBar.value >= 0.9f) // 0.9위로 progress값이 안올라간다
        {
            delayTimer.Update(); // 구라 딜레이
            if(delayTimer.IsTimeOver())
            {
                progressBar.value = 1.0f;
                btnJoin.SetActive(true);
            }
        }
    }

    public void Join()
    {
        FindObjectOfType<ConnectRule>().InitializeConnection();
        FindObjectOfType<ChatRule>().InitializeChat();

        //ao.allowSceneActivation = true;
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_CONNECTED:
                ao.allowSceneActivation = true;
                break;
        }
    }
}
