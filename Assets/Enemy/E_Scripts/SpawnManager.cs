using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public static bool spawnCtrl = false;
    public static GameObject[] clone;
    public static int spawnNum;
    public bool[] isSpawn;
    public static SpawnManager instance;
    public float spawnTime;
    public float curTime;
    public int enemyCount;
    public int maxEnemyCount;
    public static int spawnSize;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        spawnSize = 14;

        // 다시 시작하거나 다른 씬에서 넘어올 때도, 몬스터 배치되도록
        spawnCtrl = false;

        //spawnPoints = new GameObject[2];

        clone = new GameObject[spawnPoints.Length];
        isSpawn = new bool[spawnPoints.Length];

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            //spawnPoints[i] = transform.GetChild(i).gameObject;
            isSpawn[i] = false;
        }
    }

    void Update()
    {
        if (spawnCtrl == false)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                SpawnEnemy(i);
            }
        }

        spawnNum = Random.Range(0, spawnPoints.Length);

        if (curTime > spawnTime && enemyCount < maxEnemyCount)
        {
            if (!isSpawn[spawnNum])
            {
                SpawnEnemy(spawnNum);
            }
        }
        curTime += Time.deltaTime;
    }

    private void SpawnEnemy(int num)
    {
        enemyCount++;
        curTime = 0;
        spawnCtrl = true;
        clone[num] = Instantiate(enemy, spawnPoints[num]);
        isSpawn[num] = true;
    }
}
