using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharSpawner : MonoBehaviour
{
    public Player player;
    public bool isSentient;
    public Transform[] spawnpoints;
    public GameObject charaPrefab;
    private List<int> spawnedHumans = new List<int>();
    private List<int> spawnedAI = new List<int>();
    private int humanCount;
    private int aiCount;
    private int randomNum;

    
    public void Start(){
        SpawnACharacter();
    }

    public void SpawnACharacter(){ 
        //Spawns a character
        GameObject chara = Instantiate(charaPrefab, spawnpoints[0]); 
        // Debug.Log("Spawned a character");
        Character currentChara = chara.GetComponent<Character>();

        humanCount = spawnedHumans.Count;
        aiCount = spawnedAI.Count;

        //Determines the character type
        if(humanCount == 5){ //if all humans are spawned, spawn AI
            randomNum = 0;
            humanCount += 1;
        } else if(aiCount == 3){ //if all AI are spawned, spawn human
            randomNum = 1;
            aiCount += 1;
        } else{ //randomly spawn human or AI
            randomNum = Random.Range(0,2);
        }
        currentChara.type = randomNum;

        //Determines the character name
        if(currentChara.type == 0){ //If it's an AI
            do{ //Ensures that the same character is not spawned twice by checking it with the spawnedAI list
                randomNum = Random.Range(0,3);
            } while(spawnedAI.Contains(randomNum));
            spawnedAI.Add(randomNum); //Adds the spawned character to the list
        } else{ //If it's human
            do{ //Ensures that the same character is not spawned twice by checking it with the spawnedHumans list
                randomNum = Random.Range(0,5);
            } while(spawnedHumans.Contains(randomNum));
            spawnedHumans.Add(randomNum); //Adds the spawned character to the list
        }
        currentChara.characterName = randomNum;
        // Debug.Log("Character name: " + currentChara.characterName);
        // Debug.Log("Character type: " + currentChara.type);
        // Debug.Log("Character stance: " + currentChara.stance);
        
        int num = Random.Range(0,3);
        if(num == 1){
            isSentient = true;
        } else{
            isSentient = false;
        }
        isSentient = true;
        currentChara.stance = num; //Randomly determines the character's stance (Hostile, Neutral, or Compassionate)
        // Debug.Log("Setting current character from the player's pov to the one that just spawned");
        player.currentChar = currentChara; //Sets the current character to the spawned character
        // Debug.Log("Player's current interviewee set");
        currentChara.canMove = true;
    }
}
