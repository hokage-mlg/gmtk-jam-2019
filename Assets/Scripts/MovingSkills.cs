using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSkills : MonoBehaviour
{
    [SerializeField]
    public float dashDistance = 2f;

    [SerializeField]
    private float slideSpeedDefault = 7f;

    [SerializeField]
    private float minRollSpeed = 2f;

    [SerializeField]
    private float delayTime = 0.1f;

    [SerializeField]
    private float rollDurationCoeff = 1.5f;

    [SerializeField]
    public KeyCode Dash = KeyCode.E;

    [SerializeField]
    public KeyCode Roll = KeyCode.Mouse1;
  
    private Vector3 slideDir;
    private State state;
    private float slideSpeed;
    public CharacterMovement characterMovement;

    private enum State
    {
        Normal,
        DodgeRollSliding,
    }

    private void Start()
    {
        state = State.Normal;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Normal:
                if (Input.GetKeyDown(Dash))
                {
                    StartCoroutine(HandleDash());
                }
                if (Input.GetKeyDown(Roll))
                {
                    DodgeRoll();
                }
                break;
            case State.DodgeRollSliding:
                DodgeRollSliding();
                break;
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

        return (canMove ? true : false);
    }

    private IEnumerator HandleDash()
    {
        Vector3 lastMoveDir = characterMovement.lastdir();

        if (TryMove(lastMoveDir, dashDistance))
        {
            yield return new WaitForSeconds(delayTime);
            transform.position += lastMoveDir * dashDistance;
        }

    }

    private void DodgeRoll()
    {
        state = State.DodgeRollSliding;
        slideDir = (GetMouseWorldPosition() - transform.position).normalized;
        slideSpeed = slideSpeedDefault;
    }

    private void DodgeRollSliding()
    {
        TryMove(slideDir, slideSpeed * Time.deltaTime);
        characterMovement.mov = false;
        characterMovement.anim.Play("HeroIdle");
        characterMovement.walkingSound.Pause();
        transform.position += slideDir * slideSpeed * Time.deltaTime;
        slideSpeed -= slideSpeed * rollDurationCoeff * Time.deltaTime;

        if (slideSpeed < minRollSpeed)
        {
            characterMovement.mov = true;
            state = State.Normal;
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
