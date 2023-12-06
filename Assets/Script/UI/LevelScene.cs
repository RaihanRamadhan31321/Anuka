using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class LevelScene : MonoBehaviour
{
    public GameObject levelPanel;
    public CursorController cursorController;
    private LoadSceneTransition transition;
    public GameObject singa, dito;
    public TMP_Text[] namaChar;


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
        SceneManager.LoadScene(1);
    }


    // public void KeLevel1()
    // {
    //     Time.timeScale = 1;
    //     levelPanel.SetActive(false);
    //     //SceneManager.LoadScene("Scene Visto");
    //     transition.loadNext = true;
    //     cursorController.csr = false;
    // }


    public void SwitchCharacter()
    {
        Debug.Log("CEK");
        if (singa.gameObject.activeSelf)
        {
            singa.SetActive(false);
            namaChar[0].text = "DITO";

        }
        else
        {
            singa.SetActive(true);
            GameManager.Instance.currentCharacter = character.SINGA;
            namaChar[0].text = "SINGA";
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
