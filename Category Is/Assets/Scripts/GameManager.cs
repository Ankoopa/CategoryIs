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
    // Start is called before the first frame update
    void Start()
    {
        // for(int i = -275; i <= 250; i+=105)
        // {
        //     cardNum++;
            
        //     //DeckInfo.LoadingDeck();
        //     cardInfo.RandomizeCards();
        //     GameObject playerCard = Instantiate(card, new Vector3(i, generateYPos(cardNum), 0), Quaternion.identity);
        //     playerCard.transform.SetParent(playerDeck.transform, false);

        //     // GameObject enemyCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
        //     // enemyCard.transform.SetParent(enemyDeck.transform, false);
        // }
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
