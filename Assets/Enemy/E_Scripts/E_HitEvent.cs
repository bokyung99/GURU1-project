using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_HitEvent : MonoBehaviour
{
    // ���ʹ� ��ũ��Ʈ ������Ʈ�� ����ϱ� ���� ����
    public LEnemyFSM efsm;

    // �÷��̾�� �������� ������ ���� �̺�Ʈ �Լ�
    public void PlayerHit()
    {
        efsm.AttackAction();
    }
}
