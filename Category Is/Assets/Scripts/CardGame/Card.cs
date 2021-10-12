using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string CardID;
    [Header("Image")]
    public Image cardFaces; 
    public string searchCardID;
    public int cardAmount;
    
    public void DrawingCards()
    {  
        SetCard(Deck.DrawCardFromDeck());
    }
    
    public void LifeCardPerPlayer()
    {
        SetCard(Deck.FirstDealtCard());
    }

    public void DrawEnemyCards()
    {
        SetCard(Deck.DrawEnemyDeck());
    }

    public void SetCard(ScriptableCard i)
    {
        CardID = i.UCardID;
        cardFaces.sprite = i.cardImage;
        cardAmount = i.CardAmount;
        //Debug.Log(CardID);
    }
}
