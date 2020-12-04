using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ball_Movement_And_Dash : MonoBehaviour
{
    [Range(500, 1000)] [SerializeField] int dashSlow;
    [Range(0, 10000)] [SerializeField] int MaximumVelocity;
    [Range(0, 100)][SerializeField] float rotationSpeed;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] PhysicsMaterial2D[] mat;
    [SerializeField] float chargeTimer, chargeTime;

    public int Damage = 0;
    public bool IsCharging, hasMech, inVent, timeStop;

    LayerMask layer = 1 << 9;

    void Update()
    {
        if (timeStop)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (hasMech || inVent) return;
        Debug.DrawRay(transform.position, new Vector2(0, -0.15f));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f, layer);
        if (hit)
            if (Input.GetMouseButtonDown(0)) { Damage = 0; chargeTimer = 0; IsCharging = true; StartCoroutine(AutoStopCharge()); }
        if (Input.GetMouseButtonUp(0) && IsCharging) { IsCharging = false; StopAllCoroutines(); rb.velocity = new Vector2(-rb.angularVelocity * Damage / dashSlow, 0);
        }
    }
    private void FixedUpdate()
    {
        if (timeStop || hasMech || inVent) return;
        if (!IsCharging)
        {
            if (rb.angularVelocity < MaximumVelocity && rb.angularVelocity > -MaximumVelocity)
                rb.angularVelocity -= Input.GetAxis("Horizontal") * rotationSpeed;
        }
        else
        {
            if (rb.angularVelocity < MaximumVelocity * 2 && rb.angularVelocity > -MaximumVelocity * 2)
                rb.angularVelocity -= Input.GetAxis("Horizontal") * rotationSpeed * 2;
        }
        if (IsCharging) { Charge(); GetComponent<CircleCollider2D>().sharedMaterial = mat[1]; } else GetComponent<CircleCollider2D>().sharedMaterial = mat[0];
        if (Mathf.Abs(rb.angularVelocity) > 2000)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        } else GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void Charge()
    {
        rb.velocity = Vector2.zero;
        chargeTimer += Time.deltaTime;
        if (chargeTimer > chargeTime)
        {
            chargeTimer = 0;
            Damage++;
            //Activate the particle effect
        }
    }
    private IEnumerator AutoStopCharge()
    {
        yield return new WaitForSecondsRealtime(chargeTime * 3);
        IsCharging = false;
        rb.velocity = new Vector2(-rb.angularVelocity * Damage / dashSlow, 0 );
    }
    public void getInMech(GameObject mech)
    {
        transform.parent = mech.transform;
    }
}
