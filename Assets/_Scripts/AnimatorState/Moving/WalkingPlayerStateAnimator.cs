using UnityEngine;

public class WalkingPlayerStateAnimator : IdlePlayerStateAnimator
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!isPlayerMoving())
        {
            SwitchAnime(AnimeParameters.iswalking, false);
        }

        if (Input.GetKeyDown(CustomInputManager.instance.jumpKey))
        {
            SwitchAnime(AnimeParameters.isjumping, true);
        }

        if (!controller.IsGrounded && controller.Cc.velocity.y <= 0)
        {
            SwitchAnime(AnimeParameters.isfalling, true);
        }
    }
}