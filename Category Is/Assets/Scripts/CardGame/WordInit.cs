using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordInit : MonoBehaviour
{
    public TextAsset textFile;
    public List<string> wordList;
    public Text categoryText;

    // Start is called before the first frame update
    void Awake()
    {
        var content = textFile.text;
        var allWords = content.Split('\n');
        wordList = new List<string>(allWords);

        Debug.Log(wordList[0]);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
