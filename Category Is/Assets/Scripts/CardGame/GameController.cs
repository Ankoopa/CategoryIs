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
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        int RandTurnNumber = Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount + 1);
        PlayerTurnNumber = RandTurnNumber;
        Debug.Log(PlayerTurnNumber + " is first turn");
        
    }

    void Update()
    { 
        Debug.Log(PlayerTurnNumber);
        foreach(var player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == PlayerTurnNumber)
            {
                isTurn = true;
                if (isTurn && player.NickName == PhotonNetwork.LocalPlayer.NickName)
                {
                    
                   
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
        }
        else
            if (PlayerTurnNumber >= PhotonNetwork.CurrentRoom.PlayerCount)
                PlayerTurnNumber = 1;
    }

}
