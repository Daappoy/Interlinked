using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    //make the public and private variables here

    private void Start(){
        LoadSave();
    }

    private void SaveGame(){
        SaveData save = new SaveData();
        //for example, save.interviewCount = player.interviewCount 

        string filePath = Application.persistentDataPath + "/save.json";
        string currentSaveData = JsonUtility.ToJson(save);
        Debug.Log("Saving to: " + filePath);
        System.IO.File.WriteAllText(filePath, currentSaveData);
        Debug.Log("Saved successfully");
    }

    public void LoadSave(){
        string filePath = Application.persistentDataPath + "/save.json";
        if(!System.IO.File.Exists(filePath)){ //if there is no save file since it's the beginning of the run
            Debug.Log("Save file not found");
            SaveGame();
            Debug.Log("Created new save file");
        } else{ //if save file exists already (the player is continuing from where they left off)
            string currentSaveData = System.IO.File.ReadAllText(filePath);
            SaveData save = JsonUtility.FromJson<SaveData>(currentSaveData);
            Debug.Log("Successfully retrieved the last save file");
            //do the opposite of what's in save
            //for example, player.interviewCount = save.interviewCount
            Debug.Log("Successfully loaded the game");
        }

    }
}

[System.Serializable]
public class SaveData{
    //put the variables you wanna save here, declare them as public or private and whatnot
    public SaveData(){ //constructor to initialize the object, kinda like start()
        //put the base values here, like in a new game, for example
        //interviewCount = 0
    }
}