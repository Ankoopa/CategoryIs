using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;


public class GameController : MonoBehaviourPunCallbacks
{
    private bool isTurn;
    private int PlayerTurnNumber;
    public GameObject endTurnButton;
    void Start()
    {
        PlayerTurnNumber = 1;
    }
    // Update is called once per frame
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
            }else
            {
                PlayerTurnNumber += 1;
            }
        }
    }

    public void EndTurn()
    {
        if (PlayerTurnNumber < PhotonNetwork.CurrentRoom.PlayerCount)
        {
             Debug.Log("endTurn");
            PlayerTurnNumber += 1;
        }
           
        else
            PlayerTurnNumber = 1;
    }
}
