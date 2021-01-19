using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMyinnerBeast : MonoBehaviour
{
    [SerializeField] GameObject[] Target;
    private void Start()
    {
        foreach (GameObject ass in Target)
            ass.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (GameObject ass in Target)
            ass.SetActive(true);
        Destroy(gameObject);
    }
}
