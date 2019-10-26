using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponSkill
{
    private float randomShootingAngle = 0;
    private GameObject bulletPrefab;
    private AudioSource WeaponShot;
    public Pistol()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Name = "Pistol";
        Description = "Your first gun";
        ReloadTime = 0.6f;
        bulletPrefab = Resources.Load("Pistol/HeroBullet") as GameObject;
        

    }

    public override void Shoot(Vector3 mousePos, Vector3 screenPoint)
    {
        var bullet = GameObject.Instantiate(bulletPrefab, Player.transform.position, new Quaternion());
       
        

        var offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        angle += Random.Range(-randomShootingAngle, randomShootingAngle);
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.transform.Translate(Vector2.right * 0.5f);
    }
    private GameObject Player;
}
