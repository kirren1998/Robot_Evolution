using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever_Script : MonoBehaviour
{
    public bool noCamChange;
    bool inside = false;
    public GameObject leverConnection;
    [SerializeField] Canvas nothin;
    Text textPopup;
    GameObject cam;


    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        textPopup = nothin.transform.GetChild(0).GetComponent<Text>();
        textPopup.transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inside)
        {
            if (!cam.GetComponent<Player_Camera_Follow_Script>().followPlayer) return;
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            /*if (PlayerPrefs.GetInt("DifficultyLevel") >= 2)*/ cam.GetComponent<Player_Camera_Follow_Script>().WatchVentStructure(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {/*
            textPopup.enabled = true;*/
            inside = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {/*
            textPopup.enabled = false;*/
            inside = false;
        }
    }
}
