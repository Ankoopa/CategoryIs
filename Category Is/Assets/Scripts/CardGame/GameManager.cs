using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject card;
    public GameObject playerDeck;
    public GameObject enemyDeck;
    public Card cardInfo;
    public Deck DeckInfo;

    private int cardNum;
    
    void Start()
    {
        //Makes sure that each player gets a lifecard at the beginning of the game
        cardInfo.LifeCardPerPlayer();
        GameObject playerCard = Instantiate(card, new Vector3(-275, generateYPos(cardNum), 0), Quaternion.identity);
        playerCard.transform.SetParent(playerDeck.transform, false);
        //Deals cards from the deck
        for(int i = -170; i <= 250; i+=105)
        {
            cardNum++;
            
            cardInfo.DrawingCards();
            playerCard = Instantiate(card, new Vector3(i, generateYPos(cardNum), 0), Quaternion.identity);
            playerCard.transform.SetParent(playerDeck.transform, false);

            // GameObject enemyCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            // enemyCard.transform.SetParent(enemyDeck.transform, false);
        }

        DeckInfo.GameDeck.AddRange(DeckInfo.TempDeck1);
        DeckInfo.GameDeck.AddRange(DeckInfo.TempDeck2);
        DeckInfo.ShuffleDeck(DeckInfo.GameDeck);
    }

    int generateYPos(int num)
    {
        int YPos;

        if(num%2 == 0)
        {
            YPos = 20;
        }
        else
        {
            YPos = 30;
        }

        return YPos;
    }
}