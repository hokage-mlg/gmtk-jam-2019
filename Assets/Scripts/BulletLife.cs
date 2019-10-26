using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLife : MonoBehaviour
{
    // Logic
    public float Speed = 18f;
    [SerializeField]
    private float timeToDestruction = 1.2f;
    private float TTDLeft = 0;

    void Start()
    {
        var audio = GetComponent<AudioSource>();
        AudioManager.Play("WeaponShot", audio);
        TTDLeft = timeToDestruction;
    }

    void FixedUpdate()
    { 
        transform.Translate(Vector2.right * Speed * Time.fixedDeltaTime);
        TTDLeft -= Time.fixedDeltaTime;
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
            DestroyBullet();
        }
        else if (coll.gameObject.tag == "Environment")
        {
            if (coll.gameObject.GetComponent<DestructibleWall>() != null)
            {
                DestroyBullet();
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
                DestroyBullet();
            }
        }
    }

    public void DestroyBullet()
    {
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<DynamicLightInOut>().FadeOut();
        Destroy(gameObject, 1);
        Destroy(particlesEmitter.gameObject, 2);
        StopEmitter();
    }

    // Non-logic
    [SerializeField]
    private ParticleSystem particlesEmitter = null;
    [SerializeField]
    private SpriteRenderer sprite = null;

    private void StopEmitter()
    {
        particlesEmitter.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        sprite.color = new Color(0, 0, 0, 0);
    }
}
