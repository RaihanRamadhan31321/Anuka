using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FinishScr : MonoBehaviour
{
    //[SerializeField] string levelName;
    public GameObject winPanel;
    public GameObject ditoChar;
    public GameObject continueBtn;

    public RectTransform dtWinPanel;
    public RectTransform dtDito;
    public RectTransform dtContinue;

    public AudioClip winGame;
    private CursorController cursorController;

    private void Start()
    {
        cursorController = FindObjectOfType<CursorController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (SceneManager.GetActiveScene().buildIndex == 4)
            {
                GameManager.Instance.ditoUnlocked = true;
            }
            AudioManager.Instance.PlaySFX(winGame);
            AudioManager.Instance.NPCSource.Pause();
            winPanel.SetActive(false);
            continueBtn.SetActive(false);
            DTWinPanel();
            cursorController.csr = true;
            Time.timeScale = 0;
            UIManager.instance.cooldownHUD.SetActive(false);

            UnlockNewLevel();
            GameManager.Instance.cpCheck = false;
            GameManager.Instance.SavePlayer();
            //SceneController.instance.LoadScene(levelName);
        }
    }

    public void DTWinPanel()
    {
        winPanel.SetActive(true);

        dtWinPanel.anchoredPosition = new Vector2(dtWinPanel.anchoredPosition.x, 1068);
        dtDito.anchoredPosition = new Vector2(dtDito.anchoredPosition.x, -130);
        dtContinue.anchoredPosition = new Vector2(dtContinue.anchoredPosition.x, -696);

        dtWinPanel.DOAnchorPosY(-45, 2f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            dtDito.DOAnchorPosY(-726, 1f).SetUpdate(true).SetDelay(3f).OnComplete(() =>
            {
                ditoChar.SetActive(false);
                continueBtn.SetActive(true);
                dtContinue.DOAnchorPosY(-212, 3f).SetEase(Ease.OutBack).SetUpdate(true);
            });

        });



    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (GameManager.Instance.levelUnlock <= 1)
            {
                GameManager.Instance.ditoUnlocked = true;
                GameManager.Instance.levelUnlock++;
            }

        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (GameManager.Instance.levelUnlock <= 2)
            {
                GameManager.Instance.levelUnlock++;
            }

        }
        //if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        //{
        //    PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
        //    if(PlayerPrefs.GetInt("UnlockedLevel") < 4)
        //    {
        //        PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel",1) + 1);
        //    }
        //    PlayerPrefs.Save();
        //}
    }


}
