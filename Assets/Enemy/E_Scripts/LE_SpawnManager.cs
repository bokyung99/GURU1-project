using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LE_SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject LEnemy;
    public static bool spawnCtrl = false;
    public static GameObject[] clone;
    public static int spawnNum;
    public bool[] isSpawn;
    public static LE_SpawnManager instance;
    public float spawnTime;
    public float curTime;
    public int enemyCount;
    public int maxEnemyCount;
    public static int spawnSize;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        spawnSize = 9;

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
        clone[num] = Instantiate(LEnemy, spawnPoints[num]);
        isSpawn[num] = true;
    }
}
