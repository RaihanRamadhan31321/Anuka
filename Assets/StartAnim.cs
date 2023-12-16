using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class StartAnim : MonoBehaviour
{
    public RectTransform btnPrologue, btnContinue, btnLogo, btnSettings, btnSettingsPanel;


    private void Start()
    {

        btnPrologue.anchoredPosition = new Vector2(-526, btnPrologue.anchoredPosition.y);
        btnContinue.anchoredPosition = new Vector2(526, btnContinue.anchoredPosition.y);
        btnLogo.anchoredPosition = new Vector2(btnLogo.anchoredPosition.x, 229);
        btnSettings.anchoredPosition = new Vector2(292, 251);
        //btnSettingsPanel.anchoredPosition = new Vector2(1473, 849);

        StartCoroutine(IE_Display(0.3f, 0.1f));
    }



    IEnumerator IE_Display(float delayStart, float delayDisplay)
    {
        yield return new WaitForSeconds(delayStart);

        btnPrologue.DOAnchorPosX(384, 0.6f).SetEase(Ease.OutBack);
        btnContinue.DOAnchorPosX(-388, 0.6f).SetEase(Ease.OutBack);
        btnLogo.DOAnchorPosY(-229, 0.6f).SetEase(Ease.OutBack);
        btnSettings.DOAnchorPosX(-158, 0.6f).SetEase(Ease.OutBack);
        btnSettings.DOAnchorPosY(-140, 0.6f).SetEase(Ease.OutBack);

        //btnSettingsPanel.DOAnchorPosX(-111, 0.6f).SetEase(Ease.OutBack);
        //btnSettingsPanel.DOAnchorPosY(62, 0.6f).SetEase(Ease.OutBack);
    }











    /*    private void Start()
        {
            StartCoroutine(IE_Display(0.3f, 0.1f));
        }

        IEnumerator IE_Display(float delayStart, float delayDisplay)
        {
            yield return new WaitForSeconds(delayStart);
            btnPrologue.transform.DOMoveX(384, 0.6f).SetEase(Ease.OutBack);
            btnContinue.transform.DOMoveX(1533, 0.6f).SetEase(Ease.OutBack); 
        }*/


    /*    public List<RectTransform> btnTransformL;
        public List<RectTransform> btnTransformR;

        void Start()
        {
            StartCoroutine(IE_Display(0.3f, 0.1f));
        }

        IEnumerator IE_Display(float delayStart, float delayDisplay)
        {
            yield return new WaitForSeconds(delayStart);
            for (int i = 0; i < btnTransformL.Count; i++)
            {
                btnTransformL[i].DOMoveX(384, 0.6f);
                yield return new WaitForSeconds(delayDisplay);
            }
        }*/
}
