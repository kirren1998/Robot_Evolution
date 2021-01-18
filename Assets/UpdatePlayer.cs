using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayer : MonoBehaviour
{
    Text txt;
    void Start()
    {
        txt = transform.GetChild(0).GetComponent<Text>();
        txt.enabled = false;
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void DoTheThing()
    {
        StartCoroutine(yes());

    }

    IEnumerator yes()
    {
        txt.enabled = true;
        yield return new WaitForSecondsRealtime(1);
        txt.enabled = false;
    }

}
