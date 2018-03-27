using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingPlayerState : IdlePlayerState {

    public override void switchAnime()
    {
        if (moveDirection.x == 0 && moveDirection.z == 0)
            controller.anim.SetBool("iswalking", false);
    }
}
