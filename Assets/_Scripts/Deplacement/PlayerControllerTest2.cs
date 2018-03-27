using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest2 : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    private Vector3 moveDirection = Vector3.zero;
    public bool isPerso;

    CharacterController Cc;



    private void Start()
    {
        Cc = GetComponent<CharacterController>();
    }

    private void Update()
    {

        if (Cc.isGrounded)
        {
            
            if (isPerso)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                transform.TransformVector(moveDirection);
                if (moveDirection != Vector3.zero)
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
            }
            else
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
            }
            moveDirection *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else if(!Cc.isGrounded || !isPerso)
        {

            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * speed * 10);
        }
        moveDirection.y -= gravity * Time.deltaTime;


        Cc.Move(moveDirection * Time.deltaTime);
    }

}
