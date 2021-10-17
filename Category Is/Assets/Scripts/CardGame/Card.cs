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

    private List<string> cardIDs = new List<string>();
    private string[] cardArr;
    
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
        
        cardIDs.Add(CardID);
        cardArr = cardIDs.ToArray();

        Hashtable newCards = new Hashtable() { { "OwnCards", cardArr } };

        PhotonNetwork.LocalPlayer.CustomProperties = newCards;
        // Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["OwnCards"]);

        // Debug.Log(CardID);

        //DEBUG: DELETE LATER
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("OwnCards"))
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(newCards);
            //Debug.Log("TEST: " + PhotonNetwork.LocalPlayer.CustomProperties["OwnCards"].ToString());
            string[] cardStrings = (string[])PhotonNetwork.LocalPlayer.CustomProperties["OwnCards"];

            foreach(string s in cardStrings)
            {
                //Debug.Log("Card: " + s);
            }
        }
        //DEBUG: DELETE LATER
    }
}