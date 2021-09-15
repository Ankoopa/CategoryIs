using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public List<ScriptableCard> thisCard;

    [Header("Image")]
    public Sprite cardImage;

    [Header("Properties")]
    public string CardName;
    public int CardAmount;
    public List<CardAbility> CardAbilities = new List<CardAbility>();

    [Header("Card Description")]
    [SerializeField, TextArea(1, 30)] public string Description;

    public void Start()
    {
        gameObject.GetComponent<Image>().sprite = cardImage;
    }
}
