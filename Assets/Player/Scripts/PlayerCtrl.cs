using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //마우스 이동으로 카메라 회전
    private CamPosition cp;

    private void Awake()
    {
        //마우스 커서 보이지 않게 설정하고 현재 위치에 고정
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cp = GetComponent<CamPosition>();
    }

    private void Update()
    {
        UpdateRotate();
    }
    private void UpdateRotate()
    {
        //마우스 X의 이동값 받아오기
        float mouseX = Input.GetAxis("Mouse X");
        //마우스 Y의 이동값 받아오기
        float mouseY = Input.GetAxis("Mouse Y");

        cp.UpdateRotate(mouseX, mouseY);
    }
}
