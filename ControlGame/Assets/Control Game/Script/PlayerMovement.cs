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

    
    [Header("Player Attack")]
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;


    [Header("Animations")]
    public Animator animator;

    void Awake()
    {
        anim = GetComponent<Animator>();
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

    void Attack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit3"))
        {
            anim.SetBool("hit3", false);
            noOfClicks = 0;
        }


        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        //cooldown time
        if (Time.time > nextFireTime)
        {
            // Check for mouse input
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();

            }
        }
    }

    void OnClick()
    {
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            anim.SetBool("hit1", true);
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            anim.SetBool("hit1", false);
            anim.SetBool("hit2", true);
        }
        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            anim.SetBool("hit2", false);
            anim.SetBool("hit3", true);
        }
    }
    void Block()
    {
        // Add block logic here
    }
}
