using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChatRule : MonoBehaviour, IChatClientListener, ILogicEvent
{
    [SerializeField] ChatClient ChatClient;
    internal AppSettings AppSettings;
    public string UserName = "Me!";
    public static string NickName;
    EventSet EventSet;
    Text MessageText;
    InputField MessageInputField;
    [SerializeField] string[] ChannelsToAutoJoin;
    int HistoryLength = 10;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        UserName = !string.IsNullOrEmpty(NickName) ? NickName : "Me";

        AppSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        bool isAppIDpresent = !string.IsNullOrEmpty(AppSettings.AppIdChat);

        if (!isAppIDpresent)
        {
            Dbg.LogE("Chat ID is missing.");
        }
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void Update()
    {
        if (Utils.IsValid(ChatClient))
        {
            ChatClient.Service();
        }
    }

    public void InitializeChat()
    {
        ChatClient = new ChatClient(this)
        {
            UseBackgroundWorkerForSending = true
        };
        ChatClient.Connect(AppSettings.AppIdChat, "1.0", new Photon.Chat.AuthenticationValues(UserName));

        Dbg.Log($"Connecting as {UserName}");
    }

    public void OnNickNameUpdated(Text newName)
    {
        UserName = newName.text;
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_LEAVE_ROOM:
                var lobby = SceneManager.GetSceneAt(0);
                SceneManager.MoveGameObjectToScene(this.gameObject, lobby);
                break;
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnConnected()
    {
        ChatClient.Subscribe(
            ChannelsToAutoJoin[0],
            0,
            HistoryLength,
            new ChannelCreationOptions { PublishSubscribers = true });
    }

    public void SendChatMessage(string msg)
    {
        ChatClient.PublishMessage(ChannelsToAutoJoin[0], msg);
        if (Utils.IsNull(MessageInputField))
        {
            MessageInputField = GameObject.Find("InputField_Chat").GetComponent<InputField>();
            MessageInputField.text = "";
            MessageInputField.ActivateInputField();
            MessageInputField.Select();
            return;
        }

        MessageInputField.text = "";
        MessageInputField.ActivateInputField();
        MessageInputField.Select();
    }

    public void OnDisconnected()
    {

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        ChatClient.TryGetChannel(channelName, out var channel);
        if (Utils.IsNull(MessageText))
        {
            MessageText = GameObject.Find("Content").GetComponent<Text>();
            MessageText.text = channel.ToStringMessages();
            return;
        }
        MessageText.text = channel.ToStringMessages();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        ChatClient.PublishMessage(channels[0], "has Joined the game.");
    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }
}
