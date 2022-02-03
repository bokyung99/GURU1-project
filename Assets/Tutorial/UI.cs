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

        //튜토리얼0
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
        //튜토리얼 1
        else if (popUpIndex == 1)
        {
            //스페이스바 누르면 다음 튜토리얼로
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;

                GameObject.Find("Player").GetComponent<PlayerHp>().LE_DamageAction(1);
            }
        }
        //튜토리얼 2
        else if (popUpIndex == 2)
        {
            //힐팩 먹으면 다음 튜토리얼로
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetHealPack)
            {
                popUpIndex++;
            }
        }
        //튜토리얼 3
        else if (popUpIndex == 3)
        {
            //목표 아이템 습득하면 다음 튜토리얼로 
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetItem1)
            {
                popUpIndex++;
            }
        }
        else if(popUpIndex==4)
        {
            if (GameObject.Find("Player").GetComponent<Weapon_T>().getWeapon)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            if (GameObject.Find("Player").GetComponent<popup5>().popup5On)
            {
                popUpIndex++;

            }

        }
        else if (popUpIndex == 6)
        {
            if (GameObject.Find("Enemy").GetComponent<Enemy_T>().hp <= 3)
            {
                popUpIndex++;
            }

        }
        else if (popUpIndex == 7)
        {
            if (GameObject.Find("Enemy1").GetComponent<Enemy_T>().hp <= 3 && GameObject.Find("Enemy2").GetComponent<Enemy_T>().hp <= 3)
            {
                popUpIndex++;
            }
        }
    }
}
