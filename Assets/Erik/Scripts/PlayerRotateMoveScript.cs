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
                if (rb.angularVelocity < 0)
                    rb.angularVelocity += rotationSpeed * 10;
                else if (rb.angularVelocity > 0)
                    rb.angularVelocity -= rotationSpeed * 10;

            }
        }
        if (IsCharging) { Charge(); GetComponent<Rigidbody2D>().sharedMaterial = mat[1]; } else GetComponent<Rigidbody2D>().sharedMaterial = mat[0];
    }
    private void Charge()
    {
        rb.velocity = Vector2.zero;
    }
    private IEnumerator AutoStopCharge()
    {
        yield return new WaitForSecondsRealtime(1);
        IsCharging = false;
    }
}
