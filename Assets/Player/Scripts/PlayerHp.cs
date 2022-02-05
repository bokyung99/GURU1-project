using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHp : MonoBehaviour
{
    //Hit 효과 오브젝트
    public GameObject hitEffect;

    //플레이어 hp 텍스트
    [SerializeField]
    private Text textHp;

    //플레이어 체력 변수
    public int playerHp = 100;
    //최대 체력 변수
    int playerMaxHp = 100;
    //hp 슬라이더 변수
    public Slider hpSlider;

    // 아이템 총 습득 수
    public int itemTotalNum = 0;

    // 아이템1,2,3 습득 현황 확인 변수
    public int isItem1 = 0;
    public int isItem2 = 0;
    public int isItem3 = 0;

    //힐팩
    public GameObject hpPack1;
    public GameObject hpPack2;
    public GameObject hpPack3;
    public GameObject hpPack4;
    public GameObject hpPack5;

    //힐팩 습득 확인용 변수(튜토리얼)
    public bool GetHealPack = false;
    //목표아이템 습득 확인용 변수(튜토리얼)
    public bool GetItem1 = false;

    //피격 사운드
    public AudioClip playerhit;
    //힐팩 습득 사운드
    public AudioClip gethealpack;
    //아이템 습득 사운드
    public AudioClip getitem;

    void Update()
    {
        UpdateB();
    }

    void UpdateB()
    {
        //플레이어의 체력이 0 이상일 때
        if (playerHp >= 0)
        {
            //int 변수 playerHp를 string으로 변환
            string php = playerHp.ToString();
            //플레이어 hp 텍스트로 나타내기
            textHp.text = php;
        }

        //현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영
        hpSlider.value = (float)playerHp / (float)playerMaxHp;

        //hp가 0이 되면 게임 오버 나오는 것으로 대체. 플레이어 사라질 시 카메라 사라짐
        if (playerHp == 0)
        {
            //gameObject.SetActive(false);
        }

    }
    //플레이어의 피격 함수
    public void E_DamageAction(int E_damage)
    {
        //플레이어의 체력이 0보다 크면 피격 효과를 출력
        if (playerHp > 0)
        {
            //에너미의 공격력만큼 플레이어의 체력 감소
            playerHp -= E_damage;
            //피격 효과 코루틴을 시작
            StartCoroutine(PlayerHit());
        }
    }

    public void LE_DamageAction(int LE_damage)
    {
        //플레이어의 체력이 0보다 크면 피격 효과를 출력
        if (playerHp > 0)
        {
            //에너미의 공격력만큼 플레이어의 체력 감소
            playerHp -= LE_damage;
            //피격 효과 코루틴을 시작
            StartCoroutine(PlayerHit());
        }
    }

    //피격 효과 코루틴 함수
    IEnumerator PlayerHit()
    {
        //피격 사운드
        GetComponent<AudioSource>().PlayOneShot(playerhit, 0.5f);
        //피격 UI 활성화
        hitEffect.SetActive(true);
        //0.3초간 대기
        yield return new WaitForSeconds(0.3f);
        //피격 UI 비활성화
        hitEffect.SetActive(false);
    }

    //충돌 감지
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag ("HealPack1"))
        {
            //플레이어 체력이 0이 아니고 최대가 아닐 때
            if (playerHp < playerMaxHp && playerHp > 0) 
            {
                //0.5초 후 hpPlus 함수 실행
                Invoke("hpPlus1", 0.5f);

            }
            //힐팩 습득 확인(튜토리얼)
            GetHealPack = true;
        }
        if (other.CompareTag("HealPack2"))
        {
            //플레이어 체력이 0이 아니고 최대가 아닐 때
            if (playerHp < playerMaxHp && playerHp > 0)
            {
                //0.5초 후 hpPlus 함수 실행
                Invoke("hpPlus2", 0.5f);

            }
        }
        if (other.CompareTag("HealPack3"))
        {
            //플레이어 체력이 0이 아니고 최대가 아닐 때
            if (playerHp < playerMaxHp && playerHp > 0)
            {
                //0.5초 후 hpPlus 함수 실행
                Invoke("hpPlus3", 0.5f);

            }
        }
        if (other.CompareTag("HealPack4"))
        {
            //플레이어 체력이 0이 아니고 최대가 아닐 때
            if (playerHp < playerMaxHp && playerHp > 0)
            {
                //0.5초 후 hpPlus 함수 실행
                Invoke("hpPlus4", 0.5f);

            }
        }
        if (other.CompareTag("HealPack5"))
        {
            //플레이어 체력이 0이 아니고 최대가 아닐 때
            if (playerHp < playerMaxHp && playerHp > 0)
            {
                //0.5초 후 hpPlus 함수 실행
                Invoke("hpPlus5", 0.5f);

            }
        }

        else if (other.tag == "Item1")
        {
            GetComponent<AudioSource>().PlayOneShot(getitem,1.0f);
            isItem1++;
            itemTotalNum++;
            Destroy(other.gameObject);
            if (itemTotalNum == 3)
            {
                SceneManager.LoadScene("BossMap");
            }

            //목표아이템 습득 확인(튜토리얼)
            GetItem1 = true;
        }
        else if (other.tag == "Item2")
        {
            GetComponent<AudioSource>().PlayOneShot(getitem, 1.0f);
            isItem2++;
            itemTotalNum++;
            Destroy(other.gameObject);
            if (itemTotalNum == 3)
            {
                SceneManager.LoadScene("BossMap");
            }
        }
        else if (other.tag == "Item3")
        {
            GetComponent<AudioSource>().PlayOneShot(getitem, 1.0f);
            isItem3++;
            itemTotalNum++;
            Destroy(other.gameObject);
            if (itemTotalNum == 3)
            {
                SceneManager.LoadScene("BossMap");
            }
        }
        //보스 에니미와 충돌 할 때 돌진공격이 들어가도록   
        if (other.tag == "BossEnemy")
        {
            if (GameObject.Find("BossEnemy").GetComponent<BossEnemyFSM>().runattack)
            { E_DamageAction(5); }

        }
    }
    void hpPlus1()
    {
        //힐 사운드
        GetComponent<AudioSource>().PlayOneShot(gethealpack, 1.0f);
        if (playerHp + 10 <= playerMaxHp)
        {
            //플레이어 체력 회복
            playerHp += 10;
        }
        
        else if (playerHp + 10 > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }
        //hpPack 제거
        Destroy(hpPack1);
    }
    void hpPlus2()
    {
        //힐 사운드
        GetComponent<AudioSource>().PlayOneShot(gethealpack, 1.0f);
        if (playerHp + 10 <= playerMaxHp)
        {
            //플레이어 체력 회복
            playerHp += 10;
        }

        else if (playerHp + 10 > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }
        //hpPack 제거
        Destroy(hpPack2);
    }
    void hpPlus3()
    {
        //힐 사운드
        GetComponent<AudioSource>().PlayOneShot(gethealpack, 1.0f);
        if (playerHp + 10 <= playerMaxHp)
        {
            //플레이어 체력 회복
            playerHp += 10;
        }

        else if (playerHp + 10 > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }
        //hpPack 제거
        Destroy(hpPack3);
    }
    void hpPlus4()
    {
        //힐 사운드
        GetComponent<AudioSource>().PlayOneShot(gethealpack, 1.0f);
        if (playerHp + 10 <= playerMaxHp)
        {
            //플레이어 체력 회복
            playerHp += 10;
        }

        else if (playerHp + 10 > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }
        //hpPack 제거
        Destroy(hpPack4);
    }
    void hpPlus5()
    {
        //힐 사운드
        GetComponent<AudioSource>().PlayOneShot(gethealpack, 1.0f);
        if (playerHp + 10 <= playerMaxHp)
        {
            //플레이어 체력 회복
            playerHp += 10;
        }

        else if (playerHp + 10 > playerMaxHp)
        {
            playerHp = playerMaxHp;
        }
        //hpPack 제거
        Destroy(hpPack5);
    }
}
