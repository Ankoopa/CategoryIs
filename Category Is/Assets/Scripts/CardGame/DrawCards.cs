using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class DrawCards : MonoBehaviour
{
    public GameObject card;
    public GameObject cardPanel;
    public Text playerText;

    public Card cardInfo;
    public Deck deckInfo;

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
        playerCard.transform.SetParent(cardPanel.transform, false);

        for (int i = 0; i < 5; i++)
        {
            cardInfo.DrawingCards();
            playerCard = Instantiate(card);
            playerCard.transform.SetParent(cardPanel.transform, false);
        }
    }

    void AssignPlayers()
    {
        playerText.text = PhotonNetwork.LocalPlayer.NickName;

        //TODO: Instantiate opponent panels.
    }
}
