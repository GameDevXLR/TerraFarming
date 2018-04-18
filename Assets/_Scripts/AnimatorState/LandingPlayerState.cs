using UnityEngine;

public class LandingPlayerState : StateMachineBehaviour {

    PlayerController controller;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller = InGameManager.instance.playerController;
        controller.behaviour.canMove = false;
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        controller.SwitchAnime(AnimeParameters.islanding,false);
        controller.behaviour.canMove = true;
    }
}
