using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class RelodScene : MonoBehaviour
{
    [SerializeField]
    protected string NextSceneName = "";
    [SerializeField]
    protected int SceneNumber = 0;

    public bool isPointVictory { get; } = false;
    public int pointsToVictory;
    public static bool isVictory = false;
    public int TotalValue = 0;
    private float maxvalue = 0;

    protected static GameObject Canvas;

    protected virtual void Awake()
    {
        ArenaEnemySpawner spawn = GetComponent<ArenaEnemySpawner>();
        CharacterLife.isDeath = false;
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        var arena = GetComponent<ArenaEnemySpawner>();
        maxvalue = arena.EnemyCount();
        if (isPointVictory)
        {
            if (!spawn.IsInfSpawn && pointsToVictory > spawn.baseEnemyCount())
            {
                pointsToVictory = spawn.baseEnemyCount();
            }

            Canvas.transform.GetChild(3).gameObject.SetActive(true);
            Canvas.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else
        {
            Canvas.transform.GetChild(0).gameObject.SetActive(false);
        }
        isVictory = false;
        Debug.Log(isPointVictory);
    }

    public void CurrentCount(int val)
    {
        TotalValue = TotalValue + val;
    }

    private void Update()
    {
        Victory();
        Reload();
    }

    protected virtual void Victory()
    {
        if (isPointVictory)
        {
            if(Input.GetKeyDown(KeyCode.F) && Canvas.transform.GetChild(3).gameObject.activeSelf)
            {
                Canvas.transform.GetChild(3).gameObject.SetActive(false);
                Canvas.transform.GetChild(0).gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
            if (TotalValue >= pointsToVictory)
            {
                isVictory = true;
                Canvas.transform.GetChild(0).gameObject.SetActive(true);
                Debug.Log(NextSceneName);
                if (Input.GetKeyDown(KeyCode.F) && !CharacterLife.isDeath)
                {
                    if (PlayerPrefs.GetInt("CurrentScene") < SceneNumber + 1)
                        PlayerPrefs.SetInt("CurrentScene", SceneNumber + 1);
                    Canvas.transform.GetChild(0).gameObject.SetActive(false);
                    SceneManager.LoadScene(NextSceneName);
                }
            }
        }
        else
        {
            if (TotalValue >= maxvalue)
            {
                isVictory = true;
                Canvas.transform.GetChild(0).gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.F) && !CharacterLife.isDeath)
                {
                    if (PlayerPrefs.GetInt("CurrentScene") < SceneNumber + 1)
                        PlayerPrefs.SetInt("CurrentScene", SceneNumber + 1);
                    Canvas.transform.GetChild(0).gameObject.SetActive (false);
                    SceneManager.LoadScene(NextSceneName);
                }
            }
        }
    }

    protected virtual void Reload()
    {
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

/*[CustomEditor(typeof(RelodScene))]
public class MyEditorClass : Editor
{
    public override void OnInspectorGUI()
    {
        // If we call base the default inspector will get drawn too.
        // Remove this line if you don't want that to happen.
        //base.OnInspectorGUI();

        RelodScene myReload = target as RelodScene;

        myReload.NextSceneName = EditorGUILayout.TextField("NextLevel", myReload.NextSceneName);
        myReload.SceneNumber = EditorGUILayout.IntField("Scene Number", myReload.SceneNumber);
        myReload.isPointVictory = EditorGUILayout.Toggle("isPointVictory", myReload.isPointVictory);

        if (myReload.isPointVictory)
        {
            myReload.pointsToVictory = EditorGUILayout.IntField("Points to victory:", myReload.pointsToVictory);

        }
    }
}*/
