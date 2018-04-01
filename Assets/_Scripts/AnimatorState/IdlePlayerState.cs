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

        Cc.Move(moveDirection * Time.deltaTime);
        if (Cc.isGrounded)
        {
            ActionIsGrounded();
        }
        else
        {
            ActionIsNotGrounded();
        }
        controller.moveDirection = moveDirection;
        SwitchAnime();
        
    }



    public virtual void ActionIsGrounded()
    {
        moveDirection = CalculateMoveDirection();
        Jump();
    }

    public virtual void ActionIsNotGrounded()
    {
        

        if (controller.gameObject.transform.position.y <= -1)
            controller.anim.SetBool("isflying", true);
        else
        {
            Gravity();
        }
    }

    public void Jump()
    {
        if (Input.GetKey(CustomInputManager.instance.jumpKey))
        {
            moveDirection.y = controller.jumpSpeed;
        }
    }

    public Vector3 CalculateMoveDirection()
    {
        Vector3 vectDirection = CustomInputManager.instance.getDirection();

        return  vectDirection * controller.speed;
    }

    public virtual void SwitchAnime()
    {
        if (moveDirection.x != 0 || moveDirection.z != 0)
            controller.anim.SetBool("iswalking", true);
    }

    public virtual void Gravity()
    {
        moveDirection.y -= controller.gravity * Time.deltaTime;
    }
}
