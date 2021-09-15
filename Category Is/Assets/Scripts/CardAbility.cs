using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType : byte {PIECE, LIFE, DEATH, DECK, TURN, SELF}


[System.Serializable]
public struct CardAbilities
{
    public AbilityType abilityType; //Just to display/visualize
    //public List<Target> targets;
    //public ScriptableAbilities ability;
    //[Header("Targets")]
    //public List<Target> targets = new List<Target>();

    [Header ("LIFE and DEATH")]
    public static bool saved;
    public bool dead;

    [Header ("SHUFFLE")]
    public bool shuffle;

    [Header ("GREED")]
    public int CardsToPick;

    [Header ("ROTATION")]
    public static bool clockwise;

    [Header ("SKIP")]
    public bool skip;

    [Header ("STEAL")]
    //steal code

    [Header ("TIME")]
    public int TimeIncrease;
}

public class CardAbility : MonoBehaviour
{
    public void Rotation()
    {
        if (CardAbilities.clockwise){
            //code
        }
    }
}