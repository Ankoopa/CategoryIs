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
    public GameObject enemyCard;
    public GameObject playerDeck;
    public GameObject enemyCardPanel;
    public Text timerText;
    public bool isMyTurn;
    public static bool isStartingGame;
    public static bool isValid;
    [Header ("Abilities")]
    public static bool isReverseClockwise;
    public static bool isRotUsed;
    public static bool isSkip;
    public static float timeLeft;
    public static bool isTime;
    private InputField wordInput;
    public List<GameObject> cardsInDeck = new List<GameObject>();
    private bool isTimeRunning;

    void Start()
    {
        timeLeft = 15f;
        isTimeRunning = true;
        isReverseClockwise = false;
        wordInput = wordTextbox.GetComponent<InputField>();

        if (PhotonNetwork.IsMasterClient)
        {
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
                if (isTime)
                {
                    timeLeft += 10f;
                    isTime = false;
                }
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
                        EnableDisableCards(true);
                    }
                    else
                    {
                        endTurnButton.SetActive(false);
                        wordInput.text = string.Empty;
                        wordInput.interactable = false;
                        EnableDisableCards(false);
                    }
                }
            }
        }
        
    }
    public void DrawCards()
    {
        for (int i =0 ; i < 3; i++)
        {
            cardInfo.DrawingCards();
            cardsInDeck.Add(Instantiate(card, playerDeck.transform));
            base.photonView.RPC("RPC_EnemyCard", RpcTarget.OthersBuffered);
        }
        
    }
    public void OnClickEndTurn()
    {  
        if (isValid)
        {
            if (isSkip || isRotUsed)
            {
                base.photonView.RPC("RPC_EndTurn", RpcTarget.AllBufferedViaServer);
                isRotUsed = false;
                isSkip = false;
            }
            else
            {
                base.photonView.RPC("RPC_EndTurn", RpcTarget.AllBufferedViaServer);
                cardInfo.DrawingCards();
                cardsInDeck.Add(Instantiate(card, playerDeck.transform));
                base.photonView.RPC("RPC_EnemyCard", RpcTarget.OthersBuffered);
            }
        }
    }

    void EnableDisableCards(bool isEnable)
    {
        foreach (GameObject c in cardsInDeck)
        {
            c.GetComponent<Button>().interactable = isEnable;
        }
    }

    [PunRPC]
    private void RPC_EndTurn()
    {
        isTimeRunning = false;
        if (!isReverseClockwise && !isTime)
        {
            Debug.Log("ClockWise");
            if (PlayerTurnNumber < PhotonNetwork.CurrentRoom.PlayerCount)
            {
                isMyTurn = false;
                timeLeft = 15f;
                PlayerTurnNumber += 1;          
            }
            else
            {
                if (PlayerTurnNumber >= PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    timeLeft = 15f;
                    PlayerTurnNumber = 1;
                }
            }
        }
        else
        {
            Debug.Log("ReverseClockWise");
            if (PlayerTurnNumber <= PhotonNetwork.CurrentRoom.PlayerCount && PlayerTurnNumber != 1)
            {
                isMyTurn = false;
                timeLeft = 15f;
                PlayerTurnNumber -= 1;          
            }
            else
                if (PlayerTurnNumber > 0)
                {
                    timeLeft = 15f;
                    PlayerTurnNumber = PhotonNetwork.CurrentRoom.PlayerCount;
                }
        }
      
        isTimeRunning = true;
                
    }
    [PunRPC]
    private void RPC_EnemyCard()
    {
        enemyCardPanel = GameObject.Find("EnemyCardsPanel");
        cardInfo.DrawEnemyCards();
        Instantiate(enemyCard, enemyCardPanel.transform);
    }
    [PunRPC]
    private void RPC_timerCountDown()
    {
        timeLeft -=  0.1f * Time.deltaTime;
        timerText.text = Mathf.Round(timeLeft).ToString();
        if (timeLeft < 0)
        {
            timeLeft = 0;
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
