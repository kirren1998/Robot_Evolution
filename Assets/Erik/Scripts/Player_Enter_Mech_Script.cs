using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Enter_Mech_Script : MonoBehaviour
{
    #region Variables
    GameObject localMech;
    Player_Ball_Movement_And_Dash PRMC;
    Rigidbody2D rb;
    [SerializeField]float timer = 0;
#endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PRMC = GetComponent<Player_Ball_Movement_And_Dash>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(1)) timer = 0;
        if (GetComponent<Player_Ball_Movement_And_Dash>().hasMech)
        {
            transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, 0);
        }
    }
    private void SuckToMech()
    {
        if (rb.rotation == 0) rb.angularVelocity = 0;
        timer += Time.deltaTime;
        if (timer < 1f)
            rb.velocity = new Vector2((localMech.transform.position.x - transform.position.x) * 10, (localMech.transform.position.y - transform.position.y) * 10);
        else
        {
            PRMC.hasMech = true;
            PRMC.getInMech(localMech);
            localMech.GetComponentInParent<Mech_Movement_1>().isPiloted = true;
            localMech.GetComponentInParent<Mech_Movement_1>().player = gameObject;
            rb.gravityScale = 0;
            rb.angularVelocity = 0;
            rb.rotation = 0;
            rb.velocity = Vector2.zero;
            rb.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            /*transform.rotation = Quaternion.Euler(0,0,0);
            transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);*/ // Might fix with later, now its not important!
        }
    }
    #region OnTrigger functions
    private void OnTriggerStay2D(Collider2D mech)
    {
        if (mech.CompareTag("Mech") && !PRMC.hasMech)
        {
            if (localMech == null)
                localMech = mech.gameObject;
            if (Input.GetMouseButton(1))
                SuckToMech();
        }
    }
    private void OnTriggerExit2D(Collider2D mech)
    {
        localMech = null;
    }
    #endregion
}
