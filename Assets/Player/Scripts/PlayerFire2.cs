using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFire2 : MonoBehaviour
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
    public int currentBulletCount = 50;
    //�ִ� �Ѿ��� ���� (����)
    public int maxBulletCount = 50;
    //������ �ӵ�
    public float reloadTime = 1.0f;
    //������ �� �� �� �߻� x
    private bool isReload = false;

    //�ѱ� ȿ�� ������Ʈ �迭
    public GameObject[] eff_Flash;

    private bool isShoot = false;

    //���� �� ����
    public Gun currentGun;

  

    //�� ������
    public int attackPower = 3;

    //Ʈ������ enemy
    Transform Enemy;


    //Ʈ������ lenemy
    Transform LEnemy;

    Animator anim;

    //�� ���� ��ġ
    Vector3 gunDefaultPos;

    //�ݵ� ���� Ȯ��
    bool isRebound = false;

    // ������ Ƚ���� 1ȸ�� �����ϱ� ���� ����
    int reloadCtrl = 0;

    //����
    public AudioClip reload;
    public AudioClip gunshot;
    public AudioClip throwbomb;
    public AudioClip enemyhit;




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
        GetComponent<AudioSource>().PlayOneShot(reload, reloadTime);

        yield return new WaitForSeconds(reloadTime);

        currentBulletCount = 50;

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

    /*
    //������ �Լ�
    private void TryFineSight()
    {
        //���콺 ������ ��ư�� ������ ������

        if(Input.GetMouseButtonDown(1))

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
        while(currentGun.transform.localPosition!= fineSightOriginPos)
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
    */


    //�ݵ� �Լ�
    void Recoil()
    {
        //�� ��ġ�� �������
        currentGun.transform.localPosition = gunDefaultPos;
        //�� ��ġ�� �ڷ�(�ݵ�)
        currentGun.transform.Translate(Vector3.back * 0.1f);

    }

    //�ݵ� �� �ٽ� ���ƿ��� �Լ�
    IEnumerator Rebound()
    {
       
        isRebound = true;

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

        yield break;
    }


    void Start()
    {
        //�ǰ� ȿ��1 ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps1 = P1_bulletEffect.GetComponent<ParticleSystem>();
        //�ǰ� ȿ��2 ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps2 = P2_bulletEffect.GetComponent<ParticleSystem>();

        // ������ ���� �� Enemy ã��
        /*
        if (SpawnManager.spawnCtrl == true)
        {
            Debug.Log("���� ã��");
            //Enemy = GameObject.Find("Enemy").transform;
            //Enemy = Resources.Load("E_Prefabs/Enemy", typeof(GameObject)) as Transform;
            //Enemy = SpawnManager.clone.transform;
            //SpawnManager spawn;
        }

        */
        //LEnemy = GameObject.Find("LEnemy").transform;

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
            GetComponent<AudioSource>().PlayOneShot(throwbomb, 1.0f);
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
            //����
            GetComponent<AudioSource>().PlayOneShot(gunshot, 0.8f);

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
                    //�ǰ� ����
                    GetComponent<AudioSource>().PlayOneShot(enemyhit,0.2f);

                    for (int i = 0; i < SpawnManager.spawnSize; i++)
                    {

                        // ���� clone[i]�� Ŭ���� �����ư�, ���̿� �ε��� ����� clone[i]�� ������ ���ʹ̸�
                        if (SpawnManager.clone[i] != null &&
                            hitInfo.transform == SpawnManager.clone[i].transform)
                        {
                            // �� ���ʹ� ����
                            SpawnManager.clone[i].GetComponent<EnemyFSM>().HitEnemy(attackPower);
                        }
                    }

                    //�Ѿ� �Ѱ� ����
                    currentBulletCount--;
                }
                else if (hitInfo.transform.tag == "LEnemy")
                {
                    //�ǰ� ȿ���� ��ġ�� ���̰� �ε��� �������� �̵�
                    P2_bulletEffect.transform.position = hitInfo.point;

                    //�ǰ� ȿ���� forward������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                    P2_bulletEffect.transform.forward = hitInfo.normal;

                    //�ǰ� ȿ�� �÷���
                    ps2.Play();
                    //�ǰ� ����
                    GetComponent<AudioSource>().PlayOneShot(enemyhit,0.2f);

                    /*LEnemy ����
                    LEnemy.GetComponent<LEnemyFSM>().HitEnemy(attackPower);
                    */

                    for (int i = 0; i < LE_SpawnManager.spawnSize; i++)
                    {
                        // ���� clone[i]�� Ŭ���� �����ư�, ���̿� �ε��� ����� clone[i]�� ������ ���ʹ̸�
                        if (LE_SpawnManager.clone[i] != null &&
                            hitInfo.transform == LE_SpawnManager.clone[i].transform)
                        {
                            // �� ���ʹ� ����
                            LE_SpawnManager.clone[i].GetComponent<LEnemyFSM>().HitEnemy(attackPower);
                        }
                    }

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
