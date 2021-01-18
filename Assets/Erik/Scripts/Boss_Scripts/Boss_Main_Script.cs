using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Main_Script : MonoBehaviour
{
    GameObject player, rightArm, leftArm, activeArm;
    //float activationTimer;
    public bool paused;
    public int bossHealth = 10, bossStatus = 1, gettingTired;


    void Start()
    {
        player = GameObject.Find("Player");
        rightArm = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        leftArm = transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject;
    }

    void FixedUpdate()
    {
        /*activationTimer -= Time.deltaTime;

        if (activationTimer <= 0)
            BustAMoveCraig();*/
        if (paused) return;
        if (player.transform.position.x > transform.position.x)
        {
            if (rightArm.GetComponent<Boss_Arm_Follow_Player_Script>().attackPlayer == false)
            {
                leftArm.GetComponent<Boss_Arm_Follow_Player_Script>().attackPlayer = false;
                leftArm.GetComponent<Boss_Arm_Follow_Player_Script>().DoneWaiting();
                rightArm.GetComponent<Boss_Arm_Follow_Player_Script>().attackPlayer = true;
                activeArm = rightArm;
            }
        }
        else
        {
            if (leftArm.GetComponent<Boss_Arm_Follow_Player_Script>().attackPlayer == false)
            {
                rightArm.GetComponent<Boss_Arm_Follow_Player_Script>().attackPlayer = false;
                rightArm.GetComponent<Boss_Arm_Follow_Player_Script>().DoneWaiting();
                leftArm.GetComponent<Boss_Arm_Follow_Player_Script>().attackPlayer = true;
                activeArm = leftArm;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        bossHealth -= damage;
        Debug.Log(bossHealth);
        if (bossHealth < 1)
        {
            player.GetComponentInChildren<Player_Camera_Follow_Script>().followPlayer = false;
            Destroy(gameObject);
        }
        else if (bossHealth < 3)
        {
            if (bossStatus != 4)
            {
                bossStatus = 4;
                StatusChange();
            }
        }
        else if (bossHealth < 6)
        {
            if (bossStatus != 3)
            {
                bossStatus = 3;
                StatusChange();
            }
        }
        else if (bossHealth < 9)
        {
            if (bossStatus != 2)
            {
                bossStatus = 2;
                StatusChange();
            }
        } 
    }
    public void BustAMoveCraig(GameObject blade)
    {
        gettingTired++;
        if (gettingTired >= bossStatus + 1)
        {
            gettingTired = 0;
            StartCoroutine(waitingForDamage(blade));
        }
        else activeArm.GetComponent<Boss_Arm_Follow_Player_Script>().DoneWaiting();
    }
    IEnumerator waitingForDamage(GameObject blade)
    {
        paused = true;
        GetComponent<BoxCollider2D>().enabled = true;
        blade.GetComponent<BoxCollider2D>().enabled = false;
        blade.GetComponent<Boss_Arm_Follow_Player_Script>().platform.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        activeArm.GetComponent<Boss_Arm_Follow_Player_Script>().DoneWaiting();
    }
    private void StatusChange()
    {
        //might do some animation if i have time;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.isTrigger)
        {
            if (Mathf.Abs(collision.attachedRigidbody.angularVelocity) > 2000)
            {
                collision.attachedRigidbody.velocity = Vector2.zero;
                TakeDamage(collision.GetComponent<Player_Ball_Movement_And_Dash>().Damage);
                StopAllCoroutines();
                activeArm.GetComponent<Boss_Arm_Follow_Player_Script>().DoneWaiting();
                GetComponent<BoxCollider2D>().enabled = false;
                paused = false;
            }
        }
    }
}
