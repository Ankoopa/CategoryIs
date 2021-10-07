using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCard : MonoBehaviour
{
    [Header("Card Drag & Hover")]
    public HandCardHover cardDragHover;

    [HideInInspector] public int handIndex;

    // public void AddCard(CardInfo newCard, int index, PlayerType playerT)
    // {
    //     handIndex = index;
    //     playerType = playerT;

    //     Enable hover on player cards. We disable it for enemy cards.
    //     cardDragHover.canHover = true;
        //cardOutline.gameObject.SetActive(true);

        // Reveal card FRONT, hide card BACK
        // cardfront.color = Color.white;
        // cardback.color = Color.clear;

        // Set card image
        //image.sprite = newCard.image;

        // Assign description, name and remaining stats
        // description.text = newCard.description; // Description
        // cost.text = newCard.cost; // Cost
        // cardName.text = newCard.name;

        // Only set Health & Strength if CreatureCard
        // if (newCard.data is CreatureCard)
        // {
        //     health.text = ((CreatureCard)newCard.data).health.ToString();
        //     strength.text = ((CreatureCard)newCard.data).strength.ToString();
        // }
    // }
}
