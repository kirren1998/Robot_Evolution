using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_Script : MonoBehaviour
{
    Canvas checkpointReached;
    bool checkpointTouched;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.isTrigger && !checkpointReached)
        {
            PlayerPrefs.SetFloat("xspawnpos", transform.position.x);
            PlayerPrefs.SetFloat("yspawnpos", transform.position.y);
            //checkpointReached.GetComponent<UpdatePlayer>().DoTheThing();
        }
    }
}
