using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Face_Tracking : MonoBehaviour
{
    SpriteRenderer face;

    void Start()
    {
        face = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (transform.parent == null) return;
        transform.position = new Vector2(transform.parent.position.x + Input.GetAxis("Horizontal") / 10, transform.parent.position.y);
        if (Input.GetAxis("Horizontal") < 0) face.flipX = true;
        else if (Input.GetAxis("Horizontal") > 0) face.flipX = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
