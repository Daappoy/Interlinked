using System.Collections;
using UnityEngine;
using TMPro;
using System.Linq;

public class Player : MonoBehaviour
{
    
    //Interviewee
        public Character currentChar;
        public CharDialogue charDialogueScript;
    
    //Pause menu
        public GameObject MainMenuBackground;
        private bool isPaused = false;
        private bool escapeKeyPressed = false;
    
    // Question Buttons
        public GameObject questionsPanel;
        public GameObject[] nonNeutralQuestionButtons;
        public GameObject[] buttons;
        public TextMeshProUGUI[] buttonTexts;
        public int[] aiButtonIndexes = { 0, 1, 3, 5, 7, 9 };
        public int[] humanButtonIndexes = { 0, 2, 4, 5, 6, 8 };

    //Subtitles
        public GameObject panel;
        public TMP_Text panelText;
        private string stringToDisplay;
        public int currentQuestionIndex = 0;
        public int charSpecificQuestionNum = 0;
        // private int panelLetterCount;
        private bool isTypingLetterByLetter = false;
        private bool isTalking = false; //player
        public bool characterHasToRespond = false;
        private Coroutine typingCoroutine;
        public bool canSwitch = false;
    
    //Playable Character Related 
        public bool isXara = false; //if we're playing as the human right now
        public bool isLucas = false;
        public bool ranOnce = false;
        public GameObject switchCharacterButton;
        public Heartbeat heartbeat;
        public GameObject[] XaraRelatedObjects;
        public GameObject[] LucasRelatedObjects;
        public GameObject[] lines;


    public enum Types{
        AI,
        Human,
    }

    public enum Stance{
        Hostile,
        Neutral,
        Compassionate,
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
        for(int i = 0; i < buttons.Length; i++){
            buttons[i].SetActive(false);
        }
    }

    public void enablePlayerQuestions(){
        questionsPanel.SetActive(true);
        if((Types)currentChar.type == Types.AI){
            foreach (int index in aiButtonIndexes){
                if (index < buttons.Length){
                    buttons[index].SetActive(true);
                    if (index == 5){
                        buttonTexts[index].text = characterSpecificQuestions[checkCharacter()];
                    }else{
                        buttonTexts[index].text = playerOptions[index];
                    }
                }
                else{
                    Debug.LogWarning($"Index {index} is out of bounds for buttons array.");
                }
            }
        } else if((Types)currentChar.type == Types.Human){
            foreach (int index in humanButtonIndexes){
                if (index < buttons.Length){
                    buttons[index].SetActive(true);
                    if (index == 5){
                        buttonTexts[index].text = characterSpecificQuestions[checkCharacter()];
                    } else{
                        buttonTexts[index].text = playerOptions[index];
                    }
                } else{
                    Debug.LogWarning($"Index {index} is out of bounds for buttons array.");
                }
            }
        } else{
            Debug.LogWarning("Current character type is not AI or Human.");
        }
    }

