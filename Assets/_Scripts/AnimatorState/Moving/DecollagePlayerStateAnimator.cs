using UnityEngine;

public class DecollagePlayerStateAnimator : StateMachineBehaviour {

    PlayerController controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller = InGameManager.instance.playerController;
        controller.disableMovement();
    }

}
