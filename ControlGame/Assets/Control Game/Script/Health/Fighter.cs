using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1;
    bool isComboing = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        /*
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
        */

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
        if (isComboing){
            lastClickedTime += Time.deltaTime;

        }
    }

    void OnClick()
    {
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

}