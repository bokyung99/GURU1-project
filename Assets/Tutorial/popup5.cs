using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup5 : MonoBehaviour
{
    public bool popup5On = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "popup5")
        {
            popup5On = true;
        }
    }
}
