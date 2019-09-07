using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : AttackingMonster
{
    [SerializeField]
    protected float RandomShotAngle = 15f;
    [SerializeField]
    protected GameObject Bullet = null;

    protected virtual void ShootBulletStraight(Vector2 direction, GameObject bulletToSpawn, float randomAngle)
    {
        var bullet = Instantiate(bulletToSpawn, transform.position, new Quaternion());

        var audio = GetComponent<AudioSource>();
        AudioManager.Play("MonsterShot", audio);

        var offset = new Vector2(direction.x - transform.position.x, direction.y - transform.position.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        angle += Random.Range(-randomAngle, randomAngle);
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.transform.Translate(Vector2.right * 0.5f);
    }

    protected override void Attack()
    {
        var PlayerPos = Player.transform.position;
        ShootBulletStraight(PlayerPos, Bullet, RandomShotAngle);
    }

}
