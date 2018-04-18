using UnityEngine;

public class BehaviourGroundedController : BehaviourController {

	void Update () {
        
        float y = moveDirection.y;
        moveDirection = CalculateMoveDirection();
        moveDirection.y = y;
        Gravity();
        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            rotation(moveDirection);
        }
        Cc.Move(moveDirection * Time.deltaTime);
    }
}
