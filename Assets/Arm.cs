using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [Range (1, 5)][SerializeField] float butt;
    [SerializeField] Animator arm;
    public void Jumpstart()
    {
        Invoke("Jumpfinish", butt);
    }
    public void Jumpfinish()
    {
        arm.SetTrigger("Arm Jump");
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            arm.SetTrigger("Start");
        }
    }
}
