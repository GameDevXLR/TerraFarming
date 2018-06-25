using UnityEngine;

public class DecollagePlayerStateAnimator : IdlePlayerStateAnimator
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (controller.canDoAction)
        {
            SwitchAnime(AnimeParameters.isjumping, false);
        }
    }
}