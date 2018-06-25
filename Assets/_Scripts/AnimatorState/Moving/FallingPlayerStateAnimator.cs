using UnityEngine;

public class FallingPlayerStateAnimator : PlayerStateAnimator
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (controller.IsGrounded)
        {
            SwitchAnime(AnimeParameters.islanding, true);
        }
        else
        {
            if (Input.GetKey(CustomInputManager.instance.jumpKey))
            {
                SwitchAnime(AnimeParameters.isjumping, true);
            }
            //else if (controller.transform.position.y <= -1)
            //{
            //    SwitchAnime(AnimeParameters.isflying, true);
            //}
        }
    }
}