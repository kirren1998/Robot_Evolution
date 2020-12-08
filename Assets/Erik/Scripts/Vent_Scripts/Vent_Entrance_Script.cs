using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_Entrance_Script : MonoBehaviour
{
    bool canGoIn;
    GameObject Player;
    Canvas yeet;

    private void Start()
    {
        //yeet = GameObject.Find("")
        Player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!player.CompareTag("Player") || !player.isTrigger) return;
            canGoIn = true;
    }
    private void OnTriggerExit2D(Collider2D player)
    {
        if (!player.CompareTag("Player") || !player.isTrigger) return;
        canGoIn = false;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && canGoIn)
        {
            Player.GetComponent<CircleCollider2D>().enabled = false;
            Player.GetComponent<Rigidbody2D>().gravityScale = 0;
            /*if (Mathf.Abs(player.attachedRigidbody.velocity.x) < 2) return;
            player.GetComponent<Player_Ball_Vent_Script>().ballSpeed = player.attachedRigidbody.velocity.x;*/
            Player.GetComponent<Player_Ball_Movement_And_Dash>().inVent = true;
            Player.GetComponent<Player_Ball_Vent_Script>().inVent = true;
            Player.GetComponent<Player_Ball_Vent_Script>().currentNode = GetComponent<Vent_Node_Wrong_Path_Script>().nextInLine;
        }
        if (canGoIn)
        {

        }
    }
}
