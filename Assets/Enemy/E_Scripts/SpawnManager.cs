using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject enemy;
    public static bool spawnCtrl = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new GameObject[2];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SpawnEnemy();
        }

    }

    private void SpawnEnemy()
    {
        spawnCtrl = true;
        int randNum = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[randNum].transform.position,
            spawnPoints[randNum].transform.rotation);
    }
}
