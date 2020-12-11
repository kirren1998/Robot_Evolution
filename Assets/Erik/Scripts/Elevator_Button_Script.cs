using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Button_Script : MonoBehaviour
{
    public bool canPress;
    public bool canGoUp, canGoDown;
    GameObject elevator;
    // Start is called before the first frame update
    void Start()
    {
        elevator = transform.parent.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.E))
        {
            elevator.GetComponent<Elevator_Function_Script>().StartElevator(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canPress = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPress = false;
    }
}
