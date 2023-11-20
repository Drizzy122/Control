using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f; // Adjust the speed as needed
    public Transform target; // The target object the camera and player will look at
    private float timer = 0f;
    private bool isRightArrowPressed = false;
    private bool isLeftArrowPressed = false;
    private bool isUpArrowPressed = false;
    private bool isDownArrowPressed = false;

    [Header("Attack/Block")]
    bool isAttacking = false;
    bool isBlocking = false;
    public float AttackDamage = 1f;
    public float AttackDelay = 0.5f;

    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        
    }
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        // Check for right arrow key press
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isRightArrowPressed = true;
            timer = 0f;
        }

        // Check for right arrow key release
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isRightArrowPressed = false;
            // Reset velocity when the key is released
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        // Check for left arrow key press
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isLeftArrowPressed = true;
            timer = 0f;
        }

        // Check for left arrow key release
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            isLeftArrowPressed = false;
            // Reset velocity when the key is released
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        // Check for up arrow key press
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isUpArrowPressed = true;
            timer = 0f;
        }

        // Check for up arrow key release
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isUpArrowPressed = false;
            // Reset velocity when the key is released
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        // Check for down arrow key press
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isDownArrowPressed = true;
            timer = 0f;
        }

        // Check for down arrow key release
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            isDownArrowPressed = false;
            // Reset velocity when the key is released
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        // Add velocity if the arrow keys are pressed for less than half a second
        if (isRightArrowPressed || isLeftArrowPressed || isUpArrowPressed || isDownArrowPressed)
        {
            timer += Time.deltaTime;

            if (timer < 0.2f)
            {
                // Move in the specified direction relative to the object's local directions
                float horizontalInput = isRightArrowPressed ? 1f : (isLeftArrowPressed ? -1f : 0f);
                float verticalInput = isUpArrowPressed ? 1f : (isDownArrowPressed ? -1f : 0f);

                Vector3 movement = transform.right * horizontalInput + transform.forward * verticalInput;
                GetComponent<Rigidbody>().velocity = movement.normalized * speed;
            }
            else
            {
                isRightArrowPressed = false;
                isLeftArrowPressed = false;
                isUpArrowPressed = false;
                isDownArrowPressed = false;
                // Reset velocity when the time exceeds half a second
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        // Orient the player object to face the target object
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
    void Attack()
    {

    }

    void Block()

    {

    }

}