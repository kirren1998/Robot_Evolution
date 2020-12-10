using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Screen_Script : MonoBehaviour
{
    bool isPaused;
    [SerializeField] Canvas can;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                can.enabled = true;
            } 
            else
            {
                Time.timeScale = 1;
                can.enabled = false;
            }
        }
    }
}
