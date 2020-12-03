using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_Entrance_Script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!player.CompareTag("Player") || !player.isTrigger) return;
        if (Mathf.Abs(player.attachedRigidbody.velocity.x) < 2) return;
        player.GetComponent<Player_Ball_Vent_Script>().ballSpeed = player.attachedRigidbody.velocity.x;
        player.GetComponent<Player_Ball_Movement_And_Dash>().inVent = true;/*
        player.GetComponent<Player_Ball_Vent_Script>().isInMe = true;*/
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject me = transform.GetChild(i).gameObject;
            player.GetComponent<Player_Ball_Vent_Script>().node.Add(me);
        }
    }
}
