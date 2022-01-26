using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerDeck;
    public GameObject enemyPanel;
    public GameObject enemyDeck;
    public GameObject playerAvatar;

    public Card cardInfo;
    public Deck deckInfo;
    public Text playerText;

    void Start()
    {
        AssignPlayers();
    }

    void DealCards()
    {

        /*
        cardInfo.LifeCardPerPlayer();
        Instantiate(card, playerDeck.transform);

        for (int i = 0; i < 5; i++)
        {
            cardInfo.DrawingCards();
            Instantiate(card, playerDeck.transform);
        }
        */
    }

    void AssignPlayers()
    {
        playerText.text = PhotonNetwork.LocalPlayer.NickName;

        //can spawn up to 3 opponents from the player list
        foreach (Player plr in PhotonNetwork.PlayerList)
        {
            if (plr.IsLocal) continue;
            else // Instantiate opponent decks on the panel and sets their name accordingly
            {
                GameObject curDeck = GameObject.Instantiate(enemyDeck, enemyPanel.transform);
                //GameObject curDeck = PhotonNetwork.Instantiate("EnemyHand", new Vector3(enemyPanel.transform.position.x, enemyPanel.transform.position.y, 0), Quaternion.identity);
                //curDeck.transform.SetParent(enemyPanel.transform);
                Text enemyName = curDeck.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
                enemyName.text = plr.NickName;
            }
        }

        //TODO: opponent decks have cards and avatar. opponent card dimensions: w70 h105
    }
}
