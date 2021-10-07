using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCard", menuName = "Asset/Resources/Database/Card Database")]
public class ScriptableCardDB : ScriptableObject
{
    public List<ScriptableCard> allCards;
}
