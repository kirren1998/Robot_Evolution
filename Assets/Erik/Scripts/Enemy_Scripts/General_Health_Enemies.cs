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
            Destroy(transform.parent.gameObject);
        }
        if (transform.parent.name == "Darlek")
        {
            transform.parent.GetChild(0).GetComponent<Enemy_AI_Patrolling_Script>().disabled = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!player.CompareTag("Player") || player.isTrigger) return;
        if (player.GetComponent<Rigidbody2D>().angularVelocity > 2000)
        {
            TakeDamage(player.GetComponent<Player_Ball_Movement_And_Dash>().Damage);
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(player.attachedRigidbody.velocity.x, 2);
            player.attachedRigidbody.velocity = new Vector2(-player.attachedRigidbody.velocity.normalized.x * 5, player.attachedRigidbody.velocity.y + 2);
        }
    }
}
