using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_T : MonoBehaviour
{
    // ���ʹ� ���� ���
    enum EnemyState
    {

        Attack,
        Damaged,
        Die
    }

    Animator anim;

    // ���� ���� ����
    public float attackDistance = 2f;


    //ĳ���� ��Ʈ�ѷ� ������Ʈ
    CharacterController cc;


    // ���ʹ� ���� ����
    EnemyState m_State;

    // �÷��̾� �߰� ����
    public float findDistance = 8f;

    // �÷��̾� Ʈ������
    Transform player;

    // ���� �ð�
    float currentTime = 0;

    // ���� ������ �ð�
    float attackDelay = 1.5f;

    // ���ʹ� ���ݷ�
    public int attackPower = 3;


    // ���ʹ��� ü��
    public int hp = 100;
    // ���ʹ��� �ִ� ü��
    int maxHp = 100;

    // ���ʹ� hp Slider ����
    public Slider hpSlider;





    // Start is called before the first frame update
    void Start()
    {
        // �ִϸ����� ������Ʈ�� anim ������ �ҷ��´�.
        anim = GetComponent<Animator>();


        // �÷��̾��� Ʈ������ ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").transform;

        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���¸� üũ�� �ش� ���º��� ������ ����� �����ϰ� �ϰ� �ʹ�
        switch (m_State)
        {

            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Damaged:
                Damaged();
                break;
            case EnemyState.Die:
                Die();
                break;
        }

        // ���� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)hp / (float)maxHp;
    }






    void Attack()
    {
        // ����, �÷��̾ ���� ���� �̳��� �ִٸ� �÷��̾ �����Ѵ�.
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // ������ �ð����� �÷��̾ �����Ѵ�.
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerHp>().E_DamageAction(attackPower);
                print("����");
                currentTime = 0;

                // ���� �ִϸ��̼� �÷���
                anim.SetTrigger("StartAttack");
            }
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

        print("hp: " + hp);


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


        Destroy(gameObject, 4);
    }


}