using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    //Ʃ�丮�� ui �迭
    public GameObject[] popUps;
    //Ʃ�丮�� ui �ε���
    private int popUpIndex;
    //�̵�Ű ī��Ʈ
    public int count = 4;



    void Update()
    {
        //Ʃ�丮�� ui ����
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

        //Ʃ�丮��0
        if (popUpIndex == 0)
        {
            //�̵�Ű w,s,a,d ������ ���� Ʃ�丮���
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
        //Ʃ�丮�� 1
        else if (popUpIndex == 1)
        {
            //�����̽��� ������ ���� Ʃ�丮���
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;

                GameObject.Find("Player").GetComponent<PlayerHp>().LE_DamageAction(1);
            }
        }
        //Ʃ�丮�� 2
        else if (popUpIndex == 2)
        {
            //���� ������ ���� Ʃ�丮���
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetHealPack)
            {
                popUpIndex++;
            }
        }
        //Ʃ�丮�� 3
        else if (popUpIndex == 3)
        {
            //��ǥ ������ �����ϸ� ���� Ʃ�丮��� 
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
