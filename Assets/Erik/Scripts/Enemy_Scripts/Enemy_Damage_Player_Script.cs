using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage_Player_Script : MonoBehaviour
{
    GameObject player;
    [Range(1, 5)] [SerializeField] int damage = 1;
    Quaternion startRot;
    BoxCollider2D attackSquare;
    private void Start()
    {
        player = GameObject.Find("Player");
        startRot = Quaternion.identity;
        attackSquare = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Player") && player.isTrigger)
            if (Mathf.Abs(player.GetComponent<Rigidbody2D>().angularVelocity) < 2000 && player.GetComponent<Player_Health_script>().canTakeDamage)
            {
                Vector2 dir = player.transform.position - transform.position;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.normalized.x * 3, 3);
                player.GetComponent<Player_Health_script>().TakeDamage(damage);
            }
        if (player.CompareTag("Mech_Health") && player.GetComponentInParent<Mech_Movement_1>().isPiloted)
        {
            player.GetComponent<Mech_Health>().TakeDamage(damage);
        }
    }
    public void Attack()
    {
        Vector3 dir = transform.position - player.transform.position;
        Quaternion shit = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Euler(0, 0, (shit.eulerAngles.x) * -transform.parent.transform.localScale.x);
        attackSquare.enabled = true;
        Invoke("ResetAttack", 0.5f);
    }
    private void ResetAttack()
    {
        attackSquare.enabled = false;
        transform.rotation = startRot;
    }
}
