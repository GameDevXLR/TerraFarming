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
    public ParticleSystem aterrisageParticle;

    #endregion


    private void Start()
    {
        Cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        SwitchAnime(AnimeParameters.islanding,false);
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
    

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }

        set
        {
            if(isGrounded != value)
            {
                isGrounded = value;
                
                SwitchAnime(AnimeParameters.islanding, IsGrounded);
                SwitchAnime(AnimeParameters.isfalling, !IsGrounded);

                if (isGrounded)
                {
                    aterrisageParticle.Clear();
                    aterrisageParticle.Play();
                    SetAltitudeMaxFromGroundPos(0);
                }
            }
            

        }
    }

    public virtual void SwitchAnime(AnimeParameters anime, bool activate)
    {
        anim.SetBool(anime.ToString(), activate);
    }


    public void disableMovement()
    {
        behaviour.canMove = false;
        //behaviourNotGrounded.enabled = false;
    }

    public void enableMovement()
    {
        behaviour.canMove = true;
    }

    public void SetAltitudeMaxFromGroundPos(float altitudeGround)
    {
        behaviour.setMaxAltitudeWithRef(altitudeGround);
    }

}
