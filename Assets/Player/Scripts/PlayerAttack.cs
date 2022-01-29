using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ű���� F�� ������ �� AttackArea�� ���ʹ̰� �浹�ϸ�
//���� ���ݷ¸�ŭ ���ʹ��� ü���� ���̰� ���� enemyHp�� ��ȯ��

public class PlayerAttack : MonoBehaviour
{
    //���ʹ� ü��
    public float enemyHp;
    //���� ���ݷ�
    public float attackPower = 3f;

    private void Start()
    {
        //���ʹ� ü�� ���� ��������
        enemyHp = GetComponent<EnemyFSM>().enemyHp;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Ű���� F�� ������ ��
        if (Input.GetKeyDown(KeyCode.F))
        {
            //���ʹ� �±� ��ü�� �浹�ϸ�
            if (other.CompareTag("Enemy"))
            {
                //���ʹ� ü�� = ���ʹ� ü�� - ���� ���ݷ�
                enemyHp -= attackPower;
            }
        }
    }
}
