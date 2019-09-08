using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 12f;

    [SerializeField]
    private bool mov = true;

    [SerializeField]
    private bool spin = true;

    [SerializeField]
    public float dashDistance = 2f;

    private Animator anim;
    private AudioSource walkingSound;
    public Vector3 lastMoveDir;

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

            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            lastMoveDir = direction;

        }
    }

    private bool CanMove(Vector3 dir, float dashDistance)
    {
        return Physics2D.Raycast(transform.position + dir, dir, dashDistance).collider == null;
    }

    private bool TryDash(Vector3 baseMoveDir, float dashDistance)
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

            if (TryDash(lastMoveDir, dashDistance))
            {
                Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                lastMoveDir = direction;
                transform.position += lastMoveDir * dashDistance;
            }
        }
    }

    private Camera mainCamera = null;

}