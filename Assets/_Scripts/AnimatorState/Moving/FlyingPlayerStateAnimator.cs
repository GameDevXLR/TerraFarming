using UnityEngine;

public class FlyingPlayerStateAnimator : PlayerStateAnimator
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        controller.behaviour.IsFlying = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!Input.GetKey(CustomInputManager.instance.jumpKey)
            || (controller.IsGrounded && !Input.GetKey(CustomInputManager.instance.jumpKey)))
        {
            SwitchAnime(AnimeParameters.isflying, false);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller.behaviour.IsFlying = false;
    }
}