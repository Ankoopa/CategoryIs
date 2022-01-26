using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip bgm, btnClick, drawCard, owl, parrot, penguin, pigeon;
    public static AudioSource audioSrc;

    // void Awake()
    // {
    //     DontDestroyOnLoad(this.gameObject);
    // }
    void Start()
    {
       
        bgm = Resources.Load<AudioClip>("BGMGame");
        btnClick = Resources.Load<AudioClip>("4");
        drawCard = Resources.Load<AudioClip>("new_card");
        owl = Resources.Load<AudioClip>("owl");
        parrot = Resources.Load<AudioClip>("parrot");
        penguin = Resources.Load<AudioClip>("penguin");
        pigeon = Resources.Load<AudioClip>("pigeon");
        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = 0.5f;
    }

    public static void PlaySound(string clip)
    {

        if (audioSrc == null){
            return;
        }else{
            switch (clip)
            {
                case "BGMGame":
                    audioSrc.PlayOneShot(bgm);
                    break;
                case "4":
                    audioSrc.PlayOneShot(btnClick);
                    break;
                case "new_card":
                    audioSrc.PlayOneShot(drawCard);
                    break;
                case "owl":
                    audioSrc.PlayOneShot(owl);
                    break;
                case "parrot":
                    audioSrc.PlayOneShot(parrot);
                    break;
                case "penguin":
                    audioSrc.PlayOneShot(penguin);
                    break;
                case "pigeon":
                    audioSrc.PlayOneShot(pigeon);
                    break;
            }
        }
        
    }
}
