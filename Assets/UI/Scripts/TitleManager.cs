using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // �ó׸ӽ� ��� �ð�
    public float playTime;

    // Ÿ��Ʋ �޴� UI ������Ʈ
    public GameObject titleOption;

    // Update is called once per frame
    void Update()
    {
        // ���� �ð� �Ŀ� Ÿ��Ʋ �޴� ���̵��� �ڷ�ƾ Ȱ��
        StartCoroutine(OpenTitleOption());
    }

    // Ÿ��Ʋ Ÿ�ֿ̹� �°� ���̰� �ϱ�
    IEnumerator OpenTitleOption()
    {
        //��� �ð���ŭ ��ٸ��ٰ�
        yield return new WaitForSeconds(playTime);

        // Ÿ��Ʋ �޴� ���̰� ��
        titleOption.SetActive(true);
    }

    public void GameStart()
    {
        // �����ϱ� ��ư ������ �ΰ������� �Ѿ
        SceneManager.LoadScene("FirstScene");
    }

    // ���� ���� �ɼ�
    public void QuitGame()
    {
        // ���ø����̼��� �����Ѵ�.
        Application.Quit();
        Debug.Log("���� ����");
    }
}
