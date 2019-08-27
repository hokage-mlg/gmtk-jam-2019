using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEnemy : MonoBehaviour
{
    [SerializeField]
    GameObject CanvasPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        var canvasEnemyName = Instantiate(CanvasPrefab);
        EnemyName = canvasEnemyName.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public static void SetCurrentEnemy(string enemyName, GameObject enemy)
    {
        EnemyName.text = enemyName;
    }

    private GameObject gameController;
    private static TMPro.TextMeshProUGUI EnemyName;
}
