using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Heartbeat : MonoBehaviour
{   
    public Player player;
    public TextMeshProUGUI upperMonitorText;
    public TextMeshProUGUI lowerMonitorText;
    public GameObject Sentient;
    public GameObject NotSentient;
    public GameObject spawnButton;
    public CharSpawner characterSpawner;
    public Character currentCharacter;
    public GameObject DotPrefab;
    public Transform[] SpawnPointsDot;
    private GameObject newDot;
    private int points;
    private int pity = 0;
    private int guarantee = 3;
    private bool isFirstTime = true;
    public int confirmation = -1;

    public void Start(){
        Sentient.SetActive(false);
        NotSentient.SetActive(false);
        currentCharacter = player.currentChar;
    }

    public void SpawnADot(){
        Debug.Log("Spawning a Dot");
        upperMonitorText.fontSize = 50;
        upperMonitorText.text = "Scanning...";
        lowerMonitorText.text = "";
        foreach(GameObject line in player.lines){
            line.SetActive(true);
        }
        
        newDot = Instantiate(DotPrefab, SpawnPointsDot[0]);
        spawnButton.SetActive(false);
        // Debug.Log("Spawned a Dot");
    }

    public void FixedUpdate(){
        if (newDot != null && newDot.transform.position.x >= 10f){
            // Debug.Log("Dot is out of bounds");
            points = newDot.GetComponent<Dot>().points;
            Debug.Log("Total points: " + points);
            Destroy(newDot);
            giveResults();
        }
    }

    public void giveResults(){
        upperMonitorText.text = "";
        upperMonitorText.fontSize = 18;
        foreach(GameObject line in player.lines){
            line.SetActive(false);
        }

        if(points == 3){
            Debug.Log("Give Accurate Results");

            if(isFirstTime){
                int randomNum = Random.Range(0, 4);
                pity = randomNum;
                isFirstTime = false;
                Debug.Log("Number to get the read whether they're sentient or not = " + guarantee);
                Debug.Log("Starting pity = "+ pity);
            } else{
                Debug.Log("Current pity =" + pity);
                pity++;
            }

            if(pity >= guarantee){
                Debug.Log("The AI / Human wasn't lying / was lying");
                if(characterSpawner.isSentient){
                    NotSentient.SetActive(false);
                    Sentient.SetActive(true); 
                } else {
                    NotSentient.SetActive(true);
                    Sentient.SetActive(false);
                }
            } else {
                Debug.Log("Pity not yet at guarantee");
            }
        } 

        // if(confirmation == -1){
        //     Debug.Log("Not Sure Yet");
        // }
    }
}