using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playerfire_T : MonoBehaviour
{

    //���� �Ѿ� �ؽ�Ʈ
    [SerializeField]
    private Text curBullet;
    //�ִ� �Ѿ� �ؽ�Ʈ
    [SerializeField]
    private Text maxBullet;


    //�߻� ��ġ
    public GameObject firePosition;
    //��ô ����
    public GameObject bombFactory;

    //��ô �Ŀ�
    public float throwPower = 10f;

    //�ǰ� ȿ��1 ������Ʈ(�Ϲ�)
    public GameObject P1_bulletEffect;

    //�ǰ� ȿ��2 ������Ʈ(enemy������)
    public GameObject P2_bulletEffect;


    //�ǰ� ȿ�� ��ƼŬ �ý���1
    ParticleSystem ps1;
    //�ǰ� ȿ�� ��ƼŬ �ý���2
    ParticleSystem ps2;

    //���� ź������ �����ִ� �Ѿ��� ����
    public int currentBulletCount = 60;
    //�ִ� �Ѿ��� ����
    public int maxBulletCount = 60;
    //������ �ӵ�
    public float reloadTime = 1.0f;
    //������ �� �� �� �߻� x
    private bool isReload = false;

    //�ѱ� ȿ�� ������Ʈ �迭
    public GameObject[] eff_Flash;

    //������ ���� Ȯ�� ����
    private bool isFineSightMode = false;

    private bool isShoot = false;

    //���� �� ����
    public Gun currentGun;


    //�� ������
    public int attackPower = 3;

    //Ʈ������ enemy
    Transform Enemy;


    Animator anim;

    //�� ���� ��ġ
    Vector3 gunDefaultPos;

    //�� ������ ��ġ
    public Vector3 fineSightOriginPos;

    //�ݵ� ���� Ȯ��
    bool isRebound = false;

    // ������ Ƚ���� 1ȸ�� �����ϱ� ���� ����
    int reloadCtrl = 0;


    //����
    public AudioClip reload;
    public AudioClip gunshot;
    public AudioClip throwbomb;


    //���� ������ ����
    public int setDelay = 2;
    private int delay = 0;


    //������ �Լ�
    IEnumerator ReloadCoroutine()
    {
        // ������ 1ȸ
        reloadCtrl++;
        isReload = true;

        /* ������ �ִϸ��̼� �ִ°� */
        anim.SetTrigger("reloading");
        print("������");

        //������ ����
        GetComponent<AudioSource>().PlayOneShot(reload, 0.5f);

        yield return new WaitForSeconds(reloadTime);

        currentBulletCount = 60;

        isReload = false;

        // ������ ���� ���� 0���� �ٽ� ����
        reloadCtrl = 0;
    }

    //�ѱ� ȿ�� �ڷ�ƾ �Լ�
    IEnumerator ShootEffectOn(float duration)
    {
        //�����ϰ� ���� ����
        int num = Random.Range(0, eff_Flash.Length - 1);
        //����Ʈ ������Ʈ �迭���� ���� ���ڿ� �ش��ϴ� ����Ʈ ������Ʈ Ȱ��ȭ
        eff_Flash[num].SetActive(true);
        //������ �ð���ŭ ���
        yield return new WaitForSeconds(duration);
        //����Ʈ ������Ʈ ��Ȱ��ȭ
        eff_Flash[num].SetActive(false);
    }

    //������ �Լ�
    private void TryFineSight()
    {
        //���콺 ������ ��ư�� ������ ������

        if (Input.GetMouseButtonDown(1))

        {
            //������ ���� ��ȯ
            isFineSightMode = !isFineSightMode;
            //������ �ִϸ��̼�
            anim.SetBool("FineSightMode", isFineSightMode);


            if (isFineSightMode)
            {
                //������ ����
                Camera.main.fieldOfView = 30f;
                //������ �ڷ�ƾ ����
                StartCoroutine(FineSightActivateCoroutine());

                //�ܼ�â�� ������ ��� �Է�
                print("finesightModeON");
            }
            else
            {
                //������ ���
                Camera.main.fieldOfView = 60f;

                //������ ��� �ڷ�ƾ ����
                StartCoroutine(FineSightDeActivateCoroutine());

                //�ܼ�â�� ������ ��� ��� �Է�
                print("finesightModeOFF");
            }
        }
    }

    //������ �ڷ�ƾ
    IEnumerator FineSightActivateCoroutine()
    {
        while (currentGun.transform.localPosition != fineSightOriginPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, fineSightOriginPos, 0.4f);

            //���� ���� �ǵ��� ����
            if (Vector3.Distance(currentGun.transform.localPosition, fineSightOriginPos) < 0.01f)
            {
                //�� ��ġ�� ������ ��ġ��
                currentGun.transform.localPosition = fineSightOriginPos;
                isRebound = false;

                break;
            }

            yield return null;
        }

        yield break;

    }

    //������ ��� �ڷ�ƾ
    IEnumerator FineSightDeActivateCoroutine()
    {
        while (currentGun.transform.localPosition != gunDefaultPos)
        {
            currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, gunDefaultPos, 0.4f);

            //���� ���� �ǵ��� ����
            if (Vector3.Distance(currentGun.transform.localPosition, gunDefaultPos) < 0.01f)
            {
                //�� ��ġ�� ������ ��ġ��
                currentGun.transform.localPosition = gunDefaultPos;
                isRebound = false;

                break;
            }


            yield return null;
        }

        yield break;

    }



    //�ݵ� �Լ�
    void Recoil()
    {
        //������ ���°� �ƴ϶��
        if (!isFineSightMode)
        {
            //�� ��ġ�� �������
            currentGun.transform.localPosition = gunDefaultPos;
            //�� ��ġ�� �ڷ�(�ݵ�)
            currentGun.transform.Translate(Vector3.back * 0.1f);

        }
        //������ ���¶��
        else
        {
            //�� ��ġ�� �������
            currentGun.transform.localPosition = fineSightOriginPos;
            //�� ��ġ�� �ڷ�(�ݵ�)
            currentGun.transform.Translate(Vector3.back * 0.05f);
        }


    }

    //�ݵ� �� �ٽ� ���ƿ��� �Լ�
    IEnumerator Rebound()
    {

        isRebound = true;

        //������ ���°� �ƴ϶��
        if (!isFineSightMode)
        {
            while (true)
            {
                //�� ��ġ�� ������� õõ�� �ǵ�����
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, gunDefaultPos, Time.deltaTime * 3.0f);

                //���� ���� �ǵ��� ����
                if (Vector3.Distance(currentGun.transform.localPosition, gunDefaultPos) < 0.001f)
                {
                    //�� ��ġ�� ������ ��ġ��
                    currentGun.transform.localPosition = gunDefaultPos;
                    isRebound = false;

                    break;
                }
                yield return null;
            }

        }
        //������ ���¶��
        else
        {
            while (true)
            {
                //�� ��ġ�� ������� õõ�� �ǵ�����
                currentGun.transform.localPosition = Vector3.Lerp(currentGun.transform.localPosition, fineSightOriginPos, Time.deltaTime * 3.0f);

                //���� ���� �ǵ��� ����
                if (Vector3.Distance(currentGun.transform.localPosition, fineSightOriginPos) < 0.001f)
                {
                    //�� ��ġ�� ������ ��ġ��
                    currentGun.transform.localPosition = fineSightOriginPos;
                    isRebound = false;

                    break;
                }
                yield return null;
            }
        }


        yield break;

    }







    void Start()
    {
        //�ǰ� ȿ��1 ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps1 = P1_bulletEffect.GetComponent<ParticleSystem>();
        //�ǰ� ȿ��2 ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps2 = P2_bulletEffect.GetComponent<ParticleSystem>();


        Enemy = GameObject.Find("Enemy").transform;

        // �ִϸ����� ������Ʈ�� anim ������ �ҷ��´�.
        anim = GetComponent<Animator>();
        if (anim)
        {
            print("anim loading");
        }

        //�� ���� ��ġ ����
        gunDefaultPos = currentGun.transform.localPosition;


    }

    void Update()
    {
        // ���� ���°� '���� ��'�� �ƴ϶�� ���� �Ұ�
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }

        UpdateA();
        UpdateB();
    }

    void UpdateA()
    {
        if (!isReload && !isShoot && !isRebound)
        {
            //������ �Լ�
            TryFineSight();
        }

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

            //����ź ��ô ����
            GetComponent<AudioSource>().PlayOneShot(throwbomb,1.0f);

        }

        //���� �Ѿ��� 0�� �����̰�, reloadCtrl�� 0�� ��� ������
        if (currentBulletCount <= 0 && reloadCtrl == 0)
        {
            StartCoroutine(ReloadCoroutine());
        }

        //���콺 ���� ��ư�� ������ �Ѿ� �߻�
        if (Input.GetMouseButtonDown(0) && !isReload)
        {
            isShoot = true;

            //�ѱ� ȿ�� �÷���
            StartCoroutine(ShootEffectOn(0.05f));

            //���̸� ������ �� �߻�� ��ġ�� ���� ���� ����
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            //���̰� �ε��� ����� ������ ������ ������ ����
            RaycastHit hitInfo = new RaycastHit();

            //�ݵ� �Լ� ȣ��
            Recoil();

            //�ݵ� �ǵ�����
            if (!isRebound)
            {
                StartCoroutine(Rebound());
            }


            //���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� ȿ�� ǥ��
            if (Physics.Raycast(ray, out hitInfo))
            {
                //�� �߻簡 enemy�� �´´ٸ�
                if (hitInfo.transform.tag == "Enemy")
                {
                    //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
                    P2_bulletEffect.transform.position = hitInfo.point;

                    //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                    P2_bulletEffect.transform.forward = hitInfo.normal;

                    //�ǰ� ȿ�� �÷���
                    ps2.Play();

                    //Enemy ����
                    Enemy.GetComponent<Enemy_T>().HitEnemy(attackPower);

                    //�Ѿ� �Ѱ� ����
                    currentBulletCount--;
                }
                else
                {
                    //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
                    P1_bulletEffect.transform.position = hitInfo.point;

                    //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                    P1_bulletEffect.transform.forward = hitInfo.normal;

                    //�ǰ� ȿ�� �÷���
                    ps1.Play();

                    //�Ѿ� �Ѱ� ����
                    currentBulletCount--;

                }
            }

            isShoot = false;
        }

        //���콺 ���� �� ������ �� ����
        else if (Input.GetMouseButton(0) && !isReload)
        {
            //�����̰� �ɷ����� ���� ���� �߻�
            if (delay <= 0)
            {
                //�߻��ߴٸ� �����̰� �����
                delay = setDelay;

                isShoot = true;

                {

                    //�ѱ� ȿ�� �÷���
                    StartCoroutine(ShootEffectOn(0.05f));
                    //����
                    GetComponent<AudioSource>().PlayOneShot(gunshot, 0.2f);

                    //���̸� ������ �� �߻�� ��ġ�� ���� ���� ����
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                    //���̰� �ε��� ����� ������ ������ ������ ����
                    RaycastHit hitInfo = new RaycastHit();

                    //�ݵ� �Լ� ȣ��
                    Recoil();

                    //�ݵ� �ǵ�����
                    if (!isRebound)
                    {
                        StartCoroutine(Rebound());
                    }


                    //���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� ȿ�� ǥ��
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        //�� �߻簡 enemy�� �´´ٸ�
                        if (hitInfo.transform.tag == "Enemy")
                        {
                            //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
                            P2_bulletEffect.transform.position = hitInfo.point;

                            //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                            P2_bulletEffect.transform.forward = hitInfo.normal;

                            //�ǰ� ȿ�� �÷���
                            ps2.Play();

                            //Enemy ����
                            Enemy.GetComponent<Enemy_T>().HitEnemy(attackPower);


                            //�Ѿ� �Ѱ� ����
                            currentBulletCount--;
                        }
                        else
                        {
                            //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
                            P1_bulletEffect.transform.position = hitInfo.point;

                            //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                            P1_bulletEffect.transform.forward = hitInfo.normal;

                            //�ǰ� ȿ�� �÷���
                            ps1.Play();

                            //�Ѿ� �Ѱ� ����
                            currentBulletCount--;

                        }

                    }
                }

                isShoot = false;
            }
        }

        //�����̰� �ִٸ� �� �������� ���δ�.
        if (0 < delay)
        {
            delay--;
        }

    }

    void UpdateB()
    {
        //int ���� currentBulletCount�� string���� ��ȯ
        string cBC = currentBulletCount.ToString();
        //���� �Ѿ��� ���� �ؽ�Ʈ�� ��Ÿ����
        curBullet.text = cBC;
        //int ���� maxBulletCount string���� ��ȯ
        string mBC = maxBulletCount.ToString();
        maxBullet.text = mBC;
    }
}