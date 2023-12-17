using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelDT : MonoBehaviour
{
    public RectTransform dtLevel1, dtLevel2, dtLevel3, dtSelectLev, dtSelectChar, dtChar, dtButton, dtMainMenu;

    void Start()
    {
        dtSelectLev.anchoredPosition = new Vector2(470, dtSelectLev.anchoredPosition.y);
        dtLevel1.anchoredPosition = new Vector2(1440, dtLevel1.anchoredPosition.y);
        dtLevel2.anchoredPosition = new Vector2(1440, dtLevel2.anchoredPosition.y);
        dtLevel3.anchoredPosition = new Vector2(1440, dtLevel3.anchoredPosition.y);

        dtSelectChar.anchoredPosition = new Vector2(-448, dtSelectChar.anchoredPosition.y);
        dtChar.anchoredPosition = new Vector2(-1304, dtChar.anchoredPosition.y);
        dtButton.anchoredPosition = new Vector2(-1526, dtButton.anchoredPosition.y);
        dtMainMenu.anchoredPosition = new Vector2(-334, dtMainMenu.anchoredPosition.y);

        StartCoroutine(IEDoTween(0.5f));
    }

    IEnumerator IEDoTween(float delaystart)
    {
        yield return new WaitForSeconds(delaystart);


        dtSelectLev.DOAnchorPosX(-476, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            dtLevel1.DOAnchorPosX(351, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                dtLevel2.DOAnchorPosX(351, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    dtLevel3.DOAnchorPosX(351, 0.5f).SetEase(Ease.OutBack);
                });
            });
        });


        dtMainMenu.DOAnchorPosX(220, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            dtSelectChar.DOAnchorPosX(482.77f, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                dtChar.DOAnchorPosX(-466, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    dtButton.DOAnchorPosX(-640, 0.5f).SetEase(Ease.OutBack);
                });
            });
        });


    }
}
