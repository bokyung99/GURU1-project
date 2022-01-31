using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWeapon : MonoBehaviour
{
    //���� ������Ʈ
    public GameObject weapon2;
    //���� UI
    public Image weapon2UI;

    private void OnTriggerEnter(Collider other)
    {
        //�浹�� ���� ������Ʈ�� �±װ� Weapon�� ��
        if (other.CompareTag("Weapon"))
        {
            //���� ���� ���忡 ���� ���� �ð� ����
            //���� ������Ʈ ����
            Destroy(weapon2, 1f);
            //���� UI ���� ���� �Լ� �ҷ�����
            Invoke("OnUIImage", 1f);
        }
    }

    void OnUIImage()
    {
        //���� UI ���� 0.5�� ����
        Color color2 = weapon2UI.color;
        color2.a = 0.5f;
        weapon2UI.color = color2;
    }
}