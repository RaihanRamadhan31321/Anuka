using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    public GameObject exitBoong;
    public RectTransform dtExitPanel;
    private void Start()
    {
        dtExitPanel.anchoredPosition = new Vector2(dtExitPanel.anchoredPosition.x, 1059);
        StartCoroutine(IE_DisplayScene(0.3f, 0.1f));
    }
    public void gajadiDeh()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }

    IEnumerator IE_DisplayScene(float delayStart, float delayDisplay)
    {
        yield return new WaitForSeconds(delayStart);

        dtExitPanel.DOAnchorPosY(0, 0.6f).SetEase(Ease.OutBack);
    }


}
