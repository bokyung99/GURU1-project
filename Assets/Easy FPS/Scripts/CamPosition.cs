using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosition : MonoBehaviour
{
    //��ǥ�� �� Ʈ������ ������Ʈ
    public Transform target;

    void Update()
    {
        //ī�޶�� ��ǥ Ʈ�������� ��ġ ��ġ��Ŵ
        transform.position = target.position;
    }
}
