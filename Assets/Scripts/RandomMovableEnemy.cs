using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovableEnemy : EnemyMovement
{
    Vector3 direct;
    private float CoolDownBefore;
    [SerializeField]
    private float CoolDown = 3f;

    [SerializeField]
    private float RandomImpact = 1.2f;

    protected override void Start()
    {
        base.Start();
        CoolDownBefore = CoolDown;
        direct = Player.transform.position;
    }

    protected override void MoveToward()
    {
        transform.position = Vector3.MoveTowards(transform.position, direct, EnemySpeed * Time.deltaTime);
    }

    protected override void Update()
    {
        CoolDownBefore = Mathf.Max(CoolDownBefore - Time.deltaTime, 0);
        NewDirect();
        base.Update();
    }

    private void NewDirect()
    {
        if (CoolDownBefore == 0) {

            if (!soundLock)
            {               
                var audio = GetComponent<AudioSource>();
                AudioManager.Play("Cockroach", audio);
                soundLock = true;
            }
            var vect = Player.transform.position + direct / 2;
            vect.Normalize();
            var distanceBasedValue = RandomImpact * Vector2.Distance(Player.transform.position, transform.position); 
            vect.x += Random.Range(-distanceBasedValue, distanceBasedValue);
            vect.y += Random.Range(-distanceBasedValue, distanceBasedValue);
            direct = vect + Player.transform.position;

            CoolDownBefore = Random.Range(CoolDown / 3, CoolDown);
        }
    }
    private bool soundLock = false;
}
