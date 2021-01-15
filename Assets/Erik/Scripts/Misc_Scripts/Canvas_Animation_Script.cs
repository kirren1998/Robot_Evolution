using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Animation_Script : MonoBehaviour
{
    [SerializeField] Text gameOver;
    [SerializeField] GameObject restart, quit;
    public void GameOver()
    {
        gameOver.CrossFadeAlpha(255, 10, true);
        StartCoroutine(Emu());
    }
    private IEnumerator Emu()
    {
        yield return new WaitForSecondsRealtime(1);
        restart.GetComponent<Text>().CrossFadeAlpha(255, 2, true);
        yield return new WaitForSecondsRealtime(1);
        quit.GetComponent<Text>().CrossFadeAlpha(255, 2, true);
    }
}
