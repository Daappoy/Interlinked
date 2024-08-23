using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    
    public TextMeshProUGUI TotalEncounters;
    public TextMeshProUGUI MistakesMade;
    public TextMeshProUGUI Totaltimetaken;
    public TextMeshProUGUI AiReported;
    public TextMeshProUGUI Ailetgo;
    public TextMeshProUGUI HumansReported;
    public TextMeshProUGUI Humansletgo;

    public static int TotalEncounters_C;
    public static int MistakesMade_C;
    public static int Totaltimetaken_C;
    public static int AiReported_C;
    public static int Ailetgo_C;
    public static int HumansReported_C;
    public static int Humansletgo_C;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TotalEncounters.text = "Total Encounters: " + Mathf.Round(TotalEncounters_C);
        MistakesMade.text = "Mistakes Made: " + Mathf.Round(MistakesMade_C);
        // Totaltimetaken.text = "Total Time Taken: " + string.Format("{0:D2}:{1:D2}:{2:D2}", Totaltimetaken_C.Hours, Totaltimetaken_C.Minutes, Totaltimetaken_C.Seconds);
        AiReported.text = "AiReported: " + Mathf.Round(AiReported_C);
        Ailetgo.text = "AIs let go: " + Mathf.Round(Ailetgo_C);
        HumansReported.text = "Humans reported: " + Mathf.Round(HumansReported_C);
        Humansletgo.text = "Humans let go: " + Mathf.Round(Humansletgo_C);
    }
}
