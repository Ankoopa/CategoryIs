using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerListings : MonoBehaviour
{

    [SerializeField]
    private Text _nameText;
    //[SerializeField]
    //private Sprite Avatar;

    public Player Player { get; private set; }
    
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        _nameText.text = player.NickName;
    }
}
