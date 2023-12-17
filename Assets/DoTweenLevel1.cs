using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenLevel1 : MonoBehaviour
{
    FinishScript finishScript;

    public GameObject winPanel;
    public GameObject ditoChar;

    public RectTransform dtWinPanel;
    public RectTransform ditoRect;

    public void DTWinPanel()
    {
        winPanel.SetActive(true);

        dtWinPanel.anchoredPosition = new Vector2(dtWinPanel.anchoredPosition.x, -45);
        ditoRect.anchoredPosition = new Vector2(ditoRect.anchoredPosition.x, -130);

        dtWinPanel.DOAnchorPosY(1068, 0.7f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            dtWinPanel.DOAnchorPosY(664, 5f).OnComplete(() =>
            {
                ditoChar.SetActive(false);
            });
        });

    }
}
