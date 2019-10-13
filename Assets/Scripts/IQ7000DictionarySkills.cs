using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IQ7000DictionarySkills : MonoBehaviour
{ 
    public static Dictionary<string, SkillBase> skills ;
    private void Awake()
    {
        skills = new Dictionary<string, SkillBase>();

        WeirdPill Pill = new WeirdPill();
        skills.Add("SpeedAura", Pill);
        GhostMode ghost = new GhostMode();
        skills.Add("GhostMode", ghost);
        ActiveSpeedSkill active = new ActiveSpeedSkill();
        skills.Add("ActiveSpeedSkill", active);
    }

    public static SkillBase GetValue(string Name)
    {
        try
        {
            var Val = skills[Name];
            return Val;
        }
        catch
        {
            return null;
        }
    }
}
