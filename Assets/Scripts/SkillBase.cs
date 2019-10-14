using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase
{
    public string Name;
    [Multiline]
    public string Description;
    public Sprite img;
    
    public void SaveSkill()
    {
        if (PlayerPrefs.HasKey("SkillString"))
        {
            string skills = PlayerPrefs.GetString("SkillString");
            skills += Name;
            skills += " ";
            PlayerPrefs.SetString("SkillString", skills);
        }
        else
        {
            string skills = Name;
            skills += " ";
            PlayerPrefs.SetString("SkillString", skills);
        }
    }

    public virtual void ActiAcquireSkill()
    {
    }
}
