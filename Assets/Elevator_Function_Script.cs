using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Function_Script : MonoBehaviour
{
    Vector2 start, stop, direction;
    [SerializeField] bool atTheBottom = true, transitioning;
    Rigidbody2D rb;
    [Range(0, 4)] [SerializeField] float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        start = transform.position;
        stop = transform.parent.transform.GetChild(0).transform.position;
    }
    private void Update()
    {
        if (transitioning)
        {
            if (atTheBottom)
            {
                direction = stop - new Vector2(transform.position.x, transform.position.y);
                rb.velocity = direction.normalized * speed;
                if (direction.magnitude < 0.05f)
                {
                    transitioning = false;
                    rb.velocity = new Vector2(0, 0);
                    atTheBottom = false;
                }
            }
            else
            {
                direction = start - new Vector2(transform.position.x, transform.position.y);
                rb.velocity = direction.normalized * speed;
                if (direction.magnitude < 0.05f)
                {
                    transitioning = false;
                    rb.velocity = new Vector2(0, 0);
                    atTheBottom = true;
                }

            }
        }
    }
    public void StartElevator(GameObject request)
    {
        if (atTheBottom)
        {
            if (request.GetComponent<Elevator_Button_Script>().canGoUp)
                transitioning = true;
            else
                Debug.Log("This button does not alow for upwards travel");
            //make some wierd sound to signify that it did not work
        }
        else
        {
            if (request.GetComponent<Elevator_Button_Script>().canGoDown)
                transitioning = true;
            else
                Debug.Log("This button does not alow for downwards travel");
        }
    }
}
