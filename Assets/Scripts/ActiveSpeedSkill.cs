using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpeedSkill : ActiveSkill
{
    private CharacterMovement character;
    public ActiveSpeedSkill()
    {
        Name = "ActiveSpeedSkill";
        Description = "Yoo could ";
        CoolDown = 5f;
        isInstantSkill = false;   //This skill is instant or not
        ActionTime = 3f;
        //If it's not instant,than after Action Time the EndOfSkill function will be called
    }

    public override void ActiveResult()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        character = Player.GetComponent<CharacterMovement>();
        character.speed *= 2;
    }

    public override void EndOfSkill()
    {
        character.speed /= 2;
    }
}
