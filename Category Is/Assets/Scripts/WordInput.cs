using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordInput : MonoBehaviour
{
    public GameObject eventSystem;
    public InputField wordInput;
    public Text msg;
    public Text scoreTxt;
    public List<string> wordList;

    private int score;
    private string submittedWord;
    private WordInit initScript;
    private bool wordFound;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        initScript = eventSystem.GetComponent<WordInit>();
        wordList = initScript.wordList;
    }

    public void OnSubmit()
    {
        wordFound = false;
        wordInput = wordInput.GetComponent<InputField>();
        submittedWord = wordInput.text.ToLower();
        
        foreach(string word in wordList)
        {
            if (word.Contains(submittedWord))
            {
                wordFound = true;
                break;
            }
        }

        if (wordFound)
        {
            Debug.Log("accepted word");
            score++;
            scoreTxt.text = "Score: " + score.ToString();
            msg.text = "Word accepted";
        }
        else
        {
            msg.text = "Invalid word";
        }
    }
}
