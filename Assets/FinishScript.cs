using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    //[SerializeField] string levelName;
    public GameObject panelWin;
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
            AudioManager.Instance.PlaySFX(winGame);
            AudioManager.Instance.NPCSource.Pause();

            panelWin.SetActive(true);
            cursorController.csr = true;
            Time.timeScale = 0;
            UIManager.instance.cooldownHUD.SetActive(false);

            UnlockNewLevel();
            GameManager.Instance.cpCheck = false;
            GameManager.Instance.SavePlayer();
            //SceneController.instance.LoadScene(levelName);

            AudioManager.Instance.Mainmenu();
        }
    }

    void UnlockNewLevel()
    {
        if(GameManager.Instance.levelUnlock <= 3)
        {
            GameManager.Instance.levelUnlock++;
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
