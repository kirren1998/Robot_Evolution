using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformAntiSpin : MonoBehaviour
{
    GameObject pardner;
    void Start()
    {
        pardner = transform.parent.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pardner.transform.position;
    }
}
