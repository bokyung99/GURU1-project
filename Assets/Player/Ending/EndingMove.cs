using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMove : MonoBehaviour
{
    // �̵� �ӵ� ����
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // �̵� ������ �����Ѵ�.
        Vector3 dir = Vector3.forward;
        dir = dir.normalized;
        // �̵� �ӵ��� ���� �̵��Ѵ�.
        // p = p0 + vt
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
