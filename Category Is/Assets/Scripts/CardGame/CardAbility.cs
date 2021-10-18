using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType : byte {PIECE, LIFE, DEATH, DECK, TURN, SELF}

[System.Serializable]
public struct CardAbility
{
    public AbilityType abilityType; //Just to display/visualize
    //public List<Target> targets;
    //public ScriptableAbilities ability;
    //[Header("Targets")]
    //public List<Target> targets = new List<Target>();

    [Header ("LIFE and DEATH")]
    public bool saved;
    public bool dead;

    [Header ("SHUFFLE")]
    public bool shuffle;

    [Header ("GREED")]
    public bool isGreed;
    public int CardsToPick;

    [Header ("ROTATION")]
    public bool clockwise;

    [Header ("SKIP")]
    public bool skip;

    [Header ("STEAL")]
    //steal code

    [Header ("TIME")]
    public bool isTime;
    public int TimeIncrease;
}