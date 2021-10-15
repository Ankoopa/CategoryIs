using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;


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

        Hashtable hash = new Hashtable();
        hash.Add("OwnCards", CardID);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
}
