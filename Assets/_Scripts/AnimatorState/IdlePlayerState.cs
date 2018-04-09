using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : StateMachineBehaviour
{

#region variables
    protected Vector3 moveDirection = Vector3.zero;

    protected PlayerController controller;
    protected CharacterController Cc;

    #endregion

#region Unity methods

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
        controller = InGameManager.instance.playerController;
        Cc = controller.Cc;
        moveDirection = controller.moveDirection;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

        Cc.Move(moveDirection *  Time.deltaTime);
        if (controller.isGrounded)
        {
            ActionIsGrounded();
        }
        else
        {
            ActionIsNotGrounded();
        }
        
        controller.moveDirection = moveDirection;
        SwitchAnime();
        
    }

    #endregion

#region GroundFunction

    public virtual void ActionIsGrounded()
    {
        moveDirection = CalculateMoveDirection();
        Jump();
        Gravity();
    }

    public virtual void ActionIsNotGrounded()
    {
        

        if (controller.gameObject.transform.position.y <= -1)
        {
            SwitchAnimeFlying(true);
        }
        else
        {
            Gravity();
            if (Jump())
            {
                SwitchAnimeFlying(true);
            }
        }
    }

    #endregion

#region direction methods

    public bool Jump()
    {
        if (Input.GetKeyDown(CustomInputManager.instance.jumpKey))
        {
            moveDirection.y = controller.jumpSpeed;
            return true;
        }
        return false;
    }
    


    /// <summary>
    /// Calcule le vecteur directionnel
    /// </summary>
    /// <returns></returns>
    public Vector3 CalculateMoveDirection()
    {
        Vector3 vectDirection = CustomInputManager.instance.getDirection();

        return  vectDirection.normalized * controller.speed ;
    }

    /// <summary>
    /// exerce la gravité sur le personnage
    /// </summary>
    public virtual void Gravity()
    {
        if(Cc.velocity.y >= 0)
        {
            moveDirection.y -= controller.gravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y -= controller.gravity * 2 * Time.deltaTime;
        }
    }

    #endregion

#region animation

    public virtual void SwitchAnime()
    {
        if (moveDirection.x != 0 || moveDirection.z != 0)
            controller.anim.SetBool("iswalking", true);
    }

    public virtual void SwitchAnimeFlying(bool condition)
    {
        controller.anim.SetBool("isflying", condition);
    }

#endregion

}
