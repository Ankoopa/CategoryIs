using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject AvatarPanel;
    public GameObject changeAvatarPanel;
    public GameObject ChangeNameButton;
    
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
        changeAvatarPanel.SetActive(false);
    }

    public void FindRoomMenuOpen()
    {
        customMenu.SetActive(false);
        changeAvatarPanel.SetActive(true);
        findRoomMenu.SetActive(true);
        searchRoomMenu.SetActive(true);
        playerListingMenu.SetActive(false);
        ChangeNameButton.SetActive(false);
        
    }

    public void HostRoomMenuOpen()
    {
        customMenu.SetActive(false);
        createRoomMenu.SetActive(true);
        playerListingMenu.SetActive(true);
        changeAvatarPanel.SetActive(true);
    }

    public void OnWaitingRoomOpen()
    {
        searchRoomMenu.SetActive(false);
        waitingRoomMenu.SetActive(true);
        playerListingMenu.SetActive(true);
        changeAvatarPanel.SetActive(true);
        ChangeNameButton.SetActive(true);
    }

    public void OnWaitingRoomClosed()
    {
        searchRoomMenu.SetActive(true);
        waitingRoomMenu.SetActive(false);
        playerListingMenu.SetActive(false);
        ChangeNameButton.SetActive(false);
    }

    public void OnAvatarOpen()
    {
        AvatarPanel.SetActive(true);
    }

    public void OnAvatarClose()
    {
         AvatarPanel.SetActive(false);
    }

}
