using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicMonsterBulletLife : EnemyBulletLife
{
    [SerializeField]
    GameObject ToxicPuddle = null;
    [SerializeField]
    private float HomingEulerAnglesPerSecond = 45f;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        RotateToPlayer();
    }

    private float angle180fix(float angle)
    {
        if (angle > 180)
        {
            return -360 + angle;
        }
        else if (angle < -180)
        {
            return 360 + angle;
        }
        else return angle;
    }

    private void RotateToPlayer()
    {
        var PlayerPos = Player.transform.position;
        var offset = new Vector2(PlayerPos.x - transform.position.x, PlayerPos.y - transform.position.y);
        //var angle = Mathf.Clamp(
        //    Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg,
        //    -HomingEulerAnglesPerSecond, HomingEulerAnglesPerSecond) * Time.deltaTime;
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        var currentAngle = gameObject.transform.rotation.eulerAngles.z;
        var difference = angle180fix(angle - currentAngle);
        var differenceClamped = Mathf.Clamp(difference, -HomingEulerAnglesPerSecond, HomingEulerAnglesPerSecond) * Time.deltaTime;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, currentAngle + differenceClamped);
    }

    private void OnDestroy()
    {
        Instantiate(ToxicPuddle, transform.position, Quaternion.identity);
    }

    private GameObject Player;
}
