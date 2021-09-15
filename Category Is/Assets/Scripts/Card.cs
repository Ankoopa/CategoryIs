using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public string UCardID;

    [Header("Image")]
    public Sprite image;                                                                

    [Header("Properties")]
    public string CardName;
    public int CardAmount;                                                              
    public List<CardAbility> ability = new List<CardAbility>();

    [Header("Card Description")]
    [SerializeField, TextArea(1, 30)] public string Description;

    public void Start()
    {
        gameObject.GetComponent<Image>().sprite = image;
    }
    
    // public CardInfo(ScriptableCard data)
    // {

    // }
}
