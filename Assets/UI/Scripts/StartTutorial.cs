using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTutorial : MonoBehaviour
{
    public float playTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(goTutorial());
        
    }

    IEnumerator goTutorial()
    {
        yield return new WaitForSeconds(playTime);
        SceneManager.LoadScene("Tutorial");
    }
}
