using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemMove : MonoBehaviour
{


    float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        rotateItem(new Vector3(0f, (Mathf.Cos(timer) * 0.5f + 0.5f) * 360f, 0f));
    }

    public void rotateItem(Vector3 rotation)
    {
        transform.rotation = Quaternion. Euler(rotation);
    }

    
}
