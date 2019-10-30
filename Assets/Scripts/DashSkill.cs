using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DashSkill : ActiveSkill
{
    public float dashDistance = 2f;      
    public CharacterMovement characterMovement;
    public Transform transform;
    public DashSkill()
    {
        Name = "DashSkill";
        Description = "Dashing ";
        CoolDown = 5f;
        isInstantSkill = true;  
        ActionTime = 3f;    
    }
    public override void ActiveResult()
    {       
         //HandleDash();        
    }
    public bool CanMove(Vector3 dir, float dashDistance)
    {
        return Physics2D.Raycast(transform.position + dir, dir, dashDistance).collider == null;
    }
    public bool TryMove(Vector3 baseMoveDir, float dashDistance)
    {
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, dashDistance);

        if (!canMove)
        {
            moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
            canMove = moveDir.x != 0f && CanMove(moveDir, dashDistance);
        }

        else if (!canMove)
        {
            moveDir = new Vector3(0f, baseMoveDir.y).normalized;
            canMove = moveDir.y != 0f && CanMove(moveDir, dashDistance);
        }

        else if (!canMove)
        {
            moveDir = new Vector3(baseMoveDir.x, baseMoveDir.y).normalized;
            canMove = moveDir.x == moveDir.y && CanMove(moveDir, dashDistance);
        }

        return canMove;
    }
    //public void HandleDash()
    //{
    //    Debug.LogWarning("Can't dash");
    //    Vector3 lastMoveDir = characterMovement.getLastMoveDir();
    //    if (TryMove(lastMoveDir, dashDistance))
    //    {
    //        Debug.LogWarning("Can dash");
    //        transform.position += lastMoveDir * dashDistance;
    //    }
    //}
}
