using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lever_Script : MonoBehaviour
{
    bool inside;
    public GameObject leverConnection;
    [SerializeField] Canvas nothin;
    Text textPopup;


    private void Start()
    {
        textPopup = nothin.transform.GetChild(0).GetComponent<Text>();
        textPopup.transform.position = new Vector2(transform.position.x, transform.position.y + 0.1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        textPopup.enabled = true;
        inside = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inside)
        {
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            /*if (PlayerPrefs.GetInt("DifficultyLevel") >= 2)*/ GameObject.Find("Main Camera").GetComponent<Player_Camera_Follow_Script>().WatchVentStructure(leverConnection);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        textPopup.enabled = false;
        inside = false;
    }
}
