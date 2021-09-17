using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawCards : MonoBehaviour
{
    public GameObject card;
    public GameObject playerDeck;
    public GameObject enemyDeck;
    
    // Start is called before the first frame update
    public void BtnClick()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject playerCard = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
            playerCard.transform.SetParent(playerDeck.transform, false);
        }
    }
}
