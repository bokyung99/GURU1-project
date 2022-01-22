using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    //플레이어 체력 변수 (수정)
    public int playerHp = 10;
    //최대 체력 변수 (수정)
    int playermaxHp = 10;
    //hp 슬라이더 변수
    public Slider hpSlider;

    //힐팩
    GameObject hpPack;

    void Update()
    {
        //현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영한다.
        hpSlider.value = (float)playerHp / (float)playermaxHp;
        //hp가 0이 되면 플레이어 사망, 게임 종료
        if (playerHp == 0)
        {

        }
    }

    //플레이어의 피격 함수
    public void DamageAction(int damage)
    {
        //에너미의 공격력만큼 플레이어의 체력 감소
        playerHp -= damage;
    }

    //충돌 감지
    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 게임 오브젝트의 태그가 HealPack일 때
        if (collision.gameObject.tag == "HealPack")
        {
            //hpPack 제거
            Destroy(hpPack);
            //플레이어 체력 회복 (수정)
            playerHp += 2;
        }
    }
}
