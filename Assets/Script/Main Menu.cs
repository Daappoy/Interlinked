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
    public AudioManager audioManager;
    // void OnEnable()
    // {
    //     MainMenuPanel.SetActive(true);
    //     MainMenuBackGround.SetActive(true);
    //     OptionsPanel.SetActive(false);
    // }
    public void Start(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    public void OpenOptionsMenu()
    {
        audioManager.PlaySFX(audioManager.ButtonClick);
        OptionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
        MainMenuBackGround.SetActive(false);
    }

    public void CloseOptionsMenu()
    {
        audioManager.PlaySFX(audioManager.GeneralClick);
        OptionsPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
        MainMenuBackGround.SetActive(true);
    }
}
