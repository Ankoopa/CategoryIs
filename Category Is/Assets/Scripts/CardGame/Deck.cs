using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deck : MonoBehaviour
{
    public Card cardsInDeck;
    public ScriptableCardDB cards;
    public List<ScriptableCard> TempDeck1;
    public List<ScriptableCard> TempDeck2;
    public List<ScriptableCard> GameDeck;

    private HashSet<int> exclude;
    private IEnumerable<int> range;
    private int index;
    private static Deck instance;
    

    [SerializeField] public static int Sum = 0;
    
    void Awake()
    {
        LoadingDeck();
        ShuffleDeck(TempDeck1);

        if (instance == null)
        {
            instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        } 
    }


    private void LoadingDeck()
    {
        //Loads the scriptable cards from the database and add to deck
        int counterCards = 0;
        foreach (var cardsInfo in cards.allCards)
        {
                for (int i = 0; i < cardsInfo.CardAmount; i++)
                {
                    if (cardsInfo.UCardID == "01" || cardsInfo.UCardID == "02")         //Seperates the Life and Death card from the rest of the cards and put them into a temporary list;
                    {
                        TempDeck2.Add(cardsInfo);
                    }
                    else
                    {
                        TempDeck1.Add(cardsInfo);
                    }
                    
                }
            
        }
    }

    public void ShuffleDeck(IList<ScriptableCard> dList)
    {
        //using Fisher-yates-Shuffle System
        System.Random random = new System.Random();
        int n = dList.Count;

        while(n > 1)
        {
            n--;
            int rnd = random.Next(1, n+1);
            ScriptableCard value = dList[rnd];
            dList[rnd] = dList[n];                                                      //Swaps the old value to the new value
            dList[n] = value;
        }

    }

    public static ScriptableCard DrawCardFromDeck()
    {
        //Takes card from the top of the list and removes it from the deck
        instance.TempDeck1.Remove(instance.TempDeck1[0]);
        return instance.TempDeck1[0];
        
    }

    public static ScriptableCard FirstDealtCard()
    {
        //Takes a life card from the temporary list and removes it
        var rand = instance.TempDeck2[Random.Range(6, instance.TempDeck2.Count())];
        instance.TempDeck2.Remove(rand);
        return rand;
    }
}
