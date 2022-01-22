using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    //플레이어 체력 변수 (수정)
    public int hp = 10;
    //최대 체력 변수 (수정)
    int maxHp = 10;
    //hp 슬라이더 변수
    public Slider hpSlider;

    //힐팩
    public GameObject hpPack;

    public CharacterController controller;
    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    
     public AudioClip footStepSound;
     public float footStepDelay;
 
     private float nextFootstep = 0;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y <0)
            {
            velocity.y = -2f;
            }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * x + transform.forward * z;
        controller.Move(motion * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

         if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && isGrounded)
            {
             nextFootstep -= Time.deltaTime;
             if (nextFootstep <= 0) 
                {
                 GetComponent<AudioSource>().PlayOneShot(footStepSound, 0.7f);
                 nextFootstep += footStepDelay;
                }
             }

        //현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영한다.
        hpSlider.value = (float)hp / (float)maxHp;

        //hp가 0이 되면 플레이어 사망 애니매이션 재생, 게임 종료
        if (hp == 0)
        {

        }
    }

    //플레이어의 피격 함수
    public void DamageAction(int damage)
    {
        //에너미의 공격력만큼 플레이어의 체력 감소
        hp -= damage;
    }

    //충돌 감지
    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 게임 오브젝트의 태그가 HealPack일 때
        if (collision.gameObject.tag == "HealPack")
        {
            if (hp < maxHp)
            {
                //hpPack 제거
                Destroy(hpPack);
                //플레이어 체력 회복 (수정)
                hp += 1;
            }
        }
    }
}


