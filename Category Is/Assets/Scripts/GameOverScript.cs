using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{
    
    public void BackToLobby()
    {
        SceneManager.LoadScene("MainLobby");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
