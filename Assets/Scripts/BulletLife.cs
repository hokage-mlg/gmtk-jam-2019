using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLife : MonoBehaviour
{
    public float Speed = 18f;

    void FixedUpdate()
    { 
        transform.Translate(Vector2.right * Speed * Time.fixedDeltaTime);
        Destroy(gameObject, 1.2f); 
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
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "Environment")
        {
            if (coll.gameObject.GetComponent<DestructibleWall>() != null)
            {
                Destroy(coll.gameObject);
            }
            if (coll.gameObject.GetComponent<MirrorWall>() == null)
            {
                Destroy(gameObject);
            }
            else
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
        }
    }
}
