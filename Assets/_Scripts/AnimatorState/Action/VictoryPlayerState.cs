using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryPlayerState : StateMachineBehaviour {

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(CustomInputManager.instance.getDirection() != Vector3.zero)
        {
            InGameManager.instance.playerController.anim.SetBool("iswalking", true);
        }
    }
}
