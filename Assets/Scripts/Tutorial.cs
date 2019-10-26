using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private GameObject Canvas;
    private GameObject Player;
    void Start()
    {
        Tutorial1Victory = false;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        Player = GameObject.FindGameObjectWithTag("Player");
        Canvas.transform.GetChild(0).gameObject.SetActive(false);
        Canvas.transform.GetChild(1).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
      if (Tutorial1Victory)
        {
            Canvas.transform.GetChild(0).gameObject.SetActive(true);
        }
      
      if (Input.GetKeyDown (KeyCode.F) && Tutorial1Victory)
        {
            SceneManager.LoadScene("TutorialScene2");
        }

    }
    public GameObject Portal;
    public static bool Tutorial1Victory;
}
