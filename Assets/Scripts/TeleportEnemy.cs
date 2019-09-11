using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnemy : EnemyMovement
{
    [SerializeField]
    private Vector2 TpCooldownRange = new Vector2(3, 10);
    private float CoolDownBefore;
    [SerializeField]
    private float Scatter = 8f;

    private ArenaEnemySpawner arena;

    protected override void Start()
    {
        CoolDownBefore = Random.Range(TpCooldownRange.x, TpCooldownRange.y);
        arena = GameObject.FindGameObjectWithTag("GameController")
            .GetComponent<ArenaEnemySpawner>();
        base.Start();
    }

    protected override void Update()
    {
        CoolDownBefore = Mathf.Max(CoolDownBefore - Time.deltaTime, 0);
        Teleport();
        base.Update();
    }

    protected override void MoveToward()
    {
        base.MoveToward();
    }

    protected override void Rotation()
    {
        base.Rotation();
    }

    private void Teleport()
    {
        if (CoolDownBefore == 0)
        {
            int i = 0;
            while (i < 10)
            {
                i++;
                float Xpos = Random.Range(-100, 100);
                float YPos = Random.Range(-100, 100);
                var vect = new Vector2(Player.transform.position.x - Xpos, Player.transform.position.y - YPos);
                vect.Normalize();
                vect *= Scatter;
                Vector3 NVector = new Vector3(vect.x, vect.y);
                if (arena.RoomBounds.x > Mathf.Abs(Player.transform.position.x + NVector.x) && arena.RoomBounds.y > Mathf.Abs(Player.transform.position.y + NVector.y))
                {
                    var audio = GetComponent<AudioSource>();
                    AudioManager.Play("Blink", audio);
                    CoolDownBefore = Random.Range(TpCooldownRange.x, TpCooldownRange.y);
                    transform.position = Player.transform.position + NVector;
                    lifeComp.FadeIn(0.5f);
                    return;
                }
                
            }
        }
    }
}
