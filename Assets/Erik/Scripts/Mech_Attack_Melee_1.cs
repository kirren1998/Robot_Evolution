using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_Attack_Melee_1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.CompareTag("Enemy"))
            enemy.GetComponent<Enemy_Damage_Player_Script>().TakeDamage(2);
    }
}
