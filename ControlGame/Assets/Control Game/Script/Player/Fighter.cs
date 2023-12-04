using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public  Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1;
    public bool isComboing = false;
    public float attackDamage = 10f;
    public float blockReduction = 0.5f;
    public GameObject enemy;

    public BoxCollider damageTrigger;
    private void Start()
    {
        anim = GetComponent<Animator>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
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
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();

            }
        }
        if (isComboing)
        {
            lastClickedTime += Time.deltaTime;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            damageTrigger.enabled = false;
            Block();
        }

        if (Input.GetKeyUp(KeyCode.Space))
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
        enemy.GetComponentInParent<EnemyHealth>().TakeDamage(blockReduction);
        anim.SetBool("block", true);
    }
}