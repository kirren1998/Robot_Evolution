using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRotateTheCam : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;
    [SerializeField] Rigidbody2D rb;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(playerCharacter.transform.position.x - transform.position.x, playerCharacter.transform.position.y - transform.position.y, -10);
        if (Mathf.Abs(playerCharacter.GetComponent<Rigidbody2D>().velocity.x) < 10)
        cam.orthographicSize = Mathf.Abs(playerCharacter.transform.position.x - transform.position.x) * Time.deltaTime + 1 ;
    }
}
