using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
public class Abilities : MonoBehaviourPun
{
    public GameController GM;
    public Deck deck;
    private GameObject clickedCard;
    private Card cardAbilities;
    private WordGame wg;

    void Awake()
    {
        GM = GameObject.Find("TurnManager").GetComponent<GameController>();
        wg = GameObject.FindWithTag("GM").GetComponent<WordGame>();
        deck = GameObject.FindWithTag("GM").GetComponent<Deck>();
    }
    public void OnClickCard()
    {
        clickedCard = EventSystem.current.currentSelectedGameObject;
        cardAbilities = clickedCard.GetComponent<Card>();

        GM.DeleteEnemyCard(0);
        
        foreach (var ability in cardAbilities.cardAbility)
        {
            if (ability.clockwise)
            {
                GameController.isRotUsed = true;
                GM.OnClickEndTurn();
            }
            else if(ability.skip)
            {
                GameController.isSkip = true;
                GM.OnClickEndTurn(); 
            }
            else if(ability.isTime)
            {
                GameController.isTime = true;
            }
            else if(ability.shuffle)
            {
                deck.ShuffleDeck(deck.GameDeck);
            }
            else if (ability.isGreed)
            {
                GM.DrawCards();
            }
            else if (ability.dead)
            {
                wg.onClickBlinded();
            }
            // else if (ability.dead)
            // {
            //     GM.pickRandomPlayer();
            // }
        }
        GM.cardsInDeck.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
