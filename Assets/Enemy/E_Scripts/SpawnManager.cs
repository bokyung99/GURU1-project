using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // 스폰 위치 변수
    public Transform[] spawnPoints;

    // 스폰 대상 
    public GameObject enemy;

    // 스폰 여부 확인 변수
    public static bool spawnCtrl = false;

    // 스폰된 오브젝트를 담을 정적 변수. playerFire 스크립트에서 활용
    public static GameObject[] clone;

    // 스폰 간격 변수
    public float spawnTime;

    // 현재 시간, 누적된 시간 변수
    public float curTime;

    // 현재 에너미 수
    public int enemyCount;

    // 최대 에너미 수
    public int maxEnemyCount;

    // 외부 스크립트에서 접근을 위해 선언
    public static SpawnManager instance;

    // 한 자리에 하나의 에너미만 생성되도록 제어하는 변수
    public bool[] isSpawn;

    // 스폰 관리를 위한 변수
    public static int spawnNum;

    // Start is called before the first frame update
    void Start()
    {
        // 자기 자신으로 초기화
        instance = this;

        // 스폰 위치 배열 크기 설정 
        //spawnPoints = new GameObject[2];

        // 초기화
        clone = new GameObject[spawnPoints.Length];

        // 초기화
        isSpawn = new bool[spawnPoints.Length];

        // 초기화
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // index 번호에 맞는 자식 게임 오브젝트의 위치로 초기화
            //spawnPoints[i] = transform.GetChild(i).gameObject;
            isSpawn[i] = false;
        }

    }

    void Update()
    {
        // 처음 시작할 때 각각의 위치에 에너미 생성
        if (spawnCtrl == false)
        {
            spawnNum = 0;
            SpawnEnemy(spawnNum);

            spawnNum = 1;
            SpawnEnemy(spawnNum);
        }

        // 랜덤하게 스폰하기 위한 변수 초기화 
        spawnNum = Random.Range(0, spawnPoints.Length);

        // 스폰 시간보다 누적된 시간이 커지고, 최대 에너미 수보다 작을 때
        if (curTime > spawnTime && enemyCount < maxEnemyCount )
        {
            // 그 자리에 에너미가 생성되지 않은 경우에
            Debug.Log("몬스터 생성");
            if (!isSpawn[spawnNum])
            {
                // 그 자리에 에너미 생성 
                SpawnEnemy(spawnNum);
            }
        }
        // 일정 간격으로 스폰하기 위해 curTime 변수에 시간 누적
        curTime += Time.deltaTime;

    }

    private void SpawnEnemy(int x)
    {
        // 에너미 수 조절
        enemyCount++;

        // 생성되었으니 누적 시간 초기화 
        curTime = 0;
        
        // 생성되었으므로 true 값 할당
        spawnCtrl = true;
        
        // 할당된 자리에 에너미 생성
        clone[x] = Instantiate(enemy, spawnPoints[x]);

        // 생성되었으니 true 값 할당
        isSpawn[x] = true;
    }
}
