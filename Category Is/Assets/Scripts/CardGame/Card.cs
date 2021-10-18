using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public class Card : MonoBehaviour
{
    public string CardID;
    [Header("Image")]
    public Image cardFaces; 
    public string searchCardID;
    public int cardAmount;
    public List<CardAbility> cardAbility;

    private List<string> cardIDs = new List<string>();
    private string[] cardArr;
    
    public void DrawingCards()
    {
        SetCard(Deck.DrawCardFromDeck());
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
        cardAbility = i.ability;
    }
}