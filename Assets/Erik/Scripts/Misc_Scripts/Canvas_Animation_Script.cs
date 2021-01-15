using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Animation_Script : MonoBehaviour
{
    [SerializeField] Text gameOver;
    [SerializeField] GameObject restart;
    public void GameOver()
    {
        gameOver.CrossFadeAlpha(255, 10, true);
        StartCoroutine(Emu());
    }
    private IEnumerator Emu()
    {
        yield return new WaitForSecondsRealtime(2);
        restart.GetComponent<Button>().enabled = true;
        restart.GetComponent<Text>().CrossFadeAlpha(255, 2, true);
    }
    public void ReStart()
    {
        Application.Quit();
    }
}
