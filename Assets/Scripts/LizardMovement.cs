using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardMovement : EnemyMovement
{
    private float CDTLeft;
    [SerializeField]
    private float LizardBoostSpeed = 4.5f;
    [SerializeField]
    private Vector2 RandomBoostRange = new Vector2(4f, 12f);

    [SerializeField]
    private float LizardBoostTime = 2;
    private float LBTLeft;

    protected override void Start()
    {
        standardSpeed = EnemySpeed;
        CDTLeft = Rand();
        LBTLeft = 0;
        base.Start();
    }

    protected override void Update()
    {
        CDTLeft = Mathf.Max(CDTLeft - Time.deltaTime, 0);
        LBTLeft = Mathf.Max(LBTLeft - Time.deltaTime, 0);
        base.Update();
    }

    protected override void MoveToward()
    {
        ExtraSpeed();
        base.MoveToward();
    }

    protected override void Rotation()
    {
        base.Rotation();
    }

    private void ExtraSpeed()
    {
        if (LBTLeft <= 0)
        {
            EnemySpeed = standardSpeed;
        }

        if (CDTLeft == 0)
        {
            var audio = GetComponent<AudioSource>();
            AudioManager.Play("LizardRun", audio);

            CDTLeft = Rand();
            LBTLeft = LizardBoostTime;
            EnemySpeed = LizardBoostSpeed;
        }
    }
    private float Rand()
    {
        return Random.Range(RandomBoostRange.x, RandomBoostRange.y);
    }
    
    private float standardSpeed;
}
