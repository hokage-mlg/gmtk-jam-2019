using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMove : MonoBehaviour
{
    private Vector3 lastMoveDir;
    private Animator anim;
    private AudioSource walkingSound;
    private Camera mainCamera = null;

    private void Start()
    {      
        var sounds = GetComponents<AudioSource>();
        walkingSound = sounds[1];
        anim = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
        Rotation();
        HandleDash();
    }

    private void Rotation()
    {
        var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousepos, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    private void HandleMovement()
    {
        float speed = 10f;
        float moveX = 0f;
        float moveY = 0f;
        
        if (Input.GetKey(KeyCode.W))
        {
            moveY = +1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        bool isIdle = moveX == 0 && moveY == 0;

        if (isIdle)
        {
            walkingSound.Pause();
            anim.Play("HeroIdle");
            Debug.LogWarning("Stay");
        }

        else
        {
            Vector3 moveDir = new Vector3(moveX, moveY).normalized;
            Debug.LogWarning("Check move dir");
            if (TryMove(moveDir, speed * Time.deltaTime))
            {
                Debug.LogWarning("Try move");
                walkingSound.volume = Random.Range(0.4f, 0.6f);
                walkingSound.pitch = Random.Range(0.8f, 1f);
                walkingSound.Play();
                anim.Play("HeroWalking");
            }

            else
            {
                Debug.LogWarning("Stay");
                walkingSound.Pause();
                anim.Play("HeroIdle");
            }
        }
    }

    private bool CanMove(Vector3 dir, float distance)
    {
        return Physics2D.Raycast(transform.position, dir, distance).collider == null;
    }

    private bool TryMove(Vector3 baseMoveDir, float distance)
    {
        Vector3 moveDir = baseMoveDir;
        bool canMove = CanMove(moveDir, distance);
        Debug.LogWarning("Check");
        if (!canMove)
        {
            moveDir = new Vector3(baseMoveDir.x, 0f).normalized;
            canMove = moveDir.x != 0f && CanMove(moveDir, distance);

            if (!canMove)
            {
                moveDir = new Vector3(0f, baseMoveDir.y).normalized;
                canMove = moveDir.y != 0f && CanMove(moveDir, distance);
            }
        }

        if (canMove)
        {
            lastMoveDir = moveDir;
            transform.position += moveDir * distance;
            return true;
        }

        else
        {
            return false;
        }
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }

    private void HandleDash()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            float dashDistance = 5f;
            Vector3 beforeDashPosition = transform.position;
            transform.position += lastMoveDir * dashDistance;
            if (TryMove(lastMoveDir, dashDistance))
            {
                //Transform dashEffectTransform = Instantiate(pfDashEffect, beforeDashPosition, Quaternion.identity);
                //dashEffectTransform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(lastMoveDir));
                float dashEffectWidth = 3f;
                //dashEffectTransform.localScale = new Vector3(dashDistance / dashEffectWidth, 1f, 1f);
            }
        }
    }
}
