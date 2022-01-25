using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    //카메라 x축 회전 속도
    [SerializeField]
    private float rotCamXAxisSpeed = 5;
    //카메라 y축 회전 속도
    [SerializeField]
    private float rotCamYAxisSpeed = 3;

    //카메라 x축 회전 범위
    private float limitMinX = -70;
    private float limitMaxX = 50;
    private float eulerAngleX;
    private float eulerAngleY;

    public void UpdateRotate(float mouseX, float mouseY)
    {
        //마우스 좌우 이동으로 카메라 y축 회전
        eulerAngleY += mouseX * rotCamYAxisSpeed;
        //마우스 상하 이동으로 카메라 x축 회전
        eulerAngleX -= mouseY * rotCamXAxisSpeed;

        //카메라 x축 회전 범위 설정
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
