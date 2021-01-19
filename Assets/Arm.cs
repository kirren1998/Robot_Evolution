using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [Range (0, 1)][SerializeField] float butt;
    [SerializeField] Animator arm;
    Player_Ball_Movement_And_Dash player;

    private void Start()
    {
        player = GetComponentInParent<Player_Ball_Movement_And_Dash>();
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
        if (Input.GetKeyDown(KeyCode.Space)) arm.SetBool("Jump", true);
        if (Input.GetKeyUp(KeyCode.Space)) arm.SetBool("Jump", false);
    }
}
