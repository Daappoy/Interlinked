using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class CharSpawner : MonoBehaviour
{   
    [Header ("References from other scripts")]
    public Animator animator;
    public ScoreManager scoreManager;
    public Heartbeat heartbeat;
    public Player player;
    public AudioManager audioManager;
    [Header ("For Spawning")]
    public bool isSentient;
    public Transform[] spawnpoints;
    public Sprite[] characterSprites = new Sprite[10];
    public GameObject charaPrefab;
    private List<int> spawnedHumans = new List<int>();
    private List<int> spawnedAI = new List<int>();
    private int humanCount;
    private int aiCount;
    private int randomNum;
    public int totalCount;
    
    public void Start(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        player.XaraRelatedObjects = GameObject.FindGameObjectsWithTag("Xara");
        player.LucasRelatedObjects = GameObject.FindGameObjectsWithTag("LUCAS");
        player.buttons = GameObject.FindGameObjectsWithTag("Buttons");
        // Debug.Log("Found buttons");
        // Debug.Log($"Found {buttons.Length} buttons.");
        player.buttons = player.buttons.OrderBy(button => button.name).ToArray();
        player.buttonTexts = new TextMeshProUGUI[player.buttons.Length];
        for (int i = 0; i < player.buttons.Length; i++){
            player.buttonTexts[i] = player.buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            // Debug.Log($"Button {i} text is {buttonTexts[i].text}");
        }
        SpawnACharacter();
    }

    public void SpawnACharacter(){ 
        //Spawns a character
        if(totalCount == 8){
            Debug.Log("All characters have been spawned");
            scoreManager.displayEndResults();
            //call end game function
            return;
        } 
        GameObject chara = Instantiate(charaPrefab, spawnpoints[0]);
        totalCount++;
        scoreManager.decisionWasMade = false;

        
        // Debug.Log("Spawned a character");
        Character currentChara = chara.GetComponent<Character>();
        currentChara.charSpawner = this;

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

        //Generate character name
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

        //Generate Stance
        int num = Random.Range(0,2);
        if(num == 1){
            isSentient = true;
        } else{
            isSentient = false;
        }
        
        if(isSentient){//Randomly determines the character's stance (Hostile, Neutral, or Compassionate)
            num = Random.Range(0,2);
            if(num == 1){
                num = 2;
            }
            currentChara.stance = num;
        } else{ //neutral (only for non sentient ais or neutral humans)
            currentChara.stance = 1;
        }
        
        //Making sure other scripts are reset
        heartbeat.confirmation = -1;
        heartbeat.isFirstTime = true;
        currentChara.player = player;
        scoreManager.currentCharacter = currentChara;
        scoreManager.player = player;
        player.currentChar = currentChara;
        player.canAskQuestion = new bool[10];
        
        for(int i = 0 ; i < 10; i++){
            player.canAskQuestion[i] = true;
        }
        player.EnableXaraRelatedObjects();
        player.DisableLucasRelatedObjects();
        
        player.isXara = true;
        player.isLucas = false;
        player.switchCharacterButton.SetActive(false);
        player.hasAskedQuestion = false;
        
       // Debug.Log("Player's current interviewee set");

        int currentCharacterIs = player.checkCharacter(); //Checks the character's type and name
        if(currentChara == null){
            Debug.LogWarning($"Character is null");
        }
        if(currentChara.GetComponent<SpriteRenderer>().sprite == null){

            Debug.LogWarning($"Character sprite is null");
        }
        if(characterSprites[currentCharacterIs] == null){
            Debug.LogWarning($"Character sprite is null");
        }
        currentChara.GetComponent<SpriteRenderer>().sprite = characterSprites[currentCharacterIs];
        animator.SetInteger("AnimatorCurrentCharacter", currentCharacterIs);
        if(currentCharacterIs == 0){ 
            Debug.Log("Dorothy is the current character, she is an AI");
            if(currentChara.stance == 0){
                Debug.Log("Dorothy is hostile");
            } else if(currentChara.stance == 1){
                Debug.Log("Dorothy is neutral");
            } else if(currentChara.stance == 2){
                Debug.Log("Dorothy is compassionate");
            }
        } else if(currentCharacterIs == 1){ 
            Debug.Log("Lily is the current character, she is an AI");
            Debug.Log("Lily's stance: " + currentChara.stance);
            if(currentChara.stance == 0){
                Debug.Log("Lily is hostile");
            } else if(currentChara.stance == 1){
                Debug.Log("Lily is neutral");
            } else if(currentChara.stance == 2){
                Debug.Log("Lily is compassionate");
            }
        } else if(currentCharacterIs == 2){
            Debug.Log("Garry is the current character, he is an AI");
            Debug.Log("Garry's stance: " + currentChara.stance);
            if(currentChara.stance == 0){
                Debug.Log("Garry is hostile");
            } else if(currentChara.stance == 1){
                Debug.Log("Garry is neutral");
            } else if(currentChara.stance == 2){
                Debug.Log("Garry is compassionate");
            }
        } else if(currentCharacterIs == 3){
            Debug.Log("Caleb is the current character, he is human");
            Debug.Log("Caleb's stance: " + currentChara.stance);
            if(currentChara.stance == 0){
                Debug.Log("Caleb is hostile");
            } else if(currentChara.stance == 1){
                Debug.Log("Caleb is neutral");
            } else if(currentChara.stance == 2){
                Debug.Log("Caleb is compassionate");
            }
        } else if(currentCharacterIs == 4){
            Debug.Log("Isaac is the current character, he is human");
            Debug.Log("Isaac's stance: " + currentChara.stance);
            if(currentChara.stance == 0){
                Debug.Log("Isaac is hostile");
            } else if(currentChara.stance == 1){
                Debug.Log("Isaac is neutral");
            } else if(currentChara.stance == 2){
                Debug.Log("Isaac is compassionate");
            }
        } else if(currentCharacterIs == 5){
            Debug.Log("Kim is the current character, she is human");
            Debug.Log("Kim's stance: " + currentChara.stance);
            if(currentChara.stance == 0){
                Debug.Log("Kim is hostile");
            } else if(currentChara.stance == 1){
                Debug.Log("Kim is neutral");
            } else if(currentChara.stance == 2){
                Debug.Log("Kim is compassionate");
            }
        } else if(currentCharacterIs == 6){
            Debug.Log("Timmy is the current character, he is human");
            Debug.Log("Timmy's stance: " + currentChara.stance);
            if(currentChara.stance == 0){
                Debug.Log("Timmy is hostile");
            } else if(currentChara.stance == 1){
                Debug.Log("Timmy is neutral");
            } else if(currentChara.stance == 2){
                Debug.Log("Timmy is compassionate");
            }
        } else if(currentCharacterIs == 7){
            Debug.Log("Kate is the current character, she is human");
            Debug.Log("Kate's stance: " + currentChara.stance);
            if(currentChara.stance == 0){
                Debug.Log("Kate is hostile");
            } else if(currentChara.stance == 1){
                Debug.Log("Kate is neutral");
            } else if(currentChara.stance == 2){
                Debug.Log("Kate is compassionate");
            }
        }
        currentChara.canMove = true;
        audioManager.PlaySFX(audioManager.Buzzer);
        audioManager.PlaySFX(audioManager.Walking);
    }
}