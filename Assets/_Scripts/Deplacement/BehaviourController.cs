using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour {

    public float speed = 1;
    public Rigidbody rb;
    public ForceMode forceMode;
    public bool moving;
    public float maxVel = 10.0f;
    Vector3 previousMvt;
    public detectGround Detecteur;

    public int Jump = 800;

    void FixedUpdate()
    {
        float moveVertical = 0;
        float moveHorizontal = 0;
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        moving = false;

        if (Input.GetKey(CustomInputManager.instance.forwardkey))
        {
            moveVertical = speed * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey(CustomInputManager.instance.backwardKey))
        {
            moveVertical = -speed * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey(CustomInputManager.instance.leftKey))
        {
            moveHorizontal = speed * Time.deltaTime;
            moving = true;
        }
        if (Input.GetKey(CustomInputManager.instance.rightKey))
        {
            moveHorizontal = - speed * Time.deltaTime;
            moving = true;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (Detecteur.isGrounded)
        {
            
            //transform.rotation = Quaternion.LookRotation(movement);

            rb.AddForce(movement, forceMode);

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVel);

            if (Input.GetButtonDown("Jump"))
            {

                rb.AddForce(Vector3.up * Jump * Time.deltaTime);
            }
        }
        if (movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        //rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);


    }
}
