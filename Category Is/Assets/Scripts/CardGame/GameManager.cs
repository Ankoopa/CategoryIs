using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject card;
    public GameObject playerDeck;
    public GameObject enemyDeck;
    public GameObject slots, slots2;
    public GameObject[] cardSlots, cardSlots2;

    public Card cardInfo;
    public Deck DeckInfo;
    public Text playerNameText1, playerNameText2;

    private int cardNum = 0;
    
    void Start()
    {
        AssignPlayers();
        DealCards();

        DeckInfo.GameDeck.AddRange(DeckInfo.TempDeck1);
        DeckInfo.GameDeck.AddRange(DeckInfo.TempDeck2);
        DeckInfo.ShuffleDeck(DeckInfo.GameDeck);
    }

    void DealCards()
    {
        //Makes sure that each player gets a lifecard at the beginning of the game
        cardInfo.LifeCardPerPlayer();
        GameObject playerCard = Instantiate(card, new Vector3(cardSlots[0].transform.localPosition.x, GenerateYPos(cardNum, false), 0), Quaternion.identity);
        playerCard.transform.SetParent(slots.transform, false);

        //Deals cards from the deck
        foreach (GameObject slot in cardSlots)
        {
            if (slot == cardSlots[0]) continue;
            cardNum++;
            cardInfo.DrawingCards();
            playerCard = Instantiate(card, new Vector3(slot.transform.localPosition.x, GenerateYPos(cardNum, false), 0), Quaternion.identity);
            playerCard.transform.SetParent(slots.transform, false);
        }

        cardNum = 0;

        foreach (GameObject slot in cardSlots2)
        {
            cardNum++;
            cardInfo.DrawEnemyCards();
            GameObject enemyCard = Instantiate(card, new Vector3(slot.transform.localPosition.x, GenerateYPos(cardNum, true), 0), Quaternion.identity);
            enemyCard.transform.SetParent(slots2.transform, false);
        }
    }


    int GenerateYPos(int num, bool isUpper)
    {
        int YPos;

        if (num % 2 == 0)
        {
            if(isUpper) YPos = -20;
            else YPos = 20;
        }
        else YPos = 0;

        return YPos;
    }

    void AssignPlayers()
    {
        try
        {
            if (PhotonNetwork.PlayerList[0].NickName == PhotonNetwork.LocalPlayer.NickName)
            {
                playerNameText1.text = "Player 1: " + PhotonNetwork.PlayerList[0].NickName;
                playerNameText2.text = "Player 2: " + PhotonNetwork.PlayerList[1].NickName;
            }
            else
            {
                playerNameText1.text = "Player 2: " + PhotonNetwork.PlayerList[1].NickName;
                playerNameText2.text = "Player 1: " + PhotonNetwork.PlayerList[0].NickName;
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError(e);
        }
    }
}
