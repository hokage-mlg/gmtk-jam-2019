﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillsManager : MonoBehaviour
{
    public List<ActiveSkill> activeSkills = new List<ActiveSkill>();
    private List<float> CoolDown = new List<float>();
    private List<KeyCode> keys = new List<KeyCode>()
   {
       KeyCode.Alpha1,
       KeyCode.Alpha2,
       KeyCode.Alpha3,
       KeyCode.Alpha4,
       KeyCode.Alpha5,
       KeyCode.Alpha6,
       KeyCode.Alpha7,
       KeyCode.Alpha8,
       KeyCode.Alpha9,
       KeyCode.Alpha0
   };

    public void AddSkills(ActiveSkill NewSkill)
    {
        activeSkills.Add(NewSkill);
        CoolDown.Add(0f);
    }

    private void Update()
    {
        if (activeSkills.Count != 0)
        {
            for (int i = 0; i < activeSkills.Count; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    if (CoolDown[i] == 0f)
                    {
                        activeSkills[i].ActiveResult();
                        CoolDown[i] = activeSkills[i].CoolDown;
                    }
                }
            }

            for (int i = 0; i < CoolDown.Count; i++)
            {
                if (CoolDown[i] > 0f)
                {
                    CoolDown[i] -= Time.deltaTime;
                }
                else
                {
                    CoolDown[i] = 0f;
                }

            }
        }
    }
}
