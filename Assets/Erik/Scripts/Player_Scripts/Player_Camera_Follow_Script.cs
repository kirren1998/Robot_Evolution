using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera_Follow_Script : MonoBehaviour
{
    #region Variables
    public bool followPlayer = true, startFunction = true;
    GameObject playerCharacter, vent;
    Rigidbody2D rb;
    Camera cam;
    float timer;
#endregion
    // Start is called before the first frame update
    void Start()
    {
        playerCharacter = transform.parent.gameObject;
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            rb.velocity = new Vector3((playerCharacter.transform.position.x - transform.position.x) * 3, (playerCharacter.transform.position.y - transform.position.y) * 3, -10);
            if (cam.orthographicSize > 1.5)
                cam.orthographicSize -= Time.deltaTime;
            return;
        }
        rb.velocity = new Vector3((vent.transform.position.x - transform.position.x) * 2, (vent.transform.position.y - transform.position.y) * 2, -10);
        timer += Time.deltaTime;
        if (cam.orthographicSize < 3)
            cam.orthographicSize += Time.deltaTime;
        if (timer > 1 && startFunction)
        {
            vent.GetComponent<Vent_Node_Wrong_Path_Script>().StartPathChange(false);
            startFunction = false;
        }
    }
    public void WatchVentStructure(GameObject me)
    {
        GameObject törnrosa = me.GetComponent<Lever_Script>().leverConnection;
        if (me.GetComponent<Lever_Script>().noCamChange)
            vent.GetComponent<Vent_Node_Wrong_Path_Script>().StartPathChange(true);
        else
        {
            followPlayer = false;
            vent = törnrosa;
            playerCharacter.GetComponent<Player_Ball_Movement_And_Dash>().timeStop = true;
        }
    }
    public void EndVentWatching()
    {
        followPlayer = true;
        playerCharacter.GetComponent<Player_Ball_Movement_And_Dash>().timeStop = false;
        timer = 0;
        startFunction = true;
    }
}
