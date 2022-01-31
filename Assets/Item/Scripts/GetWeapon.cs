using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWeapon : MonoBehaviour
{
    //파밍할 무기 오브젝트
    public GameObject weapon2;
    //플레이어 무기 오브젝트
    public GameObject Semi;
    //무기 UI
    public Image weapon1UI;
    public Image weapon2UI;

    private void OnTriggerEnter(Collider other)
    {
        //충돌한 게임 오브젝트의 태그가 Weapon일 때
        if (other.CompareTag("Weapon"))
        {
            //무기 습득 사운드에 따라 제거 시간 조절
            //무기 오브젝트 제거
            Destroy(weapon2, 1f);
            //무기 UI 투명도 조절 함수 불러오기
            Invoke("OnUIImage", 1f);
        }
    }

    void OnUIImage()
    {
        //무기 UI 투명도 0.5로 조절
        Color color2 = weapon2UI.color;
        color2.a = 0.5f;
        weapon2UI.color = color2;
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

                Semi.SetActive(false);
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

                Semi.SetActive(true);
            }
        }
    }
}