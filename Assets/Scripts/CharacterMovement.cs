using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    private Vector2 movement;
    private SpriteRenderer CharacterSprite;
    private Animator anim;

    private AudioSource[] sounds;
    private AudioSource noise1;
    private AudioSource noise2;

    private Vector3 lastMoveDir;

    private void Start()
    {
        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];
        noise2 = sounds[1];
        CharacterSprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Movement();
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
    
    private void Movement()
    {
        Vector2 direction = new Vector2();
        direction += new Vector2(Input.GetAxis("Horizontal"), 0);
        direction += new Vector2(0, Input.GetAxis("Vertical"));
        if (direction.magnitude > 1)
        {
            direction.Normalize();
            
        }
        if (anim != null)
        {
            if (direction.magnitude == 0) 
            {
                noise2.Pause();
                anim.Play("HeroIdle");
            }
            else if (noise2.isPlaying == false)
            {
                noise2.volume = Random.Range(0.4f, 0.6f);
                noise2.pitch = Random.Range(0.8f, 1f);
                noise2.Play();            
                anim.Play("HeroWalking");
            }
        }
        //bool canMove = CanMove(direction, speed * Time.deltaTime);
        //if (!canMove)
        //{
        //    direction = new Vector3(direction.x, 0f).normalized;
        //    canMove = CanMove(direction, speed * Time.deltaTime);
        //    if (!canMove)
        //    {
        //        direction = new Vector3(0f, direction.y).normalized;
        //        canMove = CanMove(direction, speed * Time.deltaTime);
        //    }
        //}
       // if (canMove)
       // {
            transform.Translate(direction * speed * Time.deltaTime, Space.World);
            lastMoveDir = direction;
        //}
        
    }  
    
    private bool CanMove(Vector3 dir, float distance)
    {
        return Physics2D.Raycast(transform.position, dir, distance).collider == null;
    }

    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float dashDistance = 10f;
            transform.position += lastMoveDir * dashDistance;
            //transform.position = new Vector2(Mathf.Clamp(transform.position.x, -10f, 10f), Mathf.Clamp(transform.position.y, -10f, 10f));
        }
    }
}
