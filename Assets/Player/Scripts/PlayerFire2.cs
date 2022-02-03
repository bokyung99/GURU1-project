using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire2 : MonoBehaviour
{
    //현재 총알 텍스트
    [SerializeField]
    private Text curBullet;
    //최대 총알 텍스트
    [SerializeField]
    private Text maxBullet;

    //발사 위치
    public GameObject firePosition;
    //투척 무기
    public GameObject bombFactory;

    //투척 파워
    public float throwPower = 15f;

    //피격 효과1 오브젝트(일반)
    public GameObject P1_bulletEffect;

    //피격 효과2 오브젝트(enemy맞을때)
    public GameObject P2_bulletEffect;


    //피격 효과 파티클 시스템1
    ParticleSystem ps1;
    //피격 효과 파티클 시스템2
    ParticleSystem ps2;

    //현재 탄알집에 남아있는 총알의 개수
    public int currentBulletCount = 50;
    //최대 총알의 개수 (수정)
    public int maxBulletCount = 50;
    //재장전 속도
    public float reloadTime = 1.0f;
    //재장전 할 때 총 발사 x
    private bool isReload = false;

    //총구 효과 오브젝트 배열
    public GameObject[] eff_Flash;

    private bool isShoot = false;

    //현재 총 종류
    public Gun currentGun;

  

    //총 데미지
    public int attackPower = 3;

    //트랜스폼 enemy
    Transform Enemy;


    //트랜스폼 lenemy
    Transform LEnemy;

    Animator anim;

    //총 현재 위치
    Vector3 gunDefaultPos;

    //반동 상태 확인
    bool isRebound = false;

    // 재장전 횟수를 1회로 제한하기 위한 변수
    int reloadCtrl = 0;

    //사운드
    public AudioClip reload;
    public AudioClip gunshot;
    public AudioClip throwbomb;
    public AudioClip enemyhit;
    public AudioClip lenemyhit;




    //재장전 함수
    IEnumerator ReloadCoroutine()
    {
        // 재장전 1회
        reloadCtrl++;
        isReload = true;

        /* 재장전 애니메이션 넣는곳 */
        anim.SetTrigger("reloading");
        print("재장전");

        //재장전 사운드
        GetComponent<AudioSource>().PlayOneShot(reload, reloadTime);

        yield return new WaitForSeconds(reloadTime);

        currentBulletCount = 50;

        isReload = false;

        // 재장전 통제 변수 0으로 다시 설정
        reloadCtrl = 0;
    }

    //총구 효과 코루틴 함수
    IEnumerator ShootEffectOn(float duration)
    {
        //랜덤하게 숫자 뽑음
        int num = Random.Range(0, eff_Flash.Length - 1);
        //이펙트 오브젝트 배열에서 뽑힌 숫자에 해당하는 이펙트 오브젝트 활성화
        eff_Flash[num].SetActive(true);
        //지정한 시간만큼 대기
        yield return new WaitForSeconds(duration);
        //이펙트 오브젝트 비활성화
        eff_Flash[num].SetActive(false);
    }

    /*
    //정조준 함수
    private void TryFineSight()
    {
        //마우스 오른쪽 버튼을 누르면 정조준

        if(Input.GetMouseButtonDown(1))

        {
            //정조준 상태 변환
            isFineSightMode = !isFineSightMode;
            //정조준 애니메이션
            anim.SetBool("FineSightMode", isFineSightMode);

            
            if (isFineSightMode)
            {
                //정조준 실행
                Camera.main.fieldOfView = 30f;
                //정조준 코루틴 실행
                StartCoroutine(FineSightActivateCoroutine());

                //콘솔창에 정조준 모드 입력
                print("finesightModeON");
            }
            else
            {
                //정조준 취소
                Camera.main.fieldOfView = 60f;
               
                //정조준 취소 코루틴 실행
                StartCoroutine(FineSightDeActivateCoroutine());

                //콘솔창에 정조준 취소 모드 입력
                print("finesightModeOFF");
            }
        }
    }

    //정조준 코루틴
    IEnumerator FineSightActivateCoroutine()
    {
        while(currentGun.transform.localPosition!= fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, fineSightOriginPos, 0.4f);

            //만약 총이 되돌아 오면
            if (Vector3.Distance(currentGun.transform.localPosition, fineSightOriginPos) < 0.01f)
            {
                //총 위치를 원래의 위치로
                currentGun.transform.localPosition = fineSightOriginPos;
                isRebound = false;

                break;
            }

            yield return null;
        }

        yield break;

    }

    //정조준 취소 코루틴
    IEnumerator FineSightDeActivateCoroutine()
    {
        while (currentGun.transform.localPosition != gunDefaultPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, gunDefaultPos, 0.4f);

            //만약 총이 되돌아 오면
            if (Vector3.Distance(currentGun.transform.localPosition, gunDefaultPos) < 0.01f)
            {
                //총 위치를 원래의 위치로
                currentGun.transform.localPosition = gunDefaultPos;
                isRebound = false;

                break;
            }


            yield return null;
        }

        yield break;

    }
    */


    //반동 함수
    void Recoil()
    {
        //총 위치를 원래대로
        currentGun.transform.localPosition = gunDefaultPos;
        //총 위치를 뒤로(반동)
        currentGun.transform.Translate(Vector3.back * 0.1f);

    }

    //반동 후 다시 돌아오는 함수
    IEnumerator Rebound()
    {
       
        isRebound = true;

        while (true)
        {
            //총 위치를 원래대로 천천히 되돌리기
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, gunDefaultPos, Time.deltaTime * 3.0f);

            //만약 총이 되돌아 오면
            if (Vector3.Distance(currentGun.transform.localPosition, gunDefaultPos) < 0.001f)
            {
                //총 위치를 원래의 위치로
                currentGun.transform.localPosition = gunDefaultPos;
                isRebound = false;

                break;
            }
            yield return null;
        }

        yield break;
    }


    void Start()
    {
        //피격 효과1 오브젝트에서 파티클 시스템 컴포넌트 가져오기
        ps1 = P1_bulletEffect.GetComponent<ParticleSystem>();
        //피격 효과2 오브젝트에서 파티클 시스템 컴포넌트 가져오기
        ps2 = P2_bulletEffect.GetComponent<ParticleSystem>();

        // 스폰이 됐을 때 Enemy 찾기
        /*
        if (SpawnManager.spawnCtrl == true)
        {
            Debug.Log("몬스터 찾기");
            //Enemy = GameObject.Find("Enemy").transform;
            //Enemy = Resources.Load("E_Prefabs/Enemy", typeof(GameObject)) as Transform;
            //Enemy = SpawnManager.clone.transform;
            //SpawnManager spawn;
        }

        */
        //LEnemy = GameObject.Find("LEnemy").transform;

        // 애니메이터 컴포넌트를 anim 변수에 불러온다.
        anim = GetComponent<Animator>();
        if (anim)
        {
            print("anim loading");
        }

        //총 현재 위치 저장
        gunDefaultPos = currentGun.transform.localPosition;


    }

    void Update()
    {
        // 게임 상태가 '게임 중'이 아니라면 조작 불가
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        UpdateA();
        UpdateB();
    }

    void UpdateA()
    {

        //키보드 R 누르면 수류탄 투척
        if (Input.GetKeyDown(KeyCode.R))
        {
            //수류탄 오브젝트 생성 후 수류탄의 생성위치를 발사 위치로
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            //수류탄 오브젝트의 rigidbody컴포넌트 가져오기
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //카메라정면 방향으로 수류탄에 물리적인 힘 가하기
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
            //수류탄 투척 사운드
            GetComponent<AudioSource>().PlayOneShot(throwbomb);
        }

        //만약 총알이 0개 이하이고, reloadCtrl가 0일 경우 재장전
        if (currentBulletCount <= 0 && reloadCtrl == 0)
        {
            StartCoroutine(ReloadCoroutine());
        }

        //마우스 왼쪽 버튼을 누르면 총알 발사
        if (Input.GetMouseButtonDown(0) && !isReload)
        {
            isShoot = true;

            //총구 효과 플레이
            StartCoroutine(ShootEffectOn(0.05f));

            //레이를 생성한 후 발사될 위치와 진행 방향 설정
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //레이가 부딪힌 대상의 정보를 저장할 변수를 생성
            RaycastHit hitInfo = new RaycastHit();

            //반동 함수 호출
            Recoil();

            //반동 되돌리기
            if(!isRebound)
            {
                StartCoroutine(Rebound());
            }


            //레이를 발사한 후 만일 부딪힌 물체가 있으면 피격 효과 표시
            if (Physics.Raycast(ray, out hitInfo))
            {
                //총 발사가 enemy를 맞는다면
                if (hitInfo.transform.tag == "Enemy")
                {
                    //피격 효과의 위치를 레이가 부딪힌 지점으로 이동
                    P2_bulletEffect.transform.position = hitInfo.point;

                    //피격 효과의 forward방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                    P2_bulletEffect.transform.forward = hitInfo.normal;

                    //피격 효과 플레이
                    ps2.Play();
                    //피격 사운드
                    GetComponent<AudioSource>().PlayOneShot(enemyhit);

                    for (int i = 0; i < SpawnManager.spawnSize; i++){

                        // 만약 clone[i]에 클론이 생성됐고, 레이에 부딪힌 대상이 clone[i]에 생성된 에너미면
                        if(SpawnManager.clone[i] != null &&
                            hitInfo.transform == SpawnManager.clone[i].transform)
                        {
                            // 그 에너미 공격
                            SpawnManager.clone[i].GetComponent<EnemyFSM>().HitEnemy(attackPower);
                        }
                    }

                    //총알 한개 감소
                    currentBulletCount--;
                }
                else if (hitInfo.transform.tag == "LEnemy")
                {
                    //피격 효과의 위치를 레이가 부딪힌 지점으로 이동
                    P2_bulletEffect.transform.position = hitInfo.point;

                    //피격 효과의 forward방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                    P2_bulletEffect.transform.forward = hitInfo.normal;

                    //피격 효과 플레이
                    ps2.Play();
                    //피격 사운드
                    GetComponent<AudioSource>().PlayOneShot(lenemyhit);

                    /*LEnemy 공격
                    LEnemy.GetComponent<LEnemyFSM>().HitEnemy(attackPower);
                    */

                    for (int i = 0; i < LE_SpawnManager.spawnSize; i++)
                    {
                        // 만약 clone[i]에 클론이 생성됐고, 레이에 부딪힌 대상이 clone[i]에 생성된 에너미면
                        if (LE_SpawnManager.clone[i] != null &&
                            hitInfo.transform == LE_SpawnManager.clone[i].transform)
                        {
                            // 그 에너미 공격
                            LE_SpawnManager.clone[i].GetComponent<LEnemyFSM>().HitEnemy(attackPower);
                        }
                    }

                    //총알 한개 감소
                    currentBulletCount--;
                }
                else
                {
                    //피격 효과의 위치를 레이가 부딪힌 지점으로 이동
                    P1_bulletEffect.transform.position = hitInfo.point;

                    //피격 효과의 forward방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                    P1_bulletEffect.transform.forward = hitInfo.normal;

                    //피격 효과 플레이
                    ps1.Play();

                    //총알 한개 감소
                    currentBulletCount--;

                }
            }

            isShoot=false;
        }

        //마우스 왼쪽 꾹 누르면 총 연사
        else if (Input.GetMouseButton(0) && !isReload)
        {
            isShoot= true;
           
            {

                //총구 효과 플레이
                StartCoroutine(ShootEffectOn(0.05f));
                //사운드
                GetComponent<AudioSource>().PlayOneShot(gunshot);

                //레이를 생성한 후 발사될 위치와 진행 방향 설정
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                //레이가 부딪힌 대상의 정보를 저장할 변수를 생성
                RaycastHit hitInfo = new RaycastHit();

                //반동 함수 호출
                Recoil();

                //반동 되돌리기
                if (!isRebound)
                {
                    StartCoroutine(Rebound());
                }


                //레이를 발사한 후 만일 부딪힌 물체가 있으면 피격 효과 표시
                if (Physics.Raycast(ray, out hitInfo))
                {
                    //총 발사가 enemy를 맞는다면
                    if (hitInfo.transform.tag == "Enemy")
                    {
                        //피격 효과의 위치를 레이가 부딪힌 지점으로 이동
                        P2_bulletEffect.transform.position = hitInfo.point;

                        //피격 효과의 forward방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                        P2_bulletEffect.transform.forward = hitInfo.normal;

                        //피격 효과 플레이
                        ps2.Play();

                        for (int i = 0; i < SpawnManager.spawnSize; i++)
                        {
                            // 만약 clone[i]에 클론이 생성됐고, 레이에 부딪힌 대상이 clone[i]에 생성된 에너미면
                            if (SpawnManager.clone[i] != null &&
                                hitInfo.transform == SpawnManager.clone[i].transform)
                            {
                                // 그 에너미 공격
                                SpawnManager.clone[i].GetComponent<EnemyFSM>().HitEnemy(attackPower);
                            }
                        }


                        //총알 한개 감소
                        currentBulletCount--;
                    }
                    else if (hitInfo.transform.tag == "LEnemy")
                    {
                        //피격 효과의 위치를 레이가 부딪힌 지점으로 이동
                        P2_bulletEffect.transform.position = hitInfo.point;

                        //피격 효과의 forward방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                        P2_bulletEffect.transform.forward = hitInfo.normal;

                        //피격 효과 플레이
                        ps2.Play();

                        /*LEnemy 공격
                        LEnemy.GetComponent<LEnemyFSM>().HitEnemy(attackPower);
                        */

                        for (int i = 0; i < LE_SpawnManager.spawnSize; i++)
                        {
                            // 만약 clone[i]에 클론이 생성됐고, 레이에 부딪힌 대상이 clone[i]에 생성된 에너미면
                            if (LE_SpawnManager.clone[i] != null &&
                                hitInfo.transform == LE_SpawnManager.clone[i].transform)
                            {
                                // 그 에너미 공격
                                LE_SpawnManager.clone[i].GetComponent<LEnemyFSM>().HitEnemy(attackPower);
                            }
                        }

                        //총알 한개 감소
                        currentBulletCount--;
                    }
                    else
                    {
                        //피격 효과의 위치를 레이가 부딪힌 지점으로 이동
                        P1_bulletEffect.transform.position = hitInfo.point;

                        //피격 효과의 forward방향을 레이가 부딪힌 지점의 법선 벡터와 일치시킨다.
                        P1_bulletEffect.transform.forward = hitInfo.normal;

                        //피격 효과 플레이
                        ps1.Play();

                        //총알 한개 감소
                        currentBulletCount--;

                    }

                }
            }

            isShoot = false;
        }

        
    }

    void UpdateB()
    {
        //int 변수 currentBulletCount를 string으로 변환
        string cBC = currentBulletCount.ToString();
        //현재 총알의 개수 텍스트로 나타내기
        curBullet.text = cBC;
        //int 변수 maxBulletCount string으로 변환
        string mBC = maxBulletCount.ToString();
        maxBullet.text = mBC;
    }
}
