using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class PlayerListings : MonoBehaviour
{

    [SerializeField]
    private Text _nameText;
    public Image Avatar;
    public ChoosingAvatar avatarScript;
    public bool Ready = false;
    public Player Player { get; private set; }
    
    void Start()
    {
        //SetPlayerAvatar(ChoosingAvatar.ChooseAvatar());
    }
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        _nameText.text = player.NickName;
        // Avatar.sprite = ChoosingAvatar.ChooseAvatar().sprite;
    }

    public void SetPlayerAvatar(Image i)
    {
        Avatar.sprite = i.sprite;
    }
}
