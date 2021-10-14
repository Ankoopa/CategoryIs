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
    public GameObject enemyDeck;
    public GameObject playerAvatar;

    public Card cardInfo;
    public Deck deckInfo;
    public Text playerText;

    private int cardNum = 0;


    void Start()
    {
        AssignPlayers();
        DealCards();

        deckInfo.GameDeck.AddRange(deckInfo.TempDeck1);
        deckInfo.GameDeck.AddRange(deckInfo.TempDeck2);
        deckInfo.ShuffleDeck(deckInfo.GameDeck);
    }

    void DealCards()
    {
        cardInfo.LifeCardPerPlayer();
        GameObject playerCard = Instantiate(card);
        playerCard.transform.SetParent(playerDeck.transform, false);

        for (int i = 0; i < 5; i++)
        {
            cardInfo.DrawingCards();
            playerCard = Instantiate(card);
            playerCard.transform.SetParent(playerDeck.transform, false);
        }
    }

    void AssignPlayers()
    {
        playerText.text = PhotonNetwork.LocalPlayer.NickName;

        //TODO: Instantiate opponent panels.
    }
}
