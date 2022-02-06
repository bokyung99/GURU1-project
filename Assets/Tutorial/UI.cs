using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    //튜토리얼 ui 배열
    public GameObject[] popUps;
    //튜토리얼 ui 인덱스
    private int popUpIndex;
    //이동키 카운트
    public int count = 4;
    private int tabcount = 2;
    private int alphacount = 2;
    private int f1count = 2;
    private int finesightcount = 2;

    void Update()
    {
        //튜토리얼 ui 실행
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        //튜토리얼0: 이동
        if (popUpIndex == 0)
        {
            //이동키 w,s,a,d 누르면 다음 튜토리얼로
            if (Input.GetKeyDown(KeyCode.W))
            {
                count--;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                count--;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                count--;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                count--;
            }

            if (count <= 0)
            {
                popUpIndex++;
            }

        }
        //튜토리얼 1: 점프
        else if (popUpIndex == 1)
        {
            //스페이스바 누르면 다음 튜토리얼로
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;

                GameObject.Find("Player").GetComponent<PlayerHp>().LE_DamageAction(10);
            }
        }
        //튜토리얼 2: 힐팩 습득
        else if (popUpIndex == 2)
        {
            //힐팩 먹으면 다음 튜토리얼로
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetHealPack)
            {
                popUpIndex++;
            }
        }
        //튜토리얼 3: 목표 아이템 습득
        else if (popUpIndex == 3)
        {
            //목표 아이템 습득하면 다음 튜토리얼로 
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetItem1)
            {
                popUpIndex++;
            }
        }
        //튜토리얼 4: 목표 아이템 습득 현황
        else if (popUpIndex == 4)
        {
            //tab버튼 누르면
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                tabcount--;
               
            }
            if(tabcount<=0)
            {
                popUpIndex++;
            }
        }
        //튜토리얼 5: 무기 파밍
        else if(popUpIndex==5)
        {
            //새로운 무기를 얻으면
            if (GameObject.Find("Player").GetComponent<Weapon_T>().getWeapon)
            {
                popUpIndex++;
            }
        }
        //튜토리얼 6: 무기 교체
        else if (popUpIndex == 6)
        {
            //숫자키패드 1 또는 2를 누르면
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                alphacount--;
            }
            if(alphacount<=0)
            {
                popUpIndex++;
            }

        }
        //튜토리얼 7: 메뉴 조작/F1키
        else if (popUpIndex == 7)
        {
            if (GameObject.Find("Player").GetComponent<popup5>().popup7On)
            {
                popUpIndex++;

            }
        }
        //튜토리얼 7: 메뉴 조작/F1키
        else if (popUpIndex == 8)
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                f1count--;
            }
            if(f1count<=0)
            {
                popUpIndex++;
            }
        }
        //튜토리얼 8: 정조준
        else if (popUpIndex == 9)
        {
            if (GameObject.Find("Player").GetComponent<popup5>().popup9On)
            {
                finesightcount--;

            }
            if(finesightcount<=0)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 10)
        {
            if (Input.GetMouseButtonDown(1))
            {
                popUpIndex++;
            }
        }
        //튜토리얼 9: 총 쏘기/ 총 연사
        else if (popUpIndex == 11)
        {
            if (GameObject.Find("Player").GetComponent<popup5>().popup5On)
            {
                popUpIndex++;

            }

        }
        //튜토리얼 9: 수류탄
        else if (popUpIndex == 12)
        {
            if (GameObject.Find("Enemy").GetComponent<Enemy_T>().hp <= 3)
            {
                popUpIndex++;
            }

        }
        else if (popUpIndex == 13)
        {
            if (GameObject.Find("Enemy1").GetComponent<Enemy_T>().hp <= 3 && GameObject.Find("Enemy2").GetComponent<Enemy_T>().hp <= 3)
            {
                popUpIndex++;
            }
        }
    }
}
