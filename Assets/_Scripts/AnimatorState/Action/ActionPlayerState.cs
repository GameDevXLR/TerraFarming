using UnityEngine;

public class ActionPlayerState : StateMachineBehaviour
{
    public bool isInterruptWhenMove = false;
    public AnimeParameters stateEnd;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (isInterruptWhenMove && CustomInputManager.instance.GetDirection() != Vector3.zero)
        {
            InGameManager.instance.playerController.anim.SetBool(AnimeParameters.iswalking.ToString(), true);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        InGameManager.instance.playerController.anim.SetBool(stateEnd.ToString(), false);
    }
}