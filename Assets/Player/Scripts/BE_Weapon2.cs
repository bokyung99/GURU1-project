using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BE_Weapon2 : MonoBehaviour
{
    //�Ĺ��� ���� ������Ʈ
    public GameObject weapon2;
    //�÷��̾� ���� ������Ʈ
    public GameObject semi;
    //���� UI
    public Image weapon1UI;
    public Image weapon2UI;
    //���� �Ĺ� ����
    public AudioClip getweapon;

    private void OnTriggerEnter(Collider other)
    {
        //�浹�� ���� ������Ʈ�� �±װ� Weapon�� ��
        if (other.CompareTag("Weapon"))
        {
            //���� �Ĺ� ����
            GetComponent<AudioSource>().PlayOneShot(getweapon);
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

    void Update()
    {
        if (weapon2 == false)
        {
            //Ű���� ���� 1�� ������ ��
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                //������� ���� UI ���� 1�� ����
                Color color1 = weapon1UI.color;
                color1.a = 1f;
                weapon1UI.color = color1;
                //������� �ʴ� ���� UI ���� 0.5�� ����
                Color color2 = weapon2UI.color;
                color2.a = 0.5f;
                weapon2UI.color = color2;

                //����1
                semi.SetActive(false);

                //����1 ��ũ��Ʈ�� Ȱ��ȭ
                gameObject.GetComponent<BE_PlayerFire>().enabled = true;
                gameObject.GetComponent<BE_PlayerFire2>().enabled = false;
            }

            //Ű���� ���� 2�� ������ ��
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //������� ���� UI ���� 1�� ����
                Color color2 = weapon2UI.color;
                color2.a = 1f;
                weapon2UI.color = color2;
                //������� �ʴ� ���� UI ���� 0.5�� ����
                Color color1 = weapon1UI.color;
                color1.a = 0.5f;
                weapon1UI.color = color1;

                //����2
                semi.SetActive(true);

                //����2 ��ũ��Ʈ�� Ȱ��ȭ
                gameObject.GetComponent<BE_PlayerFire>().enabled = false;
                gameObject.GetComponent<BE_PlayerFire2>().enabled = true;
            }
        }
    }
}
