using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateMoveScript : MonoBehaviour
{
    [Range(500, 1000)] [SerializeField] int dashSlow;
    [Range(0, 10000)] [SerializeField] int MAXIMUMVELOCITY;
    [Range(0, 10)][SerializeField] float rotationSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PhysicsMaterial2D[] mat;
    bool IsCharging;
    float chargeTime;

    LayerMask layer = 1 << 9;

    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(transform.position, new Vector2(0, -0.15f));
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f, layer);
        if (hit)
        {
            Debug.Log(hit);
            if (Input.GetMouseButtonDown(0)) { IsCharging = true; StartCoroutine(AutoStopCharge()); }
        }
        if (Input.GetMouseButtonUp(0) && IsCharging) { IsCharging = false; StopAllCoroutines(); rb.velocity = new Vector2(-rb.angularVelocity / dashSlow, 0);
        }
        if (!IsCharging)
        {
            if (rb.angularVelocity < MAXIMUMVELOCITY && rb.angularVelocity > -MAXIMUMVELOCITY)
                rb.angularVelocity -= Input.GetAxis("Horizontal") * rotationSpeed;
            if (Input.GetKey(KeyCode.Space))
            {
                if (rb.angularVelocity < 20)
                    rb.angularVelocity += rotationSpeed * 10;
                else if (rb.angularVelocity > -20)
                    rb.angularVelocity -= rotationSpeed * 10;
                else { rb.angularVelocity = 0; rb.velocity = Vector2.zero; }

            }
        }
        else
        {
            if (rb.angularVelocity < MAXIMUMVELOCITY * 2 && rb.angularVelocity > -MAXIMUMVELOCITY * 2)
                rb.angularVelocity -= Input.GetAxis("Horizontal") * rotationSpeed * 2;
        }
        if (IsCharging) { Charge(); GetComponent<CircleCollider2D>().sharedMaterial = mat[1]; } else GetComponent<CircleCollider2D>().sharedMaterial = mat[0];
    }
    private void Charge()
    {
        rb.velocity = Vector2.zero;
        chargeTime += Time.fixedDeltaTime;
        if (chargeTime > 1)
        {
            chargeTime -= 1;
            //Activate the particle effect and add damage to the spin
        }
    }
    private IEnumerator AutoStopCharge()
    {
        yield return new WaitForSecondsRealtime(3);/*
        rb.AddForce(new Vector2(chargeTime * 10, 0));*/
        Debug.Log("Shits done yo");
        IsCharging = false;
        rb.velocity = new Vector2(-rb.angularVelocity / dashSlow, 0 );
    }
}
