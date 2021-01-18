using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_follow_player : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (player.GetComponent<Player_Health_script>().PFT.GetComponent<SpriteRenderer>().flipX) transform.localScale = new Vector2(-1, 1);
        else transform.localScale = new Vector2(1, 1);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
