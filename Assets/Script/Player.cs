using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Interviewee
    public Character currentChar;
    public CharDialogue charDialogueScript;
    //public GameObject MainMenuPanel;


    //Pause menu
    public GameObject MainMenuBackground;
    private bool isPaused = false;
    private bool escapeKeyPressed = false;
    
    // Question Buttons
    public GameObject questionsPanel;
    public GameObject button0;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public TextMeshProUGUI button0Text;
    public TextMeshProUGUI button1Text;
    public TextMeshProUGUI button2Text;
    public TextMeshProUGUI button3Text;
    public TextMeshProUGUI button4Text;
    public TextMeshProUGUI button5Text;
    public TextMeshProUGUI button6Text;
    public TextMeshProUGUI button7Text;
    public TextMeshProUGUI button8Text;
    public TextMeshProUGUI button9Text;

    //Subtitles
    public GameObject panel;
    public TMP_Text panelText;
    private string stringToDisplay;
    public int currentQuestionIndex = -1;
    public int charSpecificQuestionNum = -1;
    private int panelLetterCount;
    private bool isTypingLetterByLetter = false;
    private bool isTalking = false; //player
    public bool characterHasToRespond = false;
    public bool panelIsActive;

    public enum Types{
        AI,
        Human,
    }

    public enum AINames{
        Dorothy,
        Lily,
        Garry,
    }

    public enum HumanNames{
        Caleb,
        Isaac,
        Kim,
        Timmy,
        Kate,
    }
    
    string[] playerOptions = new string[]{
        "Why do you think you’re here?", //Q0 Always On
        "What do you think of humans?", //Q1 (for AI-s)
        "What do you think of AI-s", //Q2  (for humans)
        "What is your designated purpose?", //Q3  (for AI-s)
        "What do you think of the AI-s becoming sentient?", //Q4 (for humans)
        "*Character specific question*", //Q5 Always On
        "How do you feel about life now where you’re forced to live alongside AI?", //Q6, For humans (once they’re confirmed to be hiding their stance)
        "How do you feel about your purpose or rather, what is your *true* purpose?",// Q7, specific for AI (once they’re confirmed to be sentient)
        "Do you think that AI-s should be treated like humans?", //Q8, For humans (once they’re confirmed to be hiding their stance)
        "Have you ever considered acting against your primary directives?", // Q9, specific for AI (once they’re confirmed to be sentient)
    };

    string[] characterSpecificQuestions = new string[]{
        //AI, types = 0
        "What’d you think of the last patient you handled?", //Dorothy
        "What’s the most memorable bouquet you’ve arranged?", //Lily
        "What’s the most memorable animal you’ve taken care of?", //Garry
        //Humans, types = 1
        "What was your last case about and what’d you think of it?", //Caleb
        "What was your last lecture about and how did it go?", //Isaac  
        "What was your last performance about and how did it go?", //Kim
        "What's your favorite thing to do at the park?", //Timmy
        "What’s your favorite pastime with your family?", //Kate
    };
    
    //function for each question button
    public void disableAllQuestionButtons(){
        questionsPanel.SetActive(false);
        button0.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);
        button4.SetActive(false);
        button5.SetActive(false);
        button6.SetActive(false);
        button7.SetActive(false);
        button8.SetActive(false);
        button9.SetActive(false);
    }

    public void enableAllQuestionButtons(){
        questionsPanel.SetActive(true);
        if( (Types) currentChar.type == Types.AI){
            button0.SetActive(true);
            button0Text.text = playerOptions[0];
            button1.SetActive(true);
            button1Text.text = playerOptions[1];
            button3.SetActive(true);
            button3Text.text = playerOptions[3];
            button5.SetActive(true);
            button5Text.text = characterSpecificQuestions[checkCharacter()];
            button7.SetActive(true);
            button7Text.text = playerOptions[7];
            button9.SetActive(true);
            button9Text.text = playerOptions[9];
        } else if( (Types) currentChar.type == Types.Human){
            button0.SetActive(true);
            button0Text.text = playerOptions[0];
            button2.SetActive(true);
            button2Text.text = playerOptions[2];
            button4.SetActive(true);
            button4Text.text = playerOptions[4];
            button6.SetActive(true);
            button5.SetActive(true);
            button5Text.text = characterSpecificQuestions[checkCharacter()];
            button6Text.text = playerOptions[6];
            button8.SetActive(true);
            button8Text.text = playerOptions[8];
        }
    }

    public void question0Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 0;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }

    public void question1Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 1;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }

    public void question2Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 2;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }
    public void question3Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 3;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }
    public void question4Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 4;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }
    public void question5Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 5;    
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }
    public void question6Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 6;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }
    public void question7Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 7;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }
    public void question8Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 8;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }
    public void question9Picked(){
        if(!currentChar.canMove){
            currentQuestionIndex = 9;
            DisplayWhatWasSaid();
            enableAllQuestionButtons();
        }
    }


    //After the player clicks the button, we display the question in the subtitles below
    public void DisplayWhatWasSaid(){ //Set subtitle panel to active, set the bool to false, and determine which string you're gonna show
        isTalking = true;
        panel.SetActive(true);
        panelIsActive = true;
        DisplayString();
    }

    public void DisplayString(){
        characterHasToRespond = true;
        if(currentQuestionIndex == 5){ //if it's a character specific question, we use the character specific question array
            charSpecificQuestionNum = checkCharacter();
            // Debug.Log("The character specific question index is: " + charSpecificQuestionNum);
            stringToDisplay = characterSpecificQuestions[charSpecificQuestionNum];
            charSpecificQuestionNum = -1;
        } else{ //we use the base array filled with the player dialogues
            // Debug.Log("The question index is: " + currentQuestionIndex);
            stringToDisplay = playerOptions[currentQuestionIndex];
            currentQuestionIndex = -1;
        }
        isTypingLetterByLetter = true;
        StartCoroutine(TypeLetterByLetter(stringToDisplay)); //we start having it type out the dialogue letter by letter
        // panelLetterCount = 0;
    }
    
    public int checkCharacter(){
        if( (Types) currentChar.type == Types.AI){
            if( (AINames) currentChar.characterName == AINames.Dorothy ){
                return 0;
            } else if( (AINames) currentChar.characterName == AINames.Lily ){
                //Lily
                return 1;
            } else if( (AINames) currentChar.characterName == AINames.Garry ){
                //Garry
                return 2;
            } else{
                Debug.Log("Character not found");
                return -1;
            }
        } else if((Types) currentChar.type == Types.Human){
            if( (HumanNames) currentChar.characterName == HumanNames.Caleb){
                return 3;
            } else if((HumanNames) currentChar.characterName == HumanNames.Isaac){
                return 4;
            } else if((HumanNames) currentChar.characterName == HumanNames.Kim){
                return 5;
            } else if((HumanNames) currentChar.characterName == HumanNames.Timmy){
                return 6;
            } else if((HumanNames) currentChar.characterName == HumanNames.Kate){
                return 7;
            } else{
                Debug.Log("Character not found");
                return -1;
            }
        } else{
            Debug.Log("Character not found");
            return -1;
        }
    }

    public IEnumerator TypeLetterByLetter(string stringToDisplay){
        //set the bool to true
        panelText.text = "";
        for(int i = 0; i < stringToDisplay.Length; i++){ //this will type it till it's done
            panelText.text += stringToDisplay[i];
            panelLetterCount++;
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    // Start is called before the first frame update
    void Start(){
        Time.timeScale = 1f;
        isPaused = false;
        disableAllQuestionButtons();
        // question0Picked();
    }

    // Update is called once per frame
    void Update()
    {   
        if(isTypingLetterByLetter){ 
            isTalking = true;
        }
        
        //Dialogue Shenanigans
        if( panelIsActive && Input.GetMouseButtonDown(0)){ //if the player clicks
            // Debug.Log("Mouse Clicked");
            if(isTypingLetterByLetter == true){ 
                isTypingLetterByLetter = false; 
                // Debug.Log("Showing full line...");
                StopCoroutine(TypeLetterByLetter(stringToDisplay));
                panel.SetActive(true);
                panel.SetActive(false);
                panelText.text = stringToDisplay;
                isTalking = false; 
            } else if(isTalking == false && characterHasToRespond == false){ //if the dialogue is done typing
                // Debug.Log("Not talking and no response is needed, closing panel");
                panelText.text = ""; 
                panel.SetActive(false);
                panelIsActive = false;
                isTalking = false;
            } else if(isTalking == false && characterHasToRespond == true){
                // Debug.Log("Waiting for character to respond...");
                characterResponds();
            } else{
                Debug.Log("No conditions fulfilled");
                if(isTypingLetterByLetter == false){
                    Debug.Log("Typing letter by letter is false");
                } else{
                    Debug.Log("Typing letter by letter is true");
                }
                if(isTalking == true){
                    Debug.Log("Is not talking is false");
                } else{
                    Debug.Log("Is not talking is true");
                }
                if(characterHasToRespond == false){
                    Debug.Log("Character has to respond is false");
                } else{
                    Debug.Log("Character has to respond is true");
                }
            }
        }

        //Pause Menu
        if(Input.GetKeyDown(KeyCode.Escape) && !escapeKeyPressed){
            escapeKeyPressed = true;
            if(isPaused)
            {
                ResumeGame();        
            } else
            {
                PauseGame();
            }
        }

        if(Input.GetKeyUp(KeyCode.Escape)){
            escapeKeyPressed = false;
        }
    }
    
    public void FixedUpdate(){
        if(currentChar.GetComponent<Character>().canMove == false){
            enableAllQuestionButtons();
        } else{
            disableAllQuestionButtons();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
       // MainMenuPanel.SetActive(true);
        MainMenuBackground.SetActive(true);
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
       // MainMenuPanel.SetActive(false);
        MainMenuBackground.SetActive(false);
        isPaused = false;
    }

    private void characterResponds(){
        charDialogueScript.currentCharacter = currentChar;
        // Debug.Log("Char Dialogue Script current char is set");
        charDialogueScript.DetermineDialogue();
    }

    
}