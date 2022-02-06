using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenu : MonoBehaviour
{
    public float playTime;
    public GameObject gameClearOpt;

    private void Start()
    {
        StartCoroutine(OpenGameMenu());
    }

    IEnumerator OpenGameMenu()
    {
        yield return new WaitForSeconds(playTime);
        gameClearOpt.SetActive(true);
        yield return new WaitForSeconds(2f);
        QuitGame();
    }

    // 게임 종료 옵션
    public void QuitGame()
    {
        // 애플리케이션을 종료한다.
        Application.Quit();
        Debug.Log("끝!");
    }

    public void returnToMain()
    {
        // 게임 속도를 1배속으로 전환한다.
        SceneManager.LoadScene("TitleMenu");
    }
}
