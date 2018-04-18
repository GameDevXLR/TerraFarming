using UnityEngine;

public class JumpPlayerState : StateMachineBehaviour {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        InGameManager.instance.playerController.behaviour.Jump();
    }

}
