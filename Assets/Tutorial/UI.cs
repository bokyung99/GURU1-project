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
    private int tabcount = 2;


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

        //Ʃ�丮��0: �̵�
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
        //Ʃ�丮�� 1: ����
        else if (popUpIndex == 1)
        {
            //�����̽��� ������ ���� Ʃ�丮���
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;

                GameObject.Find("Player").GetComponent<PlayerHp>().LE_DamageAction(10);
            }
        }
        //Ʃ�丮�� 2: ���� ����
        else if (popUpIndex == 2)
        {
            //���� ������ ���� Ʃ�丮���
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetHealPack)
            {
                popUpIndex++;
            }
        }
        //Ʃ�丮�� 3: ��ǥ ������ ����
        else if (popUpIndex == 3)
        {
            //��ǥ ������ �����ϸ� ���� Ʃ�丮��� 
            if (GameObject.Find("Player").GetComponent<PlayerHp>().GetItem1)
            {
                popUpIndex++;
            }
        }
        //Ʃ�丮�� 4: ��ǥ ������ ���� ��Ȳ
        else if (popUpIndex == 4)
        {
            //tab��ư ������
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                tabcount--;
               
            }
            if(tabcount<=0)
            {
                popUpIndex++;
            }
        }
        //Ʃ�丮�� 5: ���� �Ĺ�
        else if(popUpIndex==5)
        {
            //���ο� ���⸦ ������
            if (GameObject.Find("Player").GetComponent<Weapon_T>().getWeapon)
            {
                popUpIndex++;
            }
        }
        //Ʃ�丮�� 6: ���� ��ü
        else if (popUpIndex == 6)
        {
            //����Ű�е� 1 �Ǵ� 2�� ������
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                popUpIndex++;
            }

        }
        //Ʃ�丮�� 7: �޴� ����/F1Ű
        else if (popUpIndex == 7)
        {
            if (GameObject.Find("Player").GetComponent<popup5>().popup7On)
            {
                popUpIndex++;

            }
        }
        else if (popUpIndex == 8)
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                popUpIndex++;
            }
        }
        //Ʃ�丮�� 8: ������
        else if (popUpIndex == 9)
        {
            if (GameObject.Find("Player").GetComponent<popup5>().popup9On)
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
        //Ʃ�丮�� 9: �� ���/ �� ����
        else if (popUpIndex == 11)
        {
            if (GameObject.Find("Player").GetComponent<popup5>().popup5On)
            {
                popUpIndex++;

            }

        }
        //Ʃ�丮�� 9: ����ź
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
