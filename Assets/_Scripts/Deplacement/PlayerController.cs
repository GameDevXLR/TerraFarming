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
    public bool isGrounded = true;
    public Animator anim;

    [Header("Fly")]
    public GameObject limiteFlying;
    public bool inFlyingZone = true;

    

    #endregion



    private void Start()
    {
        Cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if(moveDirection.x != 0 || moveDirection.z !=0)
        {
            rotation(moveDirection);
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


}
