using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    //�÷��̾� ü�� ���� (����)
    public int hp = 10;
    //�ִ� ü�� ���� (����)
    int maxHp = 10;
    //hp �����̴� ����
    public Slider hpSlider;

    //����
    GameObject hpPack;

    void Update()
    {
        //���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)hp / (float)maxHp;
        //hp�� 0�� �Ǹ� �÷��̾� ���, ���� ����
        if (hp == 0)
        {

        }
    }

    //�÷��̾��� �ǰ� �Լ�
    public void DamageAction(int damage)
    {
        //���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ����
        hp -= damage;
    }

    //�浹 ����
    private void OnCollisionEnter(Collision collision)
    {
        //�浹�� ���� ������Ʈ�� �±װ� healPack�� ��
        if (collision.gameObject.tag == "HealPack")
        {
            //hpPack ����
            Destroy(hpPack);
            //�÷��̾� ü�� ȸ�� (����)
            hp += 2;
        }
    }
}
