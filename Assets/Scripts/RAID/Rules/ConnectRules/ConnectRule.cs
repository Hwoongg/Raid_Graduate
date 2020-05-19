using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class ConnectRule : MonoBehaviourPunCallbacks
{
    /// <summary>
    /// Indicate the game version. just keep this value "1".
    /// 게임 버전 표시합니다. 1 로 유지.
    /// </summary>
    public string GameVersion { get; set; } = "1";
    /// <summary>
    /// The maximum number of players per room. When a room is full, it cannot be joined by new players, and so new room will be created.
    /// </summary>
    [Tooltip("The maximum number of players per room. When a room is full, it cannot be joined by new players, and so new room will be created.")]
    [SerializeField]
    byte MaxPlayersPerRoom = 3;
    /// <summary>
    /// Keep track of the current process since connection is asynchronous and is based on several callbacks from Photon,
    /// We need to keep track of this to properly adjust the behaviour when we receive call back by Photon.
    /// Typically this is used for the OnConnectedToMaster() callback.
    /// </summary>
    public bool IsConnecting { get; set; } = false;
    public string NickName;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // If AutomaticallySyncScene is true, Master client can call PhotonNetwork.Loadlevel().
        // and every connected players automatically will load the same level.
        // 마스터 클라이언트(방장) PhotonNetwork.LoadLevel() 을 사용할 수 있게 되고,
        // 같은 방에 있는 모든 클라이언트들은 같은 레벨로 자동으로 싱크를 맞춤.
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    /// <summary>
    /// Start the connection.
    /// 연결 시작.
    /// - If already connected, we attempt joining a random room.
    ///   만약 연결되었으면, 랜덤 룸으로 입장 시도.
    /// - If not yet connected, Connect this app instance to Photon Cloud Network.
    ///   연결 되어있지 않으면, 앱인스턴스를 포톤 클라우드 네트워크로 연결.
    /// </summary>
    public void InitializeConnection()
    {
        // Keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then.
        IsConnecting = true;
        // If we are not connected, initiate the connection to the server.
        if (true == PhotonNetwork.IsConnected)
        {
            // We need to attempt joinging a random room.
            //if it fails we'll get notified in 'OnJoinRandomFailed()' and we'll create new one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // We first must connect to Photon online server.
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
        //LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_CONNECTED);
    }

    //Instantiate(objMissile, LeftFireTransform[i].position, LeftFireTransform[i].rotation);
    //LogicEventListener.Invoke(eEventType.FOR_ALL, eEventMessage.ON_MISSILE_SPAWNED, LeftFireTransform[i], "MISSILE");
    //Instantiate(objMissile, RightFireTransform[i].position, RightFireTransform[i].rotation);
    //LogicEventListener.Invoke(eEventType.FOR_ALL, eEventMessage.ON_MISSILE_SPAWNED, RightFireTransform[i], "MISSILE");

    /// <summary>
    /// Called when the client is connected to the Master Server and ready for matchmaking and other tasks.
    /// </summary>
    /// <remarks>
    /// The list of available rooms won't become available unless you join a lobby via LoadBalancingClient.OpJoinLobby.
    /// You can join rooms and create them even without being in a lobby. The default lobby is used in that case.
    /// </remarks>
    /// <
    public override void OnConnectedToMaster()
    {
        Dbg.Log($"ConnectRule: OnConnectedToMaster() was called by PUN.");
        // We don't want to do anything if we are not attempting to join a room.
        // in this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called,
        //we don't want to do anything.
        if (IsConnecting)
        {
            // The first we try to do is to join a potential existing room. If there is good, else, we'll be called back with OnJoinRandomFailed().
            PhotonNetwork.JoinRandomRoom();
        }
    }

    /// <summary>
    /// Called to signal that the raw connection got established but before the client can call operation on the server.
    /// </summary>
    /// <remarks>
    /// After the (low level transport) connection is established, the client will automatically send
    /// the Authentification operation, which needs to get a response before the client can call other operations.
    /// 
    /// Your logic should wait for either: OnRegionListReceived() or OnConnectedToMaster().
    /// 
    /// This callback is useful to detect if the server can be reached at all (technically).
    /// Most often, it's enough to implement OnDisconnected().
    /// 
    /// This is not called for transitions from the masterserver to game servers.
    /// </remarks>
    /// <param name="cause"></param>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Dbg.LogW($"ConnectRule: OnDisconnected() was called by PUN with reason {cause.ToString()}");
    }

    /// <summary>
    /// Called when a previous OnJoinRandom call failed on the server.
    /// </summary>
    /// <remarks>
    /// The most common causes are that a room is full or does not exist (due to someone else being faster or closing the room).
    /// When using multiple lobbies (via OpJoinLobby or a TypedLobby parameter), another lobby might have more/fitting rooms.<br/>
    /// </remarks>
    /// <param name="returnCode">Operation ReturnCode from the server.</param>
    /// <param name="message">Debug message for the error.</param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Dbg.Log($"ConnectRule: OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\n" +
            $"Calling: PhotonNetwork.CreateRoom().");
        // If we failed to join a random room, maybe none exists or they are all full.
        // then we create a new room.
        var roomOpt = new RoomOptions()
        {
            MaxPlayers = MaxPlayersPerRoom /*Max players count is Currently 3*/
        };
        PhotonNetwork.CreateRoom(default, roomOpt);
    }

    /// <summary>
    /// Called when the LoadBalancingClient entered a room, no matter if this client created it or simply joined.
    /// </summary>
    /// <remarks>
    /// When this is called, you can access the existing players in Room.Players, their custom properties and Room.CustomProperties.
    /// In this callback, you could create player objects. For example in Unity, Instantiate a prefab for the player.
    /// If you want a match to be started "actively", enable the user to signal "ready" (using OpRaiseEvent() or a Custom Property).
    /// </remarks>
    public override void OnJoinedRoom()
    {
        Dbg.Log($"ConnectRule: OnJoinedRoom() called by PUN. Now this client is in a room.");
        LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_CONNECTED);
    }
};
