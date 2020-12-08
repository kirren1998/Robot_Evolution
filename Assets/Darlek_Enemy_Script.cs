using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Darlek_Enemy_Script : MonoBehaviour
{
    GameObject player;
    Light2D torch;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        torch = transform.GetChild(0).GetChild(0).GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInChildren<Enemy_AI_Patrolling_Script>().seen)
        {
            if (torch == null)
            {
                Quaternion nee = Quaternion.LookRotation(transform.position - player.transform.position);
                torch.transform.rotation = new Quaternion(0, 0, 0, 0);
                Debug.Log(Quaternion.LookRotation(transform.position - player.transform.position));
                torch.color = Color.red;
            }
            Vector3 dir = torch.transform.position - player.transform.position;
            Quaternion shit = Quaternion.LookRotation(dir);
            torch.transform.rotation = Quaternion.Euler(0, 0, shit.eulerAngles.x - 90);
        }
    }
}
            /*Vector3 playerPos = player.transform.position;
            playerPos.z = 5.23f;

            Vector3 darlekPos = transform.position;
            playerPos.x -= darlekPos.y;
            playerPos.y -= darlekPos.x;

            float angel = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg;
            torch.transform.rotation = Quaternion.Euler(new Vector3 (0, 0, angel - 90));*/
