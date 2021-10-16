using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class PunTurnManager : MonoBehaviourPunCallbacks
{
    /// <summary>Wraps accessing the "turn" custom properties of a room.</summary>
    public int Turn
    {
        get { return PhotonNetwork.CurrentRoom.GetTurn(); }
        private set { PhotonNetwork.CurrentRoom.SetTurn(value, true); }
    }

    public float ElapsedTimeInTurn
    {
        get { return ((float)(PhotonNetwork.ServerTimestamp - PhotonNetwork.CurrentRoom.GetTurnStart())) / 1000.0f; }
    }

    // should this not be a float?

    public int TurnDuration = 0;

    public List<int> ActionList;


    public bool IsCompletedByAll
    {
        get { return PhotonNetwork.CurrentRoom != null && Turn > 0 && this.finishedPlayers.Count == PhotonNetwork.CurrentRoom.PlayerCount; }
    }

    public bool IsFinishedByMe
    {
        get { return this.finishedPlayers.Contains(PhotonNetwork.LocalPlayer); }
    }

    public bool IsOver
    {
        get { return true; }
    }


    public IPunTurnManagerCallbacks TurnManagerListener;


    private readonly HashSet<Player> finishedPlayers = new HashSet<Player>();
    public const byte TurnManagerEventOffset = 0;
    public const byte EvMove = 1 + TurnManagerEventOffset;
    public const byte EvFinalMove = 2 + TurnManagerEventOffset;

    public void Start()
    {
        //PhotonNetwork.NetworkingClient.EventReceived = OnEvent;
    }


    public void BeginTurn()
    {
        Debug.Log("BeginTurn()");
        Turn = this.Turn + 1; // note: this will set a property in the room, which is available to the other players.
    }


    /// <summary>Call to send an action. Optionally finish the turn, too.</summary>
    /// <param name="move"></param>
    /// <param name="finished"></param>
    public void SendMove(object move, bool finished)
    {
        Debug.Log("I moved");
        if (IsFinishedByMe)
        {
            UnityEngine.Debug.LogWarning("Can't SendMove. Turn is finished by this player.");
            return;
        }

        // along with the actual move, we have to send which turn this move belongs to
        Hashtable moveHt = new Hashtable();
        moveHt.Add("turn", Turn);
        moveHt.Add("move", move);

        byte evCode = (finished) ? EvFinalMove : EvMove;

        PhotonNetwork.RaiseEvent(evCode, moveHt, new RaiseEventOptions() { CachingOption = EventCaching.AddToRoomCache }, SendOptions.SendReliable);
        if (finished)
        {
            PhotonNetwork.LocalPlayer.SetFinishedTurn(Turn);
        }


        // the server won't send the event back to the origin (by default). to get the event, call it locally 
        // (note: the order of events might be mixed up as we do this locally)
        OnEvent(evCode, moveHt, PhotonNetwork.LocalPlayer.ActorNumber);
    }


    public float GetRemainingSeconds()
    {
        return this.TurnDuration - ElapsedTimeInTurn;
    }

    public bool GetPlayerFinishedTurn(Player player)
    {
        if (player != null && this.finishedPlayers != null && this.finishedPlayers.Contains(player))
        {
            return true;
        }

        return false;
    }


    /// <summary>Called when the turns time is over.</summary>
    public void OnTurnTimeEnds()
    {
        Debug.Log("Hello");
        // callback to listener
    }


    /// <summary>Call when the turn is completed a specific players.</summary>
    public void OnPlayerFinishedTurn()
    {
        // callback to listener
    }

    /// <summary>Call when the turn is completed by all and can be finished.</summary>
    public void OnTurnComplete()
    {
        // callback to listener
    }

    /// <summary>
    /// Called internally when the turn starts due to event or BeginTurn().
    /// </summary>
    private void OnTurnStarts()
    {
    }


    public void OnEvent(byte eventCode, object content, int senderId)
    {
        Player sender = PhotonNetwork.LocalPlayer.Get(senderId);
        switch (eventCode)
        {
            case EvMove:
                {
                    Hashtable evTable = content as Hashtable;
                    int turn = (int)evTable["turn"];
                    object move = evTable["move"];
                    this.TurnManagerListener.OnPlayerMove(sender, turn, move);

                    break;
                }
            case EvFinalMove:
                {
                    Hashtable evTable = content as Hashtable;
                    int turn = (int)evTable["turn"];
                    object move = evTable["move"];

                    if (turn == this.Turn)
                    {
                        this.finishedPlayers.Add(sender);

                        this.TurnManagerListener.OnPlayerFinished(sender, turn, move);

                    }

                    if (IsCompletedByAll)
                    {
                        this.TurnManagerListener.OnTurnCompleted(this.Turn);
                    }
                    break;
                }
        }
    }


    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log("OnCustomRoomPropertiesChanged: " + propertiesThatChanged.ToStringFull());

        if (propertiesThatChanged.ContainsKey("Turn"))
        {
            this.finishedPlayers.Clear();
            this.TurnManagerListener.OnTurnBegins(this.Turn);
        }
    }

}


