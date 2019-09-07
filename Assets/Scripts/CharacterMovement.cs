using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 12f;

    [SerializeField]
    private bool mov = true;

    [SerializeField]
    private bool spin = true;

    private Animator anim;
    private AudioSource walkingSound;
    public Vector3 lastMoveDir;
    private float dashDistance = 2f;
    private void Start()
    {
        var sounds = GetComponents<AudioSource>();
        walkingSound = sounds[1];
        anim = GetComponentInChildren<Animator>();

        mainCamera = Camera.main;
    }

    private void Update()
    {      
        Movement();
        Rotation();
        HandleDash();
    }
    private void Rotation()
    {
        if (spin == true)
        {
            var mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Quaternion rot = Quaternion.LookRotation(transform.position - mousepos, Vector3.forward);
            transform.rotation = rot;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }
    }

    private void Movement()
    {
        if (mov == true)
        {
            Vector2 direction = new Vector2();
            direction += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (direction.magnitude > 1)
            {
                direction.Normalize();
            }
           
            if (anim != null)
            {
                if (direction.magnitude == 0)
                {
                    walkingSound.Pause();
                    anim.Play("HeroIdle");
                }
                else if (walkingSound.isPlaying == false)
                {
                    walkingSound.volume = Random.Range(0.4f, 0.6f);
                    walkingSound.pitch = Random.Range(0.8f, 1f);
                    walkingSound.Play();
                    anim.Play("HeroWalking");
                }
            }
            if (TryMove(direction, speed))
            {
                //Debug.LogWarning("MOVEMENTCHECK");
                transform.Translate(direction * speed * Time.deltaTime, Space.World);
                lastMoveDir = direction;               
            }
            
        }
    }

    
    private bool CanMove(Vector3 dir, float dashDistance)
    {
        return Physics2D.Raycast(transform.position, dir, dashDistance).collider == null;     
    }
    private bool TryMove(Vector3 baseMoveDir, float dashDistance)
    {
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, dashDistance);
        //Debug.LogWarning("CheckTryMove");
        if (!canMove)
        {
            //Debug.LogWarning("can't X");
            moveDir = new Vector3(baseMoveDir.x, 0f);
            canMove = moveDir.x == 0f && !CanMove(moveDir, dashDistance);

            if (!canMove)
            {
                //Debug.LogWarning("can't Y");
                moveDir = new Vector3(0f, baseMoveDir.y);
                canMove = moveDir.y == 0f && !CanMove(moveDir, dashDistance);
                if (!canMove)
                {
                    //Debug.LogWarning("AAA");
                    moveDir = new Vector3(0f, 0f);
                    canMove = moveDir.y == moveDir.x && !CanMove(moveDir, dashDistance);
                }
            }
        }

        if (canMove)
        {
            
            return true;
        }

        else
        {
            return false;
        }
    }
    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {                   
            Debug.LogWarning("DASH");
            if (TryMove(lastMoveDir, dashDistance))
            {                 
                Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;               
                lastMoveDir = direction;
                Debug.LogWarning("YES");
                Debug.LogWarning(lastMoveDir);
                transform.position += lastMoveDir * dashDistance;                
            }
            else
            {
                Debug.LogWarning("NO");
            }
        }
    }
    private Camera mainCamera = null;
}