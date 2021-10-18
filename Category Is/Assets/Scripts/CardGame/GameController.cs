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
    public Card cardInfo;
    public GameObject endTurnButton;
    public GameObject wordTextbox;
    public GameObject card;
    public GameObject playerDeck;
    public Text timerText;
    public bool isMyTurn;
    public static bool isStartingGame;
    public static bool isValid;

    private int numCards;
    private InputField wordInput;
    private bool isAlive;
    private float timeLeft;
    private bool isTimeRunning;

    void Start()
    {
        numCards = 0;
        timeLeft = 15f;
        isTimeRunning = true;
        wordInput = wordTextbox.GetComponent<InputField>();

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
            int RandTurnNumber = Random.Range(1, PhotonNetwork.CurrentRoom.PlayerCount + 1);
            base.photonView.RPC("RPC_randomPlayerTurn", RpcTarget.AllBufferedViaServer, RandTurnNumber);
        }
    }

    void Update()
    { 
        if (isStartingGame)
        {
            if (isTimeRunning)
            {
                base.photonView.RPC("RPC_timerCountDown", RpcTarget.AllBufferedViaServer);
            }
            foreach(var player in PhotonNetwork.PlayerList)
            {
                if (player.ActorNumber == PlayerTurnNumber)
                {
                    isMyTurn = true;
                    if (isMyTurn && player.NickName == PhotonNetwork.LocalPlayer.NickName)
                    {
                        endTurnButton.SetActive(true);
                        wordInput.interactable = true;
                    }
                    else
                    {
                        Debug.Log("EndTurnButtonNotOn");
                        endTurnButton.SetActive(false);
                        wordInput.text = string.Empty;
                        wordInput.interactable = false;
                    }
                }
            }
        }
        
    }
    public void OnClickEndTurn()
    {  
        if (isValid)
        {
            base.photonView.RPC("RPC_EndTurn", RpcTarget.AllBufferedViaServer);
            if(numCards > 0)
            {
                cardInfo.DrawingCards();
                Instantiate(card, playerDeck.transform);
            }
            numCards++;
        }
    }

    [PunRPC]
    private void RPC_EndTurn()
    {
        isTimeRunning = false;
        if (PlayerTurnNumber < PhotonNetwork.CurrentRoom.PlayerCount)
        {
            isMyTurn = false;
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
