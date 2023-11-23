using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour
{
    public GameObject levelPanel;
    public CursorController cursorController;
    private LoadSceneTransition transition;

    private void Start()
    {
        cursorController = FindObjectOfType<CursorController>();
        transition = FindObjectOfType<LoadSceneTransition>();
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
        transition.loadMain = true;
    }

    public void KeLevel1()
    {
        Time.timeScale = 1;
        levelPanel.SetActive(false);
        //SceneManager.LoadScene("Scene Visto");
        transition.loadNext = true;
        cursorController.csr = false;
    }


}
