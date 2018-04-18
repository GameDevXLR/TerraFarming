using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPlayerStateAnimator : IdlePlayerStateAnimator {

    public override void SwitchAnime()
    {
        if (controller.behaviour.moveDirection.x == 0 && controller.behaviour.moveDirection.z == 0)
            SwitchAnime(AnimeParameters.iswalking, false);
    }
}
