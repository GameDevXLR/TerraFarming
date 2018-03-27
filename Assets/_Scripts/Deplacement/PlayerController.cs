using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;

    private Vector3 moveDirection = Vector3.zero;

    CharacterController Cc;
    NavMeshAgent navMeshAgent;


    private void Start()
    {
        Cc = GetComponent<CharacterController>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        //navMeshAgent.updatePosition = false;
    }

    private void Update()
    {

        if (Cc.isGrounded)
    {       moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (moveDirection != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
            moveDirection.Normalize() ;
            moveDirection *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else if (!Cc.isGrounded)
        {

            transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * speed * 10);
        }
        moveDirection.y -= gravity * Time.deltaTime;


        Cc.Move(moveDirection * Time.deltaTime);
    }

}
