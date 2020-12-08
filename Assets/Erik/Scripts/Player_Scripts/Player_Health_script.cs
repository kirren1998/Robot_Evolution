using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health_script : MonoBehaviour
{
    [SerializeField] GameObject PFT;
    [SerializeField] GameObject Chrapnell;
    int Health = 5;
    public void TakeDamage(int Damage)
    {
        Health -= Damage;
        if (Health <= 0)
            Death();
    }
    public void Death()
    {
        transform.GetChild(0).GetComponent<Player_Camera_Follow_Script>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        transform.GetChild(0).GetComponent<Player_Camera_Follow_Script>().enabled = false;
        if (GameObject.Find("Player_Canvas")) GameObject.Find("Player_Canvas").GetComponent<Canvas_Animation_Script>().GameOver();
        transform.GetChild(0).transform.parent = null;
        GameObject men = Instantiate(Chrapnell, transform.position, Quaternion.identity);
        for (int i = 0; i < men.transform.childCount; i++)
            men.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        PFT.GetComponent<Player_Face_Tracking>().enabled = false;
        PFT.GetComponent<Rigidbody2D>().gravityScale = 1;
        PFT.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        Destroy(gameObject);
    }
}
