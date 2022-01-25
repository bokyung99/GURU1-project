using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    //ī�޶� x�� ȸ�� �ӵ�
    [SerializeField]
    private float rotCamXAxisSpeed = 5;
    //ī�޶� y�� ȸ�� �ӵ�
    [SerializeField]
    private float rotCamYAxisSpeed = 3;

    //ī�޶� x�� ȸ�� ����
    private float limitMinX = -70;
    private float limitMaxX = 50;
    private float eulerAngleX;
    private float eulerAngleY;

    public void UpdateRotate(float mouseX, float mouseY)
    {
        // ���� ���°� ������ �ߡ� ������ ���� ������ �� �ְ� �Ѵ�.
        if (GameManager.gm.gState != GameManager.GameState.Run)
        {
            return;
        }
        //���콺 �¿� �̵����� ī�޶� y�� ȸ��
        eulerAngleY += mouseX * rotCamYAxisSpeed;
        //���콺 ���� �̵����� ī�޶� x�� ȸ��
        eulerAngleX -= mouseY * rotCamXAxisSpeed;

        //ī�޶� x�� ȸ�� ���� ����
        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);
        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
