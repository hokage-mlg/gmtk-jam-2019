using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadleDashSkill : MonoBehaviour
{
    [SerializeField]
    public float dashDistance = 2f;
    [SerializeField]
    private float delayTime = 0.1f;
    [SerializeField]
    public KeyCode Dash = KeyCode.E;
    public CharacterMovement characterMovement;
    private void Update()
    {
        if (Input.GetKeyDown(Dash))
        {
            StartCoroutine(HandleDash());
        }
    }
    private bool CanMove(Vector3 dir, float dashDistance)
    {
        return Physics2D.Raycast(transform.position + dir, dir, dashDistance).collider == null;
    }
    private bool TryMove(Vector3 baseMoveDir, float dashDistance)
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
    private IEnumerator HandleDash()
    {
        Vector3 lastMoveDir = characterMovement.getLastMoveDir();

        if (TryMove(lastMoveDir, dashDistance))
        {
            yield return new WaitForSeconds(delayTime);
            transform.position += lastMoveDir * dashDistance;
        }
    }
}
