using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript3 : ArenaEnemySpawner
{
    private bool firstSpawn = true;
    private bool firstDeath = true;
    public bool isVictoryT = false;
    protected override void Update()
    {
        if (firstSpawn)
        {
            SpawnCertainMonsterWithoutName(enemyWaves[0].transform.GetChild(0).gameObject);
            SpawnCertainMonsterWithoutName(enemyWaves[0].transform.GetChild(1).gameObject);
            SpawnZone = true;
            SpawnСertainMonsterWithName(enemyWaves[0].transform.GetChild(2).gameObject, "Reluctance");
            MakeMonsterActive("Reluctance");
            firstSpawn = false;
        }

        if(isVictoryT && firstDeath)
        {
            KillThemAll();
            firstDeath = false;
        }
    }
}