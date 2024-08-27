using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public CharSpawner charSpawner;
    public Player player;
    public Character currentCharacter;
    public enum Types{
        AI,
        Human,
    }

    public enum Stance{
        Hostile,
        Neutral,
        Compassionate,
    }

    public TextMeshProUGUI TotalEncounters;
    public TextMeshProUGUI MistakesMade;
    public TextMeshProUGUI Totaltimetaken;
    public TextMeshProUGUI AiReported;
    public TextMeshProUGUI Ailetgo;
    public TextMeshProUGUI HumansReported;
    public TextMeshProUGUI Humansletgo;

    public GameObject gradeS;
    public GameObject gradeA;
    public GameObject gradeB;
    public GameObject gradeC;
    public GameObject gradeD;

    public static int totalScore = 0;
    public static int TotalEncounters_C;
    public static int MistakesMade_C;
    public static int Totaltimetaken_C;
    public static int AiReported_C;
    public static int Ailetgo_C;
    public static int HumansReported_C;
    public static int Humansletgo_C;

    public bool decisionWasMade = false;

    // Start is called before the first frame update
    void Start(){
        if(SceneManager.GetActiveScene().buildIndex == 2){
            TotalEncounters.text += TotalEncounters_C.ToString();
            MistakesMade.text += MistakesMade_C.ToString();
            AiReported.text += AiReported_C.ToString();
            Ailetgo.text += Ailetgo_C.ToString();
            HumansReported.text += HumansReported_C.ToString();
            Humansletgo.text += Humansletgo_C.ToString();
            Totaltimetaken.text += Totaltimetaken_C.ToString();

            if(totalScore >= 80){
                gradeS.SetActive(true);
            } else if(totalScore >= 60 && totalScore < 80){
                gradeA.SetActive(true);
            } else if(totalScore >= 40 && totalScore < 60){
                gradeB.SetActive(true);
            } else if(totalScore >= 20 && totalScore < 40){
                gradeC.SetActive(true);
            } else if(totalScore < 20){
                gradeD.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1){
            TotalEncounters_C = charSpawner.totalCount;
        }
        
        // TotalEncounters.text = "Total Encounters: " + Mathf.Round(TotalEncounters_C);
        // MistakesMade.text = "Mistakes Made: " + Mathf.Round(MistakesMade_C);
        // // Totaltimetaken.text = "Total Time Taken: " + string.Format("{0:D2}:{1:D2}:{2:D2}", Totaltimetaken_C.Hours, Totaltimetaken_C.Minutes, Totaltimetaken_C.Seconds);
        // AiReported.text = "AiReported: " + Mathf.Round(AiReported_C);
        // Ailetgo.text = "AIs let go: " + Mathf.Round(Ailetgo_C);
        // HumansReported.text = "Humans reported: " + Mathf.Round(HumansReported_C);
        // Humansletgo.text = "Humans let go: " + Mathf.Round(Humansletgo_C);
    }

    public void Reform(){
        if(!currentCharacter.canMove && !decisionWasMade){
            decisionWasMade = true;
            if(player.isLucas && !player.isXara){
                player.SwitchActivePlayableCharacter();
                player.switchCharacterButton.SetActive(false);
            } 
            player.DisableLucasRelatedObjects();
            player.DisableXaraRelatedObjects();
            player.disableAllQuestionButtons();

            if((Types) currentCharacter.type == Types.AI){
                AiReported_C += 1;
            } else if((Types) currentCharacter.type == Types.Human){
                HumansReported_C += 1;
            }

            if((Stance) currentCharacter.stance == Stance.Hostile){
                totalScore += 10;
            } else if((Stance) currentCharacter.stance == Stance.Neutral){
                totalScore -= 10;
                MistakesMade_C += 1;
            } else if((Stance) currentCharacter.stance == Stance.Compassionate){
                totalScore -= 10;
                MistakesMade_C += 1;
            }
            Debug.Log(totalScore);
            currentCharacter.canMove = true;
        } else{
            Debug.Log("Decision already made");
        }
    }

    public void LetGo(){
        if(!currentCharacter.canMove && !decisionWasMade){
            decisionWasMade = true;
            if(player.isLucas && !player.isXara){
                player.SwitchActivePlayableCharacter();
                player.switchCharacterButton.SetActive(false);
            } 
            player.DisableLucasRelatedObjects();
            player.DisableXaraRelatedObjects();
            player.disableAllQuestionButtons();

            if((Types) currentCharacter.type == Types.AI){
                Ailetgo_C += 1;
            } else if((Types) currentCharacter.type == Types.Human){
                Humansletgo_C += 1;
            }

            if((Stance) currentCharacter.stance == Stance.Hostile){
                totalScore -= 10;
                MistakesMade_C += 1;
            } else if((Stance) currentCharacter.stance == Stance.Neutral){
                totalScore += 10;
            } else if((Stance) currentCharacter.stance == Stance.Compassionate){
                totalScore += 10;
            }
            Debug.Log(totalScore);
            currentCharacter.canMove = true;
        }
    }


    public void displayEndResults(){
        SceneManager.LoadScene(2);
    }
}