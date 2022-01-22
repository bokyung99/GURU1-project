using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    //목표가 될 트랜스폼 컴포넌트
    public Transform target;

    void Update()
    {
        //카메라와 목표 트랜스폼의 위치 일치시킴
        transform.position = target.position;
    }
}
