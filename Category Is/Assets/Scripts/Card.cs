using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [Header("Card Attributes")]
    public int id;
    public string cardName;
    public int ability;
    public string cardAbilityDescription;
    
    
    public Card(int Id, string CardName, int Ability, string CardAbilityDescription)
    {
        id = Id;
        cardName = CardName;
        ability = Ability;
        cardAbilityDescription = CardAbilityDescription;
    }
}
