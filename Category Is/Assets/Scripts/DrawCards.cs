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
    
    public void BtnClick()
    {
        for(int i = 0; i < 6; i++)
        {
            cardInfo.RandomizeCards();
            GameObject playerCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(playerDeck.transform, false);

            // GameObject enemyCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            // enemyCard.transform.SetParent(enemyDeck.transform, false);
        }
    }
}
