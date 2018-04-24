﻿using UnityEngine;

public class JumpPlayerStateAnimator : PlayerStateAnimator {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, animatorStateInfo, layerIndex);
        InGameManager.instance.playerController.behaviour.Jump();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (controller.Cc.velocity.y  <= 0)
        {
            SwitchAnime(AnimeParameters.isjumping, false);
        }
        if ( animatorStateInfo.normalizedTime > 0.1)
        {
            if (Input.GetKeyDown(CustomInputManager.instance.jumpKey))
            {
                InGameManager.instance.playerController.behaviour.Jump();
            }
            if (Input.GetKey(CustomInputManager.instance.jumpKey))
            {
                SwitchAnime(AnimeParameters.isflying, true);
            }
        }
        else if (Input.GetKeyUp(CustomInputManager.instance.jumpKey))
        {
            SwitchAnime(AnimeParameters.isflying, false);
        }


    }

}
