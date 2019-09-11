using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : SkillBase
{
    public override void ActiAcquireSkill()
    {
        PassiveAura();
    }

    public virtual void PassiveAura()
    {
    }
}
