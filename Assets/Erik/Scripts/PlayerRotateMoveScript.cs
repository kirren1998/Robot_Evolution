using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateMoveScript : MonoBehaviour
{
    [Range(0, 10000)] [SerializeField] int MAXIMUMVELOCITY;
    [Range(0, 10)][SerializeField] float rotationSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PhysicsMaterial2D[] mat;
    bool IsCharging;
    float chargeTime;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { IsCharging = true; StartCoroutine(AutoStopCharge()); }
        else if (Input.GetMouseButtonUp(0)) IsCharging = false;
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
                else rb.angularVelocity = 0;

            }
        }
        else
        {
            if (rb.angularVelocity < MAXIMUMVELOCITY * 2 && rb.angularVelocity > -MAXIMUMVELOCITY * 2)
                rb.angularVelocity -= Input.GetAxis("Horizontal") * rotationSpeed * 2;
        }
        if (IsCharging) { Charge(); GetComponent<Rigidbody2D>().sharedMaterial = mat[1]; } else GetComponent<Rigidbody2D>().sharedMaterial = mat[0];
    }
    private void Charge()
    {
        rb.velocity = Vector2.zero;
        chargeTime += Time.fixedDeltaTime;
        if (chargeTime > 1)
            chargeTime -= 1;
    }
    private IEnumerator AutoStopCharge()
    {
        yield return new WaitForSecondsRealtime(3);/*
        rb.AddForce(new Vector2(chargeTime * 10, 0));*/
        IsCharging = false;
    }
}
