using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Deck : MonoBehaviour
{
    public Card cardsInDeck;
    public ScriptableCardDB cards;
    public List<ScriptableCard> deck;

    private HashSet<int> exclude;
    private IEnumerable<int> range;
    private int index;
    private static Deck instance;

    [SerializeField] public static int Sum = 0;
    
    void Awake()
    {
        LoadingDeck();
        ShuffleDeck(deck);
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
                deck.Add(cardsInfo);
            }
        }
    }

    private void ShuffleDeck(IList<ScriptableCard> dList)
    {
        //using Fisher-yates-Shuffle
        System.Random random = new System.Random();
        int n = dList.Count;

        while(n > 1)
        {
            n--;
            int rnd = random.Next(n+1);
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
        //Debug.Log(instance.RandomCardNumber());
        for (int i = 0; i < instance.deck.Count; i++)
        {
            if (instance.deck[i].UCardID == "01" || instance.deck[i].UCardID == "02")
            {
                instance.exclude = new HashSet<int>() {i};
                foreach(var e in instance.exclude)
                {
                    print("exclude" + e);
                }
                instance.range = Enumerable.Range(0, instance.deck.Count()).Where(i =>!instance.exclude.Contains(i));
                foreach(var h in instance.range)
                {
                    print("Range" + h);
                }
            }
        }
        
                var rand = new System.Random();
                instance.index = rand.Next(0, instance.deck.Count() - instance.exclude.Count);
        Debug.Log(instance.deck[instance.range.ElementAt(instance.index)]);
        return instance.deck[instance.range.ElementAt(instance.index)];
        //return instance.deck[Random.Range(0, instance.deck.Count())];
    }
}
