using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public bool isActive = true;
    public float speedRotate;

    public Vector3 moveDirection = Vector3.zero;

    public CharacterController Cc;
    public Animator anim;


    private void Start()
    {
        Cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        if(moveDirection != Vector3.zero)
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
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation), speedRotate * Time.deltaTime);
    }


}
