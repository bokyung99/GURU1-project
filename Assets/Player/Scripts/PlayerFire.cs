using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //�߻� ��ġ
    public GameObject firePosition;
    //��ô ����
    public GameObject bombFactory;

    //��ô �Ŀ�
    public float throwPower = 15f;

    //�ǰ� ȿ��1 ������Ʈ(�Ϲ�)
    public GameObject P1_bulletEffect;

    //�ǰ� ȿ��2 ������Ʈ(enemy������)
    public GameObject P2_bulletEffect;


    //�ǰ� ȿ�� ��ƼŬ �ý���1
    ParticleSystem ps1;
    //�ǰ� ȿ�� ��ƼŬ �ý���2
    ParticleSystem ps2;

    //���� ź������ �����ִ� �Ѿ��� ����
    public int currentBulletCount = 10;
    //������ �ӵ�
    public float reloadTime = 1.0f;
    //������ �� �� �� �߻� x
    private bool isReload = false;
    //�ѱ� ȿ�� ������Ʈ
    public GameObject gunFire;

    //�ѱ� ȿ�� ��ƼŬ �ý���
    ParticleSystem gf;


    //������ �Լ�
    IEnumerator ReloadCoroutine()
    {
        isReload = true;

        /* ������ �ִϸ��̼� �ִ°� */

        yield return new WaitForSeconds(reloadTime);

        currentBulletCount = 10;

        isReload = false;
    }


    void Start()
    {
        //�ǰ� ȿ��1 ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps1 = P1_bulletEffect.GetComponent<ParticleSystem>();
        //�ǰ� ȿ��2 ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps2 = P2_bulletEffect.GetComponent<ParticleSystem>();

        //�ѱ� ȿ�� ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        gf = gunFire.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        UpdateA();
        UpdateB();
    }

    void UpdateA()
    {
        //Ű���� R ������ ����ź ��ô
        if (Input.GetKeyDown(KeyCode.R))
        {
            //����ź ������Ʈ ���� �� ����ź�� ������ġ�� �߻� ��ġ��
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            //����ź ������Ʈ�� rigidbody������Ʈ ��������
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //ī�޶����� �������� ����ź�� �������� �� ���ϱ�
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

        }

        //���� �Ѿ��� 0�����ϸ� ������
        if (currentBulletCount <= 0)
        {
            StartCoroutine(ReloadCoroutine());
        }

        //���콺 ���� ��ư�� ������ �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0) && !isReload)
        {
            //���̸� ������ �� �߻�� ��ġ�� ���� ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //���̰� �ε��� ����� ������ ������ ������ ����
            RaycastHit hitInfo = new RaycastHit();

            //���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� ȿ�� ǥ��
            if (Physics.Raycast(ray, out hitInfo))
            {
                //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
                P2_bulletEffect.transform.position = hitInfo.point;

                //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                P2_bulletEffect.transform.forward = hitInfo.normal;

                //�ǰ� ȿ�� �÷���
                ps2.Play();

                currentBulletCount--;

            }
            /* else
           {
               //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
               P1_bulletEffect.transform.position = hitInfo.point;

               //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
               P1_bulletEffect.transform.forward = hitInfo.normal;

               //�ǰ� ȿ�� �÷���
               ps1.Play();

               currentBulletCount--;

           }*/
        }

        //���콺 �� ������ �� ����
        if (Input.GetMouseButton(2) && !isReload)
        {
            //���̸� ������ �� �߻�� ��ġ�� ���� ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //���̰� �ε��� ����� ������ ������ ������ ����
            RaycastHit hitInfo = new RaycastHit();


            //���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� ȿ�� ǥ��
            if (Physics.Raycast(ray, out hitInfo))
            {

                //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
                P2_bulletEffect.transform.position = hitInfo.point;

                //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                P2_bulletEffect.transform.forward = hitInfo.normal;

                //�ǰ� ȿ�� �÷���
                ps2.Play();

                currentBulletCount--;

            }
        }

    }

    void UpdateB()
    {
       if (Input.GetMouseButtonDown(0))
        {
            //�ѱ� ȿ�� �÷���
            gf.Play();
        }
    }
}
