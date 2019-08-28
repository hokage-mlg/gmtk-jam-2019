using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BouncyGrenade : MonoBehaviour
{
    public float Speed = 18f;
    [SerializeField]
    private float timeToDestruction = 1.2f;
    private float TTDLeft = 0;
    private float timeToExplode = 1.5f;
    private int bounceState = 0;
    private float damageRadius = 5f;
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
        transform.Translate(Vector2.right * Speed * Time.fixedDeltaTime);
        TTDLeft -= Time.fixedDeltaTime;
        timeToExplode -= Time.deltaTime;
        if (timeToExplode <= 0f)
        {
            ExplodeGrenade();
            Destroy(gameObject);
        }
    }

    private void ExplodeGrenade()
    {
       // onExplodeAction(transform.position);
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
            if (coll.gameObject.GetComponent<DestructibleWall>() != null)
            {
                Destroy(coll.gameObject);
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
                ExplodeGrenade();
            }
        }
    }
}