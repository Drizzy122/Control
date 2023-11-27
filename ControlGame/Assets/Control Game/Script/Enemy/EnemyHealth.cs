using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    
    private bool dead;
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        if (currentHealth > 0)
        {
 
            //animator.SetTrigger("IsHurt");
        }
        else
        {
            if (!dead)
            {
                GetComponent<EnemyScript>().enabled = false;
                dead = true;
                Destroy(gameObject, 5f); // Destroy enemy object after 1 seconds
            }
        }
    }
}