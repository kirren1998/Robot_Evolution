using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage_Player_Script : MonoBehaviour
{
    
    [SerializeField] int health = 3;
    [Range(0, 5)] [SerializeField] int damage = 1;
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!player.CompareTag("Player") || !player.isTrigger) return;
        if (Mathf.Abs(player.GetComponent<Rigidbody2D>().angularVelocity) > 2000)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(-player.GetComponent<Rigidbody2D>().velocity.x, 3);
            TakeDamage(player.GetComponent<Player_Ball_Movement_And_Dash>().Damage);
            player.GetComponent<Player_Ball_Movement_And_Dash>().Damage = 1;
        }
        else player.GetComponent<Player_Health_script>().TakeDamage(damage);
    }
    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
