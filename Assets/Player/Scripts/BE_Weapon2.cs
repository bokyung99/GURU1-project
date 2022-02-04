using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BE_Weapon2 : MonoBehaviour
{
    //플레이어 무기 오브젝트
    public GameObject semi;
    //무기 UI
    public Image weapon1UI;
    public Image weapon2UI;


    void OnUIImage()
    {
        //무기 UI 투명도 0.5로 조절
        Color color2 = weapon2UI.color;
        color2.a = 0.5f;
        weapon2UI.color = color2;
    }

    void Update()
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
                gameObject.GetComponent<BE_PlayerFire>().enabled = true;
                gameObject.GetComponent<BE_PlayerFire2>().enabled = false;
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
                gameObject.GetComponent<BE_PlayerFire>().enabled = false;
                gameObject.GetComponent<BE_PlayerFire2>().enabled = true;
            }
        }
    }
