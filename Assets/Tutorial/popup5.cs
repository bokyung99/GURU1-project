using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup5 : MonoBehaviour
{
    public bool popup5On = false;
    public bool popup7On = false;
    public bool popup9On = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "popup5")
        {
            popup5On = true;
        }
        else if (other.tag == "popup7")
        {
            popup7On = true;
        }
        else if (other.tag == "popup9")
        {
            popup9On = true;
        }

    }
}
