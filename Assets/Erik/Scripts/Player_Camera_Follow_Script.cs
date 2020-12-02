using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera_Follow_Script : MonoBehaviour
{
    GameObject playerCharacter;
    Rigidbody2D rb;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = transform.parent.gameObject;
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3((playerCharacter.transform.position.x - transform.position.x) * 3, (playerCharacter.transform.position.y - transform.position.y) * 3, -10);
    }
}
