using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage_Player_Script : MonoBehaviour
{
    [Range(0, 5)] [SerializeField] int damage = 1;
    private void OnTriggerStay2D(Collider2D player)
    {
        if (!player.CompareTag("Player") || !player.isTrigger) return;
        if (Mathf.Abs(player.GetComponent<Rigidbody2D>().angularVelocity) < 2000 && player.GetComponent<Player_Health_script>().canTakeDamage)
        {
            Vector2 dir = player.transform.position - transform.position;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * 2 * transform.localScale.x, 3 * transform.localScale.x);
            player.GetComponent<Player_Health_script>().TakeDamage(damage);
        }
    }
}
