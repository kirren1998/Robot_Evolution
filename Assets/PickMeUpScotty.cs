using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickMeUpScotty : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        collision.transform.GetChild(5).gameObject.SetActive(true);
        PlayerPrefs.SetString("HasArm", "TRUE");
        Destroy(gameObject);
    }
}
