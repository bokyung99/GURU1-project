using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    //폭발 이펙트
    public GameObject bombEffect;

    //수류탄 데미지
    public int attackPower = 10;
    //폭발 효과 반경
    public float explosionRadius = 5f;



    //충돌 했을 때
    private void OnCollisionEnter(Collision collision)
    {

        //폭발 효과 반경 내에서 몬스터 오브젝트 collider컴포넌트를 배열에 저장, 원하는 레이어 번호 적기
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 7);

        //저장된 collider 배열에 있는 모든 적들에게 수류탄 데미지를 적용
        for(int i=0; i <cols.Length;i++)
         {
            EnemyFSM efsm = cols[i].GetComponent<EnemyFSM>();
            LEnemyFSM lefsm = cols[i].GetComponent<LEnemyFSM>();
            BossEnemyFSM befsm = cols[i].GetComponent<BossEnemyFSM>();

            if (efsm != null)
            {
                efsm.HitEnemy(attackPower);
            }

            if (lefsm != null)
            {
                lefsm.HitEnemy(attackPower);
            }
            if (befsm != null)
            {
                befsm.HitEnemy(attackPower);
            }
        }

        //폭발 이펙트 생성
        GameObject eff = Instantiate(bombEffect);

        //폭발 이펙트 위치를 수류탄 오브젝트 위치와 동일시
        eff.transform.position = transform.position;

        //자기 자신 제거
        Destroy(gameObject);
    }
}
