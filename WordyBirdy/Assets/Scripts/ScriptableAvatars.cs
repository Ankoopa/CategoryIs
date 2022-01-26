using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewAvatar", menuName = "ScriptableObjects/AvatarDatabase")]
public class ScriptableAvatars : ScriptableObject 
{
    [System.Serializable]
    public class AvatarData
    {
        public Sprite AvatarImage;
        public string item_name;

    }

    public List<AvatarData> avatars = new List<AvatarData>();
}
