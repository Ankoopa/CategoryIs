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

    void Awake()
    {
        GM = GameObject.Find("TurnManager").GetComponent<GameController>();
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
                GM.AbilityUsed(GameController.CardAbilities.Rotation);
            }
            else if(ability.skip)
            {
                GM.AbilityUsed(GameController.CardAbilities.Skip);
            }
            else if(ability.isTime)
            {
                GM.AbilityUsed(GameController.CardAbilities.AddTime);
            }
            else if(ability.shuffle)
            {
                deck.ShuffleDeck(deck.GameDeck);
            }
            else if (ability.isGreed)
            {
                GM.DrawCards();
            }
        }
        GM.cardsInDeck.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
