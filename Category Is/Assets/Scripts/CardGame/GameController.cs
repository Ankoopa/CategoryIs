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
            }
        }
    }

    public void EndTurn()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PlayerTurnNumber < PhotonNetwork.CurrentRoom.PlayerCount)
        {
            isTurn = false;
            Debug.Log("endTurn");
            PlayerTurnNumber += 1;
        }
        // else
            // if (PlayerTurnNumber > PhotonNetwork.CurrentRoom.PlayerCount)
                //PlayerTurnNumber = 1;
    }
}
