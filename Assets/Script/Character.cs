using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{   
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

    public Player player;
    public int type;
    public int characterName;
    public int stance;
    
    private Rigidbody2D rb2d;

    public bool canMove = false;
    private bool courutineRan = false;
    public float speed = 0.5f;

    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    
    void FixedUpdate()
    {
        if(canMove){
            MoveRight();
        }

        if(transform.position.x >= -1.5 && transform.position.x <= 1.6){
            if(!courutineRan){
                canMove = false;
                StartCoroutine(WaitAndMove(5));
                
            }
        }
        
        if(transform.position.x > 14){
            Destroy(gameObject);
            // Debug.Log("Destroyed");
        }
    }

    IEnumerator WaitAndMove(int timeToWait)
    {
        courutineRan = true;
        if(courutineRan){
            // Debug.Log("At chair");
        }
        yield return new WaitForSeconds(60);
        canMove = true;
    }

    void MoveRight(){
        Vector3 newPosition = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        rb2d.MovePosition(newPosition);
    }
}
