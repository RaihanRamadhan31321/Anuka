using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelScene : MonoBehaviour
{
    public GameObject levelPanel;
    public CursorController cursorController;
    private LoadSceneTransition transition;
    public GameObject singa, dito;


    private void Start()
    {
        GameManager.Instance.currentCharacter = character.SINGA;
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
    public void SwitchCharacter()
    {
        Debug.Log("CEK");
        if (singa.gameObject.activeSelf)
        {
            singa.SetActive(false);
        }
        else
        {
            singa.SetActive(true);
            GameManager.Instance.currentCharacter = character.SINGA;
        }


        
        if (dito.gameObject.activeSelf)
        {
            dito.SetActive(false);
        }
        else
        {
            dito.SetActive(true);
            GameManager.Instance.currentCharacter = character.DITO;
        }


    }

}
