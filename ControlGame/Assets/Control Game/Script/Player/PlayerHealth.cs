using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;
    private Animator animator;
    private void Awake()
    { 
        animator = GetComponent<Animator>();
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
                GetComponent<PlayerMovement>().enabled = false;
                Invoke(nameof(RestartScene), 5f); // Restart scene after  seconds
            }
        }
    }
   
    private void RestartScene()
    {
        SceneManager.LoadScene("SampleScene"); // Replace "EndScreen" with the name of your end screen scene
    }
}