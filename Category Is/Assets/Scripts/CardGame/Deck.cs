using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


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
    private static ScriptableCard back;
    

    [SerializeField] public static int Sum = 0;
    
    void Awake()
    {
        LoadingDeck();
        ShuffleDeck(GameDeck);

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
        foreach (var cardsInfo in cards.allCards)
        {
                for (int i = 0; i < cardsInfo.CardAmount; i++)
                {
                    GameDeck.Add(cardsInfo);
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
            dList[rnd] = dList[n]; //Swaps the old value to the new value
            dList[n] = value;
        }

    }

    public static ScriptableCard DrawCardFromDeck()
    {
        //Takes card from the top of the list and removes it from the deck
        instance.GameDeck.Remove(instance.GameDeck[0]);
        return instance.GameDeck[0];
    }

    public static ScriptableCard DrawEnemyDeck()
    {
        return back;
    }

}
