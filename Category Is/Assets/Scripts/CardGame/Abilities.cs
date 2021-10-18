using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Abilities : MonoBehaviour
{
    private GameObject clickedCard;
    private Card cardAbilities;
    public void OnClickCard()
    {
        clickedCard = EventSystem.current.currentSelectedGameObject;
        cardAbilities = clickedCard.GetComponent<Card>();

        foreach (var ability in cardAbilities.cardAbility)
        {
            Debug.Log(ability.skip);
        }
    }
}
