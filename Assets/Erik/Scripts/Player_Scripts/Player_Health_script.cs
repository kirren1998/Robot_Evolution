using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health_script : MonoBehaviour
{
    public GameObject PFT;
    [SerializeField] GameObject Chrapnell;
    public bool canTakeDamage = true;
    int Health = 4;
    public Sprite[] mood;
    public Sprite angery;
    [SerializeField] AudioSource AS;
    public AudioClip[] music;
    public void Start()
    {
        AS = GetComponent<AudioSource>();
        PFT.GetComponent<SpriteRenderer>().sprite = mood[Health - 1];
    }
    public void FixedUpdate()
    {
        if (Health < 1) return;
        if (Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity) > 2000)
            PFT.GetComponent<SpriteRenderer>().sprite = angery;
        else 
            PFT.GetComponent<SpriteRenderer>().sprite = mood[Health-1];
    }
    public void TakeDamage(int Damage)
    {
        if (GetComponent<Player_Ball_Movement_And_Dash>().hasMech == true) return;

        Health -= Damage;
        if (Health <= 0)
        {
            Death();
            return;
        }
        StartCoroutine(Damaged());
    }
    public void Death()
    {
        PlayerPrefs.Save();
        transform.GetChild(0).GetComponent<Player_Camera_Follow_Script>().GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        transform.GetChild(0).GetComponent<Player_Camera_Follow_Script>().enabled = false;
        GameObject.Find("PauseScreen").GetComponent<Pause_Screen_Script>().GameOver();
        transform.GetChild(0).transform.parent = null;
        GameObject men = Instantiate(Chrapnell, transform.position, Quaternion.identity);
        for (int i = 0; i < men.transform.childCount; i++)
            men.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        PFT.GetComponent<Player_Face_Tracking>().enabled = false;
        PFT.GetComponent<Rigidbody2D>().gravityScale = 1;
        PFT.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
        PFT.GetComponent<BoxCollider2D>().enabled = true;
        if (GetComponent<Player_Enter_Mech_Script>().localMech != null)
        {
            GetComponent<Player_Enter_Mech_Script>().localMech.GetComponentInParent<Mech_Movement_1>().isPiloted = false;
            GetComponent<Player_Enter_Mech_Script>().localMech = null;
        }
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
    private IEnumerator Damaged()
    {
        canTakeDamage = false;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSecondsRealtime(1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        canTakeDamage = true;
    }
    public void PlayMusic(int track)
    {
        StopMusic();
        AS.clip = music[track];
        AS.Play();
    }
    public void StopMusic()
    {
        AS.Stop();
    }
}
