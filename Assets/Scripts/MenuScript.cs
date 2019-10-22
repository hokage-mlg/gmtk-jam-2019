using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu = null;

    [SerializeField]
    private GameObject Levels = null;

    [SerializeField]
    private GameObject BackBtn = null;

    [SerializeField]
    private GameObject Options = null;

    [SerializeField]
    private GameObject lvlButton = null;

    public List<string> Scenes = new List<string>();

    private void Start()
    {
        MainMenu.SetActive(true);
        BackBtn.SetActive(false);
        InstLevels();
        UpdateLevels();
        Levels.SetActive(false);
        Options.SetActive(false);
    }

    public void ClickButtonContinue()
    {
        //PlayerPrefs.DeleteAll();
        //Debug.Log(PlayerPrefs.GetInt("CurrentScene"));
        if (PlayerPrefs.HasKey("CurrentScene"))
        {
            SceneManager.LoadScene(Scenes[PlayerPrefs.GetInt("CurrentScene")]);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentScene", 0);
            SceneManager.LoadScene(Scenes[0]);
        }
    }
    
    public void ClickButtonNewGame()
    {
        PlayerPrefs.SetInt("CurrentScene", 0);
        SceneManager.LoadScene(Scenes[0]);
    }
    public void ClickOptions()
    {
        MainMenu.SetActive(false);
        Options.SetActive(true);
        BackBtn.SetActive(true);
    }

    public void ClickOptionsBack()
    {
        MainMenu.SetActive(true);
        Options.SetActive(false);      
        BackBtn.SetActive(false);
    }
    public void ClickLevels()
    {
        MainMenu.SetActive(false);
        Levels.SetActive(true);
        BackBtn.SetActive(true);
    }

    public void ClickLevelsBack()
    {
        MainMenu.SetActive(true);
        Levels.SetActive(false);
        BackBtn.SetActive(false);
    }

    private void InstLevels()
    {
        for (int i = 0; i < Scenes.Count; i++)
        {
            GameObject newBtn = Instantiate(lvlButton) as GameObject;
            newBtn.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
            newBtn.name = i.ToString();
            newBtn.transform.GetChild(0).GetComponent<Text>().fontSize = 20;
            newBtn.transform.SetParent(Levels.transform);
            newBtn.transform.localScale = new Vector3(1, 1, 1);
            newBtn.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(Scenes[System.Int32.Parse(newBtn.name)]));
        }
    }

    public void UpdateLevels()
    {
        //Debug.Log(PlayerPrefs.GetInt("CurrentScene"));
        for(int i = 0;i < Levels.transform.childCount; i++)
        {
            GameObject Btn = Levels.transform.GetChild(i).gameObject;
            Image img = Btn.GetComponent<Image>();
            if (i <= PlayerPrefs.GetInt("CurrentScene"))
            {
                img.color = new Color(0f, 255f, 0f, 255f);
                Levels.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
            }
            else
            {
                img.color = new Color(255f, 0f, 0f, 255f);
                Levels.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
            }
        }
    }
    
    public void Exit()
    {
        Application.Quit();
    }
}
