using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour
{
    public GameObject card;
    public GameObject playerDeck;
    public GameObject enemyDeck;
    public Card cardInfo;
    public Deck DeckInfo;

    private int cardNum;
    private GameObject playerCard;

    public void BtnClick()
    {
        cardInfo.LifeCardPerPlayer();
        playerCard = Instantiate(card, new Vector3(-275, generateYPos(cardNum), 0), Quaternion.identity);
        playerCard.transform.SetParent(playerDeck.transform, false);
        for(int i = -170; i <= 250; i+=105)
        {
            cardNum++;
            
            //DeckInfo.LoadingDeck();
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

    public void PickCard()
    {
        cardInfo.DrawingCards();
        playerCard = Instantiate(card, new Vector3(250, generateYPos(cardNum), 0), Quaternion.identity);
        playerCard.transform.SetParent(playerDeck.transform, false);
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
