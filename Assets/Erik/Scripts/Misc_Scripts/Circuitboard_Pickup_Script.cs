using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circuitboard_Pickup_Script : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetInt("upgrade") == 1) Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Player_Ball_Movement_And_Dash>().chipUpgrade = true;
        PlayerPrefs.SetInt("upgrade", 1);
        Destroy(gameObject);
    }
}
