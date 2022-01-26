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
        SoundManager.PlaySound("4");
        MainmenuScreen.SetActive(false);
        customMenu.SetActive(true);
    }

    public void OnlineMenuOpen()
    {
        SoundManager.PlaySound("4");
        MainmenuScreen.SetActive(false);
        onlineMenu.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SoundManager.PlaySound("4");
        MainmenuScreen.SetActive(true);
        onlineMenu.SetActive(false);
        customMenu.SetActive(false);
    }

    public void BackToCustomMenu()
    {
        SoundManager.PlaySound("4");
        customMenu.SetActive(true);
        findRoomMenu.SetActive(false);
        createRoomMenu.SetActive(false);
        playerListingMenu.SetActive(false);
        changeAvatarPanel.SetActive(false);
    }

    public void FindRoomMenuOpen()
    {
        SoundManager.PlaySound("4");
        customMenu.SetActive(false);
        changeAvatarPanel.SetActive(true);
        findRoomMenu.SetActive(true);
        searchRoomMenu.SetActive(true);
        playerListingMenu.SetActive(false);
        ChangeNameButton.SetActive(false);
        
    }

    public void HostRoomMenuOpen()
    {
        SoundManager.PlaySound("4");
        customMenu.SetActive(false);
        createRoomMenu.SetActive(true);
        playerListingMenu.SetActive(true);
        changeAvatarPanel.SetActive(true);
    }

    public void OnWaitingRoomOpen()
    {
        SoundManager.PlaySound("4");
        searchRoomMenu.SetActive(false);
        waitingRoomMenu.SetActive(true);
        playerListingMenu.SetActive(true);
        changeAvatarPanel.SetActive(true);
        ChangeNameButton.SetActive(true);
    }

    public void OnWaitingRoomClosed()
    {
        SoundManager.PlaySound("4");
        searchRoomMenu.SetActive(true);
        waitingRoomMenu.SetActive(false);
        playerListingMenu.SetActive(false);
        ChangeNameButton.SetActive(false);
    }

    public void OnAvatarOpen()
    {
        SoundManager.PlaySound("4");
        AvatarPanel.SetActive(true);
    }

    public void OnAvatarClose()
    {
        SoundManager.PlaySound("4");
         AvatarPanel.SetActive(false);
    }

}
