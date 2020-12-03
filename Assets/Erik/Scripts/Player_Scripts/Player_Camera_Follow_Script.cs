using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera_Follow_Script : MonoBehaviour
{
    #region Variables
    bool followPlayer = true;
    GameObject playerCharacter, vent;
    Rigidbody2D rb;
    Camera cam;
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
            return;
        }
        rb.velocity = new Vector3((vent.transform.position.x - transform.position.x) * 2, (vent.transform.position.y - transform.position.y) * 2, -10);
        if (vent.transform.position == transform.position) vent.GetComponent<Vent_Node_Wrong_Path_Script>().StartPathChange();
    }
    public void WatchVentStructure(GameObject me)
    {
        vent = me;
        playerCharacter.GetComponent<Player_Ball_Movement_And_Dash>().timeStop = true;
        followPlayer = false;
    }
    public void EndVentWatching()
    {
        followPlayer = true;
        playerCharacter.GetComponent<Player_Ball_Movement_And_Dash>().timeStop = false;
    }
}
