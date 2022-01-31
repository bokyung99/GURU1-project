using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetWeapon : MonoBehaviour
{
    //무기 오브젝트
    public GameObject weapon2;
    //무기 UI
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
}