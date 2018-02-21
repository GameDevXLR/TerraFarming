using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour {

    public float speed = 1;
    public Rigidbody rb;

    void FixedUpdate()
    {
        float moveVertical = 0;
        float moveHorizontal = 0;
        //var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        if (Input.GetKey(CustomInputManager.instance.forwardkey))
        {
            moveVertical = speed * Time.deltaTime;
        }
        if (Input.GetKey(CustomInputManager.instance.backwardKey))
        {
            moveVertical = -speed * Time.deltaTime;
        }
        if (Input.GetKey(CustomInputManager.instance.leftKey))
        {
            moveHorizontal = speed * Time.deltaTime;
        }
        if (Input.GetKey(CustomInputManager.instance.rightKey))
        {
            moveHorizontal = - speed * Time.deltaTime;
        }
        //var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        //transform.Rotate(0, x, 0);
        //transform.Translate(x, 0, z);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        //transform.rotation = Quaternion.LookRotation(movement);
        rb.AddForce(movement * speed);
        if(movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        //rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);


    }
}
