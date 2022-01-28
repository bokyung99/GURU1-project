using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : MonoBehaviour
{
    //���� ����Ʈ ����
    public GameObject bombEffect;

    //����ź ������
    public int attackPower = 10;
    //���� ȿ�� �ݰ�
    public float explosionRadius = 5f;

    //�浹 ���� ��
    private void OnCollisionEnter(Collision collision)
    {
        //���� ȿ�� �ݰ� ������ ���� ������Ʈ collider������Ʈ�� �迭�� ����
        Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, 1 << 10);

        //����� collider �迭�� �ִ� ��� ���鿡�� ����ź �������� ����
        for(int i=0; i <cols.Length;i++)
         {
             cols[i].GetComponent<EnemyFSM>().HitEnemy(attackPower);
            cols[i].GetComponent<LEnemyFSM>().HitEnemy(attackPower);
        } 

        //���� ����Ʈ ����
        GameObject eff = Instantiate(bombEffect);

        //���� ����Ʈ ��ġ�� ����ź ������Ʈ ��ġ�� ���Ͻ�
        eff.transform.position = transform.position;

        //�ڱ� �ڽ� ����
        Destroy(gameObject);
    }
}
