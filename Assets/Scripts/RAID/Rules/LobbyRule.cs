using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LobbyRule : RulePrototype
{
    [SerializeField] Button BtnMissionStart;
    [SerializeField] Button BtnReturnToTitle;
    ConnectRule ConnectRuleCache;
    ChatRule ChatRuleCache;

    void Start()
    {
        Dbg.LogCheckAssigned(BtnMissionStart, this);
        Dbg.LogCheckAssigned(BtnReturnToTitle, this);
        ConnectRuleCache = FindObjectOfType<ConnectRule>();
        ChatRuleCache = FindObjectOfType<ChatRule>();

        if (Utils.IsValid(ConnectRuleCache))
        {
            if (Utils.IsValid(BtnMissionStart))
            {
                BtnMissionStart.onClick.AddListener(OnMissionStartBtnClicked);
            }

            if (Utils.IsValid(BtnReturnToTitle))
            {
                BtnReturnToTitle.onClick.AddListener(OnReturnToTitleBtnClicked);
            }
        }
    }

    public void OnMissionStartBtnClicked()
    {
        //ConnectRuleCache.InitializeConnection();
        //ChatRuleCache.InitializeChat();

        SceneManager.LoadScene("Loading");
    }

    public void OnReturnToTitleBtnClicked()
    {
        base.OnLeaveRoom();
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_CONNECTED:
                //PhotonNetwork.LoadLevel("Stage1");
                break;
        }
    }
}
