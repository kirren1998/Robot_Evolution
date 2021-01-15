using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgteleport : MonoBehaviour
    
{
    GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < cam.transform.position.x -7)
        {
            transform.localPosition = new Vector2(transform.localPosition.x + 15, transform.localPosition.y);
        }
    }
}
