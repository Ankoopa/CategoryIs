using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviourPunCallbacks
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
    public GameObject enemyPanel;
    public string playerOn;
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
    public List<GameObject> playerNames = new List<GameObject>();
    public List<Transform> EnemyPlayers = new List<Transform>();
    public List<GameObject> cardsInDeck = new List<GameObject>();
    public List<GameObject> enemyCardsinHand = new List<GameObject>();
    //public List<GameObject> enemyCards = new List<GameObject>();
    private List<int> endedPlayers = new List<int>();
    private int activePlayers;
    private InputField wordInput;
    private bool isTimeRunning;

    public enum State {Winner, Loser};
    public static State myState;

    void Start()
    {
        activePlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log("Active players: " + activePlayers);
        timeLeft = 20f;
        isTimeRunning = true;
        isReverseClockwise = false;
        wordInput = wordTextbox.GetComponent<InputField>();
        foreach(Transform ep in enemyPanel.GetComponentsInChildren<Transform>())
        {
            EnemyPlayers.Add(ep);
        }
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
                base.photonView.RPC("RPC_timerCountDown", RpcTarget.AllViaServer);
            }
            foreach(var player in PhotonNetwork.PlayerList)
            {
                if ((player.ActorNumber == PlayerTurnNumber) && !endedPlayers.Contains(PlayerTurnNumber))
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
    // public void pickRandomPlayer()
    // {
    //     int rand = Random.Range(0, EnemyPlayers.Count);
    //     for (int i = 0; i < EnemyPlayers.Count; i++)
    //     {
    //         if (rand == i)
    //         {
    //             int randCard = Random.Range(0, enemyCardsinHand.Count);
    //             Debug.Log(randCard);
    //             base.photonView.RPC("RPC_DestroyCard", RpcTarget.OthersBuffered, randCard);
    //         }
    //     }
    // }
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
    public Player turnPlayer()
    {
        return PhotonNetwork.CurrentRoom.GetPlayer(PlayerTurnNumber);
    }

    // [PunRPC]
    // public void RPC_DestroyCard(int rand)
    // {
    //     Debug.Log("Destroyed card");
    //     for (int i = 0; i < enemyCardsinHand.Count; i++)
    //     {
    //         if (rand == i)
    //         {
    //             Debug.Log("Destroyed card");
    //             cardsInDeck.RemoveAt(rand);
    //             Destroy(cardsInDeck[rand]);
    //             base.photonView.RPC("RPC_EnemyCard", RpcTarget.AllBufferedViaServer);
    //         }
    //     }
    // }
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
                timeLeft = 20f;
                PlayerTurnNumber += 1;          
            }
            else
            {
                if (PlayerTurnNumber >= PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    timeLeft = 20f;
                    PlayerTurnNumber = 1;
                }
            }
        }
        else
        {
            if (PlayerTurnNumber <= PhotonNetwork.CurrentRoom.PlayerCount && PlayerTurnNumber != 1)
            {
                isMyTurn = false;
                timeLeft = 20f;
                PlayerTurnNumber -= 1;          
            }
            else
                if (PlayerTurnNumber > 0)
                {
                    timeLeft = 20f;
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
        timeLeft -=  .1f * Time.deltaTime;
        timerText.text = Mathf.Round(timeLeft).ToString();
        if (timeLeft < 0 && isMyTurn)
        {
            timeLeft = 0;
            timerText.text = "0";
            isTimeRunning = false;
            activePlayers -= 1;
            //Debug.Log("Players left: " + activePlayers);
            if(activePlayers <= 1)
            {
                myState = State.Winner;
                StartCoroutine(ReturnToLobby());
            }
            else
            {
                endedPlayers.Add(PlayerTurnNumber);
                base.photonView.RPC("RPC_EndTurn", RpcTarget.AllBufferedViaServer);
                myState = State.Loser;
                StartCoroutine(ReturnToLobby());
            }
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

    IEnumerator ReturnToLobby()
    {
        PhotonNetwork.LeaveRoom();
        while(PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene("GameOverScene");
    }
}
