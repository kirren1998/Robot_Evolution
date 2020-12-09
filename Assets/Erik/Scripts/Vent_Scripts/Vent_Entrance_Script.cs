using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_Entrance_Script : MonoBehaviour
{
    [SerializeField] GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    private void OnTriggerStay2D(Collider2D player)
    {
        if (Input.GetKey(KeyCode.E) && player.GetComponent<Player_Ball_Vent_Script>().currentNode == null)
        {
            Player.GetComponent<CircleCollider2D>().enabled = false;
            Player.GetComponent<Rigidbody2D>().gravityScale = 0;
            Player.GetComponent<Player_Ball_Movement_And_Dash>().inVent = true;
            Player.GetComponent<Player_Ball_Vent_Script>().inVent = true;
            Player.GetComponent<Player_Ball_Vent_Script>().currentNode = transform.gameObject;
        }
    }
}
            /*if (Mathf.Abs(player.attachedRigidbody.velocity.x) < 2) return;
            player.GetComponent<Player_Ball_Vent_Script>().ballSpeed = player.attachedRigidbody.velocity.x;*/
