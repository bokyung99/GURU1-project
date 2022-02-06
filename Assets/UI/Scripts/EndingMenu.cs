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

    // ���� ���� �ɼ�
    public void QuitGame()
    {
        // ���ø����̼��� �����Ѵ�.
        Application.Quit();
        Debug.Log("��!");
    }

    public void returnToMain()
    {
        // ���� �ӵ��� 1������� ��ȯ�Ѵ�.
        SceneManager.LoadScene("TitleMenu");
    }
}
