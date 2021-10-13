using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour
{
    public GameObject avatar;
    public ScriptableAvatars avatarDB;
    public Transform parent;
    public Image avatarImage;
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
                index++;
            }
        }
    }

    public void AddAvatarTolist(ScriptableAvatars.AvatarData avatar)
    {
        avatarImage.sprite = avatar.AvatarImage;
        //avatarName.text = avatar.item_name;
    }
}
