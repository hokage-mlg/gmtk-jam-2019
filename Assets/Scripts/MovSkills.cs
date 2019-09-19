using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovSkills : MonoBehaviour
{
    [SerializeField]
    public float dashDistance = 2f;

    [SerializeField]
    private float slideSpeedDefault = 7f;

    [SerializeField]
    private float minRollSpeed = 2f;

    [SerializeField]
    private float rollDurationCoeff = 1.5f;

    [SerializeField]
    public KeyCode Dash = KeyCode.E;

    [SerializeField]
    public KeyCode Roll = KeyCode.Mouse1;

    private Animator anim;
    private AudioSource walkingSound;
    private Vector3 lastMoveDir;
    private Vector3 slideDir;
    private State state;
    private float slideSpeed;
    
    private enum State
    {
        Normal,
        DodgeRollSliding,
    }

    private void Start()
    {
        var sounds = GetComponents<AudioSource>();
        walkingSound = sounds[1];
        anim = GetComponentInChildren<Animator>();       
        state = State.Normal;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Normal:              
                HandleDash();
                DodgeRoll();
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
    private void HandleDash()
    {
        
        if (Input.GetKeyDown(Dash))
        {
            if (TryMove(lastMoveDir, dashDistance))
            {
                
                Debug.LogWarning("AAAA");
                //Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
               // lastMoveDir = direction;
                //transform.position += lastMoveDir * dashDistance;
            }
            Debug.LogWarning("BBBB");
        }
    }

    private void DodgeRoll()
    {

        if (Input.GetKeyDown(Roll))
        {
            
            state = State.DodgeRollSliding;
            slideDir = (GetMouseWorldPosition() - transform.position).normalized;
            slideSpeed = slideSpeedDefault;
            walkingSound.Pause();
        }
    }

    private void DodgeRollSliding()
    {
        TryMove(slideDir, slideSpeed * Time.deltaTime);
        transform.position += slideDir * slideSpeed * Time.deltaTime;
        slideSpeed -= slideSpeed * rollDurationCoeff * Time.deltaTime;

        if (slideSpeed < minRollSpeed)
        {
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
