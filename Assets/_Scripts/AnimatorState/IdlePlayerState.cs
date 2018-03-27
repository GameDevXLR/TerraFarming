using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : StateMachineBehaviour
{
    protected Vector3 moveDirection = Vector3.zero;

    protected PlayerController controller;
    protected CharacterController Cc;

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
        }
        
        moveDirection.y -= controller.gravity * Time.deltaTime;

        switchAnime();

        Cc.Move(moveDirection * Time.deltaTime);
    }

    public Vector3 calculateMoveDirection()
    {
        Vector3 vectDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.moveDirection = vectDirection;
        vectDirection *= controller.speed;
        
        if (Input.GetButtonDown("Jump"))
        {
            vectDirection.y = controller.jumpSpeed;
        }

        return vectDirection;
    }

    public virtual void switchAnime()
    {
        if (moveDirection.x != 0 || moveDirection.z != 0)
            controller.anim.SetBool("iswalking", true);
    }
}
