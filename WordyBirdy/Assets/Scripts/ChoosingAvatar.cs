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

    public static int AvaIndex()
    {
        return AvatarSelection.spriteIndex;
    }
    public static ScriptableAvatars.AvatarData ChooseAvatar()
    {
        instance.avatarsList.avatars[AvatarSelection.spriteIndex] = instance.avatarsList.avatars[AvatarSelection.spriteIndex];
        return instance.avatarsList.avatars[AvatarSelection.spriteIndex];
    }
}
