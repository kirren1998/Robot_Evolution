using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General_Health_Enemies : MonoBehaviour
{
    [Range(0, 10)][SerializeField] int Health;
    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        if (gameObject.name == "Darlek")
        {
            GetComponentInChildren<Enemy_AI_Patrolling_Script>().seen = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!player.CompareTag("Player")) return;
        if (player.GetComponent<Rigidbody2D>().angularVelocity > 2000)
        {
            TakeDamage(player.GetComponent<Player_Ball_Movement_And_Dash>().Damage);
        }
    }
}
