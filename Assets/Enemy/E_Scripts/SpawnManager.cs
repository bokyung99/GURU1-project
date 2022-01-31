using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // ���� ��ġ ����
    public Transform[] spawnPoints;

    // ���� ��� 
    public GameObject enemy;

    // ���� ���� Ȯ�� ����
    public static bool spawnCtrl = false;

    // ������ ������Ʈ�� ���� ���� ����. playerFire ��ũ��Ʈ���� Ȱ��
    public static GameObject[] clone;

    // ���� ���� ����
    public float spawnTime;

    // ���� �ð�, ������ �ð� ����
    public float curTime;

    // ���� ���ʹ� ��
    public int enemyCount;

    // �ִ� ���ʹ� ��
    public int maxEnemyCount;

    // �ܺ� ��ũ��Ʈ���� ������ ���� ����
    public static SpawnManager instance;

    // �� �ڸ��� �ϳ��� ���ʹ̸� �����ǵ��� �����ϴ� ����
    public bool[] isSpawn;

    // ���� ������ ���� ����
    public static int spawnNum;

    // Start is called before the first frame update
    void Start()
    {
        // �ڱ� �ڽ����� �ʱ�ȭ
        instance = this;

        // ���� ��ġ �迭 ũ�� ���� 
        //spawnPoints = new GameObject[2];

        // �ʱ�ȭ
        clone = new GameObject[spawnPoints.Length];

        // �ʱ�ȭ
        isSpawn = new bool[spawnPoints.Length];

        // �ʱ�ȭ
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // index ��ȣ�� �´� �ڽ� ���� ������Ʈ�� ��ġ�� �ʱ�ȭ
            //spawnPoints[i] = transform.GetChild(i).gameObject;
            isSpawn[i] = false;
        }

    }

    void Update()
    {
        // ó�� ������ �� ������ ��ġ�� ���ʹ� ����
        if (spawnCtrl == false)
        {
            spawnNum = 0;
            SpawnEnemy(spawnNum);

            spawnNum = 1;
            SpawnEnemy(spawnNum);
        }

        // �����ϰ� �����ϱ� ���� ���� �ʱ�ȭ 
        spawnNum = Random.Range(0, spawnPoints.Length);

        // ���� �ð����� ������ �ð��� Ŀ����, �ִ� ���ʹ� ������ ���� ��
        if (curTime > spawnTime && enemyCount < maxEnemyCount )
        {
            // �� �ڸ��� ���ʹ̰� �������� ���� ��쿡
            Debug.Log("���� ����");
            if (!isSpawn[spawnNum])
            {
                // �� �ڸ��� ���ʹ� ���� 
                SpawnEnemy(spawnNum);
            }
        }
        // ���� �������� �����ϱ� ���� curTime ������ �ð� ����
        curTime += Time.deltaTime;

    }

    private void SpawnEnemy(int x)
    {
        // ���ʹ� �� ����
        enemyCount++;

        // �����Ǿ����� ���� �ð� �ʱ�ȭ 
        curTime = 0;
        
        // �����Ǿ����Ƿ� true �� �Ҵ�
        spawnCtrl = true;
        
        // �Ҵ�� �ڸ��� ���ʹ� ����
        clone[x] = Instantiate(enemy, spawnPoints[x]);

        // �����Ǿ����� true �� �Ҵ�
        isSpawn[x] = true;
    }
}
