using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IdlePlayerStateAnimator : PlayerStateAnimator
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (isPlayerMoving())
        {
            SwitchAnime(AnimeParameters.iswalking, true);
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
