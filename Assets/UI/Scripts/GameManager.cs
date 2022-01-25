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
        
    }

    void Start()
    {
        // �ʱ� ���� ���´� '���� ��' ���·� �����Ѵ�.
        gState = GameState.Run;

        // ���� ���� UI ������Ʈ���� Text ������Ʈ�� �����´�.
        gameText = gameLabel.GetComponent<Text>();

    }

    // �ɼ� ȭ�� �ѱ�
    public void OpenOptionWindow()
    {
        // �ɼ� â�� Ȱ��ȭ�Ѵ�.
        gameOption.SetActive(true);
        // ���� �ӵ��� 0������� ��ȯ�Ѵ�.
        Time.timeScale = 0f;
        // ���� ���¸� �Ͻ� ���� ���·� �����Ѵ�.
        gState = GameState.Pause;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ���� ���� �ɼ�
    public void QuitGame()
    {
        // ���ø����̼��� �����Ѵ�.
        Application.Quit();
        Debug.Log("��!");
    }

}
