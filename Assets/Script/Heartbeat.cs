using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{   
    public Player player;
    public GameObject resultPanel;
    public CharSpawner characterSpawner;
    public Character currentCharacter;
    public GameObject startButton;
    public GameObject DotPrefab;
    public Transform[] SpawnPointsDot;
    private GameObject newDot;
    private int points;
    private int pity = 0;
    private int guarantee = 3;
    private bool isFirstTime = true;
    public bool confirmation = false;
    public void Start(){
        currentCharacter = player.currentChar;
    }

    public void SpawnADot()
    {
        startButton.SetActive(false);
        newDot = Instantiate(DotPrefab, SpawnPointsDot[0]);
        // Debug.Log("Spawned a Dot");
        // AssignNewDot();
        // Dot dot = newDot.GetComponent<Dot>();
        // dot.MoveRight();
    }

    public void FixedUpdate()
    {
        if (newDot != null && newDot.transform.position.x >= 10f){
            // Debug.Log("Dot is out of bounds");
            points = newDot.GetComponent<Dot>().points;
            Debug.Log("Total points: " + points);
            Destroy(newDot);
            giveResults();
            startButton.SetActive(true);
        }
    }

    public void giveResults(){
        if(points == 3){
            Debug.Log("Give Accurate Results");
            
            if(characterSpawner.isSentient){
                //was lying
                if(isFirstTime){
                    confirmation = false;
                    int randomNum = Random.Range(0, 4);
                    pity = randomNum;
                    isFirstTime = false;
                    Debug.Log("Number to get the read whether they're sentient or not = " + guarantee);
                    Debug.Log("Starting pity = "+ pity);
                } else{
                    Debug.Log("Current pity =" + pity);
                    pity++;
                }

                if(pity == guarantee){
                    Debug.Log("The AI/Human wasn't lying");
                    confirmation = true;
                } else {
                    Debug.Log("Not Sure Yet");
                    confirmation = false;
                }
            } else{
                //was telling the truth
            }
        } else {
            Debug.Log("Say that you can't confirm the results");
        }
    }
}