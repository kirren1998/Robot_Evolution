using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    
    public void PlayButtonPress()
    {
        GameObject.Find("Global light").GetComponent<LightFade>().fadingPlay = true;
        PlayerPrefs.DeleteAll();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);

    }

    public void QuitButtonPress()
    {
        GameObject.Find("Global light").GetComponent<LightFade>().fadingQuit = true;
    }


    public void AppQuit()
    {
    Application.Quit();
    Debug.Log("Game Quit To Desktop");
    }

}
