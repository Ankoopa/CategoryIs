using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

public class GameController : MonoBehaviourPun
{
    public int PlayerTurnNumber;
    public GameObject endTurnButton;
    public GameObject wordTextbox;
    public Text timerText;

    private InputField wordInput;
    private bool isTurn;
    private bool isAlive;
    private float timeLeft;
    private bool isTimeRunning;

    void Start()
    {
        timeLeft = 15f;
        isTimeRunning = true;
        wordInput = wordTextbox.GetComponent<InputField>();

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.IsMasterClient)
        {
            int RandTurnNumber = Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount + 1);
            base.photonView.RPC("RPC_randomPlayerTurn", RpcTarget.AllBufferedViaServer, RandTurnNumber);
            Debug.Log("IsHost");
        }
        
    }

    void Update()
    { 
        if (isTimeRunning)
        {
            base.photonView.RPC("RPC_timerCountDown", RpcTarget.AllBufferedViaServer);
        }
        
        Debug.Log(PlayerTurnNumber);
        foreach(var player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == PlayerTurnNumber)
            {
                isTurn = true;
                if (isTurn && player.NickName == PhotonNetwork.LocalPlayer.NickName)
                {
                    endTurnButton.SetActive(true);
                    wordInput.interactable = true;
                }
                else
                {
                    Debug.Log("NotMyTurn");
                    endTurnButton.SetActive(false);
                    wordInput.text = string.Empty;
                    wordInput.interactable = false;
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
        isTimeRunning = false;
        if (PlayerTurnNumber < PhotonNetwork.CurrentRoom.PlayerCount)
        {
            isTurn = false;
            Debug.Log("endTurn");
            timeLeft = 15f;
            PlayerTurnNumber += 1;          
        }
        else
            if (PlayerTurnNumber >= PhotonNetwork.CurrentRoom.PlayerCount)
            {
                timeLeft = 15f;
                PlayerTurnNumber = 1;
            }
        isTimeRunning = true;
                
    }

    [PunRPC]
    private void RPC_timerCountDown()
    {
        timeLeft -=  0.1f * Time.deltaTime;
        timerText.text = Mathf.Round(timeLeft).ToString();
        if (timeLeft < 0)
        {
            timerText.text = "0";
            Debug.Log("Game Over");
            isTimeRunning = false;
        }
    }

    [PunRPC]
    private void RPC_randomPlayerTurn(int rand)
    {
        PlayerTurnNumber = rand;
        Debug.Log(PlayerTurnNumber + " is first turn");
    }
}
