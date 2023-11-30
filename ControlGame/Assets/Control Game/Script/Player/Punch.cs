using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public float attackDamage = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var isComboing = this.gameObject.GetComponentInParent<Fighter>().isComboing;
            if (isComboing)
            {
                other.gameObject.GetComponentInParent<EnemyHealth>().TakeDamage(attackDamage);
            }
        }
    }
}
