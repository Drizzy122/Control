using System.Collections.Generic;
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

   


   

    void Start()
    {
        // Initialization code can be placed here if needed
    }

    void Update()
    {
        Movement();
       
        // Add calls to other methods like Attack() or Block() if needed
    }

    void Movement()
    {
        HandleArrowKeyInput(KeyCode.RightArrow, ref isRightArrowPressed);
        HandleArrowKeyInput(KeyCode.LeftArrow, ref isLeftArrowPressed);
        HandleArrowKeyInput(KeyCode.UpArrow, ref isUpArrowPressed);
        HandleArrowKeyInput(KeyCode.DownArrow, ref isDownArrowPressed);

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

 

   
    void Block()
    {
        // Add block logic here
    }
}
