using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UIElements;

public class FinishScript : MonoBehaviour
{
    //[SerializeField] string levelName;
    public GameObject panelWin;
    public GameObject continueBtn;
    public GameObject dito;

    public AudioClip winGame;
    private CursorController cursorController;

    public RectTransform dtWinPanel;
    public RectTransform dtContinue;

    private void Start()
    {
        cursorController = FindObjectOfType<CursorController>();
        dito.SetActive(false);

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

            panelWin.SetActive(false);
            continueBtn.SetActive(false);
            cursorController.csr = true;
            Time.timeScale = 0;
            UIManager.instance.cooldownHUD.SetActive(false);
            DTWinPanel();

            UnlockNewLevel();
            GameManager.Instance.cpCheck = false;
            GameManager.Instance.SavePlayer();
            PlayerManager.instance.playerMV.currentWave = 1;
            UIManager.instance.pausePanel.SetActive(false);
            //SceneController.instance.LoadScene(levelName);
        }
    }

    public void DTWinPanel()
    {
        panelWin.SetActive(true);

        dtWinPanel.anchoredPosition = new Vector2(dtWinPanel.anchoredPosition.x, 1068);
        dtContinue.anchoredPosition = new Vector2(dtContinue.anchoredPosition.x, -696);

        dtWinPanel.DOAnchorPosY(-45, 2f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            continueBtn.SetActive(true);
        });
        dtContinue.DOAnchorPosY(-219, 3f).SetEase(Ease.OutBack).SetUpdate(true);
    }


    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if(GameManager.Instance.levelUnlock <= 1)
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