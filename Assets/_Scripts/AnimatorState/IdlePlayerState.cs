using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : StateMachineBehaviour
{

    
    Vector3 moveDirection = Vector3.zero;

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
            moveDirection = calculateMoveDirection();
            if (moveDirection != Vector3.zero)
            {
                //controller.transform.rotation = Quaternion.Slerp(controller.transform.rotation, Quaternion.LookRotation(moveDirection), 0.15F);
                controller.rotation(moveDirection);
            }
        }
        else if (!Cc.isGrounded)
        {

            controller.transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * controller.speed * 10);
        }

        moveDirection.y -= controller.gravity * Time.deltaTime;


        Cc.Move(moveDirection * Time.deltaTime);
    }

    public Vector3 calculateMoveDirection()
    {
        Vector3 vectDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        vectDirection.Normalize();
        vectDirection *= controller.speed;

        if (Input.GetButtonDown("Jump"))
        {
            vectDirection.y = controller.jumpSpeed;
        }

        return vectDirection;
    }
}
