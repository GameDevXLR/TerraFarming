using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlayerState : IdlePlayerState {

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
        Jump();

        if (controller.transform.position.y >= -0.5)
            Gravity();

        moveDirection.y *= 2;


    }
}
