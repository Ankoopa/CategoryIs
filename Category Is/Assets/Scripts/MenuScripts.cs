using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    public GameObject MainmenuScreen;
    public GameObject customMenu;
    public GameObject onlineMenu;
    public GameObject findRoomMenu;
    public GameObject createRoomMenu;
    public GameObject waitingRoomMenu;
    public GameObject searchRoomMenu;
    public GameObject playerListingMenu;

    void Start()
    {
        MainmenuScreen.SetActive(true);
        onlineMenu.SetActive(false);
        customMenu.SetActive(false);
    }
    public void CustomMenuOpen()
    {
        MainmenuScreen.SetActive(false);
        customMenu.SetActive(true);
    }

    public void OnlineMenuOpen()
    {
        MainmenuScreen.SetActive(false);
        onlineMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        MainmenuScreen.SetActive(true);
        onlineMenu.SetActive(false);
        customMenu.SetActive(false);
    }

    public void BackToCustomMenu()
    {
        customMenu.SetActive(true);
        findRoomMenu.SetActive(false);
        createRoomMenu.SetActive(false);
        playerListingMenu.SetActive(false);
    }

    public void FindRoomMenuOpen()
    {
        customMenu.SetActive(false);
        findRoomMenu.SetActive(true);
        searchRoomMenu.SetActive(true);
    }

    public void HostRoomMenuOpen()
    {
        customMenu.SetActive(false);
        createRoomMenu.SetActive(true);
        playerListingMenu.SetActive(true);
    }

    public void OnWaitingRoomOpen()
    {
        searchRoomMenu.SetActive(false);
        waitingRoomMenu.SetActive(true);
        playerListingMenu.SetActive(true);
    }
}
