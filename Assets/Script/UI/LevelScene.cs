using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour
{
    public GameObject levelPanel;
    public CursorController cursorController;

    private void Start()
    {
        cursorController = FindObjectOfType<CursorController>();
        cursorController.csr = true;
    }

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
        SceneManager.LoadScene("Scene Visto");
        SceneManager.LoadScene("Level 1");
        cursorController.csr = false;
    }


}
