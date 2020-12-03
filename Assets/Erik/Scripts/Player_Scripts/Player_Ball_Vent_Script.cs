using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ball_Vent_Script : MonoBehaviour
{
    [SerializeField] int currentNode;
    public float ballSpeed;
    Rigidbody2D rb;
    public List<GameObject> node;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (node.Count == 0) return;
        Vector2 dir = node[currentNode].transform.position - transform.position;
        Vector2 speed = dir.normalized * Mathf.Abs(ballSpeed);
        rb.velocity = speed;
        if (new Vector2(Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y))
            == new Vector2(Mathf.RoundToInt(node[currentNode].transform.position.x),
            Mathf.RoundToInt(node[currentNode].transform.position.y)))
        {
            if (node[currentNode].transform.childCount > 0 && node[currentNode].GetComponent<Vent_Node_Wrong_Path_Script>().leverBool)
            {
                GameObject noode = node[currentNode];
                node.Clear();
                for (int i = 0; i < noode.transform.childCount; i++)
                {
                    node.Add(noode.transform.GetChild(i).gameObject);
                }
                currentNode = 0;
            } else currentNode++;
            if (node.Count == currentNode)
            {
                Debug.Log("Shit");
                currentNode = 0;
                GetComponent<Player_Ball_Movement_And_Dash>().inVent = false;
                node.Clear();
            }
        }

    }
}
