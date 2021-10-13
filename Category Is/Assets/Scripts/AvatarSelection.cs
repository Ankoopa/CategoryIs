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
    public GameObject avatarImageDP;
    public List<GameObject> avatarList;
    private static ChoosingAvatar instance;
    private int index;
    //public Text avatarName;
    private void Start()
    {
    }
 
    void Update()
    {
        
        if (index < avatarDB.avatars.Count)
        {
            Debug.Log(index);
            
            for (int i = 0; i < avatarDB.avatars.Count; i++)
            {
                AddAvatarTolist(avatarDB.avatars[i]);
                GameObject avatars = Instantiate(avatar, new Vector2(avatar.transform.position.x + 1,0), Quaternion.identity, parent);
                avatarList.Add(avatars);
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
        if (newAvatar != null)
        {
            Debug.Log(newAvatar.GetComponent<Image>().sprite);
            avatarImageDP.GetComponent<Image>().sprite = newAvatar.GetComponent<Image>().sprite;
        }
        else
        {
            Debug.Log("newAvatar variable is empty");
        }
        
    }
}
