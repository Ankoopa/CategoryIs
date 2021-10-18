using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Abilities : MonoBehaviour
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

        foreach (var ability in cardAbilities.cardAbility)
        {
            if (ability.clockwise)
            {
                GameController.isReverseClockwise = true;
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
                GameController.timeLeft += 10;
            }
            else if(ability.shuffle)
            {
                deck.ShuffleDeck(deck.GameDeck);
            }
        }
        Destroy(this.gameObject);
    }
}
