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
                    if (cardsInfo.UCardID == "01" || cardsInfo.UCardID == "02")
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
        //using Fisher-yates-Shuffle
        System.Random random = new System.Random();
        int n = dList.Count;

        while(n > 1)
        {
            n--;
            int rnd = random.Next(1, n+1);
            ScriptableCard value = dList[rnd];
            dList[rnd] = dList[n];
            dList[n] = value;
        }

    }

    // private int RandomCardNumber()
    // {
    //     for (int i = 0; i < instance.deck.Count; i++)
    //     {
    //         if (instance.deck[i].UCardID == "01" || instance.deck[i].UCardID == "02")
    //         {
    //             instance.exclude = new HashSet<int>() {i};
    //             instance.range = Enumerable.Range(0, instance.deck.Count()).Where(i =>!instance.exclude.Contains(i));
    //             foreach(var h in instance.range)
    //                 {
    //                     print("index" + h);
    //                 }
    //             var rand = new System.Random();
    //             instance.index = rand.Next(0, instance.deck.Count() - instance.exclude.Count);
    //         }
    //     }
        
    //     //Debug.Log(instance.index);
    //     return instance.range.ElementAt(instance.index);
    // }
    public static ScriptableCard DrawCardFromDeck()
    {

        instance.TempDeck1.Remove(instance.TempDeck1[0]);
        return instance.TempDeck1[0];
        
    }

    public static ScriptableCard FirstDealtCard()
    {
        var rand = instance.TempDeck2[Random.Range(6, instance.TempDeck2.Count())];
        instance.TempDeck2.Remove(rand);
        return rand;
    }
}
