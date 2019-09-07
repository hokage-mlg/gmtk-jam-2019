using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletLife : MonoBehaviour
{
    [SerializeField]
    public float BulletSpeed = 12f;
    [SerializeField]
    protected float BulletLifeLength = 3f;

    protected virtual void Update()
    {
        transform.Translate(Vector2.right * BulletSpeed * Time.deltaTime, Space.Self);
        Destroy(gameObject, BulletLifeLength);
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
        }
        else if (coll.gameObject.tag == "Player")
        {
            Destroy(coll.gameObject);
            Time.timeScale = 0;
            RelodScene.PressR();
        }
    }
}
