using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    //public Transform groundCheck;
    //public float groundDistance = 1f;
    //public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    // 점프 상태 변수
    public bool isJumping = false;

    public AudioClip jumpsound;
    public AudioClip footStepSound1;
    public AudioClip footStepSound2;
    public AudioClip footStepSound3;
    public AudioClip footStepSound4;
    public float footStepDelay;

    private float nextFootstep = 0;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            GetComponent<AudioSource>().PlayOneShot(jumpsound, 0.5f);
        }

        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // 1-1. 만일, 점프 중이었고, 다시 바닥에 착지했다면...
        if (isJumping && controller.collisionFlags == CollisionFlags.Below)
        {
            // 점프 전 상태로 초기화한다.
            isJumping = false;
        }


        /* if (isGrounded && velocity.y <0)
            {
            velocity.y = -2f;
            }
        */

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 motion = transform.right * x + transform.forward * z;
        controller.Move(motion * speed * Time.deltaTime);

        // 점프 상태가 아닐 때 점프 가능함 (무한 점프 X)
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // 점프 상태로 전환
            isJumping = true;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(footStepSound1, 1.0f);
                nextFootstep += footStepDelay;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(footStepSound2, 1.0f);
                nextFootstep += footStepDelay;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(footStepSound3, 1.0f);
                nextFootstep += footStepDelay;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            nextFootstep -= Time.deltaTime;
            if (nextFootstep <= 0)
            {
                GetComponent<AudioSource>().PlayOneShot(footStepSound4, 1.0f);
                nextFootstep += footStepDelay;
            }
        }
    }
}


