using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
    public GameObject parentName;
    public Text timerText;
    public string playerOn;
    public bool isMyTurn;
    public static bool isStartingGame;
    public static bool isValid;
    [Header ("Abilities")]
    public static bool isReverseClockwise;
    public static bool isRotUsed;
    public static bool isSkip;
    public static float timeLeft;
    public static bool isTime;
    public List<GameObject> playerNames = new List<GameObject>();
    public List<GameObject> StolenCards = new List<GameObject>();
    private InputField wordInput;
    public List<GameObject> cardsInDeck = new List<GameObject>();
    public List<GameObject> enemyCardsinHand = new List<GameObject>();
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
    public void DeleteEnemyCard(int enemyCard)
    {
        base.photonView.RPC("RPC_EnemyUsedCard", RpcTarget.OthersBuffered, enemyCard);
    }
    public void DrawCards()
    {
        SoundManager.PlaySound("new_card");
        for (int i =0 ; i < 3; i++)
        {
            cardInfo.DrawingCards();
            cardsInDeck.Add(Instantiate(card, playerDeck.transform));
            base.photonView.RPC("RPC_EnemyCard", RpcTarget.OthersBuffered);
        }
        
    }
    public void OnClickSteal()
    {
        base.photonView.RPC("RPC_Steal", RpcTarget.OthersBuffered);
    }
    public void OnClickEndTurn()
    {  
        if (isValid)
        {
            if (isSkip || isRotUsed)
            {
                base.photonView.RPC("RPC_Rotation", RpcTarget.AllBufferedViaServer, isRotUsed);
                base.photonView.RPC("RPC_EndTurn", RpcTarget.AllBufferedViaServer);
                isSkip = false;
            }
            else
            {
                SoundManager.PlaySound("new_card");
                cardInfo.DrawingCards();
                cardsInDeck.Add(Instantiate(card, playerDeck.transform));
                base.photonView.RPC("RPC_EnemyCard", RpcTarget.AllBufferedViaServer);
                base.photonView.RPC("RPC_EndTurn", RpcTarget.AllBufferedViaServer);
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
    public int turnNumber()
    {
        return PlayerTurnNumber-1;
    }
    public Player turnPlayer()
    {
        return PhotonNetwork.CurrentRoom.GetPlayer(PlayerTurnNumber);
    }
    [PunRPC]
    public void RPC_Steal()
    {
        GameObject stolenCardPlayer = EventSystem.current.currentSelectedGameObject;
        Debug.Log(stolenCardPlayer.name);
    }
    [PunRPC]
    public void RPC_Rotation(bool rotation)
    {
        isRotUsed = rotation;
        if (isRotUsed)
        {
            if (!isReverseClockwise)
            {
                isReverseClockwise = true;
            }
            else
            {
                isReverseClockwise = false;
            }
                
            isRotUsed = false;
        }
    }
    [PunRPC]
    private void RPC_EndTurn()
    {
        isTimeRunning = false;
        if (!isReverseClockwise && !isTime)
        {
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
        Player pl = PhotonNetwork.LocalPlayer;
        Debug.Log(turnPlayer().NickName + " " + pl);
        if (playerOn == turnPlayer().NickName)
        {
            if (enemyCardPanel.name == playerOn)
            {
                cardInfo.DrawEnemyCards();
                Debug.Log(enemyCardPanel.GetComponentInParent<Text>().text);
                enemyCardsinHand.Add(Instantiate(enemyCard, enemyCardPanel.transform));
            }
            
            
        }
        
    }

    [PunRPC]
    public void RPC_EnemyUsedCard(int card)
    {
        GameObject deletedCard;
        deletedCard = enemyCardsinHand[card];
        enemyCardsinHand.RemoveAt(card);
        Destroy(deletedCard);
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
            isTimeRunning = false;
        }
    }

    [PunRPC]
    private void RPC_randomPlayerTurn(int rand)
    {
        PlayerTurnNumber = rand;
        foreach(GameObject playerN in GameObject.FindGameObjectsWithTag("players"))
        {
            playerNames.Add(playerN);
        }
        foreach(GameObject names in playerNames)
        {
            playerOn = names.GetComponent<Text>().text;
            parentName = names;
            enemyCardPanel = GameObject.Find("EnemyCardsPanel");
            enemyCardPanel.name = playerOn;
        }
    }

}
