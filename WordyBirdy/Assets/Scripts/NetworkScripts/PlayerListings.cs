using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class PlayerListings : MonoBehaviourPun
{

    [SerializeField]
    private Text _nameText;
    private int SpriteIndex;
    public Image Avatar;
    public ChoosingAvatar avatarScript;
    public bool Ready = false;
    public Player Player { get; private set; }
    
    public void SetPlayerInfo(Player player)
    {

        Player localPlayer = PhotonNetwork.LocalPlayer;
        Player = player;
        _nameText.text = player.NickName;
        SetPlayerAvatar();
    }

    public void SetPlayerAvatar()
    {
        Avatar.sprite = ChoosingAvatar.ChooseAvatar().AvatarImage;
        SpriteIndex = ChoosingAvatar.AvaIndex();
        photonView.RPC("_PlayerAvatar", RpcTarget.AllBufferedViaServer, SpriteIndex);
    }

}
