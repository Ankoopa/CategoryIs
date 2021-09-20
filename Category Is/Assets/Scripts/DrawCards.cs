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

    public void BtnClick()
    {
        for(int i = -275; i <= 250; i+=105)
        {
            cardNum++;

            DeckInfo.ReadCards();
            cardInfo.RandomizeCards();
            GameObject playerCard = Instantiate(card, new Vector3(i, generateYPos(cardNum), 0), Quaternion.identity);
            playerCard.transform.SetParent(playerDeck.transform, false);

            // GameObject enemyCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            // enemyCard.transform.SetParent(enemyDeck.transform, false);
        }
    }

    int generateYPos(int num)
    {
        int YPos;

        if(num%2 == 0)
        {
            YPos = 30;
        }
        else
        {
            YPos = 50;
        }

        return YPos;
    }
}
