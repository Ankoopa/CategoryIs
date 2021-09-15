using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewAbility", menuName = "Ability")]
public class ScriptableAbilities : ScriptableObject
{
//    [Header("Targets")]
//    public List<Target> targets = new List<Target>();

    [Header ("LIFE and DEATH")]
    public bool saved;
    public bool dead;

    [Header ("SHUFFLE & GREED")]
    public bool shuffle;
    public int CardsToPick = 0;

    [Header ("SKIP & ROTATION")]
    public bool clockwise;
    public bool skip;

    [Header ("STEAL & TIME")]
    //steal code
    public int TimeIncrease = 0;
}
