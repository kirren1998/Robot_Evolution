using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_Movement_1 : MonoBehaviour
{
    public GameObject player;
    public bool isPiloted = false;
    Rigidbody2D rb;
    LayerMask groundCheck = 1 << 8 | 1 << 9;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        /*if (transform.GetChild(0).GetChild(0))
        {
            isPiloted = true;
        }*/ // if there is a reload state that i need to fix then develop this
    }

    void Update()
    {
        if (!isPiloted) return;
        if (Input.GetMouseButtonDown(0)) StartCoroutine(Attack());
        if (Input.GetKeyDown(KeyCode.LeftAlt)) Exit();
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 1.4f, rb.velocity.y);/*
        if (rb.velocity.x < 0) transform.localScale = new Vector2(-1, 1);
        else transform.localScale = new Vector2(1, 1);*/
    }
    private IEnumerator Attack()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        transform.GetChild(1).gameObject.SetActive(false);
    }
    private void Exit()
    {
        player.GetComponent<Player_Ball_Movement_And_Dash>().hasMech = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        player.GetComponent<CircleCollider2D>().enabled = true;
        player.transform.parent = null;
        isPiloted = false;
    }
    private void Jump()
    {
        //ad an "charge" deleay before jumping like a bending of the knees before jumping;
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.4f, groundCheck))
            rb.velocity = new Vector2(rb.velocity.x, 3);
    }
}
