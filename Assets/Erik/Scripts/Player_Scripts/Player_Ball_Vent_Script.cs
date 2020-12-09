using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ball_Vent_Script : MonoBehaviour
{
    public float ballSpeed;
    Rigidbody2D rb;
    public GameObject currentNode;
    public bool inVent = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (!inVent || currentNode == null) return;
        Vector2 dir = currentNode.transform.position - transform.position;
        Vector2 speed = dir.normalized * 3 /*Mathf.Abs(ballSpeed)*/;
        rb.velocity = speed;
        if (dir.magnitude < 0.07f)
        {
            if (currentNode.GetComponent<Vent_Node_Wrong_Path_Script>().nextInLine != null)
            {
                if (!currentNode.GetComponent<Vent_Node_Wrong_Path_Script>().leverBool) currentNode = 
                        currentNode.GetComponent<Vent_Node_Wrong_Path_Script>().nextInLine;
                else currentNode = currentNode.transform.GetChild(0).gameObject;
            } else
            {
                GetComponent<Rigidbody2D>().gravityScale = 1;
                inVent = false;
                GetComponent<Player_Ball_Movement_And_Dash>().inVent = false;
                currentNode = null;
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    // new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)) == new Vector2(Mathf.RoundToInt(node[currentNode].transform.position.x), Mathf.RoundToInt(node[currentNode].transform.position.y

    }
}
