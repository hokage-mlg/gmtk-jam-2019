using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    public float speed = 12f;
<<<<<<< HEAD

    [SerializeField]
    public bool mov = true;

    [SerializeField]
    private bool spin = true;

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public AudioSource walkingSound;
    private Vector3 lastMoveDir;
=======
    
    private Animator anim;
    private AudioSource audio;
>>>>>>> 9aea4da9469c0af582c57ed4db51997e10d526ba

    private void Start()
    {
        audio = GetComponent<AudioSource>();       
        anim = GetComponentInChildren<Animator>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Movement();
        Rotation();
    }
<<<<<<< HEAD

    public void Rotation()
    {

        if (spin == true)
=======
    private void Rotation()
    {
        var mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousepos, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }
    
    private void Movement()
    {       
        Vector2 direction = new Vector2();
        direction += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (direction.magnitude > 1)
>>>>>>> 9aea4da9469c0af582c57ed4db51997e10d526ba
        {
            var mousepos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Quaternion rot = Quaternion.LookRotation(transform.position - mousepos, Vector3.forward);
            transform.rotation = rot;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        }
    }

    public void Movement()
    {

        if (mov == true)
        {
            Vector2 direction = new Vector2();
            direction += new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (direction.magnitude > 1)
            {
<<<<<<< HEAD
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
=======
                AudioManager.Pause("Walk", audio);
                anim.Play("HeroIdle");
            }
            else if (AudioManager.isPlaying("Walk", audio) == false)
            {
                AudioManager.Play("Walk", audio);
                anim.Play("HeroWalking");
            }        
        }
        transform.Translate(direction * speed * Time.deltaTime, Space.World);      
>>>>>>> 9aea4da9469c0af582c57ed4db51997e10d526ba
    }

    public Vector2 getLastMoveDir()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        lastMoveDir = direction;
        return lastMoveDir;
    }
    private Camera mainCamera = null;
<<<<<<< HEAD
}
=======

}
>>>>>>> 9aea4da9469c0af582c57ed4db51997e10d526ba
