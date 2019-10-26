using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenContainer : MonoBehaviour
{
    [SerializeField]
    public GameObject NewGame;

    [SerializeField]
    public GameObject LoadGame;

    [SerializeField]
    public GameObject Settings;

    [SerializeField]
    public GameObject Credits;

    [SerializeField]
    public GameObject Exit;

    public GameObject GetButtonContinue()
    {
        return LoadGame;
    }
}
