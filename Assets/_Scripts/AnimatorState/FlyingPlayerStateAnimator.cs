
using UnityEngine;

public class FlyingPlayerStateAnimator : StateMachineBehaviour {

    public int multSpeedFly;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        InGameManager.instance.playerController.behaviour.IsFlying = true;

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        InGameManager.instance.playerController.behaviour.IsFlying = false;
    }

    //public override void ActionIsGrounded()
    //{
    //    controller.anim.SetBool("isflying", false);
    //}
    //public override void ActionIsNotGrounded()
    //{
    //    moveDirection = CalculateMoveDirection();
    //    moveDirection *= multSpeedFly;
    //    moveDirection.y = controller.moveDirection.y;

    //    if (controller.InFlyingZone)
    //    {
            

    //        if (!Jump() && Fly() && Cc.velocity.y <= 0)
    //        {
    //            moveDirection.y = 0;
    //        }
    //        else
    //        {
    //            Gravity();
    //        }
    //    }
    //    else
    //    {
    //        if(controller.gameObject.transform.position.y > 0)
    //            Gravity();
    //        else
    //        {
    //            if(Cc.velocity.y < 0)
    //                moveDirection.y = 0;
    //            Jump();
    //        }
    //    }
    //}


    //public bool Fly()
    //{
    //    return Input.GetKey(CustomInputManager.instance.jumpKey);
    //}

    //public override void SwitchAnime()
    //{
    //    base.SwitchAnime();
    //    if(moveDirection.x == 0)
    //        controller.anim.SetBool("iswalking", false);
    //}


}
