using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class WordGame : MonoBehaviourPun
{
    public List<TextAsset> textFile;
    public InputField wordInput;
    public Text msg;
    //public Text scoreTxt;
    public Text lastWordTxt;
    public Text categoryTxt;
    public Text categoryReminder;
    public GameController GM;
    private int score;
    private int indexFile;
    private List<string> wordList;
    private List<string> submittedWords = new List<string>();
    private string submittedWord;
    private string lastWord = "";
    private bool wordFound;
    private bool categoryConfirmed;

    // Start is called before the first frame update
    void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int randomCategoryIndex = Random.Range(0, textFile.Count);
            base.photonView.RPC("RPC_RandomCategory", RpcTarget.AllBufferedViaServer, randomCategoryIndex);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        wordInput = wordInput.GetComponent<InputField>();
        score = 0;
        msg.text = "";
    }
    void Update()
    {
        if (categoryConfirmed)
        {
            Debug.Log(indexFile); 
            Debug.Log(textFile[indexFile].name);
            categoryTxt.text = textFile[indexFile].name;
            categoryReminder.text = textFile[indexFile].name;
            string content = textFile[indexFile].text;
            string[] allWords = content.Split('\n');
            wordList = new List<string>(allWords);
            categoryConfirmed = false;
        }
    }
    public void OnSubmit()
    {   
        SoundManager.PlaySound("4");
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
            GM.OnClickEndTurn();
        }
        else
        {
            msg.text = "'"+submittedWord+"' is an invalid word for this category.";
            GameController.isValid = false;
        }

        wordInput.text = "";
    }

    void ProcessWord()
    {
        if ((lastWord.Equals("") || submittedWord[0].Equals(lastWord[lastWord.Length - 1])) && !WordExists(submittedWord))
        {
            lastWord = submittedWord;
            submittedWords.Add(submittedWord);
            // score++;
            // scoreTxt.text = "Score: " + score.ToString();
            msg.text = submittedWord + " Word accepted";
            base.photonView.RPC("lastWordChanged", RpcTarget.AllBufferedViaServer, lastWord);
            base.photonView.RPC("UpdateValues", RpcTarget.AllBufferedViaServer, submittedWords.ToArray(), lastWord);
            GameController.isValid = true;
        }
        else if (WordExists(submittedWord))
        {
            msg.text = "'"+ submittedWord +"' has been already submitted.";
            GameController.isValid = false;
        }
        else
        {
            msg.text = "'"+submittedWord+"' must start the same letter as the last word: " + lastWord;
            GameController.isValid = false;
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
    [PunRPC]
    void lastWordChanged(string word)
    {
        if (lastWordTxt != null)
        {
            lastWordTxt.text = word.ToUpper();
        }else
            return;
    }
    [PunRPC]
    void UpdateValues(string[] subWords, string prevWord)
    {
        submittedWords = new List<string>(subWords);
        lastWord = prevWord;
    }

    [PunRPC]
    void RPC_RandomCategory(int index)
    {
       
        indexFile = index;
        Debug.Log(indexFile); 
        categoryConfirmed = true;
    }
}
