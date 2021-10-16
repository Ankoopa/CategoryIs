using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;


public class GameController : MonoBehaviourPun
{
    private bool isTurn;
    public int PlayerTurnNumber;
    public GameObject endTurnButton;
    void Start()
    {
        PlayerTurnNumber = 1;
    }

    void Update()
    {
        foreach(var player in PhotonNetwork.PlayerList)
        {
            Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber + " " + PhotonNetwork.NickName);
            if (player.ActorNumber == PlayerTurnNumber)
            {
                isTurn = true;
                Debug.Log(player.ActorNumber + " " +player.NickName + " turn");
                if (isTurn && player.NickName == PhotonNetwork.LocalPlayer.NickName)
                {
                    
                    Debug.Log(PlayerTurnNumber);
                    endTurnButton.SetActive(true);
                }
                else
                {
                    Debug.Log("NotMyTurn");
                    endTurnButton.SetActive(false);
                }
            }
        }
    }
    public void OnClickEndTurn()
    {
        
        base.photonView.RPC("RPC_EndTurn", RpcTarget.AllBufferedViaServer);
        
        //base.photonView.RPC("ChatMessage", RpcTarget.AllBufferedViaServer, "jup", "and jup.");
    }
    [PunRPC]
    private void RPC_EndTurn()
    {
        Debug.Log("endTurn");
        if (PlayerTurnNumber < PhotonNetwork.CurrentRoom.PlayerCount)
        {
            isTurn = false;
            Debug.Log("endTurn");
            PlayerTurnNumber += 1;
            //this.photonView.RPC("RPC_EndTurn", RpcTarget.AllBufferedViaServer, PlayerTurnNumber);
            
        }
        else
            if (PlayerTurnNumber >= PhotonNetwork.CurrentRoom.PlayerCount)
                PlayerTurnNumber = 1;
    }

    [PunRPC]
    void ChatMessage(string a, string b)
    {
        Debug.Log(string.Format("ChatMessage {0} {1}", a, b));
    }

}
