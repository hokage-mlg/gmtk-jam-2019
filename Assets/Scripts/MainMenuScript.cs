using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject TitleScreen = null;
    private TitleScreenContainer titleScreenContainer;

    [SerializeField]
    private GameObject SettingsScreen = null;

    [SerializeField]
    private GameObject StageSelectionScreen = null;
    

    #region Monobehaviour functions

    void Start()
    {
        SetActiveTitle(true);
        GetScenesInBuild();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivateEverything();
            SetActiveTitle(true);
        }
    }

    #endregion

    #region Technical Functions
    void GetScenesInBuild()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string[] scenesInBuild = new string[sceneCount];
        for (int i = 0; i < sceneCount; i++)
        {
            scenesInBuild[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }
        scenes = scenesInBuild;
    }

    void GrayLoadNotPlayedYet()
    {
        // TODO ВРЕМЕННЫЙ ФИКС
        if (true || (!PlayerPrefs.HasKey("CurrentScene") || PlayerPrefs.GetInt("CurrentScene") == -1))
        {
            PlayerPrefs.SetInt("CurrentScene", -1);
            var btn = titleScreenContainer.GetButtonContinue();
            var btnImage = btn.GetComponent<Image>();
            btnImage.color = new Color(0.37f, 0.37f, 0.37f); // gray
            btn.GetComponent<Button>().enabled = false;
        }
    }

    private void DeactivateEverything()
    {
        SetActiveTitle(false);
        SetActiveSettings(false);
        SetActiveStageSelection(false);
    }

    #endregion

    #region Public Functions

    // old and simple
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ClickButtonSettings()
    {
        DeactivateEverything();
        SetActiveSettings(true);
    }

    // moves to the stage selection screen
    public void ClickButtonNewGame()
    {
        DeactivateEverything();
        SetActiveStageSelection(true);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("CurrentScene", -1);
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion

    #region Screen Layour Activator Functions

    private void SetActiveTitle(bool active = true)
    {
        if (active)
        {
            TitleScreen.SetActive(true);
            titleScreenContainer = TitleScreen.GetComponent<TitleScreenContainer>();
            GrayLoadNotPlayedYet();
        }
        else
        {
            TitleScreen.SetActive(false);
        }
    }

    private void SetActiveSettings(bool active = true)
    {
        SettingsScreen.SetActive(active);
    }

    private void SetActiveStageSelection(bool active = true)
    {
        StageSelectionScreen.SetActive(active);
    }

    #endregion

    private string[] scenes = null;
}
