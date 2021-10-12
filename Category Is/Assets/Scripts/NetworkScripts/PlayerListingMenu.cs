using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlayerListings _playersList;

    private List<PlayerListings> _listing = new List<PlayerListings>();

    private void Awake()
    {
        
    }
    // private void Start() {
    //     GetCurrentRoomPlayers();
    // }
    // public void GetCurrentRoomPlayers()
    //     {
    //         foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
    //         {
    //             AddPlayerListing(playerInfo.Value);
    //         }
    //     }
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
}
