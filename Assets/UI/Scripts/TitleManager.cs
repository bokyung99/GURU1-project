using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // 시네머신 재생 시간
    public float playTime;

    // 타이틀 메뉴 UI 오브젝트
    public GameObject titleOption;

    // Update is called once per frame
    void Update()
    {
        // 일정 시간 후에 타이틀 메뉴 보이도록 코루틴 활용
        StartCoroutine(OpenTitleOption());
    }

    // 타이틀 타이밍에 맞게 보이게 하기
    IEnumerator OpenTitleOption()
    {
        //재생 시간만큼 기다리다가
        yield return new WaitForSeconds(playTime);

        // 타이틀 메뉴 보이게 함
        titleOption.SetActive(true);
    }

    public void GameStart()
    {
        // 시작하기 버튼 누르면 인게임으로 넘어감
        SceneManager.LoadScene("Intro");
    }

    // 게임 종료 옵션
    public void QuitGame()
    {
        // 애플리케이션을 종료한다.
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
