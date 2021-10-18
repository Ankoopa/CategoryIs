using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkLobby : MonoBehaviourPunCallbacks
{
    public Button submitBtn;
    public GameObject backButton;
    public GameObject CancelButton;
    public Text RoomCode;
    public Text waitingRoomCode;
    public Text roomInputField;
    public PlayerNameField playerName;
    public PlayerListingMenu playerListScript;
    private RoomOptions rmOpts;
    private bool destroyPlayersList;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Update()
    {
        if (destroyPlayersList)
        {
            DestroyListLocally();
        }
    }
    public void SubmitName()
    {
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            Debug.LogError("Player Name is null or empty");
        }
        else
        {
            PhotonNetwork.JoinRandomRoom();
            submitBtn.gameObject.SetActive(false);
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        Debug.LogError("No open games available");
        CreateRoom();
    }

    public void CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        int randRmName = Random.Range(1, 999999);
        string rCode = randRmName.ToString();

        RoomCode.text = rCode;
        rmOpts = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom(rCode, rmOpts);
        Debug.Log("Room created: Room" + randRmName);
    } 
           

    public void JoinGame()
    {
        Debug.Log("Player joined room");
        backButton.SetActive(false);
        PhotonNetwork.JoinOrCreateRoom(roomInputField.text, rmOpts, TypedLobby.Default);
    }

    public void SearchAndJoinRoom()
    {
        PhotonNetwork.JoinRoom(roomInputField.text);
    }
    public override void OnJoinedRoom()
    {
        waitingRoomCode.text = PhotonNetwork.CurrentRoom.Name;
        foreach (var playername in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(playername);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("Room creation failed with error code {0} and error message {1}", returnCode, message);
    }

    public override void OnCreatedRoom()
    {   
        Debug.Log("Created Successfuly");
        base.OnCreatedRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room creation failed. Trying again...");
        base.OnCreateRoomFailed(returnCode, message);
        CreateRoom();
    }

    public void OnBackButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < playerListScript._listing.Count; i++)
            {
                Destroy(playerListScript._listing[i].gameObject);
                playerListScript._listing.RemoveAt(i);
            }
        }
        else
        {
            for (int i = 0; i < playerListScript._listing.Count; i++)
            {
                Destroy(playerListScript._listing[i].gameObject);
                playerListScript._listing.RemoveAt(i);
            }
        }
        destroyPlayersList = true;
    }

    public void DestroyListLocally()
    {
        if (destroyPlayersList)
        {
            for (int i = 0; i < playerListScript._listing.Count; i++)
            {
                Destroy(playerListScript._listing[i].gameObject);
                playerListScript._listing.RemoveAt(i);
            }
        }
        destroyPlayersList = false;
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.Log("left room");
    }
    
    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
}
