using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f; // Adjust the speed as needed
    public Transform target; // The target object the camera and player will look at

    private float timer = 0f;
    private bool isRightArrowPressed = false;
    private bool isLeftArrowPressed = false;
    private bool isUpArrowPressed = false;
    private bool isDownArrowPressed = false;

    
    [Header("Player Attack")]
    private bool attacking = false;
    public float attackSpeed = 5f;
    public float attackDamage = 10f;
    public float attackRange = 1f;
    private float attackCooldown = 100f;
    public List<GameObject> EIList = new List<GameObject>();


    [Header("Animations")]
    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        // Initialization code can be placed here if needed
    }

    void Update()
    {
        Movement();
        Attack();
        // Add calls to other methods like Attack() or Block() if needed
    }

    void Movement()
    {
        HandleArrowKeyInput(KeyCode.D, ref isRightArrowPressed);
        HandleArrowKeyInput(KeyCode.A, ref isLeftArrowPressed);
        HandleArrowKeyInput(KeyCode.W, ref isUpArrowPressed);
        HandleArrowKeyInput(KeyCode.S, ref isDownArrowPressed);

        // Add velocity if the arrow keys are pressed for less than half a second
        if (isRightArrowPressed || isLeftArrowPressed || isUpArrowPressed || isDownArrowPressed)
        {
            HandleMovementInput();
        }

        // Orient the player object to face the target object
        if (target != null)
        {
            transform.LookAt(target);
        }
    }

    void HandleArrowKeyInput(KeyCode key, ref bool flag)
    {
        if (Input.GetKeyDown(key))
        {
            flag = true;
            timer = 0f;
        }

        if (Input.GetKeyUp(key))
        {
            flag = false;
            // Reset velocity when the key is released
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void HandleMovementInput()
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
            ResetArrowKeyFlags();
            // Reset velocity when the time exceeds half a second
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    void ResetArrowKeyFlags()
    {
        isRightArrowPressed = false;
        isLeftArrowPressed = false;
        isUpArrowPressed = false;
        isDownArrowPressed = false;
    }

    void Attack()
    {
        
    }



    void Block()
    {
        // Add block logic here
    }
}