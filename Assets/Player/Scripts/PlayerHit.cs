using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject enemy;
    public float enemyHp;

    private void OnTriggerEnter(Collider other)
    {
        //충돌한 게임 오브젝트의 태그가 Enemy일 때
        if (other.CompareTag("Enemy"))
        {
            //키보드 T를 눌렀을 때 (수정)
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
