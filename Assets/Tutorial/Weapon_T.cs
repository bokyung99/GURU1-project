using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_T : MonoBehaviour
{
    //파밍할 무기 오브젝트
    public GameObject weapon2;
    //플레이어 무기 오브젝트
    public GameObject semi;
    //무기 UI
    public Image weapon1UI;
    public Image weapon2UI;
    //무기 파밍 사운드
    public AudioClip getweapon;

    //무기 파밍 확인용 변수(튜토리얼)
    public bool getWeapon = false;

    private void OnTriggerEnter(Collider other)
    {
        //충돌한 게임 오브젝트의 태그가 Weapon일 때
        if (other.CompareTag("Weapon"))
        { //무기 파밍 사운드
            GetComponent<AudioSource>().PlayOneShot(getweapon);
            //무기 오브젝트 제거
            Destroy(weapon2, 1f);
            //무기 UI 투명도 조절 함수 불러오기
            Invoke("OnUIImage", 1f);

            getWeapon = true;
        }

    }



    void OnUIImage()
    {
        //무기 UI 투명도 0.5로 조절
        Color color2 = weapon2UI.color;
        color2.a = 0.5f;
        weapon2UI.color = color2;
    }

    void Start()
    {
        gameObject.GetComponent<Playerfire_T>().enabled = true;
        gameObject.GetComponent<Playerfire2_T>().enabled = false;
    }


    void Update()
    {

       

        if (weapon2 == false)
        {
            //키보드 숫자 1을 눌렀을 때
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                //사용중인 무기 UI 투명도 1로 조절
                Color color1 = weapon1UI.color;
                color1.a = 1f;
                weapon1UI.color = color1;
                //사용하지 않는 무기 UI 투명도 0.5로 조절
                Color color2 = weapon2UI.color;
                color2.a = 0.5f;
                weapon2UI.color = color2;

                //무기1
                semi.SetActive(false);

                //무기1 스크립트만 활성화
                gameObject.GetComponent<Playerfire_T>().enabled = true;
                gameObject.GetComponent<Playerfire2_T>().enabled = false;
            }

            //키보드 숫자 2를 눌렀을 때
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                //사용중인 무기 UI 투명도 1로 조절
                Color color2 = weapon2UI.color;
                color2.a = 1f;
                weapon2UI.color = color2;
                //사용하지 않는 무기 UI 투명도 0.5로 조절
                Color color1 = weapon1UI.color;
                color1.a = 0.5f;
                weapon1UI.color = color1;

                //무기2
                semi.SetActive(true);

                //무기2 스크립트만 활성화
                gameObject.GetComponent<Playerfire_T>().enabled = false;
                gameObject.GetComponent<Playerfire2_T>().enabled = true;
            }
        }
    }
}
