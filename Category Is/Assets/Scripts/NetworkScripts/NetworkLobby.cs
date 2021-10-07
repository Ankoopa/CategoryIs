using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(InputField))]
public class NetworkLobby : MonoBehaviourPunCallbacks
{
    const string playerNamePrefKey = "PlayerName";
    public Button submitBtn;

    InputField _inputField;
    // Start is called before the first frame update
    void Start()
    {

        string defaultName = string.Empty;
        _inputField = this.GetComponent<InputField>();

        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }
        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }

    public void SubmitName()
    {
        Debug.Log(PhotonNetwork.NickName);
        if (string.IsNullOrEmpty(PhotonNetwork.NickName) || PhotonNetwork.NickName == playerNamePrefKey)
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

    void CreateRoom()
    {
        int randRmName = Random.Range(1, 100);
        RoomOptions rmOpts = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Room" + randRmName, rmOpts);
        Debug.Log("Room created: Room" + randRmName);
    }

    void LoadLevel()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.LoadLevel("GameCardScene");
        }
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Player joined room");
        base.OnJoinedRoom();
        LoadLevel();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        LoadLevel();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room creation failed. Trying again...");
        base.OnCreateRoomFailed(returnCode, message);
        CreateRoom();
    }
}
