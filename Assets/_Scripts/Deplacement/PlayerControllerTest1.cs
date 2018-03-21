using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest1 : MonoBehaviour
{
    [Header("Player Options")]
    public float playerHeight;

    [Header("Movement Options")]
    public float movementSpeed;
    public bool smooth;
    public float smoothSpeed;

    [Header("Jump Options")]
    public float jumpForce;
    public float jumpSpeed;
    public float jumpDecrease;

    [Header("Gravity")]
    public float gravity = 2.5f;

    [Header("Physics")]
    public LayerMask discludePlayer;

    [Header("References")]
    public SphereCollider collideSphere;

    private Vector3 velocity;
    private Vector3 move;
    private bool grounded;
    private float currentGravity = 0;
    private Vector3 liftpoint = new Vector3(0,1.2f,0);
    private RaycastHit groundHit;
    private Vector3 groundCHeckPoint = new Vector3(0,-0.87f,0);
    private Vector3 vel;
    private float jumpHeight = 0;
    private bool inputJump;


    


    private void Update()
    {
        Gravity();
        SimpleMove();
        Jump();
        FinalMove();
        GroundChecking();
        CollisionCheck();
    }

    public void SimpleMove()
    {
        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        velocity += move;
    }

    public void FinalMove()
    {
        Vector3 vel = new Vector3(velocity.x, velocity.y, velocity.z) * movementSpeed;
        //velocity = (new Vector3(move.x, -currentGravity+jumpHeight, move.z) + vel) * movementSpeed;
        //velocity = transform.TransformDirection(velocity);
        vel = transform.TransformDirection(vel);
        
        transform.position += velocity * Time.deltaTime;
        
        velocity = Vector3.zero;
    }


    public void Gravity()
    {
        if(grounded == false)
        {
            velocity.y -= gravity;
        }
        else
        {
            currentGravity = 0;
        }
    }

    public void GroundChecking()
    {
        Ray ray = new Ray(transform.TransformPoint(liftpoint), Vector3.down);
        RaycastHit tempHit = new RaycastHit();

        if(Physics.SphereCast(ray, 0.17f, out tempHit, 20, discludePlayer))
        {
            GroundConfirm(tempHit);
        }
        else
        {
            grounded = false;
        }
    }

    public void GroundConfirm(RaycastHit tempHit)
    {
        //float currentSlope = Vector3.Angle(tempHit.normal, Vector3.up);
        Collider[] col = new Collider[3];
        int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(groundCHeckPoint), 0.57f, col, discludePlayer);
        grounded = false;

        for (int i = 0; i < num; i++)
        {
            if(col[i].transform == tempHit.transform)
            {
                groundHit = tempHit;
                grounded = true;
                if( inputJump == false)
                {

                    if (!smooth)
                    {
                        transform.position = new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2), transform.position.z);
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, (groundHit.point.y + playerHeight / 2), transform.position.z), smoothSpeed * Time.deltaTime);
                    }
                }
                break;
            }
        }
        if(num <= 1 && tempHit.distance <= 3.1f && inputJump == false)
        {
            if(col [0] != null)
            {
                Ray ray = new Ray(transform.TransformPoint(liftpoint), Vector3.down);
                RaycastHit hit;

                if(Physics.Raycast(ray, out hit, 3.1f, discludePlayer))
                {
                    if(hit.transform != col[0].transform)
                    {
                        grounded = false;
                        return;                    }
                }
            }
        }
        
    }


    public void CollisionCheck()
    {
        Collider[] overlaps = new Collider[4];
        int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(collideSphere.center),collideSphere.radius,overlaps ,discludePlayer, QueryTriggerInteraction.UseGlobal );

        for (int i=0; i < num; i++ )
        {
            Transform t = overlaps[i].transform;
            Vector3 dir;
            float dist;

            if(Physics.ComputePenetration(collideSphere, transform.position, transform.rotation, overlaps[i], t.position, t.rotation, out dir, out dist))
            {
                Vector3 penetrationVector = dir * dist;
                Vector3 velocityProjected = Vector3.Project(velocity, -dir);
                transform.position = transform.position + penetrationVector;
                vel -= velocityProjected;
            }
        }
    }

    private void Jump()
    {
        bool canJump = false;

        canJump = !Physics.Raycast(new Ray(transform.position, Vector3.up), playerHeight, discludePlayer);
        if(grounded && jumpHeight > 0.2f || jumpHeight <= 0.2f && grounded)
        {
            jumpHeight = 0;
            inputJump = false;
        }

        if(grounded && canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputJump = true;
                transform.position += Vector3.up * 0.6f * 2;
                jumpHeight += jumpForce;
            }
        }
        else
        {
            if (!grounded)
            {
                jumpHeight -= (jumpHeight * jumpDecrease * Time.deltaTime);
            }
        }
        velocity.y += jumpHeight;
    }
}
