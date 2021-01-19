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
        if (player.name != "Player") return;
        else if (Input.GetKey(KeyCode.E) && player.GetComponent<Player_Ball_Vent_Script>().currentNode == null)
        {
            GameObject[] me = GameObject.FindGameObjectsWithTag("AI_VisionCones");
            foreach (GameObject penis in me)
            {
                penis.GetComponent<Enemy_AI_Patrolling_Script>().seen = false;
                penis.GetComponent<Enemy_AI_Patrolling_Script>().awareness = 0;
            }
            SpriteRenderer[] yeet = Player.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer man in yeet)
            {
                man.enabled = false;
            }
            //Player.GetComponent<Player_Ball_Vent_Script>().ResetList(yeet);
            /*Player.GetComponent<SpriteRenderer>().enabled = false;
            Player.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
            */Player.GetComponent<CircleCollider2D>().enabled = false;
            Player.GetComponent<Rigidbody2D>().gravityScale = 0;
            Player.GetComponent<Player_Ball_Movement_And_Dash>().inVent = true;
            Player.GetComponent<Player_Ball_Vent_Script>().inVent = true;
            Player.GetComponent<Player_Ball_Vent_Script>().currentNode = transform.gameObject;
            if (player.GetComponent<Player_Ball_Movement_And_Dash>().inBossFight)
            {
                player.GetComponentInChildren<Player_Camera_Follow_Script>().followPlayer = true;
                Destroy(gameObject.transform.parent.parent.gameObject, 6);
                player.GetComponent<Player_Ball_Movement_And_Dash>().inBossFight = false;
            }
        }
    }
}
            /*if (Mathf.Abs(player.attachedRigidbody.velocity.x) < 2) return;
            player.GetComponent<Player_Ball_Vent_Script>().ballSpeed = player.attachedRigidbody.velocity.x;*/
