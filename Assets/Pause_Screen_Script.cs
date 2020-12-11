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
    Player_Ball_Movement_And_Dash PBM;
    public Slider[] sliddare;
    #endregion
    private void Start()
    {
        PBM = GameObject.Find("Player").GetComponent<Player_Ball_Movement_And_Dash>();
        sliddare[0].value = PBM.dashSlow;
        sliddare[1].value = PBM.MaximumVelocity;
        sliddare[2].value = PBM.stoppingPower;
        sliddare[3].value = PBM.rotationSpeed;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        PBM.dashSlow = Mathf.RoundToInt(sliddare[0].value);
        PBM.MaximumVelocity = Mathf.RoundToInt(sliddare[1].value);
        PBM.stoppingPower = Mathf.RoundToInt(sliddare[2].value);
        PBM.rotationSpeed = sliddare[3].value;
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
        SceneManager.LoadScene(stage);
        pauseMenu.enabled = false;
        settingMenu.enabled = false;
        Time.timeScale = 1;
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
}
