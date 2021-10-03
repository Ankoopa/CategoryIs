using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(InputField))]
public class PlayerNameInput : MonoBehaviour
{
    const string playerNamePrefKey = "PlayerName";
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
            PhotonNetwork.LoadLevel("GameCardScene");
        }
    }
}
