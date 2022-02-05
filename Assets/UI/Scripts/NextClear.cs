using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextClear : MonoBehaviour
{
    public float playTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(goGameClear());
    }

    IEnumerator goGameClear()
    {
        yield return new WaitForSeconds(playTime);
        SceneManager.LoadScene("GameClear");

    }
}
