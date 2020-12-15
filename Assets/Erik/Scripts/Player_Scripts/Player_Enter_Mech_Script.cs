using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Enter_Mech_Script : MonoBehaviour
{
    #region Variables
    public GameObject localMech;
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
        if (GetComponent<Player_Ball_Movement_And_Dash>().hasMech)
        {
            transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, 0);
        }
    }
    private void SuccToMech()
    {
        if (Mathf.Approximately(rb.rotation, 0)) rb.angularVelocity = 0;
        timer += Time.deltaTime;
        if (timer < 1f)
            rb.velocity = new Vector2((localMech.transform.position.x - transform.position.x) * 10, (localMech.transform.position.y - transform.position.y) * 10);
        else
        {
            PRMC.hasMech = true;
            PRMC.getInMech(localMech);
            localMech.GetComponentInParent<Mech_Movement_1>().isPiloted = true;
            localMech.GetComponentInParent<Mech_Movement_1>().player = gameObject;
            localMech.transform.parent.GetChild(2).GetComponent<BoxCollider2D>().enabled = true;
            rb.gravityScale = 0;
            rb.angularVelocity = 0;
            rb.rotation = 0;
            rb.velocity = Vector2.zero;
            rb.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            /*transform.rotation = Quaternion.Euler(0,0,0);
            transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);*/ // Might fix with later, now its not important!
            timer = 0;
        }
    }
    #region OnTrigger functions
    private void OnTriggerStay2D(Collider2D mech)
    {
        if (mech.CompareTag("Mech") && !PRMC.hasMech)
        {
            if (Input.GetKeyDown(KeyCode.Q)) timer = 0;
            if (Input.GetKey(KeyCode.Q))
            {
                if (localMech == null)
                    localMech = mech.gameObject;
                SuccToMech();
            }
            if (Input.GetKeyUp(KeyCode.Q)) localMech = null;
        }
    }
    private void OnTriggerExit2D(Collider2D mech)
    {
        //localMech = null;
    }
    #endregion
}
