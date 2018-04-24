﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourNotGroundedController : BehaviourController {
    bool isFlying;
    float referenceYFly;


    private void Update()
    {
        float y = moveDirection.y;
        moveDirection = CalculateMoveDirection();
        if (!isFlying)
        {
            moveDirection.y = y;
            Gravity();
        }
        if (moveDirection.x != 0 || moveDirection.z != 0)
        {
            rotation(moveDirection);
        }
        Cc.Move(moveDirection * Time.deltaTime);
    }



}
