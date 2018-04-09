using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlayerState : IdlePlayerState {

    public int multSpeedFly;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        controller.limiteFlying.SetActive(true);
    }

    public override void ActionIsGrounded()
    {
        controller.anim.SetBool("isflying", false);
    }
    public override void ActionIsNotGrounded()
    {



        if (controller.InFlyingZone)
        {
            Jump();
        }

        else if (Fly() && Cc.velocity.y <=0)
        {
            moveDirection = CalculateMoveDirection();
            moveDirection *= multSpeedFly;
        }

        else if(controller.transform.position.y >= -0.5)
        {
            moveDirection = CalculateMoveDirection();
            moveDirection *= multSpeedFly;
            moveDirection.y = controller.moveDirection.y;
            Gravity();

        }

        

        
    }


    public bool Fly()
    {
        return Input.GetKey(CustomInputManager.instance.jumpKey);
    }
}
