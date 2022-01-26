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
    public GameObject YourAvatar;
    private GameObject avatarImageDP;
    public GameObject PLMScript;
    public List<GameObject> avalist = new List<GameObject>();
    private int index;
    public static int spriteIndex;
    //public Text avatarName;

    void Start()
    {
    }
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
        YourAvatar.transform.position = new Vector2(avalist[spriteIndex].transform.position.x, avalist[spriteIndex].transform.position.y + 1.25f);
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
            SoundManager.PlaySound("owl");
            spriteIndex = 0;
        }
        else if (newAvatar.GetComponent<Image>().sprite.name == "Parrot Avatar")
        {
            SoundManager.PlaySound("parrot");
            spriteIndex = 1;
        }
        else if (newAvatar.GetComponent<Image>().sprite.name == "Penguin Avatar")
        {
            SoundManager.PlaySound("penguin");
            spriteIndex = 2;
        }
        else if (newAvatar.GetComponent<Image>().sprite.name == "Pigeon Avatar")
        {
            SoundManager.PlaySound("pigeon");
            spriteIndex = 3;
        }
        
        PlayerListingMenu._reInstant = true;
        
    }
    
    
}
