using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BouncyGrenade : MonoBehaviour
{
    public float Speed = 10f;
    [SerializeField]
    private float timeToDestruction = 1.2f;
    private float TTDLeft = 0;
    private float timeToExplode = 1.5f;
    private int bounceState = 0;
    private float damageRadius = 5f;
    private float thrust = 10f;
    private float MaxSpeed = 15f;
    private float Boost = 0.1f;


    void Start()
    {
        TTDLeft = timeToDestruction;
    }
    void FixedUpdate()
    {
        switch (bounceState)
        {
            default:
            case 0:
                transform.localScale += Vector3.one * 7f * Time.deltaTime;
                if (transform.localScale.x >= 2.5f) bounceState = 1;
                break;
            case 1:
                transform.localScale -= Vector3.one * 7f * Time.deltaTime;
                if (transform.localScale.x <= 1f) bounceState = 2;
                break;
            case 2:
                break;
        }

        //
        //transform.position = Vector3.Lerp(transform.position, Vector2.right, Time.deltaTime * Speed);
        //transform.Translate(Vector2.right * Speed * Time.fixedDeltaTime);
        gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right, ForceMode2D.Impulse);
        TTDLeft -= Time.fixedDeltaTime;
        timeToExplode -= Time.deltaTime;
        if (timeToExplode <= 0f)
        {

            ExplodeGrenade();

        }
    }

    private void ExplodeGrenade()
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(3, 3);
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Enemy")
        {
            var monsterComp = coll.gameObject.GetComponent<MonsterLife>();
            if (monsterComp)
            {
                monsterComp.Damage();
            }
            else
            {
                Debug.LogError("ОШИБКА: УСТАНОВИТЕ МОНСТРУ " + coll.gameObject.name + " КОМПОНЕНТ MonsterLife");
                Destroy(coll.gameObject);
            }
            ExplodeGrenade();
        }
        else if (coll.gameObject.tag == "Environment")
        {
            // Rigidbody2D Grenade = gameObject.GetComponent<Rigidbody2D>();
            //  Vector2 directionGrenade = Grenade.transform.position - transform.position;
            //  directionGrenade = directionGrenade.normalized * thrust;
            //  Grenade.AddForce(directionGrenade, ForceMode2D.Impulse);
            if (coll.gameObject.GetComponent<DestructibleWall>() != null)
            {
                //     Grenade.AddForce(directionGrenade, ForceMode2D.Impulse);
                //Destroy(coll.gameObject);
            }
            if (coll.gameObject.GetComponent<MirrorWall>() != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right,
                    float.PositiveInfinity, LayerMask.GetMask("Default"));
                if (hit)
                {
                    Vector2 reflectDir = Vector2.Reflect(transform.right, hit.normal);
                    float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg;
                    transform.eulerAngles = new Vector3(0, 0, rot);
                }
            }
            else
            {

                //    Grenade.AddForce(directionGrenade, ForceMode2D.Impulse);
                //ExplodeGrenade();
            }
        }
    }
}