    public void question0Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 0;
            DisplayWhatWasSaid();
        }
    }
    public void question1Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 1;
            DisplayWhatWasSaid();
        }
    }
    public void question2Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 2;
            DisplayWhatWasSaid();
        }
    }
    public void question3Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 3;
            DisplayWhatWasSaid();
        }
    }
    public void question4Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 4;
            DisplayWhatWasSaid();
        }
    }
    public void question5Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 5;    
            DisplayWhatWasSaid();
        }
    }
    public void question6Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 6;
            DisplayWhatWasSaid();
        }
    }
    public void question7Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 7;
            DisplayWhatWasSaid();
        }
    }
    public void question8Picked(){
        if(!currentChar.canMove){
            disableAllQuestionButtons();
            currentQuestionIndex = 8;
            DisplayWhatWasSaid();
        }
    }
    public void question9Picked(){
        if(!currentChar.canMove){
            currentQuestionIndex = 9;
            DisplayWhatWasSaid();
        }
    }

    //After the player clicks the button, we display the question in the subtitles below
    public void DisplayWhatWasSaid(){ //Set subtitle panel to active, set the bool to false, and determine which string you're gonna show
        isTalking = true;
        panel.SetActive(true);
        DisplayString();
    }

    public void DisplayString(){
        characterHasToRespond = true;
        if(currentQuestionIndex == 5){ //if it's a character specific question, we use the character specific question array
            charSpecificQuestionNum = checkCharacter();
            // Debug.Log("The character specific question index is: " + charSpecificQuestionNum);
            stringToDisplay = characterSpecificQuestions[charSpecificQuestionNum];
        } else{ //we use the base array filled with the player dialogues
            // Debug.Log("The question index is: " + currentQuestionIndex);
            stringToDisplay = playerOptions[currentQuestionIndex];
        }
        heartbeat.upperMonitorText.text = stringToDisplay;
        isTypingLetterByLetter = true;
        panelText.text = "";
        typingCoroutine = StartCoroutine(TypeLetterByLetter(stringToDisplay)); //we start having it type out the dialogue letter by letter
    }
    
    public int checkCharacter(){
        if( (Types) currentChar.type == Types.AI){
            if( (AINames) currentChar.characterName == AINames.Dorothy ){
                //Dorothy
                // Debug.Log("Current Character is Dorothy");
                return 0;
            } else if( (AINames) currentChar.characterName == AINames.Lily ){
                //Lily
                // Debug.Log("Current Character is Lily");
                return 1;
            } else if( (AINames) currentChar.characterName == AINames.Garry ){
                //Garry
                // Debug.Log("Current Character is Garry");
                return 2;
            } else{
                Debug.LogWarning($"Character not found");
                return -1;
            }
        } else if((Types) currentChar.type == Types.Human){
            if( (HumanNames) currentChar.characterName == HumanNames.Caleb){
                //Caleb
                // Debug.Log("Current Character is Caleb");
                return 3;
            } else if((HumanNames) currentChar.characterName == HumanNames.Isaac){
                //Isaac
                // Debug.Log("Current Character is Isaac");
                return 4;
            } else if((HumanNames) currentChar.characterName == HumanNames.Kim){
                //Kim
                // Debug.Log("Current Character is Kim");
                return 5;
            } else if((HumanNames) currentChar.characterName == HumanNames.Timmy){
                //Timmy
                // Debug.Log("Current Character is Timmy");
                return 6;
            } else if((HumanNames) currentChar.characterName == HumanNames.Kate){
                //Kate
                // Debug.Log("Current Character is Kate");
                return 7;
            } else{
                Debug.LogWarning($"Character not found");
                return -1;
            }
        } else{
            Debug.LogWarning($"Character not found");
            return -1;
        }
    }

    public IEnumerator TypeLetterByLetter(string stringToDisplay){
        for(int i = 0; i < stringToDisplay.Length; i++){ //this will type it till it's done
            panelText.text += stringToDisplay[i];
            yield return new WaitForSeconds(0.05f);
        }
        isTypingLetterByLetter = false;
    }
    
    public void SwitchActivePlayableCharacter(){
        switchCharacterButton.SetActive(false);

        if(ranOnce == true){
            ranOnce = false;
        }

        if(isLucas){
            isLucas = false;
            isXara = true;
            StopCoroutine(typingCoroutine);
            panelText.text = "";
            stringToDisplay = "So according to our information...";
            typingCoroutine = StartCoroutine(TypeLetterByLetter(stringToDisplay));
            DisableLucasRelatedObjects();
            EnableXaraRelatedObjects();
            enablePlayerQuestions();
        } else if(isXara){
            isLucas = true;
            isXara = false;
            foreach(GameObject line in lines){
                line.SetActive(false);
            }
            
            StopCoroutine(typingCoroutine);
            panelText.text = "";
            stringToDisplay = "Let's see what I can do";
            typingCoroutine = StartCoroutine(TypeLetterByLetter(stringToDisplay));
            DisableXaraRelatedObjects();
            EnableLucasRelatedObjects();
        } else{
            Debug.Log("There's been an error, we don't know if the player is playing as Xara or Lucas");
        }
    }

    void Start(){
        Debug.Log("Player script is running");
        Time.timeScale = 1f;
        isPaused = false;
        isXara = true;
        LucasRelatedObjects = GameObject.FindGameObjectsWithTag("LUCAS");
        // Debug.Log("Found lucas related objects");
        XaraRelatedObjects = GameObject.FindGameObjectsWithTag("Xara");
        // Debug.Log("Found xara related objects");
        
        lines = GameObject.FindGameObjectsWithTag("Line");
        Debug.Log("Found heartbeat scan's lines");

        buttons = GameObject.FindGameObjectsWithTag("Buttons");
        // Debug.Log("Found buttons");
        // Debug.Log($"Found {buttons.Length} buttons.");
        buttons = buttons.OrderBy(button => button.name).ToArray();
        buttonTexts = new TextMeshProUGUI[buttons.Length];
        for (int i = 0; i < buttons.Length; i++){
            buttonTexts[i] = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            // Debug.Log($"Button {i} text is {buttonTexts[i].text}");
        }

        EnableXaraRelatedObjects();
        disableAllQuestionButtons();
        DisableLucasRelatedObjects();
    }

    // Update is called once per frame
    void Update(){
        // if(canSwitch){
        //     canSwitch = false;
        //     can
        // }

        if(isTypingLetterByLetter){ 
            isTalking = true;
        }
        if(isXara){
            //Dialogue Shenanigans
            if( Input.GetMouseButtonDown(0)){ //if the player clicks
                // Debug.Log("Mouse Clicked");
                if(isTypingLetterByLetter == true){ 
                    StopCoroutine(typingCoroutine);
                    isTypingLetterByLetter = false; 
                    Debug.Log("Showing full line...");
                    panel.SetActive(true);
                    Debug.Log("String to display is: " + stringToDisplay);
                    panelText.text = stringToDisplay;
                    isTalking = false; 
                } else if(!isTalking && characterHasToRespond == true){
                    // Debug.Log("Waiting for character to respond...");
                    characterResponds();
                } else if(!isTalking && characterHasToRespond == false){
                    // Debug.Log("Character has responded, sending them away...");
                    if(!ranOnce && typingCoroutine != null){
                        StopCoroutine(typingCoroutine);
                        panelText.text = "";
                        typingCoroutine = StartCoroutine(TypeLetterByLetter("Alright then, what do you think Lucas?"));
                        ranOnce = true;
                    }        
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
    
    public void EnableLucasRelatedObjects(){
        foreach(GameObject obj in LucasRelatedObjects){
            obj.SetActive(true);
        }
    }

    public void EnableXaraRelatedObjects(){
        foreach(GameObject obj in XaraRelatedObjects){
            obj.SetActive(true);
        }
    }

    public void DisableLucasRelatedObjects(){
        foreach(GameObject obj in LucasRelatedObjects){
            obj.SetActive(false);
        }
    }

    public void DisableXaraRelatedObjects(){
        foreach(GameObject obj in XaraRelatedObjects){
            obj.SetActive(false);
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
        panelText.text = "";
        charDialogueScript.DetermineDialogue();
    }

    public void sendAwayCharacter(){
        // isDoneWithCharacter = true;
        currentChar.canMove = true;
    }
}