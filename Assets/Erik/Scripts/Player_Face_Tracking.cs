using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Face_Tracking : MonoBehaviour
{
    [SerializeField] GameObject player;
    void Start()
    {
    }

    void Update()
    {
        if (player == null) return;
        transform.localPosition = new Vector2(player.transform.position.x + Input.GetAxis("Horizontal") / 10, player.transform.position.y);
    }
}
