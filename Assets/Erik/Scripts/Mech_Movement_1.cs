using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech_Movement_1 : MonoBehaviour
{
    public GameObject player;
    public bool isPiloted = false;
    Rigidbody2D rb;
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
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") / 2, 0));
        Mathf.Clamp(rb.velocity.x, 0, 10);
    }
    private IEnumerator Attack()
    {
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
    }
    private void Exit()
    {
        player.GetComponent<Player_Ball_Movement_And_Dash>().hasMech = false;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;
        player.GetComponent<CircleCollider2D>().enabled = true;
        player.transform.parent = null;
        isPiloted = false;
    }
}
