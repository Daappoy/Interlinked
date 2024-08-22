using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatScanResults : MonoBehaviour
{
    public GameObject heartbeatMinigame;
    public GameObject Sentient;
    public GameObject NotSentient;
    public CharSpawner CharSpawner;
    public Heartbeat heartbeat;
    void Start()
    {
        NotSentient.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        bool isSentient = heartbeatMinigame.GetComponent<Heartbeat>().confirmation;;
        if(isSentient == true)
        {
            // Debug.Log("Is Sentient Panel ")
            NotSentient.SetActive(false);
            Sentient.SetActive(true);
        } 
    }
}
