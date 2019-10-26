using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCursorCreation : MonoBehaviour
{
    // TODO: use CharacterShooting class inheritance maybe

    [SerializeField]
    private GameObject mouseCursorObj = null;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Instantiate(mouseCursorObj);
    }

    void Update()
    {
        Cursor.visible = false;
    }
}
