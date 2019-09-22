using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RelodScene : MonoBehaviour
{
    [SerializeField]
    private int SceneNumber = 0;
    private static bool isVictory = false;
    private float TotalValue = 0;
    private float maxvalue = 0;
    [SerializeField]
    private string NextSceneName = "";
    private static GameObject Canvas;

    private void Start()
    {
        CharacterLife.isDeath = false;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        var arena = GetComponent<ArenaEnemySpawner>();
        Canvas.transform.GetChild(0).gameObject.SetActive(false);
        maxvalue = arena.EnemyCount();
        isVictory = false;
    }

    public void CurrentCount(int val)
    {
        TotalValue = TotalValue + val;
    }

    private void Update()
    {
        if (TotalValue == maxvalue)
        {
            isVictory = true;
            Canvas.transform.GetChild(0).gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F) && !CharacterLife.isDeath)
            {
                if(PlayerPrefs.GetInt("CurrentScene") < SceneNumber + 1)
                    PlayerPrefs.SetInt("CurrentScene", SceneNumber + 1);
                Canvas.transform.GetChild(0).gameObject.SetActive(false);
                SceneManager.LoadScene(NextSceneName);
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && (!isVictory || CharacterLife.isDeath))
        {
            TotalValue = 0;
            Time.timeScale = 1;
            Canvas.transform.GetChild(1).gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static void PressR()
    {
        if (isVictory && !CharacterLife.isDeath) return;
        Canvas.transform.GetChild(1).gameObject.SetActive(true);
    }
}
