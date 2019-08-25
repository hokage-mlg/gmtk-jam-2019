using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMove : MonoBehaviour
{
    private Vector3 lastMoveDir;
    private SpriteRenderer CharacterSprite;
    private Animator anim;
    private void Start()
    {
        CharacterSprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        HandleMovement();
        Rotation();
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
            anim.Play("HeroIdle");
        }
        else
        {
            Vector3 moveDir = new Vector3(moveX, moveY).normalized;
            Vector3 targetMovePosition = transform.position + moveDir * speed * Time.deltaTime;
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, moveDir, speed * Time.deltaTime);
            if (raycastHit.collider == null)
            {
                //can move
                lastMoveDir = moveDir;
                anim.Play("HeroWalking");
                transform.position = targetMovePosition;
            }
            else
            {
                //cannot move diagonally 0:40
                Vector3 testMoveDir = new Vector3(moveDir.x, 0f).normalized;
                targetMovePosition = transform.position + testMoveDir * speed * Time.deltaTime;
                raycastHit = Physics2D.Raycast(transform.position, testMoveDir, speed * Time.deltaTime);
                if (testMoveDir.x != 0f && raycastHit.collider == null)
                {
                    //can move  horizontally
                    lastMoveDir = testMoveDir;
                    anim.Play("HeroWalking");
                    transform.position = targetMovePosition;
                }
                else
                {
                    //cannot move horizontally
                    testMoveDir = new Vector3(0f, moveDir.y).normalized;
                    targetMovePosition = transform.position + testMoveDir * speed * Time.deltaTime;
                    raycastHit = Physics2D.Raycast(transform.position, testMoveDir, speed * Time.deltaTime);
                    if (testMoveDir.y != 0f && raycastHit.collider == null)
                    {
                        lastMoveDir = testMoveDir;
                        anim.Play("HeroWalking");
                        transform.position = targetMovePosition;
                    }
                    else
                    {
                        //cannot move vertically
                        anim.Play("HeroIdle");
                    }
                }
            }
        }
    }
}
