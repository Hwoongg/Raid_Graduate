using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class MenuUIRule : RulePrototype
{
    /// <summary>
    /// Exit button that bring us back to the lobby scene.
    /// </summary>
    [SerializeField] Button ExitBtn;
    /// <summary>
    /// Exit button that close the menu overlapping the stage1.
    /// </summary>
    [SerializeField] Button CloseMenuBtn;
    public static bool IsCloseMenuClicked { get; set; } = false;

    void Start()
    {
        IsCloseMenuClicked = false;
        // Error check.
        Dbg.LogCheckAssigned(ExitBtn, this);
        Dbg.LogCheckAssigned(CloseMenuBtn, this);

        if (Utils.IsValid(ExitBtn))
        {
            // Add listener to onClick.
            ExitBtn.onClick.AddListener(OnLeaveRoom);
        }

        if (Utils.IsValid(CloseMenuBtn))
        {
            CloseMenuBtn.onClick.AddListener(OnCloseMenuClicked);
        }
    }

    void Update()
    {
        // If 'Escape' key is pressed down
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // regard The menu is closed.
            OnCloseMenuClicked();
        }
    }

    public override void OnLeaveRoom()
    {
        if(AppManager.Instance().isOnline)
            base.OnLeaveRoom();
        SceneManager.LoadScene("Title");
    }

    void OnCloseMenuClicked()
    {
        if (false == IsCloseMenuClicked)
        {
            Dbg.Log("Menu is closed");
            IsCloseMenuClicked = true;
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_MENU_CLOSED);
        }
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_MENU_OPENED:
                IsCloseMenuClicked = false;
                break;
        }
    }
}
