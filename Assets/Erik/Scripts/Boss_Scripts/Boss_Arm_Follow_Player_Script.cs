using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Arm_Follow_Player_Script : MonoBehaviour
{
    LayerMask playerLayer = 1 << 8;
    public bool pauseForEffect, goindDown, resetting, attackPlayer;
    Rigidbody2D rb;
    GameObject player, bossMain;
    public GameObject platform;
    Vector3 startingPos;

    private void Start()
    {
        bossMain = transform.parent.parent.parent.parent.gameObject;
        startingPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        rb.angularVelocity = -1000;
        platform = transform.parent.GetChild(1).gameObject;
        platform.SetActive(false);
    }
    private void FixedUpdate()
    {
        
        if (!attackPlayer && !resetting && !pauseForEffect)
        {
            Vector2 dir = startingPos - transform.position;
            rb.velocity = dir.normalized;
            return;
        }
        if (resetting)
        {
            if (pauseForEffect)
            {
                Vector2 dir = startingPos - transform.position;
                rb.velocity = new Vector2(0, dir.normalized.y * bossMain.GetComponent<Boss_Main_Script>().bossStatus);
                pauseForEffect = !pauseForEffect;
            }
            if (transform.position.y > startingPos.y - 0.1f)
            {
                rb.velocity = Vector2.zero;
            }
            if (player.transform.position.y + 1 < transform.position.y && transform.position.y > startingPos.y - 0.1f)
            {
                bossMain.GetComponent<Boss_Main_Script>().paused = false;
                resetting = false;
                GetComponent<CircleCollider2D>().enabled = true;
                platform.SetActive(false);
            }
            return;
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, 3, playerLayer))
        {
            goindDown = true;
        }
        if (!pauseForEffect && goindDown)
        {
            rb.velocity = new Vector2(0, -1 * bossMain.GetComponent<Boss_Main_Script>().bossStatus);
        }
        else if (!pauseForEffect && !goindDown)
        {
            Vector2 dir = player.transform.position - transform.position;
            rb.velocity = new Vector2(dir.normalized.x * 3 * bossMain.GetComponent<Boss_Main_Script>().bossStatus, 0);
        }
    }
    public void DoneWaiting()
    {
        resetting = true;
        goindDown = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            rb.velocity = Vector2.zero;
            goindDown = false;
            pauseForEffect = true;
            bossMain.GetComponent<Boss_Main_Script>().BustAMoveCraig(gameObject);
        }
        else if (collision.tag == "Player" && collision.isTrigger)
        {
            collision.GetComponent<Player_Health_script>().TakeDamage(1);
        }
    }
}
