using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType : byte {PIECE, LIFE, DEATH, DECK, TURN, SELF}


[System.Serializable]
public struct CardAbility
{
    public AbilityType abilityType; //Just to display/visualize
    //public List<Target> targets;
    public ScriptableAbilities ability;
}