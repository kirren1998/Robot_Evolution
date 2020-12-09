using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI_Patrolling_Script : MonoBehaviour
{
    public bool seen;
    float awareness = 0, turnTimer = 0, randomTurnTimer = 3;
    GameObject Player;
    public RaycastHit2D hit;
    LayerMask groundAndWall = 1 << 9 | 1 << 16, findPlayer = 1 << 9 | 1 << 8;
    [Range(0, 10)] [SerializeField] float range, time;
    [Range(15, 100)] [SerializeField] float maxTurnTimer, minTurnTimer;
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
    private void FixedUpdate()
    {
        if (seen) awareness = time; else Mathf.Clamp(awareness -= Time.deltaTime,0, time);
        if (awareness > 0 && Vector2.Distance(transform.position, Player.transform.position) < range)
        {
            hit = Physics2D.Raycast(transform.position, Player.transform.position - transform.position, range, findPlayer);
            Debug.DrawRay(transform.position, (Player.transform.position - transform.position).normalized * range);
            if (hit)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    seen = true;
                    if (Player.transform.position.x - transform.position.x > 0)
                        transform.parent.localScale = new Vector2(-1, 1);
                    else
                        transform.parent.localScale = new Vector2(1, 1);
                }
                else seen = false;
            }
        }
        else
        {
            seen = false;
        }
        Mathf.Clamp(turnTimer -= Time.deltaTime,0, 10);
        if (!seen)
        {
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(-transform.parent.localScale.x / 2, GetComponentInParent<Rigidbody2D>().velocity.y);

            RaycastHit2D wall, ground;
            wall = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 0.5f * -transform.parent.localScale.x, groundAndWall);
            ground = Physics2D.Raycast(new Vector2(transform.position.x + (0.3f * -transform.parent.localScale.x), transform.position.y), 
                Vector2.down, 1 * -transform.parent.localScale.x, groundAndWall);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), new Vector2(0.5f * -transform.parent.localScale.x, 0), Color.cyan);
            Debug.DrawRay(new Vector2(transform.position.x + (0.3f * -transform.parent.localScale.x), transform.position.y), Vector2.down, Color.red);
            if (wall)
            {
                if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.3f), Vector2.right, 0.5f * -transform.parent.localScale.x, groundAndWall))
                    TurnAround();
                else
                    Jump();
            }
            if (!ground && turnTimer <= 0)
                TurnAround();
            randomTurnTimer -= Time.deltaTime;
            if (randomTurnTimer <= 0 && turnTimer <= 0) TurnAround();

        }
        else GetComponentInParent<Rigidbody2D>().velocity = new Vector2(-transform.parent.localScale.x, GetComponentInParent<Rigidbody2D>().velocity.y);
        //The base unaware patrolling;
    }
    private void Jump()
    {
        GetComponentInParent<Rigidbody2D>().velocity += new Vector2(0, 10);
    }
    private void TurnAround()
    {
        turnTimer = 0.3f;
        transform.parent.localScale = new Vector2(-transform.parent.localScale.x, 1);
        randomTurnTimer = Random.Range(minTurnTimer, maxTurnTimer);
    }
}
