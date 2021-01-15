using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFade : MonoBehaviour
{

    [SerializeField] float fadeSpeed = 0.1f;
    Light2D global;
    public bool fadingPlay = false;
    public bool fadingQuit = false;

    private void Start()
    {
        global = GetComponent<Light2D>();
    }

    public void Update()
    {
        if (fadingPlay)
        {
            global.intensity -= Time.deltaTime * fadeSpeed;
            if(global.intensity <= 0)
            {
                fadingPlay = false;
                GameObject.Find("SceneManager").GetComponent<MainMenuButtons>().LoadNextScene();
            }
        }

        if (fadingQuit)
        {
            global.intensity -= Time.deltaTime * fadeSpeed;
            if (global.intensity <= 0)
            {
                fadingQuit = false;
                GameObject.Find("SceneManager").GetComponent<MainMenuButtons>().AppQuit();
            }
        }

    }

}
