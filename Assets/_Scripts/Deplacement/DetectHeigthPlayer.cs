using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectHeigthPlayer : MonoBehaviour {

    public CharacterController Cc;

    public PlayerController controller;

    public LayerMask mask;

    public float distanceMax = 30;


	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        Vector3 p1 =transform.position + Cc.center;
        p1.y = p1.y - Cc.height/2 + Cc.radius;
        float distanceToObstacle = 0;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (Physics.SphereCast(p1, Cc.radius, -transform.up, out hit, distanceMax, mask))
        {
            distanceToObstacle = hit.distance;

            if (distanceToObstacle < 0.2)
            {
                controller.IsGrounded = true;

                controller.SetAltitudeMaxFromGroundPos(0);
            }
            else
            {
                controller.IsGrounded = false;
                controller.SetAltitudeMaxFromGroundPos(hit.point.y);
                
            }
        }
        else
        {
            controller.IsGrounded = false;
            controller.SetAltitudeMaxFromGroundPos(0);
        }

    }

}
