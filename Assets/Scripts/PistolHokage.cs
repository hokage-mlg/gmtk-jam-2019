using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolHokage : WeaponSkill
{
    private float randomShootingAngle = 0;
    private GameObject bulletPrefab;
    private AudioSource WeaponShot;
    private GameObject Player;
    private float ArcAngle = 45;
    private int SphereNumber = 3;
    public PistolHokage()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Name = "PistolHokage";
        Description = "Your second gun";
        ReloadTime = 0.6f;
        bulletPrefab = Resources.Load("Pistol/HeroBullet") as GameObject;
    }
    public override void Shoot(Vector3 mousePos, Vector3 screenPoint)
    {       
        for (int i = 0; i < SphereNumber; i++)
        {            
            var bullet = GameObject.Instantiate(bulletPrefab, Player.transform.position, new Quaternion());

            var offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
            var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            angle += Mathf.Lerp(-ArcAngle / 2, ArcAngle / 2, i / (SphereNumber - 1.0f));
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.transform.Translate(Vector2.right * 0.5f);
        }
    }
}
