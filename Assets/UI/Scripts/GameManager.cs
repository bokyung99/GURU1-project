using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 싱글톤 변수, 17번째 줄까지 싱글톤 기능을 위한 코드
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    // 게임 상태 구분을 위한 상수
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver
    }

    // 현재의 게임 상태 변수
    public GameState gState;

    // 게임 상태 UI 오브젝트 변수
    public GameObject gameLabel;

    // 게임 상태 UI 텍스트 컴포넌트 변수
    Text gameText;

    // 옵션 화면 UI 오브젝트 변수
    public GameObject gameOption;

    // 게임 오버 화면 UI 오브젝트 변수
   public GameObject gameOverOpt;

    // playerHp 클래스 변수
    PlayerHp playerHpMng;

    // 목표 아이템 UI 패널 오브젝트
    public GameObject ItemPanel;

    PlayerHp playerItem;

    void Update()
    {
        // 게임 오버 상태일 때 적용할 코드
        // 플레이어 hp 감소를 다루는 코드에서 hp 가지고 와서 
        // 그 hp가 0이하일 경우, 아래와 같이 코드 작성

        /* 
        if (Input.GetMouseButton(1))
        {
            // 상태 텍스트를 활성화한다.
            gameLabel.SetActive(true);

            // 상태 텍스트의 내용을 ‘Game Over’로 한다.
            gameText.text = "Game Over";

            // 상태 텍스트의 색상을 붉은색으로 한다.
            gameText.color = new Color32(255, 0, 0, 255);

            // 상태 텍스트의 자식 오브젝트의 트랜스폼 컴포넌트를 가져온다.
            Transform buttons = gameText.transform.GetChild(0);

            // 버튼 오브젝트를 활성화한다.
            buttons.gameObject.SetActive(true);

            // 상태를 ‘게임 오버’ 상태로 변경한다.
            gState = GameState.GameOver;
        }

        */

        /*
        if (playerHpMng.playerHp <= 0)
        {
            // 게임 오버 메뉴를 킨다
            gameOverOpt.SetActive(true);

            // 게임 상태를 게임 오버 상태로 변경한다.
            gState = GameState.GameOver;

        }

        */

            // f1 키를 눌렀을 때 옵션 메뉴 켜짐
            if (Input.GetKeyDown(KeyCode.F1))
        {
            // 만약 이미 옵션 메뉴가 켜진 상황이라면 
            if (gState == GameState.Pause)
            {
                // 옵션 메뉴를 끈다.
                // 옵션 창을 비활성화한다.
                gameOption.SetActive(false);

                // 게임 속도를 1배속으로 전환한다.
                Time.timeScale = 1f;

                // 게임 상태를 게임 중 상태로 변경한다.
                gState = GameState.Run;
                Debug.Log("resume");

            }
            // 옵션 메뉴가 꺼진 상황이라면
            else
            {
                // 옵션 메뉴를 킨다.
                // 옵션 창을 활성화한다.
                gameOption.SetActive(true);

                // 게임 속도를 0배속으로 전환한다.
                Time.timeScale = 0f;

                // 게임 상태를 일시 정지 상태로 변경한다.
                gState = GameState.Pause;
            }
        }

        // tab 키를 눌렀을 때 목표 아이템 습득 현황 UI 켜짐
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 만약 이미 습득 현황 UI가 켜진 상황이라면 
            if (gState == GameState.Pause)
            {
                // 옵션 메뉴를 끈다.
                // 옵션 창을 비활성화한다.
                ItemPanel.SetActive(false);

                // 게임 속도를 1배속으로 전환한다.
                Time.timeScale = 1f;

                // 게임 상태를 게임 중 상태로 변경한다.
                gState = GameState.Run;

            }
            // 옵션 메뉴가 꺼진 상황이라면
            else
            {
                // 옵션 메뉴를 킨다.
                // 옵션 창을 활성화한다.
                ItemPanel.SetActive(true);

                // 게임 속도를 0배속으로 전환한다.
                Time.timeScale = 0f;

                // 게임 상태를 일시 정지 상태로 변경한다.
                gState = GameState.Pause;
            }
        }

        // 아이템1을 습득했을 때
        if (playerItem.isItem1 == 1)
        {
            // 아이템 패널의 4번째 자식 오브젝트 트랜스폼으로 얻어오기
            Transform item1 = ItemPanel.transform.GetChild(0);

            // 아이템1 보이게 하기
            item1.gameObject.SetActive(true);
        }
        // 조건식 player -> playerItem.isItem2 으로 수정
        // 아이템2를 습득했을 때
        if (playerItem.isItem2 == 1)
        {
            // 아이템 패널의 5번째 자식 오브젝트 트랜스폼으로 얻어오기 
            Transform item2 = ItemPanel.transform.GetChild(1);

            // 아이템2 보이게 하기 
            item2.gameObject.SetActive(true);
        }
        // 조건식 player -> playerItem.isItem3 으로 수정
        // 아이템3을 습득했을 때
        if (playerItem.isItem3 == 1)
        {
            // 아이템 패널의 6번째 자식 오브젝트 트랜스폼으로 얻어오기
            Transform item3 = ItemPanel.transform.GetChild(2);
            item3.gameObject.SetActive(true);
        }
    }

    void Start()
    {
        // 처음부터 바로 실행 가능하고, 옵션 메뉴를 키거나, 게임 오버 됐을 때만 조작 X
        // 초기 게임 상태는 '게임 중' 상태로 설정한다.
        gState = GameState.Run;

        // 게임 상태 UI 오브젝트에서 Text 컴포넌트를 가져온다.
        //gameText = gameLabel.GetComponent<Text>();

        // 플레이어 오브젝트를 찾은 후, playreHp 컴포넌트 받아오기
        playerHpMng = GameObject.Find("Player").GetComponent<PlayerHp>();

        playerItem = GameObject.Find("Player").GetComponent<PlayerHp>();

    }

    // 계속하기 옵션
    public void CloseOptionWindow()
    {
        // 옵션 창을 비활성화한다.
        gameOption.SetActive(false);
        // 게임 속도를 1배속으로 전환한다.
        Time.timeScale = 1f;
        // 게임 상태를 게임 중 상태로 변경한다.
        gState = GameState.Run;
        Debug.Log("resume");
    }

    // 다시하기 옵션
    public void RestartGame()
    {
        // 게임 속도를 1배속으로 전환한다.
        Time.timeScale = 1f;
        // 현재 씬 번호를 다시 로드한다.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
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
