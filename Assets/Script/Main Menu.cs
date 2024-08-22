using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject OptionsPanel;
    public GameObject MainMenuBackGround;

    // void OnEnable()
    // {
    //     MainMenuPanel.SetActive(true);
    //     MainMenuBackGround.SetActive(true);
    //     OptionsPanel.SetActive(false);
    // }
  
    public void OpenOptionsMenu()
    {
        OptionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        MainMenuBackGround.SetActive(false);
    }

    public void CloseOptionsMenu()
    {
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        MainMenuBackGround.SetActive(true);
    }
}
