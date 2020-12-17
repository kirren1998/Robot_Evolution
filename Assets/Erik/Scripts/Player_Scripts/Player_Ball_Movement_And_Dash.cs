using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ball_Movement_And_Dash : MonoBehaviour
{
    [Range(1000, 10000)] public int stoppingPower;
    [Range(500, 1000)] public int dashSlow;
    [Range(0, 10000)] public int MaximumVelocity;
    [Range(0, 100)] public float rotationSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PhysicsMaterial2D[] mat;
    [SerializeField] float chargeTimer, chargeTime;

    public int Damage = 0;
    public bool IsCharging, hasMech, inVent, timeStop, chipUpgrade;

    LayerMask groundLayer = 1 << 9;

    void Update()
    {
        if (timeStop)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        if (hasMech || inVent) return;
        Debug.DrawRay(transform.position, new Vector2(0, -0.15f));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f, groundLayer);
        if (hit)
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.angularVelocity) < 400 || Input.GetMouseButtonDown(0) && Mathf.Abs(rb.angularVelocity) < 400)
                { Damage = 0; chargeTimer = 0; IsCharging = true; StartCoroutine(AutoStopCharge()); }
        if (Input.GetMouseButtonUp(0) && IsCharging || Input.GetKeyUp(KeyCode.Space) && IsCharging) 
        { 
            IsCharging = false; 
            StopAllCoroutines();
            rb.velocity = new Vector2(-rb.angularVelocity * Damage / dashSlow, 0);
            if (!chipUpgrade) Damage = 0;
        }
    }
    private void FixedUpdate()
    {
        if (timeStop || hasMech || inVent) return;
        if (!IsCharging)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (rb.angularVelocity < MaximumVelocity && rb.angularVelocity > -MaximumVelocity)
                    rb.angularVelocity -= Input.GetAxis("Horizontal") * rotationSpeed; 
                if (Input.GetAxis("Horizontal") < 0 && rb.angularVelocity < 0)
                {
                    if (rb.angularVelocity < 2000)
                        rb.angularVelocity += Time.deltaTime * stoppingPower * 2;
                    else rb.angularVelocity += Time.deltaTime * stoppingPower;
                }
                if (Input.GetAxis("Horizontal") > 0 && rb.angularVelocity > 0)
                {
                    if (rb.angularVelocity > 2000)
                        rb.angularVelocity -= Time.deltaTime * stoppingPower * 2;
                    else rb.angularVelocity -= Time.deltaTime * stoppingPower;
                }
            }
            else
            {
                if (Mathf.Abs(rb.velocity.x) > 0.05f)
                    rb.velocity += new Vector2(-rb.velocity.normalized.x / 60, 0);
                else rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else
        {
            if (rb.angularVelocity < MaximumVelocity * 2 && rb.angularVelocity > -MaximumVelocity * 2)
                rb.angularVelocity -= Input.GetAxis("Horizontal") * rotationSpeed * 2;
        }
        if (IsCharging) { Charge(); GetComponent<CircleCollider2D>().sharedMaterial = mat[1]; } else GetComponent<CircleCollider2D>().sharedMaterial = mat[0];
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
        if (!chipUpgrade) Damage = 0;
    }
    public void getInMech(GameObject mech)
    {
        transform.parent = mech.transform;
    }
}
