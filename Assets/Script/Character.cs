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
    public bool stanceWasRevealed = false;

    private Rigidbody2D rb2d;
    
    public CharSpawner charSpawner;
    public Animator animator;
    public bool canMove = false;
    // private bool courutineRan = false;
    public float speed = 0.5f;
    public bool inInteview;
    
    void Start(){
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = charSpawner.animator.runtimeAnimatorController;
    }
    // Update is called once per frame
    
    public void UpdateAnimator(Animator sourceAnimator)
    {
        foreach (AnimatorControllerParameter parameter in sourceAnimator.parameters)
        {
            switch (parameter.type)
            {
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(parameter.name, sourceAnimator.GetFloat(parameter.name));
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(parameter.name, sourceAnimator.GetInteger(parameter.name));
                    break;
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(parameter.name, sourceAnimator.GetBool(parameter.name));
                    break;
                case AnimatorControllerParameterType.Trigger:
                    if (sourceAnimator.GetBool(parameter.name)) // Assuming trigger is set by a bool
                    {
                        animator.SetTrigger(parameter.name);
                    }
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if(canMove){
            MoveRight();
        }

        if(transform.position.x >= -1.4 && transform.position.x <= -1.3){
            canMove = false;
            if (player != null)
            {
                // Debug.Log("Player reference is valid. Calling enablePlayerQuestions.");
                player.enablePlayerQuestions();
                inInteview = true;
            }
        }
        
        if(transform.position.x > 14){
            Destroy(gameObject);
            charSpawner.SpawnACharacter();
            // Debug.Log("Destroyed");
        }
    }

    void Update(){
        UpdateAnimator(charSpawner.animator);
    }

    void MoveRight(){
        Vector3 newPosition = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        rb2d.MovePosition(newPosition);
    }
}
