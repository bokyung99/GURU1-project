using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{

    //Hit 효과 오브젝트
    public GameObject hitEffect;

    //플레이어 체력 변수 (수정)
    public int playerHp = 10;
    //최대 체력 변수 (수정)
    int playerMaxHp = 10;
    //hp 슬라이더 변수
    public Slider hpSlider;

    //힐팩
    public GameObject hpPack;

    void Update()
    {
        UpdateB();
    }

    void UpdateB()
    {

        //현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영한다.
        hpSlider.value = (float)playerHp / (float)playerMaxHp;
        //hp가 0이 되면 플레이어 사망, 게임 종료
        if (playerHp == 0)
        {
            gameObject.SetActive(false);
        }

    }
    //플레이어의 피격 함수
    public void E_DamageAction(int E_damage)
    {
        //에너미의 공격력만큼 플레이어의 체력 감소
        playerHp -= E_damage;
        //플레이어의 체력이 0보다 크면 피격 효과를 출력
        if (playerHp > 0)
        {
            //피격 효과 코루틴을 시작
            StartCoroutine(PlayerHit());
        }
    }

    public void LE_DamageAction(int LE_damage)
    {
        //에너미의 공격력만큼 플레이어의 체력 감소
        playerHp -= LE_damage;
        //플레이어의 체력이 0보다 크면 피격 효과를 출력
        if (playerHp > 0)
        {
            //피격 효과 코루틴을 시작
            StartCoroutine(PlayerHit());
        }
    }

    //피격 효과 코루틴 함수
    IEnumerator PlayerHit()
    {
        //피격 UI 활성화
        hitEffect.SetActive(true);
        //0.3초간 대기
        yield return new WaitForSeconds(0.3f);
        //피격 UI 비활성화
        hitEffect.SetActive(false);
    }

    //충돌 감지
    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 게임 오브젝트의 태그가 HealPack일 때
        if (collision.gameObject.tag == "HealPack")
        {
            //플레이어 체력이 0이 아니고 최대가 아닐 때
            if (playerHp != 0 && playerHp < playerMaxHp)
            {
                //hpPack 제거
                Destroy(hpPack);
                //플레이어 체력 회복 (수정)
                playerHp += 1;
            }
        }
    }
}
