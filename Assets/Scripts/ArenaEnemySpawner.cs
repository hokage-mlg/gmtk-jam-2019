using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class ArenaEnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float timeToEachSpawn = 5;
    [SerializeField]
    private float timeToNextSpawn = 0;

    [SerializeField]
    protected GameObject[] enemyWaves = null;

    [SerializeField]
    protected bool SpawnZone = false;

    [SerializeField]
    private bool isInfSpawn;
    static Random random = new Random();


    public Vector2 RoomBounds = new Vector2(15, 10);

    private List<int> randomSequence;
    private int sequenceIndex = 0;

    [SerializeField]
    private EvilDictionary evilDictionary = null;

    void Awake()
    {
        InitializeFields();
        
        roomLighting = GetComponent<RoomLighting>();
        scenesController = GetComponent<RelodScene>();    
        isPointVictory = scenesController.isPointVictory;

        // Get reference for UI current enemy name
        currentEnemy = GetComponent<CurrentEnemy>();
        GameObject SpawnSquare = GameObject.FindGameObjectWithTag("SpawnZone");
        if (SpawnSquare)
        {
            SpawnScript = SpawnSquare.GetComponent<SpawnZoneScript>();
        }

        currentEvilDictionary = evilDictionary;
        randomSequence = GenerateRandom(currentEvilDictionary.EvilNames.Length / 2, currentEvilDictionary.EvilNames.Length - 1);
    }

    private void InitializeFields()
    {
        anyBoy = false;
        boysList = new List<GameObject>();
    }
    
    public static List<int> GenerateRandom(int count, int max)
    {
        List<int> result = new List<int>(count);

        // generate count random values.
        HashSet<int> candidates = new HashSet<int>();
        for (int top = max - count; top < max; top++)
        {
            // May strike a duplicate.
            int value = Random.Range(0, top);
            if (candidates.Add(value))
            {
                result.Add(value);
            }
            else
            {
                result.Add(top);
                candidates.Add(top);
            }
        }

        return result;
    }

    public static void ChangeTheBoy(GameObject oldBoy)
    {
        if (scenesController)
        {
            scenesController.CurrentCount(1);
        }
        roomLighting.AddToLight(1);

        boysList.Remove(oldBoy);
        if (boysList.Count != 0)
        {
            var nextBoy = boysList[Random.Range(0, boysList.Count)];
            CurrentEnemy.SetCurrentEnemy(nextBoy.GetComponentInChildren<TMPro.TextMeshPro>().text, nextBoy);
            nextBoy.GetComponent<MonsterLife>().MakeBoy();
            currentBoy = nextBoy;
        }
        else
        {
            anyBoy = false;
        }
    }

    private Vector2 RandomBorderSpawnPos()
    {
        var spawnPosition = new Vector2();
        var dice = Random.Range(0, 4);
        // North, South, East, West spawn positions
        switch (dice)
        {
            case 0:
                spawnPosition.y = RoomBounds.y;
                spawnPosition.x = Random.Range(-RoomBounds.x, RoomBounds.x);
                break;
            case 1:
                spawnPosition.y = -RoomBounds.y;
                spawnPosition.x = Random.Range(-RoomBounds.x, RoomBounds.x);
                break;
            case 2:
                spawnPosition.x = RoomBounds.x;
                spawnPosition.y = Random.Range(-RoomBounds.y, RoomBounds.y);
                break;
            case 3:
                spawnPosition.x = -RoomBounds.x;
                spawnPosition.y = Random.Range(-RoomBounds.y, RoomBounds.y);
                break;
        }
        return spawnPosition;
    }

    protected void SetMonsterPosition(GameObject enemy)
    {
        enemy.transform.position = RandomBorderSpawnPos();
    }

    private void SpawnMonsters(int waveNum)
    {
        var enemyWave = Instantiate(enemyWaves[waveNum], transform.position, Quaternion.identity);

        int enemiesInWave = enemyWave.transform.childCount;
        
        for (int i = 0; i < enemiesInWave; i++)
        {
            var enemy = enemyWave.transform.GetChild(i).gameObject;
            if (i == 0)
            {
                // If there is no active enemy name
                if (!anyBoy)
                {
                    anyBoy = true;
                    CurrentEnemy.SetCurrentEnemy(currentEvilDictionary.EvilNames[randomSequence[sequenceIndex]], enemy);
                    enemy.GetComponent<MonsterLife>().MakeBoy();
                    currentBoy = enemy;
                }
            }
            // Set random enemy name from the dictionary
            enemy.GetComponentInChildren<TMPro.TextMeshPro>().text = currentEvilDictionary.EvilNames[randomSequence[sequenceIndex]];
            boysList.Add(enemy);

            if (!SpawnZone)
            {
                SetMonsterPosition(enemy);
            }
            else
            {
                enemy.transform.position = SpawnScript.SpawnPosition();
            }

            sequenceIndex++;
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        EnemySpawnUpdate();
    }

    protected void KillThemAll()
    {
        while (boysList.Count != 0)
        {
           boysList[0].GetComponent<MonsterLife>().Damage(999, ignoreInvulurability: true);
        }
    }

    protected void EnemySpawnUpdate()
    {
        if (isInfSpawn)
        {
            timeToNextSpawn -= Time.deltaTime;
            if (timeToNextSpawn < 0 && spawnIndex < enemyWaves.GetLength(0) && !RelodScene.isVictory)
            {
                timeToNextSpawn = timeToEachSpawn;
                SpawnMonsters(spawnIndex);
                spawnIndex++;

                if (spawnIndex == enemyWaves.GetLength(0))
                {
                    spawnIndex = 0;
                }
            }

            if (RelodScene.isVictory)
            {
                KillThemAll();
            }
        }
        else
        {
            timeToNextSpawn -= Time.deltaTime;
            if (timeToNextSpawn < 0 && spawnIndex < enemyWaves.GetLength(0))
            {
                timeToNextSpawn = timeToEachSpawn;
                SpawnMonsters(spawnIndex);
                spawnIndex++;

                if (spawnIndex > enemyWaves.GetLength(0)) { }
            }
        }
    }

    public int EnemyCount()
    {
        if (isPointVictory)
        {
            return scenesController.pointsToVictory;
        }
        else
        {
          return baseEnemyCount ();
        }
    }

    public int baseEnemyCount ()
    {
        EnemiesCount = 0;
        foreach (var e in enemyWaves)
        {
            EnemiesCount += e.transform.childCount;
        }
        int res = EnemiesCount;
        EnemiesCount = 0;
        return res;
    }

    public void SpawnСertainMonsterWithName(GameObject monster, string name)
    {
        var enemy = Instantiate(monster, transform.position, Quaternion.identity);
        if (!anyBoy)
        {
            anyBoy = true;
            CurrentEnemy.SetCurrentEnemy(name, enemy);
            enemy.GetComponent<MonsterLife>().MakeBoy();
            currentBoy = enemy;
        }
        enemy.GetComponentInChildren<TMPro.TextMeshPro>().text = name;
        boysList.Add(enemy);
        //roomLighting.AddToLight(1);

        if (!SpawnZone)
        {
            SetMonsterPosition(enemy);
        }
        else
        {
            enemy.transform.position = SpawnScript.SpawnPosition();
        }
    }

    public void SpawnCertainMonsterWithoutName(GameObject monster)
    {

        var enemy = Instantiate(monster, transform.position, Quaternion.identity);
        if (!anyBoy)
        {
            anyBoy = true;
            CurrentEnemy.SetCurrentEnemy(currentEvilDictionary.EvilNames[randomSequence[sequenceIndex]], enemy);
            enemy.GetComponent<MonsterLife>().MakeBoy();
            currentBoy = enemy;
        }

        enemy.GetComponentInChildren<TMPro.TextMeshPro>().text = currentEvilDictionary.EvilNames[randomSequence[sequenceIndex]];
        boysList.Add(enemy);
        //roomLighting.AddToLight(1);

        if (!SpawnZone)
        {
            SetMonsterPosition(enemy);
        }
        else
        {
            enemy.transform.position = SpawnScript.SpawnPosition();
        }

        sequenceIndex++;
    }

    public void MakeMonsterActive(string name1)
    {
        GameObject currentEnemy1 = boysList.Find(x => x.GetComponentInChildren<TMPro.TextMeshPro>().text == name1);
        if (currentEnemy)
        {
            currentBoy.GetComponent<MonsterLife>().MakeNoBoy();
            currentEnemy1.GetComponent<MonsterLife>().MakeBoy();

            CurrentEnemy.SetCurrentEnemy(name1, currentEnemy1);
            boysList.Remove(currentEnemy1);
            boysList.Insert(0, currentEnemy1);
            currentBoy = currentEnemy1;
        }
    }

    private int EnemiesCount = 0;
    private static bool anyBoy = false;
    protected int spawnIndex = 0;
    private EvilDictionary currentEvilDictionary;
    private Queue<string> enemyOrder;

    protected static GameObject currentBoy;

    protected CurrentEnemy currentEnemy;

    private SpawnZoneScript SpawnScript;
    protected static List<GameObject> boysList = new List<GameObject>();

    private static RoomLighting roomLighting;
    private static RelodScene scenesController;
    private bool isPointVictory = false;
    public bool IsInfSpawn { get { return isInfSpawn; } }
}
