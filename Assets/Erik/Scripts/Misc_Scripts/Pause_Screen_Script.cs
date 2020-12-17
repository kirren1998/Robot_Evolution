using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Screen_Script : MonoBehaviour
{
    #region Variables
    int stage = 0;
    bool isPaused;
    [SerializeField] Canvas pauseMenu, settingMenu;
    #endregion
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.enabled = true;
            } 
            else
            {
                Time.timeScale = 1;
                pauseMenu.enabled = false;
                settingMenu.enabled = false;
            }
        }
    }
    public void RestartLevel()
    {
        pauseMenu.enabled = false;
        settingMenu.enabled = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(stage);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Switch()
    {
        settingMenu.enabled = !settingMenu.enabled;
        pauseMenu.enabled = !settingMenu.enabled;
    }
    public void SwtichStage()
    {
        if (stage != 0) stage = 0;
        else stage = 1;
        RestartLevel();
    }
    public void GameOver()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        GetComponentInChildren<Canvas_Animation_Script>().GameOver();
    }
}
