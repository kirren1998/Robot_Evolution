using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRotateTheCam : MonoBehaviour
{
    [SerializeField] GameObject playerCharacter;
    [SerializeField] Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       rb.velocity = new Vector3(playerCharacter.transform.position.x - transform.position.x, playerCharacter.transform.position.y - transform.position.y, -10);
    }
}
