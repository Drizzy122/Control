using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : MonoBehaviour
{
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1;
    public bool isComboing = false;
    public float blockReduction = 0.5f;
    public float attackDamage = 10f;
    public GameObject player;
    public BoxCollider damageTrigger;
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if (lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
            lastClickedTime = 0;
            isComboing = false;
        }

        //cooldown time
        if (Time.time > nextFireTime)
        {
            // Check for mouse input
            if (Input.GetKeyDown(KeyCode.F))
            {
                OnClick();

            }
        }
        if (isComboing){
            lastClickedTime += Time.deltaTime;

        }
        if (Input.GetKeyDown(KeyCode.G))
        {

            damageTrigger.enabled = false;
            Block();
        }

        if (Input.GetKeyUp(KeyCode.G))
        {

            damageTrigger.enabled = true;
            anim.SetBool("block", false);
        }
    }

    void OnClick()
    {
         anim.SetBool("block", false);
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        noOfClicks++;


        if (noOfClicks == 1)
        {
            isComboing = true;
            anim.SetTrigger("hit1");
            
            //lastClickedTime = 0f;
        }

        // Use exit time in Animator transitions and remove setBool(false) calls
        if (noOfClicks == 2 && isComboing)
        {
            anim.SetTrigger("hit2");
        }

        if (noOfClicks == 3 && isComboing)
        {
            anim.SetTrigger("hit3");
        }
    }

    void Block()
    {
        player.GetComponentInParent<PlayerHealth>().TakeDamage(blockReduction);
        anim.SetBool("block", true);
    }
}