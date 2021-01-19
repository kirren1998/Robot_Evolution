using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicChanger : MonoBehaviour
{
    [SerializeField] int musicInt;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        collision.GetComponent<Player_Health_script>().PlayMusic(musicInt);
        Destroy(gameObject);
    }
}
