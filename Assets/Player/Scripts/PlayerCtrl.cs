using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    //���콺 �̵����� ī�޶� ȸ��
    private CamPosition cp;

    private void Update()
    {
        // ���� ���� ���°� '����'��� ���콺 Ŀ���� ���̰� ���� �����ϵ���
        // ���� ���°� '���� ��'�� �ƴ϶�� Ŀ�� ���̰� ���� ����
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // visible = true �ڵ带 Ȱ���ϸ� ķ ��ȯ�� �ȵ�.
        //���콺 Ŀ�� ������ �ʰ� �����ϰ� ���� ��ġ�� ����
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
        //���콺�� �̵��� �޾ƿ���
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cp.UpdateRotate(mouseX, mouseY);
    }
}
