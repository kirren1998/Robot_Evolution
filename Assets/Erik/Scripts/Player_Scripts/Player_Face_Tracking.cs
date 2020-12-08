using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Face_Tracking : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
        player = transform.parent.gameObject;
    }

    void Update()
    {
        if (player == null) return;
        transform.position = new Vector2(player.transform.position.x + Input.GetAxis("Horizontal") / 10, player.transform.position.y);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
