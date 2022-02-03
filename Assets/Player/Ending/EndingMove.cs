using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMove : MonoBehaviour
{
    // 이동 속도 변수
    public float moveSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // 이동 방향을 설정한다.
        Vector3 dir = Vector3.forward;
        dir = dir.normalized;
        // 이동 속도에 맞춰 이동한다.
        // p = p0 + vt
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
