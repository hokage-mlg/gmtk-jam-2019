using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEnemy : EnemyMovement
{
    [SerializeField]
    private float angle = 0f;
    private bool isCircle = false;
    private float NotSpinSpeed;

    [SerializeField]
    private float radius = 1.9f;
    protected override void Start()
    {
        base.Start();
        NotSpinSpeed = EnemySpeed;
    }

    protected override void Update()
    {
        MoveToward();
        Spin();
        Rotation();
    }

    protected override void MoveToward()
    {
        if (Vector3.Distance(Player.transform.position, transform.position) > radius)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, EnemySpeed * Time.deltaTime);
            isCircle = false;
            angle = 0f;
            EnemySpeed = NotSpinSpeed;
        }
        else
        {
            isCircle = true;
        }
    }

    private void Spin()
    {
        if (isCircle)
        {
            EnemySpeed = NotSpinSpeed + 1f;
            angle += Time.deltaTime;
            var x = Mathf.Cos(angle * EnemySpeed) * (radius-0.1f);
            var y = Mathf.Sin(angle * EnemySpeed) * (radius-0.1f);
            transform.position = new Vector3(x, y, 0) + Player.transform.position;
        }
    }

    protected override void Rotation()
    {
        base.Rotation();
    }
}
