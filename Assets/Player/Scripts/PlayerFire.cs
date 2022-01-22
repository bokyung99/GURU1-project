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

    //�ǰ� ȿ�� ������Ʈ
    public GameObject P_bulletEffect;

    //�ǰ� ȿ�� ��ƼŬ �ý���
    ParticleSystem ps;

    void Start()
    {
        //�ǰ� ȿ�� ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps = P_bulletEffect.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        UpdateA();
;    }


    void UpdateA()
    {
        //���콺 �� ������ ����ź ��ô
        if (Input.GetMouseButtonDown(2))
        {
            //����ź ������Ʈ ���� �� ����ź�� ������ġ�� �߻� ��ġ��
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            //����ź ������Ʈ�� rigidbody������Ʈ ��������
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //ī�޶����� �������� ����ź�� �������� �� ���ϱ�
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

        }

        //���콺 ���� ��ư ������ �� �߻�
        if(Input.GetMouseButtonDown(0))
        {
            //���̸� ������ �� �߻�� ��ġ�� ���� ������ ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //���̰� �ε��� ����� ������ ������ ������ ����
            RaycastHit hitInfo = new RaycastHit();

            //���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� ȿ���� ǥ��
            if (Physics.Raycast(ray, out hitInfo))
            {
                //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
                P_bulletEffect.transform.position = hitInfo.point;

                //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ŵ
                P_bulletEffect.transform.forward = hitInfo.normal;
              
                //�ǰ� ȿ�� �÷���
                ps.Play();

            }

        }

    }
}
