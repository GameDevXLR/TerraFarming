using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPlayerState : StateMachineBehaviour {


    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;

    private Vector3 moveDirection = Vector3.zero;

    PlayerController controller;
    CharacterController Cc;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller = InGameManager.instance.playerController;
        Cc = controller.Cc;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (Cc.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (moveDirection != Vector3.zero)
            {
                controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
            }
            else
            {
                controller.anim.SetBool("iswalking", false);
            }
            moveDirection.Normalize() ;
            moveDirection *= speed;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        else if (!Cc.isGrounded)
        {

            controller.transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * speed * 10);
        }

        moveDirection.y -= gravity * Time.deltaTime;


        Cc.Move(moveDirection * Time.deltaTime);
    }
}
