using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_Entrance_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!player.CompareTag("Player") || !player.isTrigger) return;/*
        if (Mathf.Abs(player.attachedRigidbody.velocity.x) < 2) return;
        player.GetComponent<Player_Ball_Vent_Script>().ballSpeed = player.attachedRigidbody.velocity.x;*/
        player.GetComponent<Player_Ball_Movement_And_Dash>().inVent = true;
        player.GetComponent<Player_Ball_Vent_Script>().inVent = true;
        player.GetComponent<Player_Ball_Vent_Script>().currentNode = GetComponent<Vent_Node_Wrong_Path_Script>().nextInLine;
    }
}
