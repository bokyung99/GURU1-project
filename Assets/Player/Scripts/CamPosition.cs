using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    //��ǥ�� �� Ʈ������ ������Ʈ 
    public Transform target;

    void Update()
    {
        //ī�޶��� ��ġ�� ��ǥ Ʈ������ ��ġ�� ��ġ
        transform.position = target.position;
    }
}
