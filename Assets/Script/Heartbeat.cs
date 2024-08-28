using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Heartbeat : MonoBehaviour
{   
    public enum Types{
        AI,
        Human,
    }

    public Player player;
    public TextMeshProUGUI upperMonitorText;
    public TextMeshProUGUI lowerMonitorText;
    public AudioManager audioManager;
    public GameObject Sentient;
    public GameObject NotSentient;
    public GameObject LieDetected;
    public GameObject VitalsNormal;
    public GameObject Unsure;
    public GameObject spawnButton;
    public CharSpawner characterSpawner;
    public Character currentCharacter;
    public GameObject LinePrefab;
    public GameObject DotPrefab;
    public Transform[] SpawnPointsLine;
    public Transform[] SpawnPointsDot;
    private GameObject newDot;
    private int points;
    private int pity = 0;
    private int guarantee = 3;
    public bool isFirstTime = true;
    public int confirmation = -1;
    // public bool onEnableDone = false;

    public void OnEnable(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        points = 0;
        newDot = null;
        Sentient.SetActive(false);
        NotSentient.SetActive(false);
        LieDetected.SetActive(false);
        VitalsNormal.SetActive(false);
        Unsure.SetActive(false);
        currentCharacter = player.currentChar;
        // onEnableDone = true;
    }

    public void SpawnADot(){
        audioManager.PlaySFX(audioManager.ButtonClick);
        Debug.Log("Spawning a Dot");
        upperMonitorText.text = "Scanning...";
        lowerMonitorText.text = "";
        SpawnLines();
        
        newDot = Instantiate(DotPrefab, SpawnPointsDot[0]);
        spawnButton.SetActive(false);
        // Debug.Log("Spawned a Dot");
    }

    public void SpawnLines()
    {
        int randomIndex = Random.Range(0, SpawnPointsLine.Length);

        for(int i=0; i<SpawnPointsLine.Length; i++)
        {
            if(randomIndex != i)
            {
                Instantiate(LinePrefab, SpawnPointsLine[i].position,Quaternion.identity);
            }
        }
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
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");
        audioManager.PlaySFX(audioManager.DiscepancyFound);
        foreach(GameObject line in lines){
            line.SetActive(false);
        }
        foreach(GameObject line in  lines){
            Destroy(line);
        }
        
        upperMonitorText.text = "";
        if(points == 3){
            Debug.Log("Give Accurate Results");

            if(isFirstTime){
                int randomNum = Random.Range(0, 4);
                pity = randomNum;
                isFirstTime = false;
                // Debug.Log("Number to get the read whether they're sentient or not = " + guarantee);
                // Debug.Log("Starting pity = "+ pity);
            } else{
                // Debug.Log("Current pity =" + pity);
                pity++;
            }

            if(pity >= guarantee){
                // Debug.Log("The AI / Human wasn't lying / was lying");
                
                if(characterSpawner.isSentient){
                    confirmation = 1;
                    NotSentient.SetActive(false);
                    VitalsNormal.SetActive(false);
                    if( (Types) currentCharacter.type == Types.AI ){
                        Sentient.SetActive(true);
                    } else if( (Types) currentCharacter.type == Types.Human ){
                        LieDetected.SetActive(true);
                    }
                    player.panelText.text = "";
                    player.stringToDisplay = "We've got a hit Xara, that was a lie";
                    player.StartCoroutine(player.TypeLetterByLetter(player.stringToDisplay));
                    player.switchCharacterButton.GetComponent<Image>().sprite = player.xaraSprite;
                    player.switchCharacterButton.SetActive(true);
                } else {
                    confirmation = 0;
                    LieDetected.SetActive(false);
                    Sentient.SetActive(false);
                    if( (Types) currentCharacter.type == Types.AI ){
                        NotSentient.SetActive(true);
                    } else if( (Types) currentCharacter.type == Types.Human ){
                        VitalsNormal.SetActive(true);
                    }
                    player.panelText.text = "";
                    player.stringToDisplay = "I'm certain, this one's not hiding anything Xara";
                    currentCharacter.stanceWasRevealed = true;
                    player.StartCoroutine(player.TypeLetterByLetter(player.stringToDisplay));
                    player.switchCharacterButton.GetComponent<Image>().sprite = player.xaraSprite;
                    player.switchCharacterButton.SetActive(true);
                }
            } else {
                Debug.Log("Pity not yet at guarantee");
                Unsure.SetActive(true);
                player.panelText.text = "";
                player.stringToDisplay = "We're going to need more data, Xara";
                player.StartCoroutine(player.TypeLetterByLetter(player.stringToDisplay));
                player.switchCharacterButton.GetComponent<Image>().sprite = player.xaraSprite;
                player.switchCharacterButton.SetActive(true);
            }
        } else{
            Debug.Log("Failed minigame, give Inaccurate Results");
            Unsure.SetActive(true);
            player.stringToDisplay = "Error... Unable to infer based on heartbeat data";
            player.StartCoroutine(player.TypeLetterByLetter(player.stringToDisplay));
            player.switchCharacterButton.GetComponent<Image>().sprite = player.xaraSprite;
            player.switchCharacterButton.SetActive(true);
        }
    }
}