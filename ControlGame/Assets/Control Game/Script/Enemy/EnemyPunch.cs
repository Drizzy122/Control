using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    public float attackDamage = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var isComboing = this.gameObject.GetComponentInParent<EnemyFighter>().isComboing;

            if (isComboing)
            {
                other.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }
    }
}
