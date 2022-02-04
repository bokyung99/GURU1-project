using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BossEnemyFSM : MonoBehaviour
{
    // ���ʹ� ���� ���
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    Animator anim;

    // ���� ���� ����
    public float attackDistance = 25f;

    // �̵� �ӵ�
    public float moveSpeed = 5f;

    //ĳ���� ��Ʈ�ѷ� ������Ʈ
    CharacterController cc;


    // ���ʹ� ���� ����
    EnemyState m_State;

    // �÷��̾� �߰� ����
    public float findDistance = 25f;

    // �÷��̾� Ʈ������
    Transform player;

    // ���� �ð�
    float currentTime = 0;

    // ���� ������ �ð�
    float attackDelay = 1f;

    // ���ʹ� ���ݷ�
    public int attackPower = 3;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos;
    Quaternion originRot;

    // �̵� ���� ����
    public float moveDistance = 20f;

    // ���ʹ��� ü��
    public int hp = 15;

    // ���ʹ��� �ִ� ü��
    int maxHp = 15;

    // ���ʹ� hp Slider ����
    public Slider hpSlider;

    // ������̼� ������Ʈ ����
    NavMeshAgent smith;

    // �̻��� �������� ��Ƶ� ���� ����
    public GameObject bullet1;
    public Transform bulletPos1;

    public GameObject bullet2;
    public Transform bulletPos2;

    //���� ���ݷ�
    public int runattackPower = 3;
    //�ݶ��̴� ����
    public BoxCollider boxcollider;
    //���� ���� Ȯ�ο� ����
    private bool runattack = false;


    // Start is called before the first frame update
    void Start()
    {
        // �ִϸ����� ������Ʈ�� anim ������ �ҷ��´�.
        anim = GetComponent<Animator>();

        // ������ ���ʹ� ���´� ���� �Ѵ�.
        m_State = EnemyState.Idle;

        // �÷��̾��� Ʈ������ ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").transform;

        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        // �ڽ��� �ʱ� ��ġ �����ϱ�
        originPos = transform.position;

        // �ڽ��� �ʱ� ��ġ�� ȸ�� ���� �����ϱ�
        originPos = transform.position;
        originRot = transform.rotation;

        // ������̼� ������Ʈ ������Ʈ �޾ƿ���
        smith = GetComponent<NavMeshAgent>();

        //�ݶ��̴� ������Ʈ �޾ƿ���
        boxcollider = GetComponent<BoxCollider>();
        //�ݶ��̴� ��Ȱ��ȭ ��Ű��
        boxcollider.enabled = false;

        StartCoroutine(Think());

    }

    // Update is called once per frame
    void Update()
    {
        

        // ���� ���¸� üũ�� �ش� ���º��� ������ ����� �����ϰ� �ϰ� �ʹ�
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }

        // ���� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)hp / (float)maxHp;
    }

    void Idle()
    {

        // ����, �÷��̾���� �Ÿ��� �׼� ���� ���� �̳���� Move ���·� ��ȯ�Ѵ�
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Idle -> Move");

            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        // ���� ���� ��ġ�� �ʱ� ��ġ���� �̵� ���� ������ �Ѿ�ٸ�...
        if (Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // ���� ���¸� ����(Return)�� ��ȯ�Ѵ�.
            m_State = EnemyState.Return;
            print("���� ��ȯ: Move -> Return");
        }
        // ����, �÷��̾���� �Ÿ��� ���� ���� ���̶�� �÷��̾ ���� �̵��Ѵ�.
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            // �̵� ���� ����
            //Vector3 dir = (player.position - transform.position).normalized;

            // ĳ���� ��Ʈ�ѷ��� �̿��� �̵��ϱ�
            //cc.Move(dir * moveSpeed * Time.deltaTime);

            // �÷��̾ ���� ������ ��ȯ�Ѵ�.
            //transform.forward = dir;

            // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ�Ѵ�.
            smith.isStopped = true;
            smith.ResetPath();

            // ������̼����� �����ϴ� �ּ� �Ÿ��� ���� ���� �Ÿ��� �����Ѵ�.
            smith.stoppingDistance = attackDistance;

            // ������̼��� �������� �÷��̾��� ��ġ�� �����Ѵ�.
            smith.destination = player.position;
        }
        else
        {
            m_State = EnemyState.Attack;
            print("���� ��ȯ: Move -> Attack");

            currentTime = attackDelay;

            anim.SetTrigger("MoveToAttackDelay");
        }

    }

    void Attack()
    {
        if (!runattack)
        {
            // ����, �÷��̾ ���� ���� �̳��� �ִٸ� �÷��̾ �����Ѵ�.
            if (Vector3.Distance(transform.position, player.position) < attackDistance)
            {
                smith.isStopped = true;

                // ������ �ð����� �÷��̾ �����Ѵ�.
                currentTime += Time.deltaTime;
                if (currentTime > attackDelay)
                {
                    player.GetComponent<PlayerHp>().E_DamageAction(attackPower);
                    print("����");
                    currentTime = 0;

                    // ���� �ִϸ��̼� �÷���
                    anim.SetTrigger("StartAttack");
                    StartCoroutine("Shot");
                }
            }
            // �׷��� �ʴٸ�, ���� ���¸� �̵�(Move)���� ��ȯ�Ѵ�(���߰� �ǽ�)
            else
            {
                m_State = EnemyState.Move;
                print("���� ��ȯ: Attack -> Move");
                currentTime = 0;

                // �̵� �ִϸ��̼� �÷���
                anim.SetTrigger("AttackToMove");
            }
        }
    }

    IEnumerator Shot()
    {
        yield return new WaitForSeconds(1.0f);

        // �̻��� �ν��Ͻ�ȭ
        GameObject instantBullet1 = Instantiate(bullet1, transform.position, transform.rotation);
        Rigidbody rigidBullet1 = instantBullet1.GetComponent<Rigidbody>();
        rigidBullet1.velocity = transform.forward * 100;

        GameObject instantBullet2 = Instantiate(bullet2, transform.position, transform.rotation);
        Rigidbody rigidBullet2 = instantBullet2.GetComponent<Rigidbody>();
        rigidBullet2.velocity = transform.forward * 100;

        yield return new WaitForSeconds(1.5f);
    }

    void Return()
    {
        // ����, �ʱ� ��ġ������ �Ÿ��� 0.1f �̻��̶�� �ʱ� ��ġ ������ �̵��Ѵ�.
        if (Vector3.Distance(transform.position, originPos) > 0.1f)
        {
            //Vector3 dir = (originPos - transform.position).normalized;
            //cc.Move(dir * moveSpeed * Time.deltaTime);

            // ���� �������� ������ ��ȯ�Ѵ�.
            //transform.forward = dir;

            // ������̼��� �������� �ʱ� ����� ��ġ�� �����Ѵ�.
            smith.destination = originPos;

            // ������̼����� �����ϴ� �ּ� �Ÿ��� 0���� �����Ѵ�.
            smith.stoppingDistance = 0;
        }
        // �׷��� �ʴٸ�, �ڽ��� ��ġ�� �ʱ� ��ġ�� �����ϰ� ���� ���¸� ���� ��ȯ�Ѵ�.
        else
        {
            // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ�Ѵ�.
            smith.isStopped = true;
            smith.ResetPath();

            // ��ġ ���� ȸ�� ���� �ʱ� ���·� ��ȯ�Ѵ�.
            transform.position = originPos;
            transform.rotation = originRot;

            // hp�� �ٽ� ȸ���Ѵ�.
            hp = maxHp;

            m_State = EnemyState.Idle;
            print("���� ��ȯ: Return -> Idle");

            // ��� �ִϸ��̼����� ��ȯ�ϴ� Ʈ�������� ȣ���Ѵ�.
            anim.SetTrigger("MoveToIdle");
        }
    }

    void Damaged()
    {
        // �ǰ� ���¸� ó���ϱ� ���� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(DamageProcess());
    }

    // ������ ó���� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        // �ǰ� ��� �ð���ŭ ��ٸ���
        yield return new WaitForSeconds(1.0f);

        // ���� ���¸� �̵� ���·� ��ȯ�Ѵ�.
        m_State = EnemyState.Move;
        print("���� ��ȯ: Damaged -> Move");
    }

    // ������ ���� �Լ�
    public void HitEnemy(int hitPower)
    {
        // ����, �̹� �ǰ� �����̰ų� ��� ���� �Ǵ� ���� ���¶�� �ƹ��� ó���� ���� �ʰ� �Լ��� �����Ѵ�.
        if (m_State == EnemyState.Die)
        {
            return;
        }

        // �÷��̾��� ���ݷ¸�ŭ ���ʹ��� ü���� ���ҽ�Ų��.
        hp -= hitPower;

        // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ�Ѵ�.
        smith.isStopped = true;
        smith.ResetPath();

        // ���ʹ��� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ�Ѵ�.
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("���� ��ȯ: Any State -> Damaged");

            // �ǰ� �ִϸ��̼��� �÷����Ѵ�.
            anim.SetTrigger("Damaged");
            Damaged();
        }
        // �׷��� �ʴٸ� ���� ���·� ��ȯ�Ѵ�.
        else
        {
            m_State = EnemyState.Die;
            print("���� ��ȯ: Any state -> Die");

            // ���� �ִϸ��̼��� �÷����Ѵ�.
            anim.SetTrigger("Die");
            Die();
        }
    }

    void Die()
    {
        // ���� ���� �ǰ� �ڷ�ƾ�� �����Ѵ�.
        StopAllCoroutines();

        // ���� ���¸� ó���ϱ� ���� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        // ĳ���� ��Ʈ�ѷ� ������Ʈ�� ��Ȱ��ȭ��Ų��.
        cc.enabled = false;

        // 2�� ���� ��ٸ� �Ŀ� �ڱ� �ڽ��� �����Ѵ�.
        yield return new WaitForSeconds(2f);
        print("�Ҹ�!");
        Destroy(gameObject);
    }

    //�浹 �� �� ���������� ������
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            player.GetComponent<PlayerHp>().E_DamageAction(runattackPower);
            print("��������");
        }
    }

    //� ������ ���� ����
    IEnumerator Think()
    {
        yield return new WaitForSeconds(0.1f);

        int ranAction = Random.Range(0, 5);
        switch (ranAction)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                StartCoroutine(Think());
                break;
            case 4:
                smith.isStopped = true;
                StartCoroutine(RunAttack());
                break;

        }

    }




    //���� ����
    IEnumerator RunAttack()
    {
        runattack = true;

        //�߰� ���� ���� ���ϸ��̼� �ִ� ��

        yield return new WaitForSeconds(1.5f);//�����ؼ� �ٰ����� ���ϸ��̼� �ð�
        boxcollider.enabled = true;

        yield return new WaitForSeconds(0.3f);//������ ���� �ð�
        boxcollider.enabled = false;

        yield return new WaitForSeconds(1f);//��ī���� ���� ���ϸ��̼� �� �ð�-2f�� �� 

        runattack = false;
        StartCoroutine(Think());
    }


}