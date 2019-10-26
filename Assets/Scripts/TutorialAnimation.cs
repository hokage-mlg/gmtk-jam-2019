using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    private GameObject pref;
    private float timeToNextAnim = 3f;
    private float currTime = 0f;
    private GameObject gm;
    private void Awake()
    {
        pref = Resources.Load("Tutorial") as GameObject;
    }

    private void Update()
    {
        currTime -= Time.deltaTime;
        if (!RelodScene.isVictory)
        {
            if (currTime < 0f)
            {
                gm = Instantiate(pref, pref.transform.position, Quaternion.identity);
                currTime = timeToNextAnim;
            }
            if (currTime > 0 && currTime < 1f)
            {
                Destroy(gm);
            }
        }
        else
        {
            if (gm)
                Destroy(gm);
        }
    }
}
