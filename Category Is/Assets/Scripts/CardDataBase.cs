using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardDataBase : MonoBehaviour
{
    public ScriptableCardDB cards;
    
    private static CardDataBase instance;

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log(instance + "card");
            Destroy(gameObject);
        }
    }


    public static ScriptableCard GetCardByID(string ID)
    {
        return instance.cards.allCards.FirstOrDefault(i => i.UCardID == ID);
    }

    public static ScriptableCard GetRandomCard()
    {
        Debug.Log(instance);
        return instance.cards.allCards[Random.Range(0, instance.cards.allCards.Count())];
    }
}
