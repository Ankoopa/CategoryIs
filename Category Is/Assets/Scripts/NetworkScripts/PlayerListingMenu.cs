using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlayerListings _playersList;

    public Text _readyUpText;
    
    [SerializeField]
    private List<PlayerListings> _listing = new List<PlayerListings>();
    private bool _ready = false;
    
    public NetworkLobby networkScript;

    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
        Debug.Log("Setting ready");
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (_ready)
        {
            Debug.Log("Setting text ready");
            _readyUpText.text = "Ready";
        }
         
        else
        {
             Debug.Log("Setting text not ready");
            _readyUpText.text = "Not Ready";
        }

    }
    public override void OnJoinedRoom()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }
    private void AddPlayerListing(Player player)
    {
        PlayerListings listing = Instantiate(_playersList, _content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listing.Add(listing);
        }
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listing.FindIndex(x => x.Player == otherPlayer);
        if (index != 1)
        {
            Destroy(_listing[index].gameObject);
            _listing.RemoveAt(index);
        }
    }

    public void OnClickReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);
        }
    }
    
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        networkScript.OnBackButtonClicked();
    }
    public void LoadLevel()
    {
            //PhotonNetwork.CurrentRoom.IsOpen = false;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < _listing.Count; i++)
            {
                Debug.Log(_listing[i].Player + " is ready " + _listing[i].Ready);
                if (_listing[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (!_listing[i].Ready)
                        return;
                }
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel("GameCardScene");
        }   
    }
    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = _listing.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listing[index].Ready = ready;
        }
    }
}
