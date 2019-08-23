using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject Bullet = null;

    [SerializeField]
    private float reloadTimeSec = 1.5f;

    [SerializeField]
    private float randomShootingAngle = 10f;

    [SerializeField]
    private GameObject mouseCursorObj = null;
    private GameObject mouseCursorInst;

    private void Start()
    {
        Cursor.visible = false;
        GameObject.Instantiate(mouseCursorObj);
    }

    private void CmdShoot(Vector3 mousePos, Vector3 screenPoint)
    {
        var bullet = Instantiate(Bullet, transform.position, new Quaternion());
        var audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.Play();
        }

        var offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        angle += Random.Range(-randomShootingAngle, randomShootingAngle);
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.transform.Translate(Vector2.right * 0.5f);
    }

    private void Update()
    {
        if (reloadTimeLeft > 0)
        {
            reloadTimeLeft -= Time.deltaTime;
        }

        if (Input.GetButton("Fire1") && reloadTimeLeft <= 0)
        {
            Vector3 mousePos = Input.mousePosition;
            var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
            CmdShoot(mousePos, screenPoint);
            reloadTimeLeft = reloadTimeSec;
        }
    }

    private float reloadTimeLeft = 0;

}
