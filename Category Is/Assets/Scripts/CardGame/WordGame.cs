using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordGame : MonoBehaviour
{
    public TextAsset textFile;
    public GameObject eventSystem;
    public InputField wordInput;
    public Text categoryText;
    public Text msg;
    public Text scoreTxt;

    private int score;
    private List<string> wordList;
    private List<string> submittedWords = new List<string>();
    private string submittedWord;
    private string lastWord = "";
    private bool wordFound;

    // Start is called before the first frame update
    void Awake()
    {
        string content = textFile.text;
        string[] allWords = content.Split('\n');
        wordList = new List<string>(allWords);
        wordInput = wordInput.GetComponent<InputField>();
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        msg.text = "";
    }

    public void OnSubmit()
    {
        wordFound = false;
        submittedWord = wordInput.text.ToLower();

        foreach (string word in wordList)
        {
            if (word.Contains(submittedWord))
            {
                wordFound = true;
                break;
            }
        }

        if (wordFound)
        {
            ProcessWord();
        }
        else
        {
            msg.text = "'"+submittedWord+"' is an invalid word for this category.";
        }

        wordInput.text = "";
    }

    void ProcessWord()
    {
        if ((lastWord.Equals("") || submittedWord[0].Equals(lastWord[lastWord.Length - 1])) && !WordExists(submittedWord))
        {
            lastWord = submittedWord;
            submittedWords.Add(submittedWord);
            Debug.Log("accepted word");
            score++;
            scoreTxt.text = "Score: " + score.ToString();
            msg.text = "Word accepted";
        }
        else if (WordExists(submittedWord))
        {
            msg.text = "'"+ submittedWord +"' has been already submitted.";
        }
        else
        {
            msg.text = "'"+submittedWord+"' must start the same letter as the last word: " + lastWord;
        }
    }

    bool WordExists(string word)
    {
        bool exists;
        int result = submittedWords.IndexOf(word);

        if (result == -1) exists = false;
        else exists = true;

        return exists;
    }
}
