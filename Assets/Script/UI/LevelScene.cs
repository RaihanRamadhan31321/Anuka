using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour
{
    public GameObject levelPanel;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            KeMainMenu();
        }
    }


    public void KeMainMenu()
    {
        Time.timeScale = 1;
        levelPanel.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void KeLevel1()
    {
        Time.timeScale = 1;
        levelPanel.SetActive(false);
        SceneManager.LoadScene("Level 1");
    }


}
