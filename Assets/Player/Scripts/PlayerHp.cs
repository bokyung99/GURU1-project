using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHp : MonoBehaviour
{
    //Hit ȿ�� ������Ʈ
    public GameObject hitEffect;

    //�÷��̾� hp �ؽ�Ʈ
    [SerializeField]
    private Text textHp;

    //�÷��̾� ü�� ����
    public int playerHp = 100;
    //�ִ� ü�� ����
    int playerMaxHp = 100;
    //hp �����̴� ����
    public Slider hpSlider;

    // ������ �� ���� ��
    public int itemTotalNum = 0;

    // ������1,2,3 ���� ��Ȳ Ȯ�� ����
    public int isItem1 = 0;
    public int isItem2 = 0;
    public int isItem3 = 0;

    //����
    public GameObject hpPack;

    //���� ���� Ȯ�ο� ����(Ʃ�丮��)
    public bool GetHealPack = false;
    //��ǥ������ ���� Ȯ�ο� ����(Ʃ�丮��)
    public bool GetItem1 = false;

    //�ǰ� ����
    public AudioClip playerhit;
    //���� ���� ����
    public AudioClip gethealpack;
    //������ ���� ����
    public AudioClip getitem;

    void Update()
    {
        UpdateB();
    }

    void UpdateB()
    {
        //�÷��̾��� ü���� 0 �̻��� ��
        if (playerHp >= 0)
        {
            //int ���� playerHp�� string���� ��ȯ
            string php = playerHp.ToString();
            //�÷��̾� hp �ؽ�Ʈ�� ��Ÿ����
            textHp.text = php;
        }

        //���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ�
        hpSlider.value = (float)playerHp / (float)playerMaxHp;

        //hp�� 0�� �Ǹ� ���� ���� ������ ������ ��ü. �÷��̾� ����� �� ī�޶� �����
        if (playerHp == 0)
        {
            //gameObject.SetActive(false);
        }

    }
    //�÷��̾��� �ǰ� �Լ�
    public void E_DamageAction(int E_damage)
    {
        //�÷��̾��� ü���� 0���� ũ�� �ǰ� ȿ���� ���
        if (playerHp > 0)
        {
            //���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ����
            playerHp -= E_damage;
            //�ǰ� ȿ�� �ڷ�ƾ�� ����
            StartCoroutine(PlayerHit());
        }
    }

    public void LE_DamageAction(int LE_damage)
    {
        //�÷��̾��� ü���� 0���� ũ�� �ǰ� ȿ���� ���
        if (playerHp > 0)
        {
            //���ʹ��� ���ݷ¸�ŭ �÷��̾��� ü�� ����
            playerHp -= LE_damage;
            //�ǰ� ȿ�� �ڷ�ƾ�� ����
            StartCoroutine(PlayerHit());
        }
    }

    //�ǰ� ȿ�� �ڷ�ƾ �Լ�
    IEnumerator PlayerHit()
    {
        //�ǰ� ����
        GetComponent<AudioSource>().PlayOneShot(playerhit, 0.3f);
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
            if (playerHp < playerMaxHp && playerHp > 0) 
            {
                //0.5�� �� hpPlus �Լ� ����
                Invoke("hpPlus", 0.5f);

                //���� ���� Ȯ��(Ʃ�丮��)
                GetHealPack = true;
            }
        }

        else if (other.tag == "Item1")
        {
            GetComponent<AudioSource>().PlayOneShot(getitem);
            isItem1++;
            itemTotalNum++;
            Destroy(other.gameObject);
            if (itemTotalNum == 3)
            {
                SceneManager.LoadScene("Ending");
            }

            //��ǥ������ ���� Ȯ��(Ʃ�丮��)
            GetItem1 = true;
        }
        else if (other.tag == "Item2")
        {
            GetComponent<AudioSource>().PlayOneShot(getitem);
            isItem2++;
            itemTotalNum++;
            Destroy(other.gameObject);
            if (itemTotalNum == 3)
            {
                SceneManager.LoadScene("Ending");
            }
        }
        else if (other.tag == "Item3")
        {
            GetComponent<AudioSource>().PlayOneShot(getitem);
            isItem3++;
            itemTotalNum++;
            Destroy(other.gameObject);
            if (itemTotalNum == 3)
            {
                SceneManager.LoadScene("Ending");
            }
        }
    }
    void hpPlus() //(���� ���� �� �ð� ����)
    {
        //�� ����
        GetComponent<AudioSource>().PlayOneShot(gethealpack, 0.5f);
        //�÷��̾� ü�� ȸ�� (����)
        playerHp += 1;
        //hpPack ����
        Destroy(hpPack);
    }
}
