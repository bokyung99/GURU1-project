using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic : MonoBehaviour
{
    public float playTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("FirstScene");
        }
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(playTime);
        SceneManager.LoadScene("FirstScene");

    }
}
