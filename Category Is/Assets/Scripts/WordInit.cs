using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordInit : MonoBehaviour
{
    public TextAsset textFile;
    private List<string> wordList;

    // Start is called before the first frame update
    void Start()
    {
        var content = textFile.text;
        var allWords = content.Split('\n');
        wordList = new List<string>(allWords);

        Debug.Log(wordList);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
