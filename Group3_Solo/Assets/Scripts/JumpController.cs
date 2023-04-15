using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
    public bool grounded;
    public float groundcheckdistance;
    public float buffercheckdistance = 0.1f;
    public int jumpvalue = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        groundcheckdistance = 1.1f;
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * jumpvalue, ForceMode2D.Impulse);
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, groundcheckdistance);
        if (hit.collider != null)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
}

