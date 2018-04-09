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
        moveDirection = CalculateMoveDirection();
        moveDirection *= multSpeedFly;
        moveDirection.y = controller.moveDirection.y;

        if (controller.InFlyingZone)
        {
            

            if (!Jump() && Fly() && Cc.velocity.y <= 0)
            {
                moveDirection.y = 0;
            }
            else
            {
                Gravity();
            }
        }
        else
        {
            if(controller.gameObject.transform.position.y > 0)
                Gravity();
            else
            {
                if(Cc.velocity.y < 0)
                    moveDirection.y = 0;
                Jump();
            }
        }
    }


    public bool Fly()
    {
        return Input.GetKey(CustomInputManager.instance.jumpKey);
    }

    public override void SwitchAnime()
    {
        base.SwitchAnime();
        if(moveDirection.x == 0)
            controller.anim.SetBool("iswalking", false);
    }
}
