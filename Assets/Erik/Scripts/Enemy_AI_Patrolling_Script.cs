using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_Patrolling_Script : MonoBehaviour
{
    bool seen;
    float awareness = 0;
    [SerializeField] GameObject Player;
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
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player")) seen = true;
    }
    private void Update()
    {
        if (seen) awareness = 2; else Mathf.Clamp(awareness -= Time.deltaTime,0, 2);
        if (awareness > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.transform.position - transform.position, Vector2.Distance(transform.position, Player.transform.position), findPlayer);
            if (hit.transform.CompareTag("Player"))
            {
                seen = true;
            }
            else seen = false;
        }
        else;

        RaycastHit2D wall, ground;
        wall = Physics2D.Raycast(new Vector2(transform.position.x + 1, transform.position.y), Vector2.down, 1, groundAndWall);
        ground = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 1), Vector2.left, 1, groundAndWall);
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

    }
}
