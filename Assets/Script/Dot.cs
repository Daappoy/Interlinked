using UnityEngine;

public class Dot : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private bool canAwardPoint;
    public int points = 0;
    public float speed = 2f;
    public AudioManager audioManager;


    void Start(){
        // Debug.Log("Dot script is running");
        points = 0;
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        MoveRight();
    }

    void Update(){
        if(canAwardPoint == true){
            // Debug.Log("Can get points");
            if(Input.GetKeyDown(KeyCode.E))
            {
                audioManager.PlaySFX(audioManager.Thump);
                Debug.Log("Points + 1!");
                points += 1;
                canAwardPoint = false;
            }
        }
    }

    public void MoveRight(){
        // Debug.Log("Move right function called");
        Vector3 newPosition = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called with " + other.tag);
        if (other.tag == "Line"){
            Debug.Log("Dot is on the Line");
            canAwardPoint = true;
        }
        else{
            Debug.Log("Other tag is not 'Line', it is " + other.tag);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        Debug.Log("OnTriggerExit2D called with " + other.tag);
        canAwardPoint = false;
    }
    
}