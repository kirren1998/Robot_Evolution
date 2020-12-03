﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ball_Movement_And_Dash : MonoBehaviour
{
    [Range(500, 1000)] [SerializeField] int dashSlow;
    [Range(0, 10000)] [SerializeField] int MaximumVelocity;
    [Range(0, 10)][SerializeField] float rotationSpeed;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] PhysicsMaterial2D[] mat;
    [SerializeField] float chargeTime;

    public int Damage = 0;
    public bool IsCharging, hasMech, inVent;

    LayerMask layer = 1 << 9;

    void Update()
    {
        if (hasMech || inVent) return;
        Debug.DrawRay(transform.position, new Vector2(0, -0.15f));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f, layer);
        if (hit)
            if (Input.GetMouseButtonDown(0)) { Damage = 0; chargeTime = 0; IsCharging = true; StartCoroutine(AutoStopCharge()); }
        if (Input.GetMouseButtonUp(0) && IsCharging) { IsCharging = false; StopAllCoroutines(); rb.velocity = new Vector2(-rb.angularVelocity * Damage / dashSlow, 0);
        }
        if (!IsCharging)
        {
            if (rb.angularVelocity < MaximumVelocity && rb.angularVelocity > -MaximumVelocity)
                rb.angularVelocity -= Input.GetAxis("Horizontal") * rotationSpeed;
            /*if (Input.GetKey(KeyCode.Space))
            {
                if (rb.angularVelocity < 20)
                    rb.angularVelocity += rotationSpeed;
                else if (rb.angularVelocity < -20)
                    rb.angularVelocity -= rotationSpeed;
                else { Debug.Log("shit"); rb.angularVelocity = 0; rb.velocity = Vector2.zero; }

            }*/ // Skipping this for now, might implement later
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
        chargeTime += Time.deltaTime;
        if (chargeTime > 0.3f)
        {
            chargeTime = 0;
            Damage++;
            //Activate the particle effect and add damage to the spin
        }
    }
    private IEnumerator AutoStopCharge()
    {
        yield return new WaitForSecondsRealtime(0.9f);/*
        rb.AddForce(new Vector2(chargeTime * 10, 0));*/
        IsCharging = false;
        rb.velocity = new Vector2(-rb.angularVelocity * Damage / dashSlow, 0 );
    }
    public void getInMech(GameObject mech)
    {
        transform.parent = mech.transform;
    }
}
