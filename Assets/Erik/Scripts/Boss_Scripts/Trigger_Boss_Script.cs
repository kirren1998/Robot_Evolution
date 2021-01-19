using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Boss_Script : MonoBehaviour
{
    GameObject player;

    private void Update()
    {
        if (player == null) return;
        Mathf.Clamp(player.transform.position.x,
            transform.position.x - GetComponent<BoxCollider2D>().size.x / 2,
            transform.position.x + GetComponent<BoxCollider2D>().size.x / 2);
        Debug.Log(transform.position.x - GetComponent<BoxCollider2D>().size.x / 2 + "_" + transform.position.x + GetComponent<BoxCollider2D>().size.x / 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponentInChildren<Player_Camera_Follow_Script>().BossFight(gameObject);
            GameObject.Find("Boss").GetComponent<Boss_Main_Script>().enabled = true;
            transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
            collision.GetComponent<Player_Ball_Movement_And_Dash>().inBossFight = true;
        }
    }
}
