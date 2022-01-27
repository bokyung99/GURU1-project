using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject enemy;
    public int enemyHp;
    public int enemyMaxHp;
    // ���ʹ� hp Slider ����
    public Slider hpSlider;

    private void Update()
    {
        // ���� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)enemyHp / (float)enemyMaxHp;
    }
    private void OnTriggerEnter(Collider other)
    {
        //�浹�� ���� ������Ʈ�� �±װ� Enemy�� ��
        if (other.CompareTag("Enemy"))
        {
            //Ű���� T�� ������ �� (����)
            if (Input.GetKeyDown(KeyCode.T))
            {
                enemyHp --;
            }
        }
    }
}
