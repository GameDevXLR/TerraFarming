using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlayerState : IdlePlayerState {
    public override void ActionIsGrounded()
    {
        controller.anim.SetBool("isflying", false);
    }
    public override void ActionIsNotGrounded()
    {
        moveDirection = CalculateMoveDirection();
        Jump();
        Gravity();
    }
}
