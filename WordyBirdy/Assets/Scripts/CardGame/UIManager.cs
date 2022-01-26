using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public void OnCategoryShowEnds()
    {
        this.gameObject.SetActive(false);
        GameController.isStartingGame = true;
    }

}
