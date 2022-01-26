using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //마우스 이동으로 카메라 회전
    private CamPosition cp;

    private void Update()
    {
        // 만약 게임 상태가 '중지'라면 마우스 커서가 보이고 조작 가능하도록
        // 게임 상태가 '게임 중'이 아니라면 커서 보이고 조작 가능
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // visible = true 코드를 활용하면 캠 전환이 안됨.
        //마우스 커서 보이지 않게 설정하고 현재 위치에 고정
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cp = GetComponent<CamPosition>();
        }
        UpdateRotate();
    }

    private void UpdateRotate()
    {
        //마우스의 이동값 받아오기
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cp.UpdateRotate(mouseX, mouseY);
    }
}
