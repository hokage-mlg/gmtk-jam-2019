using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdPill : PassiveSkill
{
    private GameObject Player;

    public WeirdPill()
    {
        Name = "SpeedAura";
        Description = "Yellow strange pill";
    }

    public override void PassiveAura()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        CharacterMovement character = Player.GetComponent<CharacterMovement>();
        character.speed *= 2;
    }


}
