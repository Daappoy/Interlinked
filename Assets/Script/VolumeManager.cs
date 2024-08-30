using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        
        if(PlayerPrefs.HasKey("value"))
        {
            Debug.Log("There is a key");
            load();
        } else {
            PlayerPrefs.SetFloat("value", 1);
            load();
        }
        
    }
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        Save();
    }

    public void load()
    {
        Debug.Log(PlayerPrefs.GetFloat("value"));
        volumeSlider.value = PlayerPrefs.GetFloat("value");
        AudioListener.volume = volumeSlider.value;
        Save();
    }
    
    public void Save()
    {
        PlayerPrefs.SetFloat("value", volumeSlider.value);
        PlayerPrefs.Save();
    }
}
