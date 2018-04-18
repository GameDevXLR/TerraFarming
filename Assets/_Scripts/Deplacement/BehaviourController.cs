using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour {
    
    public Vector3 moveDirection = Vector3.zero;

    public float speed = 6f;
    public float gravity = 20f;
    public float speedRotate;
    public float jumpSpeed = 8f;

    [SerializeField]
    bool isFlying;
    float referenceYFly;

    public bool canMove = true;

    public CharacterController Cc;

    private void Start()
    {
        if (!CustomInputManager.instance)
        {
            Debug.Log("You don't have a CustomInputManager");
        }
        if(Cc == null)
        {
            Debug.Log("Where is your CharactereController?");
        }
        
    }


    private void Update()
    {
        float y = moveDirection.y;
        if(canMove)
            moveDirection = CalculateMoveDirection();
        else
        {
            moveDirection = Vector3.zero;
        }
        moveDirection.y = y;
        if (isFlying)
        {
            if(transform.position.y <= referenceYFly)
                Jump();
            Gravity(gravity/2);
        }
        else
        {
            
            Gravity();
        }

        

        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            rotation(moveDirection);
        }
        Cc.Move(moveDirection * Time.deltaTime);
    }


    /// <summary>
    /// Calcule le vecteur directionnel
    /// </summary>
    /// <returns></returns>
    public Vector3 CalculateMoveDirection()
    {
        Vector3 vectDirection = CustomInputManager.instance.getDirection();

        return vectDirection.normalized * speed;
    }


    /// <summary>
    /// exerce la gravité sur le personnage
    /// </summary>
    public virtual void Gravity()
    {
        if (Cc.velocity.y >= 0)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y -= gravity * 2 * Time.deltaTime;
        }
    }

    public virtual void Gravity(float gravity)
    {
        if (Cc.velocity.y >= 0)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            moveDirection.y -= gravity * 2 * Time.deltaTime;
        }
    }

    public void rotation(Vector3 direction)
    {
        Vector3 rotation = new Vector3(direction.x, 0, direction.z);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotation.normalized), speedRotate * Time.deltaTime);
    }

    public void Jump()
    {
        moveDirection.y = jumpSpeed;
    }

    public bool IsFlying
    {
        get
        {
            return isFlying;
        }

        set
        {
            isFlying = value;
            referenceYFly = transform.position.y;
        }
    }
}
