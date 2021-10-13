using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ChoosingAvatar : MonoBehaviour
{

    //public Image AvatarImage;
    public ScriptableAvatars avatarsList;
    private static ChoosingAvatar instance;
    void Start()
    {
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

    // public static ScriptableAvatars.AvatarData avatarList()
    // {
    //     int index =0;
    //     for (int i = 0; i < instance.avatarsList.avatars.Count; i++)
    //     {
    //         instance.avatarsList.avatars[index] = instance.avatarsList.avatars[i];
    //     }
    //     return instance.avatarsList.avatars[index];
    // }

    public static ScriptableAvatars.AvatarData DefaultAvatar()
    {
        int index = 0;
        instance.avatarsList.avatars[index] = instance.avatarsList.avatars[0];
        return instance.avatarsList.avatars[index];
    }
    // public static Image ChooseAvatar()
    // {
    //     instance.avatarsList.avatars[]
    //     return instance.defaultImage;
    // }
}
