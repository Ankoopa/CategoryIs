using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card cardsInDeck;
    public CardAndAmount[] startingdeck;
    public Deck deck;

    public void ReadCards()
    {
        cardsInDeck.SetCard(CardDataBase.DeckBuild(cardsInDeck.CardID, cardsInDeck.cardAmount));
    }

    public void LoadingDeck()
    {
        for (int i = 0; i < deck.startingdeck.Length; i++)
        {
            // CardAndAmount card = deck.startingdeck[i];
            // for (int n = 0; n < card.cardAmount; ++n)
            // {
            //     //deck.startingdeck.Add(card.cardAmount);
            // }
        }
    }
}
