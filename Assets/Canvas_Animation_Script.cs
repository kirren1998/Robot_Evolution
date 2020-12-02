using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Animation_Script : MonoBehaviour
{
    public void GameOver()
    {
        Debug.Log("Shit");
        transform.GetChild(1).GetComponent<Text>().CrossFadeAlpha(1, 2, true);
        Debug.Log("Shit");
    }
}
