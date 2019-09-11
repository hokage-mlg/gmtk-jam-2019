using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    protected float EnemySpeed = 2f;
    protected GameObject Player;
    protected SpriteRenderer sprite;
    protected MonsterLife lifeComp;

    protected virtual void Start()
    {
        lifeComp = GetComponent<MonsterLife>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void MoveAndRotate()
    {
        if (allowMovement)
        {
            if (lifeComp.FadeInLeft == 0) {
                MoveToward();
            }
            Rotation();
        }
    }

    protected virtual void Update()
    {
        MoveAndRotate();
    }

    protected virtual void MoveToward()
    {      
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, EnemySpeed * Time.deltaTime);
    }

    protected virtual void Rotation()
    {
        float z = Mathf.Atan2((Player.transform.position.y - transform.position.y), (Player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z);
    }

    public void StopMovement(float time)
    {
        allowMovement = false;
        StartCoroutine(EnableMovement(time));
    }

    private IEnumerator EnableMovement(float wait)
    {
        yield return new WaitForSeconds(wait);
        
        allowMovement = true;
    }

    private bool allowMovement = true;
}
