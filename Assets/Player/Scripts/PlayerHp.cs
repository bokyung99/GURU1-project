using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHp : MonoBehaviour
{
    //Hit ȿ�� ������Ʈ
    public GameObject hitEffect;


    //�÷��̾� ü�� ���� (����)
    public int playerHp = 10;
    //�ִ� ü�� ���� (����)
    int playerMaxHp = 10;
    //hp �����̴� ����
    public Slider hpSlider;

    //����
    public GameObject hpPack;

    void Update()
    {
        UpdateB();
    }

    void UpdateB()
    {

        //���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)playerHp / (float)playerMaxHp;
        //hp�� 0�� �Ǹ� ���� ���� ������ ������ ��ü. �÷��̾� ����� �� ī�޶� �����.
        if (playerHp == 0)
        {
            //gameObject.SetActive(false);
        }

    }
    //�÷��̾��� �ǰ� �Լ�
    public void E_DamageAction(int E_damage)
    {
        //���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ����
        playerHp -= E_damage;
        //�÷��̾��� ü���� 0���� ũ�� �ǰ� ȿ���� ���
        if (playerHp > 0)
        {
            //�ǰ� ȿ�� �ڷ�ƾ�� ����
            StartCoroutine(PlayerHit());
        }
    }

    public void LE_DamageAction(int LE_damage)
    {
        //���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ����
        playerHp -= LE_damage;
        //�÷��̾��� ü���� 0���� ũ�� �ǰ� ȿ���� ���
        if (playerHp > 0)
        {
            //�ǰ� ȿ�� �ڷ�ƾ�� ����
            StartCoroutine(PlayerHit());
        }
    }

    //�ǰ� ȿ�� �ڷ�ƾ �Լ�
    IEnumerator PlayerHit()
    {
        //�ǰ� UI Ȱ��ȭ
        hitEffect.SetActive(true);
        //0.3�ʰ� ���
        yield return new WaitForSeconds(0.3f);
        //�ǰ� UI ��Ȱ��ȭ
        hitEffect.SetActive(false);
    }

    //�浹 ����
    private void OnTriggerEnter(Collider other)
    {
        //�浹�� ���� ������Ʈ�� �±װ� HealPack�� ��
        if (other.CompareTag ("HealPack"))
        {
            //�÷��̾� ü���� 0�� �ƴϰ� �ִ밡 �ƴ� ��
            //if (playerHp != 0 && playerHp < playerMaxHp)
            {
                //�÷��̾� ü�� ȸ�� (����)
                playerHp += 1;
                //hpPack ����
                Destroy(hpPack, 0.2f);
            }
        }
    }
}
