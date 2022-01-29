using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//키보드 F를 눌렀을 때 AttackArea와 에너미가 충돌하면
//근접 공격력만큼 에너미의 체력이 깎이고 깎인 enemyHp를 반환함

public class PlayerAttack : MonoBehaviour
{
    //에너미 체력
    public float enemyHp;
    //근접 공격력
    public float attackPower = 3f;

    private void Start()
    {
        //에너미 체력 변수 가져오기
        enemyHp = GetComponent<EnemyFSM>().enemyHp;
    }
    private void OnTriggerEnter(Collider other)
    {
        //키보드 F를 눌렀을 때
        if (Input.GetKeyDown(KeyCode.F))
        {
            //에너미 태그 물체와 충돌하면
            if (other.CompareTag("Enemy"))
            {
                //에너미 체력 = 에너미 체력 - 근접 공격력
                enemyHp -= attackPower;
            }
        }
    }
}
