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
    
    public void RandomizeCards()
    {  
        SetCard(CardDataBase.GetRandomCard());
    }
    
    // public void SearchCard()
    // {
    //     SetCard(CardDataBase.GetCardByID(searchCardID));
    // }

    public void SetCard(ScriptableCard i)
    {
        CardID = i.UCardID;
        cardFaces.sprite = i.cardImage;
        cardAmount = i.CardAmount;
        Debug.Log(CardID);
    }
}
