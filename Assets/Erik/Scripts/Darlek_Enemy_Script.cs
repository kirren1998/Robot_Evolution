using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Darlek_Enemy_Script : MonoBehaviour
{
    GameObject player;
    Light2D torch;
    BoxCollider2D attack;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        torch = transform.GetChild(0).GetChild(0).GetComponent<Light2D>();
        attack = transform.GetChild(1).GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponentInChildren<Enemy_AI_Patrolling_Script>().seen)
        {
            Vector3 dir = torch.transform.position - player.transform.position;
            Quaternion shit = Quaternion.LookRotation(dir);
            torch.transform.rotation = Quaternion.Euler(0, 0, (shit.eulerAngles.x - 90) * -transform.localScale.x);
            if (torch.pointLightInnerAngle > 15) torch.pointLightInnerAngle -= Time.deltaTime * 100;
            if (torch.pointLightOuterAngle > torch.pointLightInnerAngle + 1) torch.pointLightOuterAngle -= Time.deltaTime * 100;
            torch.color = new Color(1, Mathf.Clamp(torch.color.g - Time.deltaTime, 0, 1), 0);
            attack.enabled = true;
        }
        else
        {
            attack.enabled = false;
            torch.transform.rotation = Quaternion.Euler(0, 0, - 90 * -transform.localScale.x);
            if (torch.pointLightInnerAngle < 25) torch.pointLightInnerAngle += Time.deltaTime * 50;
            if (torch.pointLightOuterAngle < 95) torch.pointLightOuterAngle += Time.deltaTime * 50;
            torch.color = new Color(1, Mathf.Clamp(torch.color.g + Time.deltaTime / 10, 0, 1), 0);
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
