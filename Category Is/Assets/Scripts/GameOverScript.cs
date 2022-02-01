using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverScript : MonoBehaviour
{
    public Text Results;
    private void Awake()
    {
        Results.text = GameController.myState.ToString();
    }
    public void BackToLobby()
    {
        SceneManager.LoadScene("MainLobby");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
