using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AvatarSelection : MonoBehaviour
{
    public GameObject avatar;
    public ScriptableAvatars avatarDB;
    public Transform parent;
    public Image avatarImage;
    private GameObject avatarImageDP;
    public GameObject PLMScript;
    public List<GameObject> avalist = new List<GameObject>();
    private int index;
    public static int spriteIndex;
    //public Text avatarName;

 
    void Update()
    {
        
        if (index < avatarDB.avatars.Count)
        {
            for (int i = 0; i < avatarDB.avatars.Count; i++)
            {
                AddAvatarTolist(avatarDB.avatars[i]);
                GameObject avatars = Instantiate(avatar, new Vector2(avatar.transform.position.x + 1,0), Quaternion.identity, parent);
                avalist.Add(avatars);
                index++;
            }
        }

    }

    public void AddAvatarTolist(ScriptableAvatars.AvatarData avatar)
    {
        avatarImage.sprite = avatar.AvatarImage;
        //avatarName.text = avatar.item_name;
    }

    public void ChangeAvatar()
    {
        GameObject newAvatar = EventSystem.current.currentSelectedGameObject;
        avatarImageDP = GameObject.Find("PlayerAvatarImage");
        avatarImageDP.GetComponent<Image>().sprite = newAvatar.GetComponent<Image>().sprite;

        if (newAvatar.GetComponent<Image>().sprite.name == "Owl Avatar")
        {
            spriteIndex = 0;
        }
        else if (newAvatar.GetComponent<Image>().sprite.name == "Parrot Avatar")
        {
            spriteIndex = 1;
        }
        else if (newAvatar.GetComponent<Image>().sprite.name == "Penguin Avatar")
        {
            spriteIndex = 2;
        }
        else if (newAvatar.GetComponent<Image>().sprite.name == "Pigeon Avatar")
        {
            spriteIndex = 3;
        }

        PlayerListingMenu._reInstant = true;
        
    }
    
    
}
