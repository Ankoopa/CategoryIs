using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosingAvatar : MonoBehaviour
{
    private Transform[] alist;
    // public GameObject yourAvatarText;
    public GameObject Avatar;
    public GameObject AvatarImage;
    [SerializeField]
    private List<GameObject> avatarImageList = new List<GameObject>();

    private Image defaultImage;
    private static ChoosingAvatar instance;
    void Start()
    {
        alist = GetComponentsInChildren<Transform>(true);
        foreach(Transform child in alist)
        {
            if (child.tag == "Avatars")
            avatarImageList.Add(child.gameObject);
        }

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
    public void DefaultAvatar()
    {
        defaultImage = Avatar.GetComponent<Image>();
        for (int i = 0; i < avatarImageList.Count; i++)
        {
            defaultImage.sprite = avatarImageList[0].GetComponent<Image>().sprite;
        }
        
    }
    public static Image ChooseAvatar()
    {
        // int Rand = Random.Range(0, instance.avatarImageList.Count);
        
        for (int i = 0; i < instance.avatarImageList.Count; i++)
        {
            instance.defaultImage.sprite = instance.avatarImageList[0].GetComponent<Image>().sprite;
        }

        return instance.defaultImage;
    }
}
