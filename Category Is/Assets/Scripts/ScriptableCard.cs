using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public struct CardAndAmount
{
    public ScriptableCard card;
    public int cardAmount;
}
[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class ScriptableCard : ScriptableObject
{
    public string UCardID;
    
    [Header ("Image")]
    public Sprite cardImage;                                                                //card image

    [Header("Properties")]
    public string CardName;
    public int CardAmount;                                                              //Amount of cards in Available
    public List<CardAbility> ability = new List<CardAbility>();

    [Header("Card Description")]
    [SerializeField, TextArea(1,30)] public string Description;

}
