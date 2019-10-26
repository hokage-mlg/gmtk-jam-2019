using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D Enemy = other.GetComponent<Rigidbody2D>();
            if (Enemy != null)
            {
                Enemy.drag = 1;
                Vector2 direction = Enemy.transform.position - transform.position;
                direction = direction.normalized * thrust;
                Enemy.AddForce(direction, ForceMode2D.Impulse);
                
                // TODO: Redo this script for the new AI if necessary
                // var moveComp = Enemy.GetComponent<EnemyMovement>();
                // if (moveComp)
                // {
                //     moveComp.StopMovement(0.7f);
                // }
                // else
                // {
                //     Debug.LogWarning("No Move Component on enemy? Is it ok?");
                // }
            }
        }
    }   
}