public interface IPunTurnManagerCallbacks
{
    void OnTurnBegins(int turn);

    // when a turn is completed (finished by all players)
    void OnTurnCompleted(int turn);

    // when a player moved (but did not finish the turn)
    void OnPlayerMove(Player player, int turn, object move);

    // when a player finishes a turn (includes the action/move of that player)
    void OnPlayerFinished(Player player, int turn, object move);

    // when a turn completes due to a time constraint (timeout for a turn)
    void OnTurnTimeEnds(int turn);
}


public static class TurnExtensions
{
    public static readonly string TurnPropKey = "Turn"; // currently ongoing turn number
    public static readonly string TurnStartPropKey = "TStart"; // start (server) time for currently ongoing turn (used to calculate end)
    public static readonly string FinishedTurnPropKey = "FToA"; // Finished Turn of Actor (followed by number)

    public static void SetTurn(this Room room, int turn, bool setStartTime = false)
    {
        if (room == null || room.CustomProperties == null)
        {
            return;
        }

        Hashtable turnProps = new Hashtable();
        turnProps[TurnPropKey] = turn;
        if (setStartTime)
        {
            turnProps[TurnStartPropKey] = PhotonNetwork.ServerTimestamp;
        }

        room.SetCustomProperties(turnProps);
    }

    public static int GetTurn(this RoomInfo room)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnPropKey))
        {
            return 0;
        }

        return (int)room.CustomProperties[TurnPropKey];
    }

    /// <summary>Returns the start time when the turn began. This can be used to calculate how long it's going on.</summary>
    public static int GetTurnStart(this RoomInfo room)
    {
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnStartPropKey))
        {
            return 0;
        }

        return (int)room.CustomProperties[TurnStartPropKey];
    }


    /// gets the player's finished turn (from the ROOM properties)
    public static int GetFinishedTurn(this Player player)
    {
        Room room = PhotonNetwork.CurrentRoom;
        if (room == null || room.CustomProperties == null || !room.CustomProperties.ContainsKey(TurnPropKey))
        {
            return 0;
        }

        string propKey = FinishedTurnPropKey + player.UserId;
        return (int)room.CustomProperties[propKey];
    }

    /// sets the player's finished turn (in the ROOM properties)
    public static void SetFinishedTurn(this Player player, int turn)
    {
        Room room = PhotonNetwork.CurrentRoom;
        if (room == null || room.CustomProperties == null)
        {
            return;
        }

        string propKey = FinishedTurnPropKey + player.UserId;
        Hashtable finishedTurnProp = new Hashtable();
        finishedTurnProp[propKey] = turn;

        room.SetCustomProperties(finishedTurnProp);
    }
}