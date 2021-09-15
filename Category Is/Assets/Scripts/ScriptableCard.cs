using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class ScriptableCard : ScriptableObject
{

    public string UCardID;
    
    [Header ("Image")]
    public Sprite image;                                                                //card image

    [Header("Properties")]
    public string CardName;
    public int CardAmount;                                                              //Amount of cards in Available
    public List<CardAbility> ability = new List<CardAbility>();
    [SerializeField, TextArea(1,30)] public string Description;

}
