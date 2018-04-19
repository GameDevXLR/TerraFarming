using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

#region variables editor
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public bool isActive = true;
    public float speedRotate;

    public Vector3 moveDirection = Vector3.zero;

    public CharacterController Cc;
    public bool isGrounded;
    public Animator anim;

    [Header("Fly")]
    public GameObject limiteFlying;
    public bool inFlyingZone = true;

    public BehaviourController behaviour;

    bool isJumping;

    float yRefFalling;

    #endregion


    private void Start()
    {
        Cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        SwitchAnime(AnimeParameters.islanding,false);
    }

    private void Update()
    {
        moveDirection = behaviour.moveDirection;
        if(moveDirection.x != 0 || moveDirection.z !=0)
        {
            SwitchAnime(AnimeParameters.iswalking, true);
        }
        else if (moveDirection.x == 0 && moveDirection.z == 0)
        {
            SwitchAnime(AnimeParameters.iswalking, false);
        }

        if (isGrounded)
        {
            if(Input.GetKeyDown(CustomInputManager.instance.jumpKey))
                SwitchAnime(AnimeParameters.isjumping, true);
            
        }
        else if(!isGrounded)
        {
            if(Cc.velocity.y <= 0)
            {
                if(Cc.velocity.y <= 0.1)
                {
                    SwitchAnime(AnimeParameters.isfalling, true);
                }
                SwitchAnime(AnimeParameters.isjumping, false);
                
                if(transform.position.y < -1)
                {
                    SwitchAnime(AnimeParameters.isflying, true);
                    SwitchAnime(AnimeParameters.isfalling, false);
                }
            }
            if (Input.GetKeyDown(CustomInputManager.instance.jumpKey))
            {
                SwitchAnime(AnimeParameters.isflying, true);
            }
            else if (Input.GetKeyUp(CustomInputManager.instance.jumpKey))
            {
                SwitchAnime(AnimeParameters.isflying, false);
            }
        }
    }

    private void OnDisable()
    {
        anim.SetBool("iswalking", false);
    }

    public void rotation(Vector3 direction)
    {
        Vector3 rotation = new Vector3(direction.x, 0, direction.z);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation.normalized), speedRotate * Time.deltaTime);
    }

    public bool InFlyingZone
    {
        get
        {
            return inFlyingZone;
        }

        set
        {
            inFlyingZone = value;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }

        set
        {
            isGrounded = value;

            disableMovement();
            enableMovement();

            SwitchAnime(AnimeParameters.islanding, IsGrounded);
            SwitchAnime(AnimeParameters.isfalling, !IsGrounded);

        }
    }

    public virtual void SwitchAnime(AnimeParameters anime, bool activate)
    {
        anim.SetBool(anime.ToString(), activate);
    }


    public void disableMovement()
    {
        behaviour.enabled = false;
        //behaviourNotGrounded.enabled = false;
    }

    public void enableMovement()
    {
        if (isGrounded)
        {
            behaviour.enabled = true;
        }
        else
        {
            behaviour.enabled = true;
        }
    }

}
