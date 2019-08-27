using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : EnemyMovement
{
    private float CoolDownBefore;
    [SerializeField]
    private float CoolDown = 18f;
    [SerializeField]
    private float GhostBoostSpeed = 7f;
    [SerializeField]
    private bool PacifistInBoost = true;
    private BoxCollider2D BoxCollider;
    
    protected override void Start()
    {
        standardSpeed = EnemySpeed;
        BoxCollider = GetComponent<BoxCollider2D>();
        CoolDownBefore = CoolDown / 2;
        sprite = GetComponentInChildren<SpriteRenderer>();
        base.Start();
    }

    protected override void Update()
    {
        CoolDownBefore = Mathf.Max(CoolDownBefore - Time.deltaTime, 0);
        GhostMode();
        base.Update();
    }

    private void GhostMode()
    {
        if (CoolDownBefore < 3f)
        {
            BoxCollider.isTrigger = false;
            EnemySpeed = standardSpeed;
            var s = sprite.color;
            s.a = 1f;
            sprite.color = s;
        }

        if (CoolDownBefore == 0)
        {
            var audio = GetComponent<AudioSource>();
            AudioManager.Play("Ghost", audio);

            BoxCollider.isTrigger = PacifistInBoost;
            EnemySpeed = GhostBoostSpeed;
            var s = sprite.color;
            s.a = 0.5f;
            sprite.color = s;

            CoolDownBefore = CoolDown;
        }
    }

    private float standardSpeed;
}
