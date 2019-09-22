using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugSceneChanger : MonoBehaviour
{
    [SerializeField]
    private readonly string[] SceneNames = {
        "Level1",
        "Level2",
        "Level3",
        "Level4",
        "Level5",
        "Level6",
        "Level7",
        "Level8",
        "Level9",
        "TutorialScene",
    };

    [SerializeField]
    private readonly KeyCode[] Buttons = { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
        KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9, KeyCode.Alpha0
    };

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Input.GetKeyDown(Buttons[i]) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt))
            {
                SceneManager.LoadScene(SceneNames[i]);
            }
        }
    }
}
