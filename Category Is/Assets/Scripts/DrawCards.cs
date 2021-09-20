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
    public void BtnClick()
    {
        for(int i = -275; i <= 250; i+=105)
        {
            DeckInfo.ReadCards();
            cardInfo.RandomizeCards();
            GameObject playerCard = Instantiate(card, new Vector3(i, -50+(generateYPos()), 0), Quaternion.identity);
            playerCard.transform.SetParent(playerDeck.transform, false);

            // GameObject enemyCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            // enemyCard.transform.SetParent(enemyDeck.transform, false);
        }
    }

    int generateYPos()
    {
        int YPos;
        do
        {
            YPos = Random.Range(-10, 10);
        }
        while (YPos > -5 && YPos < 5);

        return YPos;
    }
}
