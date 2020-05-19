using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Rule : RulePrototype
{
    public static bool IsMenuOpened { get; set; } = false;
    public static bool IsChatUIOpened { get; set; } = false;
    public static bool IsSceneLoaded { get; set; } = false;
    [SerializeField] GameObject PlayerPf;
    public Scene[] Scenes { get; set; } = new Scene[2];
    public List<GameObject> ChatSceneGOs { get; set; } = new List<GameObject>();
    public List<GameObject> MenuSceneGOs { get; set; } = new List<GameObject>();

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("Title");
    }

    void Start()
    {
        Dbg.LogCheckAssigned(PlayerPf, this);

        //if (Application.isEditor)
        //{
        //    var loadedLevel = SceneManager.GetSceneByName("Stage1");
        //    if (true == loadedLevel.isLoaded)
        //    {
        //        SceneManager.SetActiveScene(loadedLevel);
        //    }
        //}

        if (Utils.IsNull(WeaponView.LocalPlayerInst) && Utils.IsNull(HealthView.LocalPlayerInst))
        {
            var player = PhotonNetwork.Instantiate(PlayerPf.name, new Vector3(0.0f, 50.0f, 150.0f), Quaternion.identity);
            player.name = "Player";
        }

        // Preload all the required scenes for the stage1.
        //AsyncOperation[] additionalSceneStatus = new AsyncOperation[2]{
        //    SceneManager.LoadSceneAsync("ChatUI", LoadSceneMode.Additive),
        //    SceneManager.LoadSceneAsync("MenuUI", LoadSceneMode.Additive)
        //};

        SceneManager.LoadScene("ChatUI", LoadSceneMode.Additive);
        SceneManager.LoadScene("MenuUI", LoadSceneMode.Additive);

        Scenes[0] = SceneManager.GetSceneAt(1);
        Scenes[1] = SceneManager.GetSceneAt(2);

        // GetRootGameObjects works properly after the scene has been fully loaded!
        StartCoroutine(Yielder.GetCoroutine(() =>
        {
            Scenes[0].GetRootGameObjects(ChatSceneGOs);
            SwitchChatUI(false);

            Scenes[1].GetRootGameObjects(MenuSceneGOs);
            SwitchMenuUI(false);
        }, 0.25f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && false == IsMenuOpened)
        {
            IsMenuOpened = true;
            SwitchMenuUI(true);
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_MENU_OPENED);
        }
        else
            if (Input.GetKeyDown(KeyCode.Escape) && true == IsMenuOpened)
        {
            IsMenuOpened = false;
            SwitchMenuUI(false);
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_MENU_CLOSED);
        }

        if (Input.GetKeyDown(KeyCode.T) && false == IsChatUIOpened)
        {
            IsChatUIOpened = true;
            SwitchChatUI(true);
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_CHAT_UI_OPENED);
        }
        else
            if (Input.GetKeyDown(KeyCode.T) && true == IsChatUIOpened)
        {
            IsChatUIOpened = false;
            SwitchChatUI(true);
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_CHAT_UI_CLOSED);
        }
    }

    void SwitchChatUI(bool flag)
    {
        int len = ChatSceneGOs.Count;
        for (int i = 0; i < len; ++i)
        {
            if (flag)
            {
                ChatSceneGOs[i].SetActive(true);
            }
            else
            {
                ChatSceneGOs[i].SetActive(false);
            }
        }
    }

    void SwitchMenuUI(bool flag)
    {
        int len = MenuSceneGOs.Count;
        for (int i = 0; i < len; ++i)
        {
            if (flag)
            {
                MenuSceneGOs[i].SetActive(true);
            }
            else
            {
                MenuSceneGOs[i].SetActive(false);
            }
        }
    }

    //IEnumerator LoadMenu()
    //{
    //    enabled = false;
    //    yield return SceneManager.LoadSceneAsync("Stage1Menu", LoadSceneMode.Additive);
    //    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Stage1Menu"));
    //    enabled = true;
    //}

    void LoadStage()
    {
        if (false == PhotonNetwork.IsMasterClient)
        {
            Dbg.LogE($"Trying to load a level but we are not the master client.");
        }
        PhotonNetwork.LoadLevel("Stage1");
    }

    public override void OnLeaveRoom()
    {
        // OnLeaveRoom for Stage1 is called on Stage1Menu in Stage1Menu scene.
    }

    /// <summary>
    /// Called when a remote player entered the room. This player is already added to the playerlist.
    /// </summary>
    /// <remarks>
    /// If your game starts with a certain number of players, this callback can be useful to check the
    /// Room.playerCount and find out if you can start.
    /// </remarks>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Dbg.Log($"Stage1Rule: OnPlayerEnteredRoom() called.");
        Dbg.Log($"{newPlayer.NickName} has joined the game.");
        if (true == PhotonNetwork.IsMasterClient)
        {
            LoadStage();
        }
    }

    /// <summary>
    /// Called when a remote player left the room or became inactive. Check otherPlayer.IsInactive.
    /// </summary>
    /// <remarks>
    /// If another player leaves the room or if the server detects a lost connection, this callback will
    /// be used to notify your game logic.
    /// 
    /// Depending on the room's setup, players may become inactive, which means they may return and retake their spot
    /// in the room. In such cases, the player stays in the Room.Players dictionary.
    /// 
    /// If the player is not just inactive, it gets removed from the Room.Players dictionary, before the callback is called.
    /// </remarks>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Dbg.Log($"Stage1Rule: OnPlayerLeftRoom() called.");
        Dbg.Log($"{otherPlayer.NickName} has lefted the game.");
        if (true == PhotonNetwork.IsMasterClient)
        {
            LoadStage();
        }
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_MENU_CLOSED:
                IsMenuOpened = false;
                SwitchMenuUI(false);
                break;

            case eEventMessage.ON_CHAT_UI_CLOSED:
                IsChatUIOpened = false;
                SwitchChatUI(false);
                break;

            case eEventMessage.ON_LEAVE_ROOM:
                SceneManager.UnloadSceneAsync("ChatUI");
                SceneManager.UnloadSceneAsync("MenuUI");
                ChatSceneGOs.Clear();
                MenuSceneGOs.Clear();
                var me = GameObject.Find("Player");
                Destroy(me);
                break;
        }
    }
};
