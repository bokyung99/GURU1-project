using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject enemy;
    public int enemyHp;
    public int enemyMaxHp;
    // 에너미 hp Slider 변수
    public Slider hpSlider;

    private void Update()
    {
        // 현재 hp(%)를 hp 슬라이더의 value에 반영한다.
        hpSlider.value = (float)enemyHp / (float)enemyMaxHp;
    }
    private void OnTriggerEnter(Collider other)
    {
        //충돌한 게임 오브젝트의 태그가 Enemy일 때
        if (other.CompareTag("Enemy"))
        {
            //키보드 T를 눌렀을 때 (수정)
            if (Input.GetKeyDown(KeyCode.T))
            {
                enemyHp --;
            }
        }
    }
}
