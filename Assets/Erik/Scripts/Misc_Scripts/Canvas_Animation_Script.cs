using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_Animation_Script : MonoBehaviour
{
    [SerializeField] Text gameOver;
    [SerializeField] GameObject restart, restartFromCheckpoint, quit;
    public void GameOver()
    {
        gameOver.CrossFadeAlpha(255, 1, true);
        StartCoroutine(Emu());
    }
    private IEnumerator Emu()
    {
        yield return new WaitForSecondsRealtime(1);
        restartFromCheckpoint.SetActive(true);
        restartFromCheckpoint.GetComponent<Text>().CrossFadeAlpha(255, 2, true);
        yield return new WaitForSecondsRealtime(1);
        restart.SetActive(true);
        restart.GetComponent<Text>().CrossFadeAlpha(255, 2, true);
        yield return new WaitForSecondsRealtime(0.6f);
        quit.SetActive(true);
        quit.GetComponent<Text>().CrossFadeAlpha(255, 2, true);

    }
    public void RestartGame()
    {
    }
    public void ResetToCheckpoint()
    {
        PlayerPrefs.SetInt("restart", 1);
        SceneManager.LoadScene(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
