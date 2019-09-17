﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet = null;

    [SerializeField]
    private float randomShootingAngle = 0;

    [SerializeField]
    private GameObject mouseCursorObj = null;

    private void Start()
    {
        weapon = new Pistol();
        mainCamera = Camera.main;
        Cursor.visible = false;
        GameObject.Instantiate(mouseCursorObj);
        shootSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (reloadTimeLeft > 0)
        {
            reloadTimeLeft -= Time.deltaTime;
        }
        else if(Input.GetButton("Fire1"))
        {
            Vector3 mousePos = Input.mousePosition;
            var screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);
            weapon.Shoot(mousePos,screenPoint);
            reloadTimeLeft = weapon.ReloadTime;
        }
    }

    private float reloadTimeLeft = 0;
    private Camera mainCamera;
    private AudioSource shootSound;
    private WeaponSkill weapon;
}
