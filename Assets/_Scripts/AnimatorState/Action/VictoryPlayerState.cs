using UnityEngine;

public class VictoryPlayerState : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (CustomInputManager.instance.GetDirection() != Vector3.zero)
        {
            InGameManager.instance.playerController.anim.SetBool("iswalking", true);
        }
    }
}