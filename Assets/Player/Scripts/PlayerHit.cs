using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject enemy;
    public float enemyHp;

    private void OnTriggerEnter(Collider other)
    {
        //�浹�� ���� ������Ʈ�� �±װ� Enemy�� ��
        if (other.CompareTag("Enemy"))
        {
            //Ű���� T�� ������ �� (����)
            if (Input.GetKeyDown(KeyCode.T))
            {
                //StartCoroutine(EnemyHit());
            }
        }
    }
    /*
    IEnumerator EnemyHit()
    {
        GameObject.Find("Enemy").GetComponent<EnemyFSM>();
        

    }
    */
}
