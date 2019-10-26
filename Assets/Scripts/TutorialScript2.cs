using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class TutorialScript2 : ArenaEnemySpawner
{
    [SerializeField]
    private float timeToEachLight = 7.0f;
    [SerializeField]
    private float timeToNextLight = 3.0f;
    protected override void Update()
    {
        base.Update();
        lightUpdate();
    }

    private void lightUpdate ()
    {
        timeToNextLight -= Time.deltaTime;
        if (timeToNextLight < 3.0f && timeToNextLight > 0.0f)
        {
            LightEnemy();
        }

        if (timeToNextLight <= 0)
        {
            NoLightEnemy();
            timeToNextLight = timeToEachLight;
        }
    }

    private void LightEnemy ()
    {
        for (int i = 0; i < boysList.Count; i++)
        {
            if (boysList[i].gameObject.GetComponent<MonsterLife>().isBoy())
            {
                boysList[i].gameObject.GetComponent<Light2D>().pointLightOuterRadius = Mathf.Clamp(2/timeToNextLight,0,2);
              
            }
        }
    }

    private void NoLightEnemy()
    {
        for (int i = 0; i < boysList.Count; i++)
        {
            if (boysList[i].gameObject.GetComponent<MonsterLife>().isBoy())
            {
                boysList[i].gameObject.GetComponent<Light2D>().pointLightOuterRadius = 0.0f;
            }
        }
    }
}
