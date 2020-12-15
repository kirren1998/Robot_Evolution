using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_Health : MonoBehaviour
{
    [SerializeField] int health;

    public void TakeDamage(int Damage)
    {
        Debug.Log("Shjt");
        health -= Damage;
        if (health <= 0)
        {
            GetComponentInParent<Mech_Movement_1>().Exit();
            Destroy(transform.parent);
        }
    }
}
