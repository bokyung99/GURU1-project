using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy_T : MonoBehaviour
{
    // 에너미 상태 상수
    enum EnemyState
    {

        Attack,
        Damaged,
        Die
    }

    Animator anim;

    // 공격 가능 범위
    public float attackDistance = 2f;


    //캐릭터 콘트롤러 컴포넌트
    CharacterController cc;


    // 에너미 상태 변수
    EnemyState m_State;

    // 플레이어 발견 범위
    public float findDistance = 8f;

    // 플레이어 트랜스폼
    Transform player;

    // 누적 시간
    float currentTime = 0;

    // 공격 딜레이 시간
    float attackDelay = 1.5f;

    // 에너미 공격력
    public int attackPower = 3;


    // 에너미의 체력
    public int hp = 100;
    // 에너미의 최대 체력
    int maxHp = 100;

    // 에너미 hp Slider 변수
    public Slider hpSlider;





    // Start is called before the first frame update
    void Start()
    {
        // 애니메이터 컴포넌트를 anim 변수에 불러온다.
        anim = GetComponent<Animator>();


        // 플레이어의 트랜스폼 컴포넌트 받아오기
        player = GameObject.Find("Player").transform;

        // 캐릭터 콘트롤러 컴포넌트 받아오기
        cc = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        // 현재 상태를 체크해 해당 상태별로 정해진 기능을 수행하게 하고 싶다
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

        // 현재 hp(%)를 hp 슬라이더의 value에 반영한다.
        hpSlider.value = (float)hp / (float)maxHp;
    }






    void Attack()
    {
        // 만일, 플레이어가 공격 범위 이내에 있다면 플레이어를 공격한다.
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // 일정한 시간마다 플레이어를 공격한다.
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                player.GetComponent<PlayerHp>().E_DamageAction(attackPower);
                print("공격");
                currentTime = 0;

                // 공격 애니메이션 플레이
                anim.SetTrigger("StartAttack");
            }
        }

    }


    void Damaged()
    {
        // 피격 상태를 처리하기 위한 코루틴을 실행한다.
        StartCoroutine(DamageProcess());
    }

    // 데미지 처리용 코루틴 함수
    IEnumerator DamageProcess()
    {
        // 피격 모션 시간만큼 기다린다
        yield return new WaitForSeconds(1.0f);

    }

    // 데미지 실행 함수
    public void HitEnemy(int hitPower)
    {
        // 만일, 이미 피격 상태이거나 사망 상태 또는 복귀 상태라면 아무런 처리도 하지 않고 함수를 종료한다.
        if (m_State == EnemyState.Die)
        {
            return;
        }

        // 플레이어의 공격력만큼 에너미의 체력을 감소시킨다.
        hp -= hitPower;

        print("hp: " + hp);


        // 에너미의 체력이 0보다 크면 피격 상태로 전환한다.
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("상태 전환: Any State -> Damaged");

            // 피격 애니메이션을 플레이한다.
            anim.SetTrigger("Damaged");
            Damaged();
        }
        // 그렇지 않다면 죽음 상태로 전환한다.
        else
        {
            m_State = EnemyState.Die;
            print("상태 전환: Any state -> Die");

            // 죽음 애니메이션을 플레이한다.
            anim.SetTrigger("Die");
            Die();


        }
    }

    void Die()
    {
        // 진행 중인 피격 코루틴을 중지한다.
        StopAllCoroutines();


        Destroy(gameObject, 4);
    }


}