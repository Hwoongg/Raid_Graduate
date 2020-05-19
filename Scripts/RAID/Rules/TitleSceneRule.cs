using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Photon.Pun;
using MEC;

public class TitleSceneRule : RulePrototype
{
    [SerializeField] UIUsername UserName;
    [SerializeField] UIPassword Password;
    [SerializeField] Button LoginBtn;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] Animator[] FlyAwayAnims = new Animator[2];
    /// <summary>
    /// 
    /// </summary>
    bool IsAnyKeyDown = false;

    void Start()
    {
        Dbg.LogCheckAssigned(UserName, this);
        Dbg.LogCheckAssigned(Password, this);
        Dbg.LogCheckAssigned(LoginBtn, this);

        if (Utils.IsValid(LoginBtn))
        {
            LoginBtn.onClick.AddListener(OnLogin);
        }

        Dbg.LogCheckAssigned(FlyAwayAnims[0], this);
        Dbg.LogCheckAssigned(FlyAwayAnims[1], this);
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.FADER_FULLY_APPEARED:
                PhotonNetwork.LoadLevel("Lobby");
                break;
        }
    }

    void OnLogin()
    {
        FlyAwayAnims[0].SetTrigger("FlyAway");
        FlyAwayAnims[1].SetTrigger("FlyAway");
        LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_ANYKEY_PRESSED);
    }
};
