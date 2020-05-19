using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class RulePrototype : MonoBehaviourPunCallbacks, ILogicEvent
{
    /// <summary>
    /// Is already moving the nextscene?
    /// </summary>
    protected bool IsMovingNextScene = false;

    EventSet EventSet;

    public override void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    public abstract void OnInvoked(eEventMessage msg, params object[] obj);

    public override void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    /// <summary>
    /// Called when the local user/client left a room, so the game's logic can clean up it's internal state.
    /// </summary>
    /// <remarks>
    /// When leaving a room, the LoadBalancingClient will disconnect the Game Server and connect to the Master Server.
    /// This wraps up multiple internal actions.
    /// Wait for the callback OnConnectedToMaster, before you use lobbies and join or create rooms.
    /// </remarks>
    public override void OnLeftRoom()
    {
        
    }

    /// <summary>
    /// Called when the player is leaving from PhotonNetwork Room explicitly. it's used for abstraction.
    /// </summary>
    public virtual void OnLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
};
