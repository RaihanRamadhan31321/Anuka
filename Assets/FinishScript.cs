using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    [SerializeField] string levelName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            GameManager.Instance.cpCheck = false;
            GameManager.Instance.SavePlayer();
            SceneController.instance.LoadScene(levelName);
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
