using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_Patrolling_Script : MonoBehaviour
{
    public bool seen;
    float awareness = 0;
    GameObject Player;
    public RaycastHit2D hit;
    LayerMask groundAndWall = 1 << 9 , findPlayer = 1 << 9 | 1 << 8;
    /*bool inside;
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            inside = true;
        }
    }
    private void Update()
    {
        if (inside)
        {
            awareness += Time.deltaTime;
            float yet = Vector2.Distance(transform.position, Player.transform.position);
        }
    }*/ // Do this later you numb-nut
    private void Start()
    {
        Player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player")) seen = true;
    }
    private void Update()
    {
        if (seen) awareness = 2; else Mathf.Clamp(awareness -= Time.deltaTime,0, 2);
        if (awareness > 0 && Vector2.Distance(transform.position, Player.transform.position) < 2)
        {
            hit = Physics2D.Raycast(transform.position, Player.transform.position - transform.position, 2, findPlayer);
            Debug.DrawRay(transform.position, (Player.transform.position - transform.position).normalized * 2);
            if (hit.transform.CompareTag("Player"))
                seen = true;
            else seen = false;
        }
        else;
        //The base unaware patrolling;
        RaycastHit2D wall, ground;
        wall = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 0.5f * transform.localScale.x, groundAndWall);
        ground = Physics2D.Raycast(new Vector2(transform.position.x + (0.3f * transform.localScale.x), transform.position.y), 
            Vector2.down, 1 * transform.localScale.x, groundAndWall);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right, Color.cyan);
        Debug.DrawRay(new Vector2(transform.position.x + (0.3f * transform.localScale.x), transform.position.y), Vector2.down, Color.cyan);
        if (wall)
            Jump();
        if (!ground)
            TurnAround();
    }
    private void Jump()
    {

    }
    private void TurnAround()
    {
        transform.parent.localScale = new Vector2(-transform.parent.localScale.x, 1);
    }
}
