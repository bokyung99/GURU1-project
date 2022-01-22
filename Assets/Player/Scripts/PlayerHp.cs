using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    //�÷��̾� ü�� ���� (����)
    public int playerHp = 10;
    //�ִ� ü�� ���� (����)
    int playermaxHp = 10;
    //hp �����̴� ����
    public Slider hpSlider;

    //����
    GameObject hpPack;

    void Update()
    {
        //���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)playerHp / (float)playermaxHp;
        //hp�� 0�� �Ǹ� �÷��̾� ���, ���� ����
        if (playerHp == 0)
        {

        }
    }

    //�÷��̾��� �ǰ� �Լ�
    public void DamageAction(int damage)
    {
        //���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ����
        playerHp -= damage;
    }

    //�浹 ����
    private void OnCollisionEnter(Collision collision)
    {
        //�浹�� ���� ������Ʈ�� �±װ� HealPack�� ��
        if (collision.gameObject.tag == "HealPack")
        {
            //hpPack ����
            Destroy(hpPack);
            //�÷��̾� ü�� ȸ�� (����)
            playerHp += 2;
        }
    }
}
