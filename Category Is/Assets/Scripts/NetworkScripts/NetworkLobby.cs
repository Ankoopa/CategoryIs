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
    public Text roomInputField;
    public PlayerNameField playerName;
    private RoomOptions rmOpts;
    
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void SubmitName()
    {
        Debug.Log(PhotonNetwork.NickName);
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
        // for (int i = 3; i <= rCode.Length; i+=4)
        // {
        //     rCode = rCode.Insert(i, " ");
        // }
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
        foreach (var playername in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(playername);
        }
        //Debug.Log(PhotonNetwork.NickName + " has joined room");
        // if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        // {
        //     LoadLevel();
        // }       
        
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogErrorFormat("Room creation failed with error code {0} and error message {1}", returnCode, message);
    }

    // public override void OnPlayerEnteredRoom(Player newPlayer)
    // {
    //     base.OnPlayerEnteredRoom(newPlayer);
    //     LoadLevel();
    // }

    public override void OnCreatedRoom()
    {   
        Debug.Log("Created Successfuly");
        base.OnCreatedRoom();
        //LoadLevel();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room creation failed. Trying again...");
        base.OnCreateRoomFailed(returnCode, message);
        CreateRoom();
    }

    public void OnBackButtonClicked()
    {
        Debug.Log("Left the room");
        backButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);
    }

    public void JoinLobbyOnClick()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }
}
