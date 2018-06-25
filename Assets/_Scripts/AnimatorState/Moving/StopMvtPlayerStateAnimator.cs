using UnityEngine;

public class StopMvtPlayerStateAnimator : PlayerStateAnimator
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        controller.disableMovement();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller.enableMovement();
    }
}