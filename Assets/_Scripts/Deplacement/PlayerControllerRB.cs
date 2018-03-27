using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerRB : MonoBehaviour {

    public int speed = 1000;
    public int Jump = 800;
    public float maxVel = 5;
    public int speedTurn = 100;

    Rigidbody rb;
    public detectGround detect;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 mouvement = new Vector3(0,0,v);

        
        if (detect.isGrounded)
        {
            rb.AddForce(transform.TransformDirection(mouvement) * speed * Time.deltaTime);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVel);
            if(Input.GetButtonDown("Jump"))
                rb.AddForce(Vector3.up * Jump * Time.deltaTime);

        }
        transform.Rotate(Vector3.up * h * speedTurn * Time.deltaTime);
        

    }
}
