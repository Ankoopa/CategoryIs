using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject card;
    public GameObject playerDeck;
    public GameObject enemyPanel;
    public GameObject enemyDeck;
    public GameObject playerAvatar;

    private List<GameObject> playerCards = new List<GameObject>();

    public Card cardInfo;
    public Deck deckInfo;
    public Text playerText;

    void Start()
    {
        DealCards();
        AssignPlayers();

        deckInfo.GameDeck.AddRange(deckInfo.TempDeck1);
        deckInfo.GameDeck.AddRange(deckInfo.TempDeck2);
        deckInfo.ShuffleDeck(deckInfo.GameDeck);
    }

    void DealCards()
    {
        cardInfo.LifeCardPerPlayer();
        Instantiate(card, playerDeck.transform);

        for (int i = 0; i < 5; i++)
        {
            cardInfo.DrawingCards();
            Instantiate(card, playerDeck.transform);
        }
        //TODO: each local player has their own set of cards. their set must be reflected on the server side.
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
                GameObject curDeck = Instantiate(enemyDeck, enemyPanel.transform);
                Text enemyName = curDeck.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
                enemyName.text = plr.NickName;
                GameObject enemyCardPanel = curDeck.transform.GetChild(1).gameObject;
            }
        }

        //TODO: opponent decks have cards and avatar. opponent card dimensions: w70 h105
    }
}
