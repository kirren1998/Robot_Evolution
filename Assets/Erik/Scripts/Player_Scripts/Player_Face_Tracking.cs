using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Face_Tracking : MonoBehaviour
{
    void Update()
    {
        if (transform.parent == null) return;
        transform.position = new Vector2(transform.parent.position.x + Input.GetAxis("Horizontal") / 10, transform.parent.position.y);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
