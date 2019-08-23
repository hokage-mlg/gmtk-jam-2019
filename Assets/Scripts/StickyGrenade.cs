using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StickyGrenade : MonoBehaviour
{
    public static void Create(Transform pfStickyGrenade, Vector3 spawnPosition, Vector3 targetPosition, Action<Vector3> onExplodeAction)
    {
        StickyGrenade stickyGrenade = Instantiate(pfStickyGrenade,spawnPosition, Quaternion.identity).GetComponent<StickyGrenade>();

        stickyGrenade.Setup(targetPosition, onExplodeAction);
    }

    private Action<Vector3> onExplodeAction;
    private int bounceState;

    private void Setup(Vector3 targetPosition, Action<Vector3> onExplodeAction)
    {
        this.onExplodeAction = onExplodeAction;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPosition);
        float moveSpeed = Mathf.Clamp(distance * 4f, 50f, 250f);
        gameObject.GetComponent<Rigidbody2D>().velocity = moveDirection * moveSpeed;
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = -1000f;
        bounceState = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StopMoving();
    }

    private void Update()
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
    }




    private void StopMoving()
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<CircleCollider2D>());
    }
}
