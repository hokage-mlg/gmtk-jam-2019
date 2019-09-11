using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkillsManager : MonoBehaviour
{
    public List<ActiveSkill> activeSkills = new List<ActiveSkill>();
    private List<float> CoolDown = new List<float>();
    private List<float> ActionSkills = new List<float>();
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
        ActionSkills.Add(0f);
    }

    private void Update()
    {
        
       for (int i = 0; i < activeSkills.Count; i++)
       {
            if (Input.GetKeyDown(keys[i]) && CoolDown[i] == 0f)
            {
                
                    activeSkills[i].ActiveResult();
                    if (!activeSkills[i].isInstantSkill)
                    {
                        CoolDown[i] = activeSkills[i].CoolDown + activeSkills[i].ActionTime;
                        activeSkills[i].isActive = true;
                    }
                    else
                    {
                        CoolDown[i] = activeSkills[i].CoolDown;
                    }
                
            }
        }

        for (int i = 0; i < CoolDown.Count; i++)
        {

            if (CoolDown[i] > 0f)
            {
                if (!activeSkills[i].isInstantSkill)
                {
                    if (CoolDown[i] < activeSkills[i].CoolDown && activeSkills[i].isActive)
                    {
                        activeSkills[i].EndOfSkill();
                        activeSkills[i].isActive = false;
                    }
                }
                CoolDown[i] = Mathf.Max(0, CoolDown[i] - Time.deltaTime);
            }
        }

    }
}
