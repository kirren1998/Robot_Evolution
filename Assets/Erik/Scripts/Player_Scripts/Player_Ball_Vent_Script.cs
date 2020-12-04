using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ball_Vent_Script : MonoBehaviour
{
    public float ballSpeed;
    Rigidbody2D rb;
    public GameObject currentNode;
    public bool inVent;

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

            
    // new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)) == new Vector2(Mathf.RoundToInt(node[currentNode].transform.position.x), Mathf.RoundToInt(node[currentNode].transform.position.y

    }
    private void OnTriggerEnter2D(Collider2D vent)
    {
        if (currentNode == null) return;
        if (vent.GetComponent<Vent_Node_Wrong_Path_Script>().nextInLine != null && vent.CompareTag("Vent"))
        {
            if (!vent.GetComponent<Vent_Node_Wrong_Path_Script>().leverBool) currentNode = vent.GetComponent<Vent_Node_Wrong_Path_Script>().nextInLine;
            else currentNode = vent.transform.GetChild(0).gameObject;
        } else
        {
            inVent = false;
            GetComponent<Player_Ball_Movement_And_Dash>().inVent = false;
            currentNode = null;
        }
    }
}
