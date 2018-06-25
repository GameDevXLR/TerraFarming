using UnityEngine;

public class DetectHeigthPlayer : MonoBehaviour
{
    public CharacterController Cc;

    public PlayerController controller;

    public LayerMask mask;

    public float distanceMax = 30;

    public RaycastHit hit;

    // Update is called once per frame
    private void Update()
    {
        Vector3 p1 = transform.position + Cc.center;
        p1.y = p1.y - Cc.height / 2 + Cc.radius;
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
                controller.shadowObject.SetActive(true);
                controller.IsGrounded = false;
                controller.SetAltitudeMaxFromGroundPos(hit.point.y);
                Vector3 vect = hit.point;
                vect.y += 0.1f;
                controller.shadowObject.transform.position = vect;
            }
        }
        else
        {
            controller.IsGrounded = false;
            controller.SetAltitudeMaxFromGroundPos(0);
            controller.shadowObject.SetActive(false);
        }
    }
}