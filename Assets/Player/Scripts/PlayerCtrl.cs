using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //���콺 �̵����� ī�޶� ȸ��
    private CamPosition cp;

    private void Awake()
    {
        //���콺 Ŀ�� ������ �ʰ� �����ϰ� ���� ��ġ�� ����
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
        //���콺�� �̵��� �޾ƿ���
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cp.UpdateRotate(mouseX, mouseY);
    }
}
