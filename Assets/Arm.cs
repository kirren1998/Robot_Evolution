using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [Range (1, 5)][SerializeField] float butt;
    [SerializeField] Animator arm;
    Player_Ball_Movement_And_Dash player;

    private void Start()
    {
        player = GetComponentInParent<Player_Ball_Movement_And_Dash>();
    }
    public void Jumpstart()
    {
        Invoke("Jumpfinish", butt);
    }
    public void Jumpfinish()
    {
        arm.SetBool("Jump", false);
    }
    public void Jump()
    {
        player.Jump();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            arm.SetBool("Jump", true);
        }
    }
}
