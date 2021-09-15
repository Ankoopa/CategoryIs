using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType : byte {PIECE, LIFE, DEATH, DECK, TURN, SELF}


[System.Serializable]
public struct CardAbility
{
    public AbilityType abilityType; //Just to display/visualize
    //public List<Target> targets;
    // public ScriptableAbilities ability;
    //[Header("Targets")]
    //public List<Target> targets = new List<Target>();

    [Header ("LIFE and DEATH")]
    public bool saved;
    public bool dead;

    [Header ("SHUFFLE & GREED")]
    public bool shuffle;
    public int CardsToPick;

    [Header ("SKIP & ROTATION")]
    public bool clockwise;
    public bool skip;

    [Header ("STEAL & TIME")]
    //steal code
    public int TimeIncrease;
}