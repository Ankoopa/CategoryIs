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
            
            //DontDestroyOnLoad(gameObject);
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

    public static ScriptableAvatars.AvatarData ChooseAvatar()
    {
        instance.avatarsList.avatars[AvatarSelection.spriteIndex] = instance.avatarsList.avatars[AvatarSelection.spriteIndex];
        return instance.avatarsList.avatars[AvatarSelection.spriteIndex];
    }
    // public static Image ChooseAvatar()
    // {
    //     instance.avatarsList.avatars[]
    //     return instance.defaultImage;
    // }
}
