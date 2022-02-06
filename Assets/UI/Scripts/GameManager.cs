using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // �̱��� ����, 17��° �ٱ��� �̱��� ����� ���� �ڵ�
    public static GameManager gm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
    }

    // ���� ���� ������ ���� ���
    public enum GameState
    {
        Ready,
        Run,
        Pause,
        GameOver
    }

    // ������ ���� ���� ����
    public GameState gState;

    // ���� ���� UI ������Ʈ ����
    public GameObject gameLabel;

    // ���� ���� UI �ؽ�Ʈ ������Ʈ ����
    Text gameText;

    // �ɼ� ȭ�� UI ������Ʈ ����
    public GameObject gameOption;

    // ���� ���� ȭ�� UI ������Ʈ ����
   public GameObject gameOverOpt;

    // playerHp Ŭ���� ����
    PlayerHp playerHpMng;

    // ��ǥ ������ UI �г� ������Ʈ
    public GameObject ItemPanel;

    PlayerHp playerItem;

    void Update()
    {
        // ���� ���� ������ �� ������ �ڵ�
        // �÷��̾� hp ���Ҹ� �ٷ�� �ڵ忡�� hp ������ �ͼ� 
        // �� hp�� 0������ ���, �Ʒ��� ���� �ڵ� �ۼ�

        /* 
        if (Input.GetMouseButton(1))
        {
            // ���� �ؽ�Ʈ�� Ȱ��ȭ�Ѵ�.
            gameLabel.SetActive(true);

            // ���� �ؽ�Ʈ�� ������ ��Game Over���� �Ѵ�.
            gameText.text = "Game Over";

            // ���� �ؽ�Ʈ�� ������ ���������� �Ѵ�.
            gameText.color = new Color32(255, 0, 0, 255);

            // ���� �ؽ�Ʈ�� �ڽ� ������Ʈ�� Ʈ������ ������Ʈ�� �����´�.
            Transform buttons = gameText.transform.GetChild(0);

            // ��ư ������Ʈ�� Ȱ��ȭ�Ѵ�.
            buttons.gameObject.SetActive(true);

            // ���¸� ������ ������ ���·� �����Ѵ�.
            gState = GameState.GameOver;
        }

        */

        if (playerHpMng.playerHp <= 0)
        {
            // ���� ���� �޴��� Ų��
            gameOverOpt.SetActive(true);

            // ���� ���¸� ���� ���� ���·� �����Ѵ�.
            gState = GameState.GameOver;

        }

            // f1 Ű�� ������ �� �ɼ� �޴� ����
            if (Input.GetKeyDown(KeyCode.F1))
        {
            // ���� �̹� �ɼ� �޴��� ���� ��Ȳ�̶�� 
            if (gState == GameState.Pause)
            {
                // �ɼ� �޴��� ����.
                // �ɼ� â�� ��Ȱ��ȭ�Ѵ�.
                gameOption.SetActive(false);

                // ���� �ӵ��� 1������� ��ȯ�Ѵ�.
                Time.timeScale = 1f;

                // ���� ���¸� ���� �� ���·� �����Ѵ�.
                gState = GameState.Run;
                Debug.Log("resume");

            }
            // �ɼ� �޴��� ���� ��Ȳ�̶��
            else
            {
                // �ɼ� �޴��� Ų��.
                // �ɼ� â�� Ȱ��ȭ�Ѵ�.
                gameOption.SetActive(true);

                // ���� �ӵ��� 0������� ��ȯ�Ѵ�.
                Time.timeScale = 0f;

                // ���� ���¸� �Ͻ� ���� ���·� �����Ѵ�.
                gState = GameState.Pause;
            }
        }

        // tab Ű�� ������ �� ��ǥ ������ ���� ��Ȳ UI ����
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // ���� �̹� ���� ��Ȳ UI�� ���� ��Ȳ�̶�� 
            if (gState == GameState.Pause)
            {
                // �ɼ� �޴��� ����.
                // �ɼ� â�� ��Ȱ��ȭ�Ѵ�.
                ItemPanel.SetActive(false);

                // ���� �ӵ��� 1������� ��ȯ�Ѵ�.
                Time.timeScale = 1f;

                // ���� ���¸� ���� �� ���·� �����Ѵ�.
                gState = GameState.Run;

            }
            // �ɼ� �޴��� ���� ��Ȳ�̶��
            else
            {
                // �ɼ� �޴��� Ų��.
                // �ɼ� â�� Ȱ��ȭ�Ѵ�.
                ItemPanel.SetActive(true);

                // ���� �ӵ��� 0������� ��ȯ�Ѵ�.
                Time.timeScale = 0f;

                // ���� ���¸� �Ͻ� ���� ���·� �����Ѵ�.
                gState = GameState.Pause;
            }
        }

        // ������1�� �������� ��
        if (playerItem.isItem1 == 1)
        {
            // ������ �г��� 4��° �ڽ� ������Ʈ Ʈ���������� ������
            Transform item1 = ItemPanel.transform.GetChild(0);

            // ������1 ���̰� �ϱ�
            item1.gameObject.SetActive(true);
        }
        // ���ǽ� player -> playerItem.isItem2 ���� ����
        // ������2�� �������� ��
        if (playerItem.isItem2 == 1)
        {
            // ������ �г��� 5��° �ڽ� ������Ʈ Ʈ���������� ������ 
            Transform item2 = ItemPanel.transform.GetChild(1);

            // ������2 ���̰� �ϱ� 
            item2.gameObject.SetActive(true);
        }
        // ���ǽ� player -> playerItem.isItem3 ���� ����
        // ������3�� �������� ��
        if (playerItem.isItem3 == 1)
        {
            // ������ �г��� 6��° �ڽ� ������Ʈ Ʈ���������� ������
            Transform item3 = ItemPanel.transform.GetChild(2);
            item3.gameObject.SetActive(true);
        }
    }

    void Start()
    {
        // ó������ �ٷ� ���� �����ϰ�, �ɼ� �޴��� Ű�ų�, ���� ���� ���� ���� ���� X
        // �ʱ� ���� ���´� '���� ��' ���·� �����Ѵ�.
        gState = GameState.Run;

        // ���� ���� UI ������Ʈ���� Text ������Ʈ�� �����´�.
        //gameText = gameLabel.GetComponent<Text>();

        // �÷��̾� ������Ʈ�� ã�� ��, playreHp ������Ʈ �޾ƿ���
        playerHpMng = GameObject.Find("Player").GetComponent<PlayerHp>();

        playerItem = GameObject.Find("Player").GetComponent<PlayerHp>();

    }

    // ����ϱ� �ɼ�
    public void CloseOptionWindow()
    {
        // �ɼ� â�� ��Ȱ��ȭ�Ѵ�.
        gameOption.SetActive(false);
        // ���� �ӵ��� 1������� ��ȯ�Ѵ�.
        Time.timeScale = 1f;
        // ���� ���¸� ���� �� ���·� �����Ѵ�.
        gState = GameState.Run;
        Debug.Log("resume");
    }

    // �ٽ��ϱ� �ɼ�
    public void RestartGame()
    {
        // ���� �ӵ��� 1������� ��ȯ�Ѵ�.
        Time.timeScale = 1f;
        // ���� �� ��ȣ�� �ٽ� �ε��Ѵ�.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("FirstScene");
    }

    public void RestartTutorial()
    {
        // ���� �ӵ��� 1������� ��ȯ�Ѵ�.
        Time.timeScale = 1f;
        // ���� �� ��ȣ�� �ٽ� �ε��Ѵ�.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Tutorial");
    }

    public void RestartBoss()
    {
        // ���� �ӵ��� 1������� ��ȯ�Ѵ�.
        Time.timeScale = 1f;
        // ���� �� ��ȣ�� �ٽ� �ε��Ѵ�.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("BossMap");
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
