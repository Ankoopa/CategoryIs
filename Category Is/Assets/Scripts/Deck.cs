using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card cardsInDeck;
    public ScriptableCardDB cards;
    public List<ScriptableCard> deck;

    [SerializeField] public static int Sum = 0;
    
    void Awake()
    {
        LoadingDeck();
    }
    public void LoadingDeck()
    {
        //Debug.Log(Sum);
        int counterCards = 0;
        foreach (var cardsInfo in cards.allCards)
        {
            for (int i = 0; i < cardsInfo.CardAmount; i++)
            {
                deck.Add(cardsInfo);
            }
        }
    }
}
