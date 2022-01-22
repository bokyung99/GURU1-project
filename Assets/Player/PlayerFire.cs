using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //발사 위치
    public GameObject firePosition;
    //투척 무기
    public GameObject bombFactory;

    //투척 파워
    public float throwPower = 15f;

    void Update()
    {
        //마우스 휠 누르면 수류탄 투척
        if(Input.GetMouseButtonDown(2))
        {
            //수류탄 오브젝트 생성 후 수류탄의 생성위치를 발사 위치로
            GameObject bomb = Instantiate(bombFactory);
            bomb.transform.position = firePosition.transform.position;

            //수류탄 오브젝트의 rigidbody컴포넌트 가져오기
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            //카메라정면 방향으로 수류탄에 물리적인 힘 가하기
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);

        }
    }
}
